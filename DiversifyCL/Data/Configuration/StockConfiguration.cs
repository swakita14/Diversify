using DiversifyCL.Models.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiversifyCL.Data.Configuration
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

         
            
            builder.Property(x => x.PurchaseDate)
                .HasColumnName("PurchaseDate")
                .HasColumnType("date")
                .IsRequired();

            builder.Property(x => x.SoldDate)
                .HasColumnName("SoldDate")
                .HasColumnType("date");

            builder.Property(x => x.InvestmentAmount)
                .HasColumnName("InvestmentAmount")
                .HasColumnType("decimal");


            
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
