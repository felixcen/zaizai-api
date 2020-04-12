using System;
using System.Collections.Generic;
using System.Text;

namespace ZaizaiDate.Common.Dto
{
    public interface IUserDetailedDto : IUserBasicDto
    {
        IEnumerable<IPhotoDto> Photos { get; }
    }
}
