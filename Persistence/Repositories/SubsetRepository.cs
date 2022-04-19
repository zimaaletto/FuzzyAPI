using Domain.Entities;
using Domain.RepositoryInterfaces;

namespace Persistence.Repositories
{
    public class SubsetRepository : GenericRepository<Subset>, ISubsetRepository
    {
        public SubsetRepository(FuzzyContext context) : base(context) { }
    }
}
