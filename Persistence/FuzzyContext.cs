using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public sealed class FuzzyContext : DbContext
    {
        public FuzzyContext(DbContextOptions options) : base(options) { }

        public DbSet<FuzzyLogicArea> FuzzyLogicAreas { get; set; }
        public DbSet<Rule> Rules { get; set; }
        public DbSet<Subset> Subsets { get; set; }
        public DbSet<Term> Terms { get; set; }
    }
}