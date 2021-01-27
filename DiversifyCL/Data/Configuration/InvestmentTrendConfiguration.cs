using System;
using System.Collections.Generic;
using System.Text;
using DiversifyCL.Models.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiversifyCL.Data.Configuration
{
    public class InvestmentTrendConfiguration : IEntityTypeConfiguration<InvestmentTrend>
    {
        public void Configure(EntityTypeBuilder<InvestmentTrend> builder)
        {
            builder.ToTable("InvestmentTotals");

            // Change as needed for primary key
            builder.HasKey(x => x.InvestmentTrendsId);

            // Configure any foreign keys

            builder.Property(x => x.InvestmentTrendsId)
                .HasColumnName("InvestmentTrendsId")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(x => x.DateModified)
                .HasColumnName("DateModified")
                .HasColumnType("date")
                .IsRequired();

            builder.Property(x => x.InvestmentAmount)
                .HasColumnName("InvestmentAmount")
                .HasColumnType("decimal")
                .IsRequired();

            builder.Property(x => x.User)
                .HasColumnName("AspNetUserId")
                .HasColumnType("nvarchar")
                .HasMaxLength(900)
                .IsRequired();

            builder.Property(x => x.Company)
                .HasColumnName("Company")
                .HasColumnType("int")
                .IsRequired();
        }
    }
}
