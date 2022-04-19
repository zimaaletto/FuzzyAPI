
namespace Domain.RepositoryInterfaces
{
    public interface IRepositoryManager
    {
        IFuzzyLogicAreaRepository FuzzyLogicArea { get; }
        IRuleRepository Rule { get; }
        ISubsetRepository Subset { get; }
        ITermRepository Term { get; }
        Task SaveAsync();
    }
}