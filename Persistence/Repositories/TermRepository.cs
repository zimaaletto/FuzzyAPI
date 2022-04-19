using Domain.Entities;
using Domain.RepositoryInterfaces;

namespace Persistence.Repositories
{
    public class TermRepository : GenericRepository<Term>, ITermRepository
    {
        public TermRepository(FuzzyContext context) : base(context) { }
    }
}
