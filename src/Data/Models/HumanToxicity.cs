using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ProplanetReplicationTool.Data.Models
{    /// <summary>
     /// Represent an human toxicity in the database
     /// inherits from Entity
     /// </summary>
    [Table("human_toxicities")]
    public class HumanToxicity : Entity
    {
        /// <summary>
        /// Inhalation systemic long toxicity
        /// </summary>
        [Column("w_inhalation_systemic_long")]
        [Required]
        public float WInhalationSystemicLong { get; set; }

        /// <summary>
        /// Inhalation systemic short toxicity
        /// </summary>
        [Column("w_inhalation_systemic_short")]
        [Required]
        public float WInhalationSystemicShort { get; set; }

        /// <summary>
        /// Inhalation local long toxicity
        /// </summary>
        [Column("w_inhalation_local_long")]
        [Required]
        public float WInhalationLocalLong { get; set; }

        /// <summary>
        /// Inhalation local short toxicity
        /// </summary>
        [Column("w_inhalation_local_short")]
        [Required]
        public float WInhalationLocalShort { get; set; }

        /// <summary>
        /// Dermal systemic long toxicity
        /// </summary>
        [Column("w_dermal_systemic_long")]
        [Required]
        public float WDermalSystemicLong { get; set; }

        /// <summary>
        /// Dermal systemic short toxicity
        /// </summary>
        [Column("w_dermal_systemic_short")]
        [Required]
        public float WDermalSystemicShort { get; set; }

        /// <summary>
        /// Dermal local long toxicity
        /// </summary>
        [Column("w_dermal_local_long")]
        [Required]
        public float WDermalLocalLong { get; set; }

        /// <summary>
        /// Dermal local short toxicity
        /// </summary>
        [Column("w_dermal_local_short")]
        [Required]
        public float WDermalLocalShort { get; set; }

        /// <summary>
        /// Eyes local toxicity
        /// </summary>
        [Column("w_eyes_local")]
        [Required]
        public float WEyesLocal { get; set; }

        /// <summary>
        /// Oral systemic long toxicity
        /// </summary>
        [Column("p_inhalation_systemic_long")]
        [Required]
        public float PInhalationSystemicLong { get; set; }

        /// <summary>
        /// Inhalation systemic short toxicity
        /// </summary>
        [Column("p_inhalation_systemic_short")]
        [Required]
        public float PInhalationSystemicShort { get; set; }

        /// <summary>
        /// Inhalation local long toxicity
        /// </summary>
        [Column("p_inhalation_local_long")]
        [Required]
        public float PInhalationLocalLong { get; set; }

        /// <summary>
        /// Inhalation local short toxicity
        /// </summary>
        [Column("p_inhalation_local_short")]
        [Required]
        public float PInhalationLocalShort { get; set; }

        /// <summary>
        /// Dermal systemic long toxicity
        /// </summary>
        [Column("p_dermal_systemic_long")]
        [Required]
        public float PDermalSystemicLong { get; set; }

        /// <summary>
        /// Dermal systemic short toxicity
        /// </summary>
        [Column("p_dermal_systemic_short")]
        [Required]
        public float PDermalSystemicShort { get; set; }

        /// <summary>
        /// Dermal local long toxicity
        /// </summary>
        [Column("p_dermal_local_long")]
        [Required]
        public float PDermalLocalLong { get; set; }

        /// <summary>
        /// Dermal local short toxicity
        /// </summary>
        [Column("p_dermal_local_short")]
        [Required]
        public float PDermalLocalShort { get; set; }

        /// <summary>
        /// Eyes local toxicity
        /// </summary>
        [Column("p_eyes_local")]
        [Required]
        public float PEyesLocal { get; set; }

        /// <summary>
        /// Oral systemic long toxicity
        /// </summary>
        [Column("p_oral_systemic_long")]
        [Required]
        public float POralSystemicLong { get; set; }

        /// <summary>
        /// Oral systemic short toxicity
        /// </summary>
        [Column("p_oral_systemic_short")]
        [Required]
        public float POralSystemicShort { get; set; }

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