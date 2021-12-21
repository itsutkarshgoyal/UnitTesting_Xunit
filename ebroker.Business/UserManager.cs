using ebroker.Business.Interface;
using System;
using ebroker.Common.DTO;
using ebroker.DataLayer;
using System.Linq;
using System.Collections.Generic;
using ebroker.Common.Helper;

namespace ebroker.Business
{
    public class UserManager: IUserManager
    {
        public IUserRepositary _userRepositary;

        public IStockRepositary _stockRepositary;

        public ICurrentDateTime _currentDateTime;

        const double BROKER_CHARGE_PERCENTAGE = 0.005;

        const int MIN_BROKER_CHARGE = 20;

        public UserManager(IUserRepositary userRepositary, IStockRepositary stockRepositary, ICurrentDateTime currentDateTime)
        {
            this._userRepositary = userRepositary;
            this._stockRepositary = stockRepositary;
            this._currentDateTime = currentDateTime;
        }

        public IEnumerable<UserDetailDTO> GetAllUsers() {
            return this._userRepositary.GetAllUsers();
        }

        public UserDetailDTO GetUserDetail(int userid)
        {
            return this._userRepositary.GetUserDetail(userid);
        }

        public UserDetailDTO AddFund(int userid, int amount) {
            var userDetail = this._userRepositary.GetUserDetail(userid);
            if (userDetail != null && amount > 0) {
                if (amount > 100000) {
                    amount = Convert.ToInt32(amount - (amount * BROKER_CHARGE_PERCENTAGE / 100));
                }

                userDetail.Balance = userDetail.Balance + amount;
                return  this._userRepositary.UpdateUserDetail(userDetail);
            }

            return new UserDetailDTO();
        }

        public UserAccountDTO BuyEquity(UserAccountDTO userAccountDTO) {
            var existingAccount = this._userRepositary.GetUserAccountDetail(userAccountDTO.UserId);
            var userDetail = this._userRepositary.GetUserDetail(userAccountDTO.UserId);
            var stockDetail = this._stockRepositary.GetStockDetail(userAccountDTO.StockId);
            var newBalance = userDetail.Balance - (userAccountDTO.Quantity * stockDetail.Price);
            var currentTime = this._currentDateTime.GetUserDate();
            var result = new UserAccountDTO();
            if (newBalance >= 0 && this.IsValidDate(currentTime)) {
                if (existingAccount == null || existingAccount.Id <= 0)
                {
                    result =  this._userRepositary.InsertUserAccount(userAccountDTO);
                }
                else {
                    existingAccount.Quantity = existingAccount.Quantity + userAccountDTO.Quantity;
                    result = this._userRepositary.UpdateUserAccount(existingAccount);
                }

                userDetail.Balance = newBalance;
                this._userRepositary.UpdateUserDetail(userDetail);
            }

            return result;
        }

        public UserAccountDTO SellEquity(UserAccountDTO userAccountDTO)
        {
            var existingAccount = this._userRepositary.GetUserAccountDetail(userAccountDTO.UserId);
            var userDetail = this._userRepositary.GetUserDetail(userAccountDTO.UserId);
            var stockDetail = this._stockRepositary.GetStockDetail(userAccountDTO.StockId);
            var amount =  (userAccountDTO.Quantity * stockDetail.Price * BROKER_CHARGE_PERCENTAGE)/100;
            var currentTime = this._currentDateTime.GetUserDate();
            var result = new UserAccountDTO();
            amount = amount < MIN_BROKER_CHARGE ? MIN_BROKER_CHARGE : amount;

            if (userDetail.Balance - amount > 0 && this.IsValidDate(currentTime) && existingAccount !=null ) {
                var quantity = existingAccount.Quantity - userAccountDTO.Quantity;
                if (quantity >= 0) {
                    existingAccount.Quantity = quantity;
                    if (quantity == 0)
                    {
                        this._userRepositary.DeleteUserAccount(existingAccount.UserId);
                    }
                    else {
                      result =  this._userRepositary.UpdateUserAccount(existingAccount);
                    }

                    userDetail.Balance = userDetail.Balance + userAccountDTO.Quantity * stockDetail.Price - Convert.ToInt32(amount);
                    this._userRepositary.UpdateUserDetail(userDetail);
                }
            }

            return result;
        }

        private bool IsValidDate(DateTime currentTime) {
            bool isvalid = true;
            if (currentTime.Date.DayOfWeek.Equals("Sunday") || currentTime.Date.DayOfWeek.Equals("Saturday") || currentTime.Hour < 9 || currentTime.Hour > 15) {
                isvalid = false;
            }
            return isvalid;
        }
    }
}
