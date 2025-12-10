using ProplanetReplicationTool.Data.Models;
using ProplanetReplicationTool.Data.Models.DTOs;
using ProplanetReplicationTool.Services.Interfaces;
using System.Text;
using System.Text.Json;
using ProplanetReplicationTool.Data.Repositories;
using AutoMapper;
using System.Security.Cryptography;

using System.Text;

namespace ProplanetReplicationTool.Services
{
    public class NovaService : INovaService
    {
        private readonly HttpClient _httpClient;
        private readonly string _novaUrl;
        private readonly ILogger<NovaService> _logger;
        private readonly IRepository<Material> _materialRepository;
        private readonly IRepository<SBTest> _sbTestRepository;
        private readonly IRepository<SBTestInput> _sbTestInputRepository;

        public NovaService(HttpClient httpClient, IConfiguration configuration, ILogger<NovaService> logger, IRepository<Material> materialRepository, IRepository<SBTest> sbTestRepository, IRepository<SBTestInput> sbTestInputRepository)
        {
            _httpClient = httpClient;
            _novaUrl = configuration["ReplicationToolFrontend:environmentVariables:NOVA_URL"];
            _logger = logger;
            _sbTestRepository = sbTestRepository;
            _materialRepository = materialRepository;
            _sbTestInputRepository = sbTestInputRepository;
        }

        public async Task<(List<ScenarioDTO> scenarios, List<EmissionDTO> emissions)> GetAllScenariosAndEmissions()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_novaUrl}/sb4p/scenarios");
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogCritical($"Failed to retrieve scenarios. Status code: {response.StatusCode}");
                    return (new List<ScenarioDTO>(), new List<EmissionDTO>());
                }

                var jsonResponse = await response.Content.ReadAsStringAsync();
                
                var emissions = JsonSerializer.Deserialize<List<EmissionDTO>>(jsonResponse);
                var scenarios = JsonSerializer.Deserialize<List<ScenarioDTO>>(jsonResponse);
                if (scenarios == null || emissions == null)
                {
                    _logger.LogCritical("Failed to deserialize the request.");
                    return (new List<ScenarioDTO>(), new List<EmissionDTO>());
                }
                scenarios = scenarios.DistinctBy(s => s.ScenarioName).ToList();
                return (scenarios, emissions);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception in GetAllScenarios: {ex.Message}");
                return (new List<ScenarioDTO>(), new List<EmissionDTO>());
            }
        }

        public async Task<List<ScenarioDTO>> GetAllScenarios()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_novaUrl}/sb4p/scenarios");
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogCritical($"Failed to retrieve scenarios. Status code: {response.StatusCode}");
                    return new List<ScenarioDTO>();
                }

                var jsonResponse = await response.Content.ReadAsStringAsync();
                var scenarios = JsonSerializer.Deserialize<List<ScenarioDTO>>(jsonResponse);
                scenarios = scenarios.DistinctBy(s => s.ScenarioName).ToList();
                if (scenarios == null)
                {
                    _logger.LogCritical("Failed to deserialize the request.");
                    return new List<ScenarioDTO>();
                }

                return scenarios;
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception in GetAllScenarios: {ex.Message}");
                return new List<ScenarioDTO>();
            }
        }

        public async Task<List<string>> GetAllRespiratoryVolumeRate()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_novaUrl}/lungdeposition/respiratoryVolumeRate");
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogCritical($"Failed to retrieve respiratory volume rate. Status code: {response.StatusCode}");
                    return new List<string>();
                }

                var jsonResponse = await response.Content.ReadAsStringAsync();
                var respiratoryVolumesRates = JsonSerializer.Deserialize<List<string>>(jsonResponse);
                if (respiratoryVolumesRates == null)
                {
                    _logger.LogCritical("Failed to deserialize the request.");
                    return new List<string>();
                }

                return respiratoryVolumesRates;
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception in GetAllRespiratoryVolumeRate: {ex.Message}");
                return new List<string>();
            }
        }

        public async Task<List<MaterialDTO>> GetAllNanoMaterials()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_novaUrl}/sb4p/substances");
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogCritical($"Failed to retrieve nano materials. Status code: {response.StatusCode}");
                    return new List<MaterialDTO>();
                }

                var jsonResponse = await response.Content.ReadAsStringAsync();
                var nanoMaterials = JsonSerializer.Deserialize<List<MaterialDTO>>(jsonResponse);
                nanoMaterials = nanoMaterials.DistinctBy(s => s.NanomaterialName).ToList();
                if (nanoMaterials == null)
                {
                    _logger.LogCritical("Failed to deserialize nano Materials.");
                    return new List<MaterialDTO>();
                }
                return nanoMaterials;
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception in GetAllNanoMaterials: {ex.Message}");
                return new List<MaterialDTO>();
            }
        }

        public async Task<(bool isAlreadySaved, string jsonParserNova)> GetPostNovaResults(NovaDTO novaDTO)
        {
            try
            {
                bool isAlreadySaved = false;
                string fingerprint = ComputeFingerprint(novaDTO);
                SBTestInput? sbTestInput = await _sbTestInputRepository.GetSingleOrDefaultAsync(sbTestInput => sbTestInput.Fingerprint == fingerprint);
                if (sbTestInput == null)
                {
                    string jsonResponse = await GetResultsFromNova(novaDTO);
                    if (jsonResponse == "") throw new Exception($"Something went wront getting results from nova");
                    return (isAlreadySaved, jsonResponse);
                }
                else
                {
                    isAlreadySaved = true;
                    SBTest? novaResult = await _sbTestRepository.GetByIdAsync(sbTestInput.SBTestId);
                    if (novaResult == null) throw new Exception($"Nova test not found in DDBB with id {sbTestInput.SBTestId}");

                    return (isAlreadySaved, novaResult.Result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception saving Nova test information in DDBB: {ex.Message}");
                return (false, "");
            }
        }

        public async Task<bool> StorePostNovaInformation(NovaDTO novaTestInput, string jsonParserNovaResult)
        {
            try
            {
                bool isNovaInformationStored = false;
                string testName = $"{novaTestInput.NanomaterialName!.Trim()}_{novaTestInput.ScenarioName!.Trim()}";

                Guid? novaTestResultId = await StoreNovaResult(testName, jsonParserNovaResult);
                if (novaTestResultId != null)
                {
                    bool isNovaInputDDBB = await StoreNovaInput(novaTestResultId.Value, novaTestInput);
                    isNovaInformationStored = isNovaInputDDBB ? true : false;
                }

                return isNovaInformationStored;
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception saving Nova test information in DDBB: {ex.Message}");
                return false;
            }
        }

        public EmissionDTO MapToEmissionDTO(SBTestInput input)
        {
            return new EmissionDTO
            {
                EmissionToAirRegional = input.EmissionToAirRegional,
                EmissionToLakeWaterRegional = input.EmissionToLakeWaterRegional,
                EmissionToFreshWaterRegional = input.EmissionToFreshWaterRegional,
                EmissionToSeaWaterRegional = input.EmissionToSeaWaterRegional,
                EmissionToNaturalSoilRegional = input.EmissionToNaturalSoilRegional,
                EmissionToAgriculturalSoilRegional = input.EmissionToAgriculturalSoilRegional,
                EmissionToOtherSoilRegional = input.EmissionToOtherSoilRegional,
                EmissionToAirContinental = input.EmissionToAirContinental,
                EmissionToLakeWaterContinental = input.EmissionToLakeWaterContinental,
                EmissionToFreshWaterContinental = input.EmissionToFreshWaterContinental,
                EmissionToSeaWaterContinental = input.EmissionToSeaWaterContinental,
                EmissionToNaturalSoilContinental = input.EmissionToNaturalSoilContinental,
                EmissionToAgriculturalSoilContinental = input.EmissionToAgriculturalSoilContinental,
                EmissionToOtherSoilContinental = input.EmissionToOtherSoilContinental,
                EmissionToAirModerate = input.EmissionToAirModerate,
                EmissionToWaterModerate = input.EmissionToWaterModerate,
                EmissionToSoilModerate = input.EmissionToSoilModerate,
                EmissionToAirArctic = input.EmissionToAirArctic,
                EmissionToWaterArctic = input.EmissionToWaterArctic,
                EmissionToSoilArctic = input.EmissionToSoilArctic,
                EmissionToAirTropical = input.EmissionToAirTropical,
                EmissionToWaterTropical = input.EmissionToWaterTropical,
                EmissionToSoilTropical = input.EmissionToSoilTropical
            };
        }

        private async Task<string> GetResultsFromNova(NovaDTO novaDTO)
        {
            try
            {
                var json = JsonSerializer.Serialize(novaDTO);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{_novaUrl}/sb4p/results", content);
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogCritical($"Failed to post nova input. Status code: {response.StatusCode}");
                    throw new Exception($"Failed to post nova input. Status code: {response.StatusCode}");
                }
                string responseContent = await response.Content.ReadAsStringAsync();

                return responseContent;
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception in PostNovaTest: {ex.Message}");
                return "";
            }
        }

        private async Task<Guid> StoreNovaResult(string testName, string jsonParserNovaResult)
        {
            try
            {
                var sbTest = new SBTest
                {
                    Name = testName,
                    Result = jsonParserNovaResult,
                };

                var saveNovaResult = await _sbTestRepository.AddAsync(sbTest);
                if (!saveNovaResult.Item1) throw new Exception($"Something went wront trying to save nova result data in the DDBB");

                return sbTest.Id;
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception in PostNovaResult: {ex.Message}");
                throw new Exception($"Something went wront trying to save nova result data in the DDBB");
            }
        }

        private async Task<bool> StoreNovaInput(Guid sbTestId, NovaDTO novaDTO)
        {
            try
            {
                string fingerprint = ComputeFingerprint(novaDTO);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<NovaDTO, SBTestInput>();
                });

                IMapper mapper = config.CreateMapper();
                SBTestInput sbTestInput = mapper.Map<SBTestInput>(novaDTO);
                sbTestInput.Fingerprint = fingerprint;
                sbTestInput.SBTestId = sbTestId;

                var saveNovaInputResponse = await _sbTestInputRepository.AddAsync(sbTestInput);

                return saveNovaInputResponse.Item1;
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception in PostNovaInput: {ex.Message}");
                throw new Exception($"Something went wront trying to save nova input data in the DDBB");
            }
        }

        private string ComputeFingerprint(NovaDTO dto)
        {
            var sb = new StringBuilder();
            sb.Append(dto.ScenarioName?.Trim().ToLowerInvariant() ?? "");
            sb.Append("|");
            sb.Append(dto.NanomaterialName?.Trim().ToLowerInvariant() ?? "");
            sb.Append("|");
            sb.Append(dto.MolecularWeight);
            sb.Append("|");
            sb.Append(dto.EmissionToAirRegional);
            sb.Append("|");
            sb.Append(dto.EmissionToLakeWaterRegional);
            sb.Append("|");
            sb.Append(dto.EmissionToFreshWaterRegional);
            sb.Append("|");
            sb.Append(dto.EmissionToSeaWaterRegional);
            sb.Append("|");
            sb.Append(dto.EmissionToNaturalSoilRegional);
            sb.Append("|");
            sb.Append(dto.EmissionToAgriculturalSoilRegional);
            sb.Append("|");
            sb.Append(dto.EmissionToOtherSoilRegional);
            sb.Append("|");
            sb.Append(dto.EmissionToAirContinental);
            sb.Append("|");
            sb.Append(dto.EmissionToLakeWaterContinental);
            sb.Append("|");
            sb.Append(dto.EmissionToFreshWaterContinental);
            sb.Append("|");
            sb.Append(dto.EmissionToSeaWaterContinental);
            sb.Append("|");
            sb.Append(dto.EmissionToNaturalSoilContinental);
            sb.Append("|");
            sb.Append(dto.EmissionToAgriculturalSoilContinental);
            sb.Append("|");
            sb.Append(dto.EmissionToOtherSoilContinental);
            sb.Append("|");
            sb.Append(dto.EmissionToAirArctic);
            sb.Append("|");
            sb.Append(dto.EmissionToWaterArctic);
            sb.Append("|");
            sb.Append(dto.EmissionToSoilArctic);
            sb.Append("|");
            sb.Append(dto.EmissionToAirTropical);
            sb.Append("|");
            sb.Append(dto.EmissionToWaterTropical);
            sb.Append("|");
            sb.Append(dto.EmissionToSoilTropical);
            sb.Append("|");
            sb.Append(dto.EmissionToAirModerate);
            sb.Append("|");
            sb.Append(dto.EmissionToWaterModerate);
            sb.Append("|");
            sb.Append(dto.EmissionToSoilModerate);

            var inputBytes = Encoding.UTF8.GetBytes(sb.ToString());

            using (var sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(inputBytes);
                var hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
                return hashString;
            }
        }
    }
}