using Domain.Entities;
using Domain.RepositoryInterfaces;

namespace Persistence.Repositories
{
    public class FuzzyLogicAreaRepository : GenericRepository<FuzzyLogicArea>, IFuzzyLogicAreaRepository
    {
        public FuzzyLogicAreaRepository(FuzzyContext context) : base(context) { }
    }
}
