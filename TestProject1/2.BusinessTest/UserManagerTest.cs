using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using ebroker.Business;
using ebroker.DataLayer;
using ebroker.Common.Helper;
using ebroker.Common.DTO;

namespace eBroker.UnitTest._2.BusinessTest
{
    public class UserManagerTest
    {
        UserManager _userManager;
        Mock<IUserRepositary> _userRepositary;
        Mock<IStockRepositary> _stockRepositary;
        Mock<ICurrentDateTime> _currentDateTime;
        public UserManagerTest() {
            _userRepositary = new Mock<IUserRepositary>();
            _stockRepositary =  new Mock<IStockRepositary>();
            _currentDateTime = new Mock<ICurrentDateTime>();
            _userManager = new UserManager(_userRepositary.Object,_stockRepositary.Object,_currentDateTime.Object);
        }

        [Fact]
        public void GetAllUsers__userRepositary_GetAllUsers_Called_Once() {
            // Act
            _userManager.GetAllUsers();

            // Assert
            _userRepositary.Verify(x => x.GetAllUsers(), Times.Once);
        }

        [Fact]
        public void GetAllUsers__userRepositary_GetAllUsers_Return_List()
        {
            // Arrange
            _userRepositary.Setup(x => x.GetAllUsers()).Returns(new List<UserDetailDTO>());
            // Act
            var result = _userManager.GetAllUsers();
            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void GetUserDetail__userRepositary_GetUserDetail_Called_Once()
        {
            //Arrange
            int userid = 1;
            // Act
            _userManager.GetUserDetail(userid);

            _userRepositary.Verify(x => x.GetUserDetail(userid), Times.Once);
        }

        [Fact]
        public void GetUserDetail__userRepositary_GetUserDetail_Return_Detail()
        {
            // Arrange
            int userid = 1;
            _userRepositary.Setup(x => x.GetUserDetail(userid)).Returns(new UserDetailDTO());

            // Act
            var result = _userManager.GetUserDetail(userid);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void AddFund__userRepositary_AddFund_Verify_Called()
        {
            // Arrange
            int userid = 1; int amount = 100;
            UserDetailDTO userDetail = new UserDetailDTO();
            _userRepositary.Setup(x => x.GetUserDetail(userid)).Returns(userDetail);
            _userRepositary.Setup(x => x.UpdateUserDetail(userDetail));

            // Act
            _userManager.AddFund(userid, amount);

            // Assert
            _userRepositary.Verify(x => x.GetUserDetail(userid), Times.Once);
            _userRepositary.Verify(x => x.UpdateUserDetail(userDetail), Times.Once);

        }

        [Fact]
        public void AddFund__userRepositary_AddFund_Verify_Amount_Brokerage()
        {
            // Arrange
            int userid = 1; int amount = 100001;
            UserDetailDTO userDetail = new UserDetailDTO();
            var expectedResult = new UserDetailDTO() { Id = 0, Name = string.Empty, Balance = 99996 };
            _userRepositary.Setup(x => x.GetUserDetail(userid)).Returns(userDetail);
            _userRepositary.Setup(x => x.UpdateUserDetail(userDetail)).Returns(expectedResult);

            // Act
            var result = _userManager.AddFund(userid, amount);

            // Assert
            Assert.Equal(result.Balance,expectedResult.Balance);
        }

        [Fact]
        public void BuyEquity__userRepositary_BuyEquity_Verify_Called()
        {
            // Arrange
            UserAccountDTO userAccountDTO = new UserAccountDTO() { UserId=1,StockId=1,Quantity=1};
            UserDetailDTO userDetail = new UserDetailDTO() { Id = 1,Name=string.Empty,Balance=100 };
            StockDTO stock = new StockDTO() { Id=1 , Name= string.Empty,Price=20};
            _userRepositary.Setup(x => x.GetUserAccountDetail(userAccountDTO.UserId)).Returns(userAccountDTO);
            _userRepositary.Setup(x => x.GetUserDetail(userAccountDTO.UserId)).Returns(userDetail);
            _stockRepositary.Setup(x => x.GetStockDetail(userAccountDTO.StockId)).Returns(stock);
            _currentDateTime.Setup(x => x.GetUserDate()).Returns(new DateTime(2020,12,21).AddHours(10));

            // Act
            _userManager.BuyEquity(userAccountDTO);

            // Assert
            _userRepositary.Verify(x => x.GetUserAccountDetail(userAccountDTO.UserId), Times.Once);
            _userRepositary.Verify(x => x.GetUserDetail(userAccountDTO.UserId), Times.Once);
            _stockRepositary.Verify(x => x.GetStockDetail(userAccountDTO.StockId), Times.Once);
        }

        [Fact]
        public void BuyEquity__userRepositary_BuyEquity_Verify_Return_UserAccountDetail()
        {
            // Arrange
            UserAccountDTO userAccountDTO = new UserAccountDTO() { UserId = 1, StockId = 1, Quantity = 1 ,Id=0};
            UserDetailDTO userDetail = new UserDetailDTO() { Id = 1, Name = string.Empty, Balance = 100 };
            StockDTO stock = new StockDTO() { Id = 1, Name = string.Empty, Price = 20 };
            _userRepositary.Setup(x => x.GetUserAccountDetail(userAccountDTO.UserId)).Returns(userAccountDTO);
            _userRepositary.Setup(x => x.GetUserDetail(userAccountDTO.UserId)).Returns(userDetail);
            _userRepositary.Setup(x => x.InsertUserAccount(userAccountDTO)).Returns(userAccountDTO);
            _stockRepositary.Setup(x => x.GetStockDetail(userAccountDTO.StockId)).Returns(stock);
            _currentDateTime.Setup(x => x.GetUserDate()).Returns(new DateTime(2020, 12, 21).AddHours(10));

            // Act
            var result = _userManager.BuyEquity(userAccountDTO);

            // Assert
            Assert.Equal(result, userAccountDTO);
        }

        [Fact]
        public void BuyEquity__userRepositary_BuyEquity_Verify_Called_UpdateUserAccount()
        {
            // Arrange
            UserAccountDTO userAccountDTO = new UserAccountDTO() { UserId = 1, StockId = 1, Quantity = 1,Id=1 };
            UserDetailDTO userDetail = new UserDetailDTO() { Id = 1, Name = string.Empty, Balance = 100 };
            StockDTO stock = new StockDTO() { Id = 1, Name = string.Empty, Price = 20 };
            _userRepositary.Setup(x => x.GetUserAccountDetail(userAccountDTO.UserId)).Returns(userAccountDTO);
            _userRepositary.Setup(x => x.GetUserDetail(userAccountDTO.UserId)).Returns(userDetail);
            _userRepositary.Setup(x => x.InsertUserAccount(userAccountDTO)).Returns(userAccountDTO);
            _stockRepositary.Setup(x => x.GetStockDetail(userAccountDTO.StockId)).Returns(stock);
            _currentDateTime.Setup(x => x.GetUserDate()).Returns(new DateTime(2020, 12, 21).AddHours(10));

            // Act
            var result = _userManager.BuyEquity(userAccountDTO);

            // Assert
            _userRepositary.Verify(x => x.UpdateUserAccount(userAccountDTO), Times.Once);
        }

        [Fact]
        public void BuyEquity__userRepositary_BuyEquity_Verify_Not_Valid_Time()
        {
            // Arrange
            UserAccountDTO userAccountDTO = new UserAccountDTO() { UserId = 1, StockId = 1, Quantity = 1, Id = 1 };
            UserDetailDTO userDetail = new UserDetailDTO() { Id = 1, Name = string.Empty, Balance = 100 };
            StockDTO stock = new StockDTO() { Id = 1, Name = string.Empty, Price = 20 };
            _userRepositary.Setup(x => x.GetUserAccountDetail(userAccountDTO.UserId)).Returns(userAccountDTO);
            _userRepositary.Setup(x => x.GetUserDetail(userAccountDTO.UserId)).Returns(userDetail);
            _userRepositary.Setup(x => x.InsertUserAccount(userAccountDTO)).Returns(userAccountDTO);
            _stockRepositary.Setup(x => x.GetStockDetail(userAccountDTO.StockId)).Returns(stock);
           // _currentDateTime.Setup(x => x.GetUserDate()).Returns(new DateTime(2020, 12, 21).AddHours(1));
            var expectedResult = new UserAccountDTO();

            // Act
            var result = _userManager.BuyEquity(userAccountDTO);

            // Assert
            Assert.Equal(result.UserId, expectedResult.UserId);
        }

        [Fact]
        public void SellEquity__userRepositary_SellEquity_Verify_Called()
        {
            // Arrange
            UserAccountDTO userAccountDTO = new UserAccountDTO() { UserId = 1, StockId = 1, Quantity = 1 };
            UserDetailDTO userDetail = new UserDetailDTO() { Id = 1, Name = string.Empty, Balance = 100 };
            StockDTO stock = new StockDTO() { Id = 1, Name = string.Empty, Price = 20 };
            _userRepositary.Setup(x => x.GetUserAccountDetail(userAccountDTO.UserId)).Returns(userAccountDTO);
            _userRepositary.Setup(x => x.GetUserDetail(userAccountDTO.UserId)).Returns(userDetail);
            _stockRepositary.Setup(x => x.GetStockDetail(userAccountDTO.StockId)).Returns(stock);
            _currentDateTime.Setup(x => x.GetUserDate()).Returns(new DateTime(2020, 12, 21).AddHours(10));

            // Act
            _userManager.SellEquity(userAccountDTO);

            // Assert
            _userRepositary.Verify(x => x.GetUserAccountDetail(userAccountDTO.UserId), Times.Once);
            _userRepositary.Verify(x => x.GetUserDetail(userAccountDTO.UserId), Times.Once);
            _stockRepositary.Verify(x => x.GetStockDetail(userAccountDTO.StockId), Times.Once);
        }

        [Fact]
        public void SellEquity__userRepositary_SellEquity_Verify_Return_UserAccountDetail()
        {
            // Arrange
            UserAccountDTO userAccountDTO = new UserAccountDTO() { UserId = 1, StockId = 1, Quantity = 1, Id = 1 };
            UserDetailDTO userDetail = new UserDetailDTO() { Id = 1, Name = string.Empty, Balance = 100 };
            StockDTO stock = new StockDTO() { Id = 1, Name = string.Empty, Price = 20 };
            _userRepositary.Setup(x => x.GetUserAccountDetail(userAccountDTO.UserId)).Returns(new UserAccountDTO() { UserId = 1, StockId = 1, Quantity = 2, Id = 1 });
            _userRepositary.Setup(x => x.GetUserDetail(userAccountDTO.UserId)).Returns(userDetail);
            _userRepositary.Setup(x => x.UpdateUserAccount(It.IsAny<UserAccountDTO>())).Returns(userAccountDTO);
            _stockRepositary.Setup(x => x.GetStockDetail(userAccountDTO.StockId)).Returns(stock);
            _currentDateTime.Setup(x => x.GetUserDate()).Returns(new DateTime(2020, 12, 21).AddHours(10));

            // Act
            var result = _userManager.SellEquity(userAccountDTO);

            // Assert
            Assert.Equal(result, userAccountDTO);
        }

        [Fact]
        public void SellEquity__userRepositary_SellEquity_Verify_Call_DeleteUserAccount_Once()
        {
            // Arrange
            UserAccountDTO userAccountDTO = new UserAccountDTO() { UserId = 1, StockId = 1, Quantity = 1, Id = 1 };
            UserDetailDTO userDetail = new UserDetailDTO() { Id = 1, Name = string.Empty, Balance = 100 };
            StockDTO stock = new StockDTO() { Id = 1, Name = string.Empty, Price = 20 };
            _userRepositary.Setup(x => x.GetUserAccountDetail(userAccountDTO.UserId)).Returns(new UserAccountDTO() { UserId = 1, StockId = 1, Quantity = 1, Id = 1 });
            _userRepositary.Setup(x => x.GetUserDetail(userAccountDTO.UserId)).Returns(userDetail);
            _userRepositary.Setup(x => x.UpdateUserAccount(It.IsAny<UserAccountDTO>())).Returns(userAccountDTO);
            _stockRepositary.Setup(x => x.GetStockDetail(userAccountDTO.StockId)).Returns(stock);
            _currentDateTime.Setup(x => x.GetUserDate()).Returns(new DateTime(2020, 12, 21).AddHours(10));

            // Act
            var result = _userManager.SellEquity(userAccountDTO);

            // Assert
            _userRepositary.Verify(x => x.DeleteUserAccount(userAccountDTO.UserId), Times.Once);
        }

    }
}
