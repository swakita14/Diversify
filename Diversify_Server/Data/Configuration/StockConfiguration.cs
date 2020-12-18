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
            
            builder.Property(x => x.PurchaseDate)
                .HasColumnName("PurchaseDate")
                .HasColumnType("date")
                .IsRequired();

            builder.Property(x => x.SoldDate)
                .HasColumnName("SoldDate")
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
            
            builder.Property(x => x.InvestmentAmount)
                .HasColumnName("InvestmentAmount")
                .HasColumnType("decimal");

            builder.Property(x => x.Sector)
                .HasColumnName("Sector")
                .HasColumnType("int")
                .IsRequired();
            
            builder.Property(x => x.Status)
                .HasColumnName("Status")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(x => x.User)
                .HasColumnName("AspNetUserId")
                .HasColumnType("nvarchar")
                .HasMaxLength(900)
                .IsRequired();

        }
	}
}
