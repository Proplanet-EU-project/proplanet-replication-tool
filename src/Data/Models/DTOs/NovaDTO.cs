using AutoMapper;
using System.Text.Json.Serialization;

namespace ProplanetReplicationTool.Data.Models.DTOs
{    /// <summary>
     /// Represents the emission entity
     /// </summary>
    public class NovaDTO
    {
        /// <summary>
        /// scenario name
        /// </summary>
        [JsonPropertyName("scenario")]
        public string? ScenarioName { get; set; }

        /// <summary>
        /// substance name
        /// </summary>
        [JsonPropertyName("substance")]
        public string? NanomaterialName { get; set; }

        /// <summary>
        /// molecular weight
        /// </summary>
        [JsonPropertyName("molweight")]
        public float MolecularWeight { get; set; }

        /// <summary>
        /// regional air emission
        /// </summary>
        [JsonPropertyName("e_aR")]
        public float EmissionToAirRegional { get; set; }

        /// <summary>
        /// regional lake water emission
        /// </summary>
        [JsonPropertyName("e_w0R")]
        public float EmissionToLakeWaterRegional { get; set; }

        /// <summary>
        /// regional fresh water emission
        /// </summary>
        [JsonPropertyName("e_w1R")]
        public float EmissionToFreshWaterRegional { get; set; }

        /// <summary>
        /// regional sea water emission
        /// </summary>
        [JsonPropertyName("e_w2R")]
        public float EmissionToSeaWaterRegional { get; set; }

        /// <summary>
        /// regional natural soil emission
        /// </summary>
        [JsonPropertyName("e_s1R")]
        public float EmissionToNaturalSoilRegional { get; set; }

        /// <summary>
        /// regional agricultural soil emission
        /// </summary>
        [JsonPropertyName("e_s2R")]
        public float EmissionToAgriculturalSoilRegional { get; set; }

        /// <summary>
        /// regional other soil emission
        /// </summary>
        [JsonPropertyName("e_s3R")]
        public float EmissionToOtherSoilRegional { get; set; }

        /// <summary>
        /// continental air emission
        /// </summary>
        [JsonPropertyName("e_aC")]
        public float EmissionToAirContinental { get; set; }

        /// <summary>
        /// continental lake water emission
        /// </summary>
        [JsonPropertyName("e_w0C")]
        public float EmissionToLakeWaterContinental { get; set; }

        /// <summary>
        /// continental fresh water emission
        /// </summary>
        [JsonPropertyName("e_w1C")]
        public float EmissionToFreshWaterContinental { get; set; }

        /// <summary>
        /// continental sea water emission
        /// </summary>
        [JsonPropertyName("e_w2C")]
        public float EmissionToSeaWaterContinental { get; set; }

        /// <summary>
        /// continental natural soil emission
        /// </summary>
        [JsonPropertyName("e_s1C")]
        public float EmissionToNaturalSoilContinental { get; set; }

        /// <summary>
        /// continental cultural soil emission
        /// </summary>
        [JsonPropertyName("e_s2C")]
        public float EmissionToAgriculturalSoilContinental { get; set; }

        /// <summary>
        /// continental other soil emission
        /// </summary>
        [JsonPropertyName("e_s3C")]
        public float EmissionToOtherSoilContinental { get; set; }

        /// <summary>
        /// artic air emission
        /// </summary>
        [JsonPropertyName("e_aA")]
        public float EmissionToAirArctic { get; set; }

        /// <summary>
        /// artic water emission
        /// </summary>
        [JsonPropertyName("e_w2A")]
        public float EmissionToWaterArctic { get; set; }

        /// <summary>
        /// artic soil emission
        /// </summary>
        [JsonPropertyName("e_sA")]
        public float EmissionToSoilArctic { get; set; }

        /// <summary>
        /// tropical air emission
        /// </summary>
        [JsonPropertyName("e_aT")]
        public float EmissionToAirTropical { get; set; }

        /// <summary>
        /// tropical water emission
        /// </summary>
        [JsonPropertyName("e_w2T")]
        public float EmissionToWaterTropical { get; set; }

        /// <summary>
        /// artic soil emission
        /// </summary>
        [JsonPropertyName("e_sT")]
        public float EmissionToSoilTropical { get; set; }

        /// <summary>
        /// moderate air emission
        /// </summary>
        [JsonPropertyName("e_aM")]
        public float EmissionToAirModerate { get; set; }

        /// <summary>
        /// moderate water emission
        /// </summary>
        [JsonPropertyName("e_w2M")]
        public float EmissionToWaterModerate { get; set; }

        /// <summary>
        /// moderate soil emission
        /// </summary>
        [JsonPropertyName("e_sM")]
        public float EmissionToSoilModerate { get; set; }

        public NovaDTO(ScenarioDTO scenario, EmissionDTO emission, MaterialDTO material)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ScenarioDTO, NovaDTO>();
                cfg.CreateMap<EmissionDTO, NovaDTO>();
                cfg.CreateMap<MaterialDTO, NovaDTO>();
            });

            IMapper mapper = config.CreateMapper();
            mapper.Map(scenario, this);
            mapper.Map(emission, this);
            mapper.Map(material, this);
        }
    }
}