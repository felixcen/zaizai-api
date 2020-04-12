using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZaizaiDate.Database.Entity;

namespace ZaizaiDate.Business.Service
{
    public interface IUserManagementService
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;

        Task<AppUser> GetUserAsync(long userId);
        Task<IEnumerable<AppUser>> GetUsersAsync();

        Task SaveAllAsync();
    }
}
