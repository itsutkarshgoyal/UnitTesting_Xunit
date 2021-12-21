using ebroker.Common.DTO;
using ebroker.Data.Database;
using System;
using System.Collections.Generic;

namespace ebroker.Common.Helper
{
    public class Mapper
    {
        public static UserDetail  MapUserDetailDTO(UserDetailDTO userDetailDTO) {
            UserDetail userDetail = new UserDetail();
            userDetail.Id = userDetailDTO.Id;
            userDetail.Name = userDetailDTO.Name;
            userDetail.Balance = userDetailDTO.Balance;
            return userDetail;
        }

        public static UserDetailDTO MapUserDetail(UserDetail userDetail)
        {
            UserDetailDTO userDetailDTO = new UserDetailDTO();
            userDetailDTO.Id = userDetail.Id;
            userDetailDTO.Name = userDetail.Name;
            userDetailDTO.Balance = userDetail.Balance;
            return userDetailDTO;
        }

        public static UserAccount MapUserAccount(UserAccountDTO userAccountDTO)
        {
            UserAccount userAccount = new UserAccount();
            userAccount.Id = userAccountDTO.Id;
            userAccount.UserId = userAccountDTO.UserId;
            userAccount.Quantity = userAccountDTO.Quantity;
            userAccount.StockId = userAccountDTO.StockId;
            return userAccount;
        }
    }
}
