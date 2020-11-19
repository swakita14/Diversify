using Diversify_Server.Models.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diversify_Server.Data.Configuration
{
    public class SectorConfiguration : IEntityTypeConfiguration<Sector>
    {
        public void Configure(EntityTypeBuilder<Sector> builder)
        {
            builder.ToTable("Sector");

            // Change as needed for primary key
            builder.HasKey(x => x.SectorId);

            // Configure any foreign keys

            builder.Property(x => x.SectorId)
                .HasColumnName("SectorId")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(x => x.SectorName)
                .HasColumnName("SectorName")
                .HasColumnType("nvarchar(64)");
        }
    }
}
