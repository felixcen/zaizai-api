using System;
using System.Collections.Generic;
using System.Text;

namespace ZaizaiDate.Common.Dto
{
    public interface IUserBasicDto
    {
        long Id { get; }
        string UserName { get; }
        string Gender { get; }
        int Age { get; }

        string KnownAs { get; }

        string Introduction { get; }

        string LookingFor { get; }

        string Interests { get; }

        string City { get; }

        string Country { get; }
        DateTimeOffset LastActive { get; }

        string PhotoUrl { get; }
    }
}
