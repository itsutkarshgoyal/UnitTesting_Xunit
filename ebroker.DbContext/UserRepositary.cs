using System;
using System.Collections.Generic;
using System.Text;
using ebroker.Common.DTO;
using ebroker.Data.Database;
using ebroker.Common.Helper;
using System.Linq;
using System.Diagnostics.CodeAnalysis;

namespace ebroker.DataLayer
{
    public class UserRepositary : IUserRepositary
    {
        private AppDbContext _appcontext;

        [ExcludeFromCodeCoverage]
        public UserRepositary(AppDbContext appContext = null)
        {
            if (appContext != null)
            {
                _appcontext = appContext;
            }
            else
            {
                _appcontext = new AppDbContext();
            }
        }

        public IEnumerable<UserDetailDTO> GetAllUsers()
        {
            IList<UserDetailDTO> userList = new List<UserDetailDTO>();
                var users = from r in _appcontext.UserDetail select r;
                foreach (var data in users)
                {
                    UserDetailDTO user = new UserDetailDTO();
                    user.Id = data.Id;
                    user.Name = data.Name;
                    user.Balance = data.Balance;
                    userList.Add(user);
                }
            return userList;
        }

        public UserDetailDTO GetUserDetail(int userId)
        {
            UserDetailDTO userDetailDTO = new UserDetailDTO();
                var user = _appcontext.UserDetail.FirstOrDefault(item => item.Id == userId);
                if (user != null) {
                    userDetailDTO = Mapper.MapUserDetail(user);
                }

            return userDetailDTO;
        }

        public UserAccountDTO GetUserAccountDetail(int userID)
        {
            UserAccountDTO userAccount = new UserAccountDTO();
                var data = _appcontext.UserAccount.FirstOrDefault(item => item.UserId == userID);
                if (data != null) {
                    userAccount.UserId = data.UserId;
                    userAccount.Quantity = data.Quantity;
                    userAccount.StockId = data.StockId;
                    userAccount.Id = data.Id;
                }
            return userAccount;
        }

        public UserAccountDTO InsertUserAccount(UserAccountDTO userAccountDTO)
        {
            UserAccount userAccount = Mapper.MapUserAccount(userAccountDTO);
            _appcontext.UserAccount.Add(userAccount);
            _appcontext.SaveChanges();
            return userAccountDTO;
        }

        public UserDetailDTO InsertUserDetail(UserDetailDTO userDetailDTO)
        {
            UserDetail userDetail = Mapper.MapUserDetailDTO(userDetailDTO);
            _appcontext.UserDetail.Add(userDetail);
            _appcontext.SaveChanges();

            return userDetailDTO;
        }

        public UserAccountDTO UpdateUserAccount(UserAccountDTO userAccountDTO)
        {
            UserAccount userAccount = Mapper.MapUserAccount(userAccountDTO);
            _appcontext.UserAccount.Update(userAccount);
            _appcontext.SaveChanges();
            return userAccountDTO;
        }

        public UserDetailDTO UpdateUserDetail(UserDetailDTO userDetailDTO)
        {
            UserDetail userDetail = Mapper.MapUserDetailDTO(userDetailDTO);
            _appcontext.UserDetail.Update(userDetail);
            _appcontext.SaveChanges();
           return userDetailDTO;
        }

        public void DeleteUserAccount(int userAccountID)
        {
                var item = _appcontext.UserAccount.FirstOrDefault(item => item.UserId == userAccountID);
                if (item != null)
                {
                _appcontext.UserAccount.Remove(item);
                _appcontext.SaveChanges();
                }
            }
    }
}
