using Microsoft.AspNetCore.Mvc;
using ProplanetReplicationTool.Data.Models;
using ProplanetReplicationTool.Data.Repositories;

namespace ProplanetReplicationTool.Controllers
{
    /// <summary>
    /// EcosystemToxicity controller class
    /// </summary>
    [Route("ecosystem_toxicities")]
    [ApiController]
    public class EcosystemToxicityController : ControllerBase
    {
        private readonly IRepository<Material> _materialRepository;
        private readonly IRepository<EcosystemToxicity> _ecosystemToxicityRepository;

        /// <summary>
        /// Constructor injecting the needed repositories
        /// </summary>
        /// <param name="materialRepository"></param>
        /// <param name="ecosystemToxicityRepository"></param>
        public EcosystemToxicityController(IRepository<Material> materialRepository, IRepository<EcosystemToxicity> ecosystemToxicityRepository)
        {
            _materialRepository = materialRepository;
            _ecosystemToxicityRepository = ecosystemToxicityRepository;
        }

        /// <summary>
        /// Endpoint to create a ecosystem toxicity for a material by material name
        /// </summary>
        /// <param name="materialName"></param>
        /// <param name="ecosystemToxicity"></param>
        [HttpPost("{materialName}")]
        public async Task<IActionResult> CreateEcosystemToxicity(string materialName, EcosystemToxicity ecosystemToxicity)
        {
            try
            {
                var materialsDB = await _materialRepository.GetSingleOrDefaultAsync(m => m.Name == materialName);
                if (materialsDB == null)
                {
                    return NotFound($"Material with name '{materialName}' not found.");
                }

                ecosystemToxicity.MaterialId = materialsDB.Id;
                var ecosystemToxicityCreated = await _ecosystemToxicityRepository.AddAsync(ecosystemToxicity);
                if (!ecosystemToxicityCreated.Item1)
                {
                    return BadRequest("Failed to create the human toxicity.");
                }
                return Ok(ecosystemToxicity);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the material.");
            }
        }
    }
}