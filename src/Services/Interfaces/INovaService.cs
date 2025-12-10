using ProplanetReplicationTool.Data.Models;
using ProplanetReplicationTool.Data.Models.DTOs;

namespace ProplanetReplicationTool.Services.Interfaces
{
    public interface INovaService
    {
        /// <summary>
        /// Get all scenarios/emissions from Nova
        /// </summary>
        /// <returns>List Scenarios</returns>
        Task<(List<ScenarioDTO> scenarios, List<EmissionDTO> emissions)> GetAllScenariosAndEmissions();

        /// <summary>
        /// Get all scenarios from Nova
        /// </summary>
        /// <returns></returns>
        Task<List<ScenarioDTO>> GetAllScenarios();

        /// <summary>
        /// Get all Respiratory Volume Rate from Nova
        /// </summary>
        /// <returns>List of Respiratory Volume Rates</returns>
        Task<List<string>> GetAllRespiratoryVolumeRate();

        /// <summary>
        /// Get all nano materials from Nova
        /// </summary>
        /// <returns>List NanoMaterials</returns>
        Task<List<MaterialDTO>> GetAllNanoMaterials();

        /// <summary>
        /// Post Nova test
        /// </summary>
        /// <param name="novaDTO"></param>
        /// <returns>Result of test</returns>
        Task<(bool isAlreadySaved, string jsonParserNova)> GetPostNovaResults(NovaDTO novaDTO);

        /// <summary>
        /// Main method to store Nova information in DDBB
        /// </summary>
        /// <param name="novaTest"></param>
        /// /// <param name="jsonParserNova"></param>
        Task<bool> StorePostNovaInformation(NovaDTO novaTest, string jsonParserNova);

        /// <summary>
        /// Mapper from SBTestInput to EmissionDTO
        /// </summary>
        /// <param name="input"></param>
        EmissionDTO MapToEmissionDTO(SBTestInput input);
    }
}