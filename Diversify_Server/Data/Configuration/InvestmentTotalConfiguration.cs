using Diversify_Server.Models.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diversify_Server.Data.Configuration
{
    public class InvestmentTotalConfiguration : IEntityTypeConfiguration<InvestmentTotal>
    {
        public void Configure(EntityTypeBuilder<InvestmentTotal> builder)
        {
            builder.ToTable("InvestmentTotals");

            // Change as needed for primary key
            builder.HasKey(x => x.InvestmentTotalId);

            // Configure any foreign keys

            builder.Property(x => x.InvestmentTotalId)
                .HasColumnName("InvestmentTotalId")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(x => x.Symbol)
                .HasColumnName("Symbol")
                .HasColumnType("nvarchar(50)")
                .IsRequired();

            builder.Property(x => x.InvestedAmount)
                .HasColumnName("InvestedAmount")
                .HasColumnType("decimal")
                .IsRequired();

            builder.Property(x => x.User)
                .HasColumnName("AspNetUserId")
                .HasColumnType("nvarchar")
                .HasMaxLength(900)
                .IsRequired();
            
            builder.Property(x => x.Sector)
                .HasColumnName("Sector")
                .HasColumnType("int")
                .IsRequired();


        }
    }
}
