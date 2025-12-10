using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using ProplanetReplicationTool.Data.Models;

namespace ProplanetReplicationTool.Data
{
    /// <summary>
    /// Application Database Context. It is used to interact with the database using Entity Framework Core
    /// and to define the tables and relationships between them
    /// also to define the default values for the columns
    /// </summary>
    /// <param name="options"></param>
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        /// <summary>
        /// Override the OnModelCreating method to define the default values for the columns and the relationships between the tables
        /// </summary>
        /// <param name="builder">An instance of ModelBuilder class that is used to configure the model for the database context</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // call the base class method
            base.OnModelCreating(builder);
        }

        /// <summary>
        /// Create a new instance of the ApplicationDbContext, used to interact with the database
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public static ApplicationDbContext Create(DbContextOptions<ApplicationDbContext> options)
        {
            return new ApplicationDbContext(options);
        }

        // Define the tables
        // DbSet is a collection of entities that can be queried from the database
        // It is used to query and save instances of the entity class
        // DbSet<X> is used to query and save instances of the X class

        /// <summary>
        /// Table of Materials in the database
        /// </summary>
        public DbSet<Material> Materials { get; set; }

        /// <summary>
        /// Table of EcosystemToxicities in the database
        /// </summary>
        public DbSet<EcosystemToxicity> EcosystemToxicities { get; set; }

        /// <summary>
        /// Table of HumanToxicities in the database
        /// </summary>
        public DbSet<HumanToxicity> HumanToxicities { get; set; }

        /// <summary>
        /// Table of RinaTests in the database
        /// </summary>
        public DbSet<RinaTest> RinaTests { get; set; }

        /// <summary>
        /// Table of SBTests in the database
        /// </summary>
        public DbSet<SBTest> SBTests { get; set; }

        /// <summary>
        /// Table of SBTestInputs in the database
        /// </summary>
        public DbSet<SBTestInput> SBTestInputs { get; set; }
    }
}