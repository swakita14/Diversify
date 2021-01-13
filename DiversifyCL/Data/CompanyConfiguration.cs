using DiversifyCL.Models.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiversifyCL.Data
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("Company");

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

            builder.Property(x => x.Sector)
                .HasColumnName("Sector")
                .HasColumnType("int")
                .IsRequired();


            builder.Property(x => x.ProfitMargin)
                .HasColumnName("ProfitMargin")
                .HasColumnType("decimal");

            builder.Property(x => x.PERatio)
                .HasColumnName("PERatio")
                .HasColumnType("decimal");

            builder.Property(x => x.PayoutRatio)
                .HasColumnName("PayoutRatio")
                .HasColumnType("decimal");

            builder.Property(x => x.DateUpdated)
                .HasColumnName("DateUpdated")
                .HasColumnType("date")
                .IsRequired();


        }
    }
}
