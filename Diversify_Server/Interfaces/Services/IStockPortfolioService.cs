﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Diversify_Server.Models;
using Diversify_Server.Models.Database;
using Diversify_Server.Models.ViewModels;

namespace Diversify_Server.Interfaces.Services
{
    public interface IStockPortfolioService
    {
        /**
         *  Retrieves all the stocks that the user has added 
         */
        Task<List<StockTransactionViewModel>> GetCurrentUserStockTransaction();

        Task<IEnumerable<StockPortfolioViewModel>> StockPortfolioGroupByCompany();

        Task<IEnumerable<StockPortfolioViewModel>> StockPortfolioGroupBySector();

        Task<InvestmentTotalViewModel> GetUserInvestmentTotal();

        Task AddStock(CompanyOverviewModel model, decimal investmentAmount, DateTime purchaseDateTime);

        Task SellStock(string symbol, decimal amount);

    }
}