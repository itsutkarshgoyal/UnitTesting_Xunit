using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using ebroker.DataLayer;
using ebroker.Business;

namespace eBroker.UnitTest._2.BusinessTest
{
    public class StockManagerTest
    {
        Mock<IStockRepositary> _stockRepositary;

        StockManager _stockManager;
        public StockManagerTest() {
            _stockRepositary = new Mock<IStockRepositary>();
            _stockManager = new StockManager(_stockRepositary.Object);
        }

        [Fact]
        public void GetStockList_stockRepositary_GetAllStocks_Called_Once() {

            //Act
            _stockManager.GetStockList();

            // Assert
            _stockRepositary.Verify(x => x.GetAllStocks(), Times.Once);
        }


        [Fact]
        public void GetStockDetail_stockRepositary_GetStockDetail_Called_Once()
        {
            // Arrrange
            int stockid = 1;
            //Act
            _stockManager.GetStockDetail(stockid);

            // Assert
            _stockRepositary.Verify(x => x.GetStockDetail(stockid), Times.Once);
        }
    }
}
