using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using FuzzyLogicApi.Models.SubsetDTOs;

namespace FuzzyLogicApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SubsetController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public SubsetController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateSubsetDTO subsetDTO)
        {
            var createdSubsetDTO = await _serviceManager
                .SubsetService
                .CreateSubsetAsync(subsetDTO);

            return Ok(createdSubsetDTO);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllByTermIdAsync(int termId)
        {
            var subsetDTOs = await _serviceManager
                .SubsetService
                .GetAllSubsetsByTermIdAsync(termId);

            return Ok(subsetDTOs);
        }
    }
}
