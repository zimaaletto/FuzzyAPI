using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Domain.Entities;

namespace FuzzyLogicApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProcessController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public ProcessController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost]
        public async Task<IActionResult> Process(string processingParams, string resultTermName)
        {
            var result = await _serviceManager.ProcessService.ProcessData(processingParams, resultTermName);
            return Ok(result);
        }
    }
}
