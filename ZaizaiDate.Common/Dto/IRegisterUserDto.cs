using System;
using System.Collections.Generic;
using System.Text;

namespace ZaizaiDate.Common.Dto
{
    public interface IRegisterUserDto
    {
        string UserName { get; }
        string Password { get; }
    }
}
