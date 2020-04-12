using System;

namespace ZaizaiDate.Database.Entity
{
    public class Photo
    {
        public long Id { get; set; }
        public string Url { get; set; }

        public string Description { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public bool IsMain { get; set; }

        public AppUser User { get; set; }

        public long UserId { get; set; }


    }
}