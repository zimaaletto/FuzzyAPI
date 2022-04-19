using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration
{
    internal sealed class RuleConfiguration : IEntityTypeConfiguration<Rule>
    {
        public void Configure(EntityTypeBuilder<Rule> builder)
        {
            builder.ToTable("Rules");
            builder.HasKey(rule => rule.Id);
            builder.Property(rule => rule.Id).ValueGeneratedOnAdd();

            builder.HasOne(rule => rule.FuzzyLogicArea)
                .WithMany(fla => fla.Rules)
                .HasForeignKey(rule => rule.FuzzyLogicAreaId);
        }
    }
}
