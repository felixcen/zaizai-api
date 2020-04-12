using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZaizaiDate.Common.Dto;

namespace ZaizaiDate.Api.ViewModels
{
    public class UserListViewModel : IUserForListDto
    {
        public long Id { get; set; }

        public string UserName { get; set; }

        public string Gender { get; set; }

        public int Age { get; set; }

        public string KnownAs { get; set; }

        public string Introduction { get; set; }

        public string LookingFor { get; set; }

        public string Interests { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public DateTimeOffset LastActive { get; set; }

        public string PhotoUrl { get; set; }
    }
}
