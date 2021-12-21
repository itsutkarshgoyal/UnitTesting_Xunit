using System;
using System.Collections.Generic;
using ebroker.Common.DTO;
using ebroker.Data.Database;
using System.Linq;
using System.Diagnostics.CodeAnalysis;

namespace ebroker.DataLayer
{
    public class StockRepositary : IStockRepositary
    {
        private  AppDbContext _appcontext;

        [ExcludeFromCodeCoverage]
        public StockRepositary(AppDbContext appContext=null) {
            if (appContext != null)
            {
                _appcontext = appContext;
            }
            else {
                _appcontext = new AppDbContext();
             }
        }

        public IEnumerable<StockDTO> GetAllStocks()
        {
            IList<StockDTO> stockList = new List<StockDTO>();
                var stocks = from r in _appcontext.Stock select r;
                foreach (var data in stocks)
                {
                    StockDTO stock = new StockDTO();
                    stock.Id = data.Id;
                    stock.Name = data.Name;
                    stock.Price = data.Price;
                    stockList.Add(stock);
                }

            return stockList;
        }

        public StockDTO GetStockDetail(int stockId)
        {
            StockDTO stock = new StockDTO();
                var data = _appcontext.Stock.FirstOrDefault(item => item.Id == stockId);
                if (data != null) {
                    stock.Id = data.Id;
                    stock.Name = data.Name;
                    stock.Price = data.Price;
                }

            return stock;
        }
    }
}
