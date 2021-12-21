using ebroker.Data.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace ebroker.Common.DTO
{
    public class StockDTO
    {
        public StockDTO()
        {
           // UserAccount = new HashSet<UserAccount>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }

       // public virtual ICollection<UserAccount> UserAccount { get; set; }
    }
}
