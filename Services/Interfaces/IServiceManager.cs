
namespace Services.Interfaces
{
    public interface IServiceManager
    {
        IFuzzyLogicAreaService FuzzyLogicAreaService { get; }
        IRuleService RuleService { get; }
        ISubsetService SubsetService { get; }
        ITermService TermService { get; }
        IProcessService ProcessService { get; }
    }
}
