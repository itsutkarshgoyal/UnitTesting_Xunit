using ebroker.Common.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ebroker.Business.Interface
{
   public interface IUserManager
    {
        IEnumerable<UserDetailDTO> GetAllUsers();
        UserDetailDTO GetUserDetail(int userid);
        UserDetailDTO AddFund(int userid, int amount);
        UserAccountDTO BuyEquity(UserAccountDTO userAccountDTO);
        UserAccountDTO SellEquity(UserAccountDTO userAccountDTO);
    }
}
