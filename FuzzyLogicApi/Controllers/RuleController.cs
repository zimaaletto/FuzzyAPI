using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using FuzzyLogicApi.Models.RuleDTOs;

namespace FuzzyLogicApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RuleController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public RuleController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateRuleDTO ruleDTO)
        {
            var createdRuleDTO = await _serviceManager
                .RuleService
                .CreateRuleAsync(ruleDTO);

            return Ok(createdRuleDTO);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllByFuzzyLogicAreaIdAsync(int fuzzyLogicAreaId)
        {
            var ruleDTOs = await _serviceManager
                .RuleService
                .GetAllRulesByFuzzyLogicAreaIdAsync(fuzzyLogicAreaId);

            return Ok(ruleDTOs);
        }
    }
}
