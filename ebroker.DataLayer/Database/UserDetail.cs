using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ebroker.Data.Database
{
    public partial class UserDetail
    {
        public UserDetail()
        {
            UserAccount = new HashSet<UserAccount>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("name")]
        [StringLength(100)]
        public string Name { get; set; }
        [Column("balance")]
        public int Balance { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<UserAccount> UserAccount { get; set; }
    }
}
