﻿using Diversify_Server.Data.Configuration;
using Microsoft.EntityFrameworkCore;
using Diversify_Server.Models.Database;

namespace Diversify_Server.Data
{
    public class DiversifyContext : DbContext
    {
        public DiversifyContext(DbContextOptions<DiversifyContext> options)
            : base(options)
        {
        }

        public DbSet<Sector> Sectors { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<InvestmentTotal> InvestmentTotals { get; set; }
        public DbSet<Company> Companies { get; set; }

        /**
         * Model Builder
         */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SectorConfiguration());
            
            modelBuilder.ApplyConfiguration(new StockConfiguration());

            modelBuilder.ApplyConfiguration(new InvestmentTotalConfiguration());

            modelBuilder.ApplyConfiguration(new CompanyConfiguration());
        }
    }
}
