using Diversify_Server.Models.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diversify_Server.Data.Configuration
{
    public class IndustryConfiguration : IEntityTypeConfiguration<Industry>
    {
        public void Configure(EntityTypeBuilder<Industry> builder)
        {
            builder.ToTable("Industry");

            // Change as needed for primary key
            builder.HasKey(x => x.IndustryId);

            // Configure any foreign keys

            builder.Property(x => x.IndustryId)
                .HasColumnName("IndustryId")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(x => x.IndustryName)
                .HasColumnName("IndustryName")
                .HasColumnType("nchar");
        }
    }
}
