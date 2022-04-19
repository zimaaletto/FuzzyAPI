using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration
{
    internal sealed class FuzzyLogicAreaConfiguration : IEntityTypeConfiguration<FuzzyLogicArea>
    {
        public void Configure(EntityTypeBuilder<FuzzyLogicArea> builder)
        {
            builder.ToTable("FuzzyLogicAreas");
            builder.HasKey(fla => fla.Id);
            builder.Property(fla => fla.Id).ValueGeneratedOnAdd();
        }
    }
}
