using ebroker.Business.Interface;
using ebroker.Common.DTO;
using ebroker.DataLayer;
using System.Collections.Generic;

namespace ebroker.Business
{
    public class StockManager:IStockManager
    {
        public IStockRepositary _stockRepositary;

        public StockManager(IStockRepositary stockRepositary) {
            this._stockRepositary = stockRepositary;
        }

        public IEnumerable<StockDTO> GetStockList() {
            return this._stockRepositary.GetAllStocks();
        }

        public StockDTO GetStockDetail(int stockID) {
            return this._stockRepositary.GetStockDetail(stockID);
        }
    }
}
