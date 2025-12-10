using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProplanetReplicationTool.Data.Models
{    /// <summary>
     /// Represent a Rina test in the database
     /// inherits from Entity
     /// </summary>
    [Table("rina_tests")]
    public class RinaTest : Entity
    {
        /// <summary>
        /// LogPow of rina test
        /// </summary>
        [Column("log_pow")]
        [Required]
        public float LogPow { get; set; }

        /// <summary>
        /// LogKow of rina test
        /// </summary>
        [Column("log_kow")]
        [Required]
        public float LogKow { get; set; }

        /// <summary>
        /// LumoHomo of rina test
        /// </summary>
        [Column("lumo_homo")]
        [Required]
        public float LumoHomo { get; set; }

        /// <summary>
        /// Distance of rina test
        /// </summary>
        [Column("distance")]
        [Required]
        public float Distance { get; set; }

        /// <summary>
        /// Interaction energy of rina test
        /// </summary>
        [Column("interaction_energy")]
        [Required]
        public float InteractionEnergy { get; set; }

        /// <summary>
        /// Wca of rina test
        /// </summary>
        [Column("wca")]
        [Required]
        public float Wca { get; set; }

        /// <summary>
        /// Toxicity of rina test
        /// </summary>
        [Column("toxicity")]
        [Required]
        public float Toxicity { get; set; }

        #region Relationships

        /// <summary>
        /// Material id relationship
        /// </summary>
        [Column("material_id")]
        [Required]
        public Guid MaterialId { get; set; }

        /// <summary>
        /// Substance relationship
        /// </summary>
        public Material Material { get; set; }

        #endregion Relationships
    }
}