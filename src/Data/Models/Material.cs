using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProplanetReplicationTool.Data.Models
{    /// <summary>
     /// Represents a material in the database
     /// inherits from Entity
     /// </summary>
    [Table("materials")]
    public class Material : Entity
    {
        /// <summary>
        /// Name of the material
        /// </summary>
        [Column("name")]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Formula of the material
        /// </summary>
        [Column("formula")]
        [Required]
        public string Formula { get; set; }

        /// <summary>
        /// Cas of the material
        /// </summary>
        [Column("cas")]
        [Required]
        public string Cas { get; set; }

        /// <summary>
        /// Category of the material
        /// </summary>
        [Column("category")]
        [Required]
        public string Category { get; set; }

        /// <summary>
        /// Number of carbon atoms in the material
        /// </summary>
        [Column("total_carbon_atoms")]
        [Required]
        public int TotalCarbonAtoms { get; set; }

        /// <summary>
        /// molecular weight of the material
        /// </summary>
        [Column("molecular_weight")]
        [Required]
        public float MolecularWeight { get; set; }

        #region Relationships

        /// <summary>
        /// Ecosystem toxicity of the material (one to tone)
        /// </summary>
        public EcosystemToxicity? EcosystemToxicity { get; set; }

        /// <summary>
        /// Human toxicity of the material (one to one)
        /// </summary>
        public HumanToxicity? HumanToxicity { get; set; }

        /// <summary>
        /// Rina test of the material (one to one)
        /// </summary>
        public RinaTest? RinaTest { get; set; }

        #endregion Relationships
    }
}