using Diversify_Server.Models.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diversify_Server.Data.Configuration
{
    public class StockConfiguration : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.ToTable("Stock");

            // Change as needed for primary key
            builder.HasKey(x => x.StockId);

            // Configure any foreign keys

            builder.Property(x => x.StockId)
                .HasColumnName("StockId")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(x => x.Name)
                .HasColumnName("Name")
                .HasColumnType("nchar")
                .IsRequired();

            builder.Property(x => x.Symbol)
                .HasColumnName("Symbol")
                .HasColumnType("nchar")
                .IsRequired();

            builder.Property(x => x.Exchange)
                .HasColumnName("Exchange")
                .HasColumnType("nchar");

            builder.Property(x => x.EPS)
                .HasColumnName("EPS")
                .HasColumnType("decimal");

            builder.Property(x => x.DividendYield)
                .HasColumnName("DividendYield")
                .HasColumnType("nchar")
                .IsRequired();

            builder.Property(x => x.ExDividendDate)
                .HasColumnName("ExDividendDate")
                .HasColumnType("date");

            builder.Property(x => x.ProfitMargin)
                .HasColumnName("ProfitMargin")
                .HasColumnType("decimal");

            builder.Property(x => x.PERatio)
                .HasColumnName("PERatio")
                .HasColumnType("decimal");

            builder.Property(x => x.PayoutRatio)
                .HasColumnName("PayoutRatio")
                .HasColumnType("decimal");

            builder.Property(x => x.Sector)
                .HasColumnName("Sector")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(x => x.Industry)
                .HasColumnName("Industry")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(x => x.User)
                .HasColumnName("User")
                .HasColumnType("nvarchar")
                .HasMaxLength(900)
                .IsRequired();

        }
	}
}
