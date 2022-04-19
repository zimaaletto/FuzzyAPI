using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration
{
    internal sealed class SubsetConfiguration : IEntityTypeConfiguration<Subset>
    {
        public void Configure(EntityTypeBuilder<Subset> builder)
        {
            builder.ToTable("Subsets");
            builder.HasKey(subset => subset.Id);
            builder.Property(subset => subset.Id).ValueGeneratedOnAdd();

            builder.HasOne(subset => subset.Term)
                .WithMany(fla => fla.Subsets)
                .HasForeignKey(subset => subset.TermId);
        }
    }
}
