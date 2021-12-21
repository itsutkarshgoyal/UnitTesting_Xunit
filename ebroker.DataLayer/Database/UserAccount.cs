using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ebroker.Data.Database
{
    public partial class UserAccount
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("stock_id")]
        public int StockId { get; set; }
        [Column("quantity")]
        public int Quantity { get; set; }

        [ForeignKey(nameof(StockId))]
        [InverseProperty("UserAccount")]
        public virtual Stock Stock { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(UserDetail.UserAccount))]
        public virtual UserDetail User { get; set; }
    }
}
