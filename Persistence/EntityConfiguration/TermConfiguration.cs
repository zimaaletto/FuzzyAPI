using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration
{
    internal sealed class TermConfiguration : IEntityTypeConfiguration<Term>
    {
        public void Configure(EntityTypeBuilder<Term> builder)
        {
            builder.ToTable("Terms");
            builder.HasKey(term => term.Id);
            builder.Property(term => term.Id).ValueGeneratedOnAdd();

            builder.HasOne(term => term.FuzzyLogicArea)
                .WithMany(fla => fla.Terms)
                .HasForeignKey(term => term.FuzzyLogicAreaId);
        }
    }
}
