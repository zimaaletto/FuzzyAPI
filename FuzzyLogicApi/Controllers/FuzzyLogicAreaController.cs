using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using FuzzyLogicApi.Models.FuzzyLogicAreaDTOs;

namespace FuzzyLogicApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class FuzzyLogicAreaController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public FuzzyLogicAreaController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateFuzzyLogicAreaDTO fuzzyLogicAreaDTO)
        {
            var createdFuzzyLogicAreaDTO = await _serviceManager
                .FuzzyLogicAreaService
                .CreateFuzzyLogicAreaAsync(fuzzyLogicAreaDTO);

            return Ok(createdFuzzyLogicAreaDTO);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var fuzzyLogicAreasDTOs = await _serviceManager
                .FuzzyLogicAreaService
                .GetAllFuzzyLogicAreasAsync();

            return Ok(fuzzyLogicAreasDTOs);
        }
    }
}
