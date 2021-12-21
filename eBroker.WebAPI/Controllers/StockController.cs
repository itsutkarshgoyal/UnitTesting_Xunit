using ebroker.Common.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ebroker.Business.Interface;

namespace eBroker.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StockController : ControllerBase
    {
        public IStockManager _stockManager;

        public StockController(IStockManager stockManager) {
            this._stockManager = stockManager;
        }

        [HttpGet]
        [Route("GetStocksList")]
        public IEnumerable<StockDTO> GetStocksList()
        {
            return this._stockManager.GetStockList();
        }

        [HttpGet]
        [Route("GetStockDetail")]
        public StockDTO GetStockDetail(int stockId)
        {
            return this._stockManager.GetStockDetail(stockId);
        }
    }
}
