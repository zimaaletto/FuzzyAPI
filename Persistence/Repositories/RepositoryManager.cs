using Domain.RepositoryInterfaces;

namespace Persistence.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly FuzzyContext _fuzzyContext;
        public RepositoryManager(FuzzyContext fuzzyContext) => _fuzzyContext = fuzzyContext;
        public IFuzzyLogicAreaRepository FuzzyLogicArea => new FuzzyLogicAreaRepository(_fuzzyContext);
        public IRuleRepository Rule => new RuleRepository(_fuzzyContext);
        public ISubsetRepository Subset => new SubsetRepository(_fuzzyContext);
        public ITermRepository Term => new TermRepository(_fuzzyContext);
        public async Task SaveAsync()
        {
            await _fuzzyContext.SaveChangesAsync();
        }
    }
}
