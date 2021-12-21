using ebroker.Common.DTO;
using System.Collections.Generic;

namespace ebroker.DataLayer
{
    public interface IStockRepositary
    {
        IEnumerable<StockDTO> GetAllStocks();
        StockDTO GetStockDetail(int stockId);
    }
}