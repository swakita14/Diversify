using Diversify_Server.Models.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diversify_Server.Data.Configuration
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("Sector");

            // Change as needed for primary key
            builder.HasKey(x => x.CompanyId);

            // Configure any foreign keys

            builder.Property(x => x.Name)
                .HasColumnName("Name")
                .HasColumnType("nvarchar(128)")
                .IsRequired();

            builder.Property(x => x.Symbol)
                .HasColumnName("Symbol")
                .HasColumnType("nvarchar(32)")
                .IsRequired();

            builder.Property(x => x.Exchange)
                .HasColumnName("Exchange")
                .HasColumnType("nvarchar(32)");

            builder.Property(x => x.EPS)
                .HasColumnName("EPS")
                .HasColumnType("decimal");

            builder.Property(x => x.DividendYield)
                .HasColumnName("DividendYield")
                .HasColumnType("decimal")
                .IsRequired();

            builder.Property(x => x.ExDividendDate)
                .HasColumnName("ExDividendDate")
                .HasColumnType("date");
        }
    }
}
