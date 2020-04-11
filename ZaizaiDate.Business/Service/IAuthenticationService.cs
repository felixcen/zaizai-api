using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZaizaiDate.Common.Dto;
using ZaizaiDate.Database.Entity;

namespace ZaizaiDate.Business.Service
{
    public interface IAuthenticationService
    {
        Task<AppUser> RegisterAsync(IRegisterUserDto user);

        Task<AppUser> LoginAsync(IUserLoginDto user);

        Task<bool> UserExists(string username);
    }
}
