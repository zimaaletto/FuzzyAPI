using Domain.Entities;
using Domain.RepositoryInterfaces;

namespace Persistence.Repositories
{
    public class RuleRepository : GenericRepository<Rule>, IRuleRepository
    {
        public RuleRepository(FuzzyContext context) : base(context) { }
    }
}
