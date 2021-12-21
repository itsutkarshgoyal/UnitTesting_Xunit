using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ebroker.Common.DTO;
using ebroker.Business.Interface;

namespace eBroker.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        public IUserManager _userManager;

        public UserController(IUserManager userManager) {
            this._userManager = userManager;
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public IEnumerable<UserDetailDTO> GetAllUsers()
        {
            return this._userManager.GetAllUsers();
        }

        [HttpPost]
        [Route("AddFund")]
        public void AddFund(int userId, int amount) {
            this._userManager.AddFund(userId, amount);
        }

        [HttpGet]
        [Route("GetUserDetail")]
        public UserDetailDTO GetUserDetail(int userId) {
            return this._userManager.GetUserDetail(userId);
        }

        [HttpPost]
        [Route("BuyEquity")]
        public void BuyEquity(UserAccountDTO userAccountDTO){
            this._userManager.BuyEquity(userAccountDTO);
        }

        [HttpPost]
        [Route("SellEquity")]
        public void SellEquity(UserAccountDTO userAccountDTO) {
            this._userManager.SellEquity(userAccountDTO);
        }

    }
}
