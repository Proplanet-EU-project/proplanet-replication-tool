using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProplanetReplicationTool.Data.Models
{    /// <summary>
     /// Represent a Nova test result in the database
     /// inherits from Entity
     /// </summary>
    [Table("sb_tests")]
    public class SBTest : Entity
    {
        /// <summary>
        /// Name of the Nova test result
        /// </summary>
        [Column("name")]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Result of the Nova test in json format
        /// </summary>
        [Column("result", TypeName = "jsonb")]
        [Required]
        public string Result { get; set; }

        #region Relationships

        /// <summary>
        /// Substance relationship
        /// </summary>
        public SBTestInput SBTestInput { get; set; }

        #endregion Relationships
    }
}