using Microsoft.AspNetCore.Mvc;
using ProplanetReplicationTool.Data.Models;
using ProplanetReplicationTool.Data.Repositories;

namespace ProplanetReplicationTool.Controllers
{
    /// <summary>
    /// HumanToxicity controller class
    /// </summary>
    [Route("human_toxicities")]
    [ApiController]
    public class HumanToxicityController : ControllerBase
    {
        private readonly IRepository<Material> _materialRepository;
        private readonly IRepository<HumanToxicity> _humanToxicityRepository;

        /// <summary>
        /// Constructor injecting the needed repositories
        /// </summary>
        /// <param name="materialRepository"></param>
        /// <param name="humanToxicityRepository"></param>
        public HumanToxicityController(IRepository<Material> materialRepository, IRepository<HumanToxicity> humanToxicityRepository)
        {
            _materialRepository = materialRepository;
            _humanToxicityRepository = humanToxicityRepository;
        }

        /// <summary>
        /// Endpoint to create a human toxicity for a material by material name
        /// </summary>
        /// <param name="materialName"></param>
        /// <param name="humanToxicity"></param>
        [HttpPost("{materialName}")]
        public async Task<IActionResult> CreateHumanToxicity(string materialName, HumanToxicity humanToxicity)
        {
            try
            {
                var materialsDB = await _materialRepository.GetSingleOrDefaultAsync(m => m.Name == materialName);
                if (materialsDB == null)
                {
                    return NotFound($"Material with name '{materialName}' not found.");
                }

                humanToxicity.MaterialId = materialsDB.Id;
                var humanToxicityCreated = await _humanToxicityRepository.AddAsync(humanToxicity);
                if (!humanToxicityCreated.Item1)
                {
                    return BadRequest("Failed to create the human toxicity.");
                }
                return Ok(humanToxicity);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the material.");
            }
        }
    }
}