using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using ProplanetReplicationTool.Data.Models;

namespace ProplanetReplicationTool.Data
{
    /// <summary>
    /// Class DbInitializer. Database migration and seeding utilities
    /// </summary>
    public class DbInitializer
    {
        /// <summary>
        /// Initializes the database
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public static void Initialize(IServiceProvider serviceProvider)
        {
            //DI
            var dbContextFactory = serviceProvider.GetRequiredService<IDbContextFactory<ApplicationDbContext>>();
            using var applicationDbContext = dbContextFactory.CreateDbContext();

            var webHostEnvironment = serviceProvider.GetRequiredService<IWebHostEnvironment>();

            //Migrations
            var isConnected = false;
            while (!isConnected)
            {
                try
                {
                    applicationDbContext.Database.Migrate();
                    isConnected = true;
                }
                catch (Exception ex)
                {
                    var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred migrating the DB.");
                }
                Thread.Sleep(1_000);
            }

            #region Common Seeding

            // This seeding will happen in production and in development

            #endregion Common Seeding

            #region Production Seeding

            if (webHostEnvironment.IsProduction())

            {
                // This seeding will only happen in production. Do not include example data here
            }

            #endregion Production Seeding

            if (!webHostEnvironment.IsDevelopment()) return;

            #region Development Seeding

            #region Example seeding

            if (!applicationDbContext.Materials.Any())
            {
                var materials = new List<Material>()
                {
                    new Material { Name = "Ethanol", Formula = "C2H5OH", Cas = "64-17-5", Category = "Alcohol", TotalCarbonAtoms = 2, MolecularWeight = 46.08f },
                    new Material { Name = "Methane", Formula = "CH4", Cas = "74-82-8", Category = "Hydrocarbon", TotalCarbonAtoms = 1, MolecularWeight = 16.04f },
                    new Material { Name = "Benzene", Formula = "C6H6", Cas = "71-43-2", Category = "Aromatic Hydrocarbon", TotalCarbonAtoms = 6, MolecularWeight = 78.11f },
                    new Material { Name = "Acetone", Formula = "C3H6O", Cas = "67-64-1", Category = "Ketone", TotalCarbonAtoms = 3, MolecularWeight = 58.08f },
                    new Material { Name = "Carbon Dioxide", Formula = "CO2", Cas = "124-38-9", Category = "Inorganic Compound", TotalCarbonAtoms = 1, MolecularWeight = 44.01f },
                    new Material { Name = "Hexadecyltrimethoxysilane", Formula = "C19H42O3Si", Cas = "16415-12-6", Category = "test", TotalCarbonAtoms = 16, MolecularWeight = 347 },
                    new Material { Name = "Hexamethyldisiloxane", Formula = "C6H18OSi2", Cas = "107-46-0", Category = "test", TotalCarbonAtoms = 1, MolecularWeight = 162 },
                    new Material { Name = "Trimethoxyphenylsilane", Formula = "(CH3O)3SiC6H5", Cas = "2996-92-1", Category = "test", TotalCarbonAtoms = 6, MolecularWeight = 198 },
                    new Material { Name = "Polysiloxanes, di-Me, hydroxy-terminated", Formula = "HOSi", Cas = "70131-67-8", Category = "test", TotalCarbonAtoms = 2, MolecularWeight = 550 },
                    new Material { Name = "Polysiloxanes, di-Me (Silicon oil)", Formula = "C2H8O2Si", Cas = "63148-62-9", Category = "test", TotalCarbonAtoms = 2, MolecularWeight = 237 },
                    new Material { Name = "Acetic acid", Formula = "CH3COOH", Cas = "64-19-7", Category = "test", TotalCarbonAtoms = 0, MolecularWeight = 60 },
                    new Material { Name = "Starch (Corn starch)", Formula = "C6H10O5", Cas = "9005-25-8", Category = "test", TotalCarbonAtoms = 0, MolecularWeight = 342 },
                    new Material { Name = "Zinc oxide (ZnO)", Formula = "ZnO", Cas = "1314-13-2", Category = "test", TotalCarbonAtoms = 0, MolecularWeight = 81 },
                    new Material { Name = "Sodium alginate", Formula = "C6H7NaO6", Cas = "9005-38-3", Category = "test", TotalCarbonAtoms = 0, MolecularWeight = 198 },
                    new Material { Name = "Glycerol", Formula = "C3H8O3", Cas = "56-81-5", Category = "test", TotalCarbonAtoms = 0, MolecularWeight = 92 },
                    new Material { Name = "Chitosan", Formula = "C56H103N9O39", Cas = "9012-76-4", Category = "test", TotalCarbonAtoms = 0, MolecularWeight = 1527 },
                    new Material { Name = "2-Octenylsuccinic anhydride", Formula = "C12H18O3", Cas = "42482-06-4", Category = "test", TotalCarbonAtoms = 0, MolecularWeight = 210 },
                    new Material { Name = "Dodecyltriethoxysilane", Formula = "C18H40O3Si", Cas = "18536-91-9", Category = "test", TotalCarbonAtoms = 12, MolecularWeight = 333 },
                    new Material { Name = "Methyltrimethoxysilane", Formula = "C4H12O3Si", Cas = "1185-55-3", Category = "test", TotalCarbonAtoms = 1, MolecularWeight = 136 },
                    new Material { Name = "Octyltrimethoxysilane", Formula = "CH3(CH2)7Si(OCH3)3", Cas = "3069-40-7", Category = "test", TotalCarbonAtoms = 8, MolecularWeight = 12 }
                };

                applicationDbContext.Materials.AddRange(materials);
                applicationDbContext.SaveChanges();
            }

            var ethanol = applicationDbContext.Materials.SingleOrDefault(m => m.Name == "Ethanol");
            var methane = applicationDbContext.Materials.SingleOrDefault(m => m.Name == "Methane");
            var benzene = applicationDbContext.Materials.SingleOrDefault(m => m.Name == "Benzene");
            var acetone = applicationDbContext.Materials.SingleOrDefault(m => m.Name == "Acetone");
            var carbonDioxide = applicationDbContext.Materials.SingleOrDefault(m => m.Name == "Carbon Dioxide");

            if (!applicationDbContext.HumanToxicities.Any())
            {
                var humanToxicities = new List<HumanToxicity>
                {
                    new HumanToxicity { WInhalationSystemicLong = 0.5f, WInhalationSystemicShort = 0.3f, WInhalationLocalLong = 0.2f, WInhalationLocalShort = 0.1f,
                                       WDermalSystemicLong = 0.4f, WDermalSystemicShort = 0.2f, WDermalLocalLong = 0.3f, WDermalLocalShort = 0.2f, WEyesLocal = 0.1f,
                                       PInhalationSystemicLong = 0.6f, PInhalationSystemicShort = 0.4f, PInhalationLocalLong = 0.3f, PInhalationLocalShort = 0.2f,
                                       PDermalSystemicLong = 0.5f, PDermalSystemicShort = 0.3f, PDermalLocalLong = 0.4f, PDermalLocalShort = 0.2f,
                                       PEyesLocal = 0.1f, POralSystemicLong = 0.7f, POralSystemicShort = 0.4f, MaterialId = ethanol.Id },

                    new HumanToxicity { WInhalationSystemicLong = 0.8f, WInhalationSystemicShort = 0.5f, WInhalationLocalLong = 0.4f, WInhalationLocalShort = 0.3f,
                                       WDermalSystemicLong = 0.6f, WDermalSystemicShort = 0.4f, WDermalLocalLong = 0.5f, WDermalLocalShort = 0.3f, WEyesLocal = 0.2f,
                                       PInhalationSystemicLong = 0.9f, PInhalationSystemicShort = 0.6f, PInhalationLocalLong = 0.5f, PInhalationLocalShort = 0.4f,
                                       PDermalSystemicLong = 0.7f, PDermalSystemicShort = 0.5f, PDermalLocalLong = 0.6f, PDermalLocalShort = 0.4f,
                                       PEyesLocal = 0.2f, POralSystemicLong = 0.8f, POralSystemicShort = 0.5f, MaterialId = benzene.Id },

                    new HumanToxicity { WInhalationSystemicLong = 0.4f, WInhalationSystemicShort = 0.2f, WInhalationLocalLong = 0.1f, WInhalationLocalShort = 0.1f,
                                       WDermalSystemicLong = 0.3f, WDermalSystemicShort = 0.1f, WDermalLocalLong = 0.2f, WDermalLocalShort = 0.1f, WEyesLocal = 0.05f,
                                       PInhalationSystemicLong = 0.5f, PInhalationSystemicShort = 0.3f, PInhalationLocalLong = 0.2f, PInhalationLocalShort = 0.2f,
                                       PDermalSystemicLong = 0.4f, PDermalSystemicShort = 0.2f, PDermalLocalLong = 0.3f, PDermalLocalShort = 0.2f,
                                       PEyesLocal = 0.1f, POralSystemicLong = 0.6f, POralSystemicShort = 0.3f, MaterialId = acetone.Id }
                };

                applicationDbContext.HumanToxicities.AddRange(humanToxicities);
                applicationDbContext.SaveChanges();
            }

            if (!applicationDbContext.EcosystemToxicities.Any())
            {
                var ecosystemToxicities = new List<EcosystemToxicity>
                {
                    new EcosystemToxicity { AquaticFreshwater = 0.5f, AquaticMarinewater = 0.3f, AquaticStp = 0.2f, AquaticSedimentFreshwater = 0.4f,
                                            AquaticSedimentMarinewater = 0.3f, Air = 0.1f, TerrestrialSoil = 0.2f, PredatorsOralPoisoning = 0.5f, MaterialId = ethanol.Id },

                    new EcosystemToxicity { AquaticFreshwater = 0.8f, AquaticMarinewater = 0.5f, AquaticStp = 0.3f, AquaticSedimentFreshwater = 0.6f,
                                            AquaticSedimentMarinewater = 0.5f, Air = 0.3f, TerrestrialSoil = 0.4f, PredatorsOralPoisoning = 0.7f, MaterialId = methane.Id },

                    new EcosystemToxicity { AquaticFreshwater = 0.3f, AquaticMarinewater = 0.2f, AquaticStp = 0.1f, AquaticSedimentFreshwater = 0.2f,
                                            AquaticSedimentMarinewater = 0.1f, Air = 0.05f, TerrestrialSoil = 0.1f, PredatorsOralPoisoning = 0.2f, MaterialId = carbonDioxide.Id }
                };

                applicationDbContext.EcosystemToxicities.AddRange(ecosystemToxicities);
                applicationDbContext.SaveChanges();
            }

            #endregion Example seeding

            #endregion Development Seeding
        }
    }
}