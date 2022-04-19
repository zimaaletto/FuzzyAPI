using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using FuzzyLogicApi.Models.TermDTOs;

namespace FuzzyLogicApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TermController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public TermController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateTermDTO termDTO)
        {
            var createdTermDTO = await _serviceManager
                .TermService
                .CreateTermAsync(termDTO);

            return Ok(createdTermDTO);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllByFuzzyLogicAreaIdAsync(int fuzzyLogicAreaId)
        {
            var termDTOs = await _serviceManager
                .TermService
                .GetAllTermsByFuzzyLogicAreaIdAsync(fuzzyLogicAreaId);

            return Ok(termDTOs);
        }
    }
}
