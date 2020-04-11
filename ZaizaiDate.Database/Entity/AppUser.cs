using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ZaizaiDate.Database.Entity
{
    public class AppUser
    {
        public long Id { get; set; }

        [MaxLength(256)]
        [Required]
        public string UserName { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
         
        public DateTimeOffset CreatedDate { get; set; }
    }
}
