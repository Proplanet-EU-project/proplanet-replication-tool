using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProplanetReplicationTool.Data.Models.DTOs
{    /// <summary>
     /// Represents the scenario entity
     /// </summary>
    public class MaterialDTO
    {
        /// <summary>
        /// scenario name
        /// </summary>
        [JsonPropertyName("material")]
        public string NanomaterialName { get; set; }

        /// <summary>
        /// molecular weight
        /// </summary>
        [JsonPropertyName("molweight")]
        public float MolecularWeight { get; set; }

        /// <summary>
        /// Number of carbon atoms in the material
        /// </summary>
        [JsonPropertyName("total_carbon_atoms")]
        public int TotalCarbonAtoms { get; set; }

        [NotMapped]
        public int LowerMol { get; set; } = 1;

        [NotMapped]
        public int UpperMol { get; set; } = 5;

        [NotMapped]
        public bool UseInLungDeposition { get; set; } = true;

        [NotMapped]
        public bool UseInPerformance { get; set; } = true;

        [NotMapped]
        public int ModifiedTotalCarbonAtoms { get; set; } = 0;
    }
}