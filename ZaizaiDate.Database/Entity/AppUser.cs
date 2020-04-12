using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ZaizaiDate.Database.Entity
{
    public class AppUser
    { 
        public AppUser()
        {
            CreatedDate = DateTimeOffset.Now;
        }
        public static AppUser Create(string username)
        {
            return new AppUser()
            {
                UserName = username
            };
        }

        public long Id { get; set; }
         
        public string UserName { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
         
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
         
        public string KnownAs { get; set; }
         
        public string Introduction { get; set; }
         
        public string LookingFor { get; set; }
         
        public string Interests { get; set; }
         
        public string City { get; set; }
         
        public string Country { get; set; }
        public DateTimeOffset LastActive { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public byte[] Timestamp { get; set; }

        public ICollection<Photo> Photos { get; set; }
    }
}
