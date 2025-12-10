using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProplanetReplicationTool.Data.Models;
using ProplanetReplicationTool.Data.Repositories;

namespace ProplanetReplicationTool.Controllers
{
    /// <summary>
    /// Material controller class
    /// </summary>
    [Route("materials")]
    [ApiController]
    public class MaterialController : ControllerBase
    {
        private readonly IRepository<Material> _materialRepository;

        /// <summary>
        /// Constructor injecting the needed repositories
        /// </summary>
        /// <param name="materialRepository"></param>
        public MaterialController(IRepository<Material> materialRepository)
        {
            _materialRepository = materialRepository;
        }

        /// <summary>
        /// Endpoint to retrieve all materials
        /// </summary>
        [HttpGet("")]
        public async Task<IActionResult> GetAllMaterials()
        {
            try
            {
                var materials = await _materialRepository.GetAllAsync();
                if (materials == null || materials.Count == 0)
                {
                    return NotFound("Materials not found.");
                }

                return Ok(materials);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the materials.");
            }
        }

        /// <summary>
        /// Endpoint to retrieve a material by name
        /// </summary>
        /// <param name="materialName"></param>
        [HttpGet("{materialName}")]
        public async Task<IActionResult> GetMaterialByName(string materialName)
        {
            try
            {
                var material = await _materialRepository.GetSingleOrDefaultAsync(
                    predicate: m => m.Name == materialName,
                    include: q => q
                        .Include(m => m.EcosystemToxicity)
                        .Include(m => m.HumanToxicity)
                );

                if (material == null)
                {
                    return NotFound($"No material with name {materialName} found");
                }

                return Ok(material);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the material.");
            }
        }

        /// <summary>
        /// Endpoint to create a material
        /// </summary>
        /// <param name="material"></param>
        [HttpPost("")]
        public async Task<IActionResult> CreateMaterial(Material material)
        {
            try
            {
                var materialsDB = await _materialRepository.GetSingleOrDefaultAsync(m => m.Name == material.Name);
                if (materialsDB != null)
                {
                    return Conflict($"Material with name '{material.Name}' already exists.");
                }

                var materialCreated = await _materialRepository.AddAsync(material);
                if (!materialCreated.Item1)
                {
                    return BadRequest("Failed to create material.");
                }
                return Ok(material);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the material.");
            }
        }

        /// <summary>
        /// Endpoint to remove a material by name
        /// </summary>
        /// <param name="materialName"></param>
        //[HttpDelete("{materialName}")]
        //public async Task<IActionResult> DeleteMaterial(string materialName)
        //{
        //    try
        //    {
        //        var materialToDelete = await _materialRepository.GetSingleOrDefaultAsync(m => m.Name == materialName);
        //        if (materialToDelete == null)
        //        {
        //            return NotFound($"Material with name '{materialName}' not found.");
        //        }
        //        var result = await _materialRepository.RemoveAsync(materialToDelete.Id);
        //        if (!result.Item1)
        //        {
        //            return BadRequest("Failed to delete material.");
        //        }
        //        return Ok($"Material with name '{materialName}' deleted successfully.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting the material.");
        //    }
        //}
    }
}