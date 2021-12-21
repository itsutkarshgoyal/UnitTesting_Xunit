using ebroker.Data.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace ebroker.Common.DTO
{
    public class UserAccountDTO
    {

        public int Id { get; set; }
        public int UserId { get; set; }
        public int StockId { get; set; }
        public int Quantity { get; set; }
      //  public virtual Stock Stock { get; set; }
    
       // public virtual UserDetail User { get; set; }
    }
}
