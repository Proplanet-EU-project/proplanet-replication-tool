using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProplanetReplicationTool.Data.Models
{    /// <summary>
     /// Represent a Nova test input in the database
     /// inherits from Entity
     /// </summary>
    [Table("sb_tests_inputs")]
    public class SBTestInput : Entity
    {
        /// <summary>
        /// Name of the Nova test input
        /// </summary>
        [Column("scenario_name")]
        [Required]
        public string ScenarioName { get; set; }

        /// <summary>
        /// Name of the substance
        /// </summary>
        [Column("nanomaterial_name")]
        [Required]
        public string NanomaterialName { get; set; }

        /// <summary>
        /// Molecular weight of the substance
        /// </summary>
        [Column("molecular_weight")]
        [Required]
        public float MolecularWeight { get; set; }

        /// <summary>
        /// Regional emission air
        /// </summary>
        [Column("regional_emission_air")]
        [Required]
        public float EmissionToAirRegional { get; set; }  // e_aR

        /// <summary>
        /// Regional emission water0
        /// </summary>
        [Column("regional_emission_water0")]
        [Required]
        public float EmissionToLakeWaterRegional { get; set; } // e_w0R

        /// <summary>
        /// Regional emission water1
        /// </summary>
        [Column("regional_emission_water1")]
        [Required]
        public float EmissionToFreshWaterRegional { get; set; } // e_w1R

        /// <summary>
        /// Regional emission water2
        /// </summary>
        [Column("regional_emission_water2")]
        [Required]
        public float EmissionToSeaWaterRegional { get; set; } // e_w2R

        /// <summary>
        /// Regional emission natural soil
        /// </summary>
        [Column("regional_emission_natural_soil")]
        [Required]
        public float EmissionToNaturalSoilRegional { get; set; } // e_s1R

        /// <summary>
        /// Regional emission agricultural soil
        /// </summary>
        [Column("regional_emission_agricultural_soil")]
        [Required]
        public float EmissionToAgriculturalSoilRegional { get; set; } // e_s2R

        /// <summary>
        /// Regional emission other soil
        /// </summary>
        [Column("regional_emission_other_soil")]
        [Required]
        public float EmissionToOtherSoilRegional { get; set; } // e_s3R

        /// <summary>
        /// Continental emission air
        /// </summary>
        [Column("continental_emission_air")]
        [Required]
        public float EmissionToAirContinental { get; set; } // e_aC

        /// <summary>
        /// Continental emission water0
        /// </summary>
        [Column("continental_emission_water0")]
        [Required]
        public float EmissionToLakeWaterContinental { get; set; } // e_w0C

        /// <summary>
        /// Continental emission water1
        /// </summary>
        [Column("continental_emission_water1")]
        [Required]
        public float EmissionToFreshWaterContinental { get; set; } // e_w1C

        /// <summary>
        /// Continental emission water2
        /// </summary>
        [Column("continental_emission_water2")]
        [Required]
        public float EmissionToSeaWaterContinental { get; set; } // e_w2C

        /// <summary>
        /// Continental emission natural soil
        /// </summary>
        [Column("continental_emission_natural_soil")]
        [Required]
        public float EmissionToNaturalSoilContinental { get; set; } // e_s1C

        /// <summary>
        /// Continental emission agricultural soil
        /// </summary>
        [Column("continental_emission_agricultural_soil")]
        [Required]
        public float EmissionToAgriculturalSoilContinental { get; set; } // e_s2C

        /// <summary>
        /// Continental emission other soil
        /// </summary>
        [Column("continental_emission_other_soil")]
        [Required]
        public float EmissionToOtherSoilContinental { get; set; } // e_s3C

        /// <summary>
        /// Artic emission air
        /// </summary>
        [Column("artic_emission_air")]
        [Required]
        public float EmissionToAirArctic { get; set; } // e_aA

        /// <summary>
        /// Artic emission water2
        /// </summary>
        [Column("artic_emission_water2")]
        [Required]
        public float EmissionToWaterArctic { get; set; } // e_w2A

        /// <summary>
        /// Artic emission soil
        /// </summary>
        [Column("artic_emission_soil")]
        [Required]
        public float EmissionToSoilArctic { get; set; } // e_sA

        /// <summary>
        /// Tropical emission air
        /// </summary>
        [Column("tropical_emission_air")]
        [Required]
        public float EmissionToAirTropical { get; set; } // e_aT

        /// <summary>
        /// Tropical emission water2
        /// </summary>
        [Column("tropical_emission_water2")]
        [Required]
        public float EmissionToWaterTropical { get; set; } // e_w2T

        /// <summary>
        /// Tropical emission soil
        /// </summary>
        [Column("tropical_emission_soil")]
        [Required]
        public float EmissionToSoilTropical { get; set; } // e_sT

        /// <summary>
        /// Moderate emission air
        /// </summary>
        [Column("moderate_emission_air")]
        [Required]
        public float EmissionToAirModerate { get; set; } // e_aM

        /// <summary>
        /// Moderate emission water2
        /// </summary>
        [Column("moderate_emission_water2")]
        [Required]
        public float EmissionToWaterModerate { get; set; } // e_w2M

        /// <summary>
        /// Moderate emission soil
        /// </summary>
        [Column("moderate_emission_soil")]
        [Required]
        public float EmissionToSoilModerate { get; set; } // e_sM

        /// <summary>
        /// Unique fingerprint generate from novaTest entity
        /// </summary>
        [Column("fingerprint")]
        [Required]
        public string Fingerprint { get; set; }

        #region Relationships

        /// <summary>
        /// Nova test id relationship
        /// </summary>
        [Column("sb_test_id")]
        [Required]
        public Guid SBTestId { get; set; }

        /// <summary>
        /// Nova test relationship
        /// </summary>
        public SBTest SBTest { get; set; }

        #endregion Relationships
    }
}