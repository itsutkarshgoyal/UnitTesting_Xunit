using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ebroker.Common.DTO;
using ebroker.Data.Database;
using ebroker.DataLayer;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;


namespace eBroker.UnitTest._3.RepositaryTest
{
   public  class UserRepositaryTest
    {
        private DbContextOptions<AppDbContext> Options;
        private UserRepositary _userRepositary;

        public UserRepositaryTest()
        {
            Options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "eBrokerInMemory"+ Guid.NewGuid().ToString()).Options;
            using (var Context = new AppDbContext(Options))
            {
                Context.Stock.Add(new Stock() { Id = 1, Name = "Infi", Price = 100 });
                Context.UserAccount.Add(new UserAccount() { Id = 1,UserId=1, StockId = 1, Quantity = 1 });
                Context.UserDetail.Add(new UserDetail() { Id = 1, Name = "Rahul", Balance = 1000 });

                Context.SaveChanges();
            }

        }

        [Fact]
        public void GetUsers_Return_Single_User()
        {
            using (var Context = new AppDbContext(Options))
            {
                // Arrange
                _userRepositary = new UserRepositary(Context);

                // Act
                var result = _userRepositary.GetAllUsers();
                // Assert
                Assert.Single(result);
            }
        }

        [Fact]
        public void GetUserDetail_Return_User_Detail()
        {
            using (var Context = new AppDbContext(Options))
            {
                // Arrange
                var userid = 1;
                _userRepositary = new UserRepositary(Context);

                // Act
                var result = _userRepositary.GetUserDetail(userid);
                // Assert
                Assert.NotNull(result);
            }
        }

        [Fact]
        public void GetUserAccountDetail_Return_User_AccountDetail()
        {
            using (var Context = new AppDbContext(Options))
            {
                // Arrange
                var userid = 1;
                _userRepositary = new UserRepositary(Context);

                // Act
                var result = _userRepositary.GetUserAccountDetail(userid);
                // Assert
                Assert.NotNull(result);
            }
        }

        [Fact]
        public void InsertUserAccount_Return_User_AccountDetail()
        {
            using (var Context = new AppDbContext(Options))
            {
                // Arrange
                var userAccount = new UserAccountDTO() { Id = 2, UserId = 2, StockId = 2 };
                _userRepositary = new UserRepositary(Context);

                // Act
                 _userRepositary.InsertUserAccount(userAccount);
                var newEntry = Context.UserAccount.FirstOrDefault(x => x.Id == 2);

                // Assert
                Assert.NotNull(newEntry);
            }
        }


        [Fact]
        public void InsertUserDetail_Return_UserDetail()
        {
            using (var Context = new AppDbContext(Options))
            {
                // Arrange
                var userDetail = new UserDetailDTO() { Id = 2, Name = "", Balance = 1000 };
                _userRepositary = new UserRepositary(Context);

                // Act
               _userRepositary.InsertUserDetail(userDetail);
                var newEntry = Context.UserDetail.FirstOrDefault(x => x.Id == 2);

                // Assert
                Assert.NotNull(newEntry);
            }
        }

        [Fact]
        public void UpdateUserAccount_Return_UpdateUserAccount()
        {
            using (var Context = new AppDbContext(Options))
            {
                // Arrange
                var userAccount = new UserAccountDTO() { Id = 1, UserId = 1, StockId = 1,Quantity=2 };
                _userRepositary = new UserRepositary(Context);

                // Act
                var result = _userRepositary.UpdateUserAccount(userAccount);
                var newEntry = Context.UserAccount.FirstOrDefault(x => x.Id == 1);

                // Assert
                Assert.Equal(result.Quantity, newEntry.Quantity);
            }
        }


        [Fact]
        public void UpdateUserDetail_Return_Update_UserDetail()
        {
            using (var Context = new AppDbContext(Options))
            {
                // Arrange
                var userDetail = new UserDetailDTO() { Id = 1, Name = "", Balance = 3000 };
                _userRepositary = new UserRepositary(Context);

                // Act
                var result = _userRepositary.UpdateUserDetail(userDetail);
                var newEntry = Context.UserDetail.FirstOrDefault(x => x.Id == 1);

                // Assert
                Assert.Equal(newEntry.Balance, userDetail.Balance);
            }
        }

        [Fact]
        public void DeleteUserAccount_Delete_DeleteUserAccount()
        {
            using (var Context = new AppDbContext(Options))
            {
                // Arrange
                var userId = 1;
                _userRepositary = new UserRepositary(Context);
                var existingEntry = Context.UserAccount.FirstOrDefault(x => x.Id == 1);

                // Act
                _userRepositary.DeleteUserAccount(userId);
                var newEntry = Context.UserAccount.FirstOrDefault(x => x.Id ==1);
                // Assert
                Assert.NotNull(existingEntry);
                Assert.Null(newEntry);
            }
        }

    }
}
