using System;
using System.Collections.Generic;
using System.Text;
using ebroker.Business.Interface;
using ebroker.Common.DTO;
using eBroker.WebAPI.Controllers;
using Moq;
using Xunit;

namespace eBroker.UnitTest._1.PresentationTest
{
    public class UserControllerTest
    {
        Mock<IUserManager> _userManager;
        UserController _userController;

        public UserControllerTest() {
            _userManager = new Mock<IUserManager>();
            _userController = new UserController(_userManager.Object);
        }

        [Fact]
        public void GetAllUsers__userManager_GetAllUsers_Called_Once() {

            //Act
            _userController.GetAllUsers();

            // Assert
            _userManager.Verify(x => x.GetAllUsers(), Times.Once);
        }

        [Fact]
        public void AddFund__userManager_AddFund_Called_Once()
        {
            // Arrange
            int userId = 1;
            int amount = 100;
            //Act
            _userController.AddFund(userId,amount);

            // Assert
            _userManager.Verify(x => x.AddFund(userId,amount), Times.Once);
        }

        [Fact]
        public void GetUserDetail__userManager_GetUserDetail_Called_Once()
        {
            // Arrange
            int userId = 1;

            //Act
            _userController.GetUserDetail(userId);

            // Assert
            _userManager.Verify(x => x.GetUserDetail(userId), Times.Once);
        }

        [Fact]
        public void BuyEquity__userManager_BuyEquity_Called_Once()
        {
            // Arrange
            UserAccountDTO userAccountDTO = new UserAccountDTO();

            //Act
            _userController.BuyEquity(userAccountDTO);

            // Assert
            _userManager.Verify(x => x.BuyEquity(userAccountDTO), Times.Once);
        }

        [Fact]
        public void SellEquity__userManager_SellEquity_Called_Once()
        {
            // Arrange
            UserAccountDTO userAccountDTO = new UserAccountDTO();

            //Act
            _userController.SellEquity(userAccountDTO);

            // Assert
            _userManager.Verify(x => x.SellEquity(userAccountDTO), Times.Once);
        }
    }
}
