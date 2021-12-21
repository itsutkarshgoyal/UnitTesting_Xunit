using System;
using System.Collections.Generic;
using System.Text;
using ebroker.Data.Database;
using ebroker.DataLayer;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;


namespace eBroker.UnitTest._3.RepositaryTest
{
    public class StockRepositaryTest
    {
        private DbContextOptions<AppDbContext> Options;
        private StockRepositary _stockRepositary;

        public StockRepositaryTest()
        {
            Options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "eBrokerInMemory" + Guid.NewGuid().ToString()).Options;
            using (var Context = new AppDbContext(Options))
            {
                Context.Stock.Add(new Stock() { Id = 1 ,Name="Infi",Price=100});
                Context.SaveChanges();
            }
        }

        [Fact]
        void GetAllStocks_Called() {
            using (var Context = new AppDbContext(Options))
            {
                // Arrange
                _stockRepositary = new StockRepositary(Context);

                // Act
                var result = _stockRepositary.GetAllStocks();
                
                // Assert
                Assert.NotEmpty(result);
            }
        }

        [Fact]
        void GetStockDetail_Called()
        {
            using (var Context = new AppDbContext(Options))
            {
                // Arrange
                var stockid = 1;
                _stockRepositary = new StockRepositary(Context);
                
                // Act
                var result = _stockRepositary.GetStockDetail(stockid);
                
                // Assert
                Assert.NotNull(result);
            }
        }
    }
}
