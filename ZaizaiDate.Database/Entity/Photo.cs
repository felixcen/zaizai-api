using System;
using System.ComponentModel.DataAnnotations;

namespace ZaizaiDate.Database.Entity
{
    public class Photo
    {
        public Photo()
        {
            CreatedDate = DateTimeOffset.Now;
        }
        public long Id { get; set; }
         
        public string Url { get; set; }
         
        public string Description { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public bool IsMain { get; set; }

        public AppUser User { get; set; }

        public long UserId { get; set; }

        public byte[] Timestamp { get; set; }
    }
}