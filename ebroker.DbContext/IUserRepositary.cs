using ebroker.Common.DTO;
using System.Collections.Generic;

namespace ebroker.DataLayer
{
    public interface IUserRepositary
    {
        void DeleteUserAccount(int userAccountID);
        IEnumerable<UserDetailDTO> GetAllUsers();
        UserDetailDTO GetUserDetail(int userId);
        UserAccountDTO GetUserAccountDetail(int userID);
        UserAccountDTO InsertUserAccount(UserAccountDTO userAccountDTO);
        UserDetailDTO InsertUserDetail(UserDetailDTO userDetailDTO);
        UserAccountDTO UpdateUserAccount(UserAccountDTO userAccountDTO);
        UserDetailDTO UpdateUserDetail(UserDetailDTO userDetailDTO);
    }
}