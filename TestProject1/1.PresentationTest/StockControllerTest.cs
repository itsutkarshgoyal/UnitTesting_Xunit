using ebroker.Business;
using ebroker.Business.Interface;
using eBroker.WebAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;

namespace eBroker.UnitTest._1.PresentationTest
{
    public class StockControllerTest
    {
        private Mock<IStockManager> _stockManager;
        private StockController _stockController;
        public StockControllerTest() {
            _stockManager = new Mock<IStockManager>();
            _stockController = new StockController(_stockManager.Object);
        }


        [Fact]
        public void GetStocksList_stockManager_GetStockList_Called_Once() {

            //Act
            _stockController.GetStocksList();

            // Assert
            _stockManager.Verify(x => x.GetStockList(),Times.Once);
        }

        [Fact]
        public void GetStocksList_stockManager_GetStockDetail_Called_Once()
        {
            // Arrange
            int stockId = 1;

            // Act
            _stockController.GetStockDetail(stockId);
            
            // Assert
            _stockManager.Verify(x => x.GetStockDetail(stockId), Times.Once);
        }
    }
}
