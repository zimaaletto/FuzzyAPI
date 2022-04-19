using Domain.RepositoryInterfaces;
using System.Linq.Expressions;

namespace Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected FuzzyContext RepositoryContext { get; set; }
        public GenericRepository(FuzzyContext RepoContext) => RepositoryContext = RepoContext;
        public IQueryable<T> FindAll()
        {
            return RepositoryContext.Set<T>().AsQueryable();
        }
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return RepositoryContext.Set<T>().Where(expression).AsQueryable();
        }
        public async Task Create(T entity)
        {
            await RepositoryContext.Set<T>().AddAsync(entity);
        }
        public void Update(T entity)
        {
            RepositoryContext.Set<T>().Update(entity);
        }
        public void Delete(T entity)
        {
            RepositoryContext.Remove(entity);
        }
    }
}
