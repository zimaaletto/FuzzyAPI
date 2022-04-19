using FuzzyLogicApi.Models.RuleDTOs;

namespace Services.Interfaces
{
    public interface IRuleService
    {
        Task<List<RuleDTO>> GetAllRulesByFuzzyLogicAreaIdAsync(int fuzzyLogicAreaId);
        Task<RuleDTO> CreateRuleAsync(CreateRuleDTO createRuleDTO);
    }
}
