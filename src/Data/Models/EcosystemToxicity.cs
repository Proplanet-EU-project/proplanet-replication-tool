using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ProplanetReplicationTool.Data.Models
{    /// <summary>
     /// Represent an ecosystem toxicity in the database
     /// inherits from Entity
     /// </summary>
    [Table("ecosystem_toxicities")]
    public class EcosystemToxicity : Entity
    {
        /// <summary>
        /// Aquatic freshwater toxicity
        /// </summary>
        [Column("aquatic_freshwater")]
        [Required]
        public float AquaticFreshwater { get; set; }

        /// <summary>
        /// Aquatic marine water toxicity
        /// </summary>
        [Column("aquatic_marinewater")]
        [Required]
        public float AquaticMarinewater { get; set; }

        /// <summary>
        /// Aquatic stp toxicity
        /// </summary>
        [Column("aquatic_stp")]
        [Required]
        public float AquaticStp { get; set; }

        /// <summary>
        /// Aquatic sediment freshwater toxicity
        /// </summary>
        [Column("aquatic_sediment_freshwater")]
        [Required]
        public float AquaticSedimentFreshwater { get; set; }

        /// <summary>
        /// Aquarium sediment marine water toxicity
        /// </summary>
        [Column("aquatic_sediment_marinewater")]
        [Required]
        public float AquaticSedimentMarinewater { get; set; }

        /// <summary>
        /// Air toxicity
        /// </summary>
        [Column("air")]
        [Required]
        public float Air { get; set; }

        /// <summary>
        /// Terrestrial soil toxicity
        /// </summary>
        [Column("terrestrial_soil")]
        [Required]
        public float TerrestrialSoil { get; set; }

        /// <summary>
        /// Predators oral poisoning
        /// </summary>
        [Column("predators_oral_poisoning")]
        [Required]
        public float PredatorsOralPoisoning { get; set; }

        #region Relationships

        /// <summary>
        /// Material id relationship
        /// </summary>
        [Column("material_id")]
        [Required]
        public Guid MaterialId { get; set; }

        /// <summary>
        /// Material relationship
        /// </summary>
        [JsonIgnore]
        [NotMapped]
        public Material? Material { get; set; }

        #endregion Relationships
    }
}