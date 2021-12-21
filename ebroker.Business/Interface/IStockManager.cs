using ebroker.Common.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ebroker.Business.Interface
{
    public interface IStockManager
    {
        IEnumerable<StockDTO> GetStockList();
        StockDTO GetStockDetail(int stockID);
    }
}
