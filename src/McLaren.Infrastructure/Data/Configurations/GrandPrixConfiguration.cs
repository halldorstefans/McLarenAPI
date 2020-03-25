using McLaren.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace McLaren.Infrastructure.Data.Configurations
{
    public class GrandPrixConfiguration : IEntityTypeConfiguration<GrandPrix>
    {
        public void Configure(EntityTypeBuilder<GrandPrix> builder)
        {
            builder.HasKey(e => e.id);
            builder.Property(e => e.id).IsRequired();
        }
    }
}