using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZaizaiDate.Database.DatabaseContext;
using ZaizaiDate.Database.Entity;

namespace ZaizaiDate.Business.Service
{
    public class UserManagementService : IUserManagementService
    {
        private readonly ZaiZaiDateDbContext _dbContext;
        public UserManagementService(ZaiZaiDateDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add<T>(T entity) where T : class
        {
            _dbContext.Add(entity);
        }
        public void Delete<T>(T entity) where T : class
        {
            _dbContext.Remove(entity);
        }

        public async Task<AppUser> GetUserAsync(long userId)
        {
            var user = await _dbContext.Users.Include(b => b.Photos).SingleOrDefaultAsync(d => d.Id == userId).ConfigureAwait(false);
            return user;
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            var users = await _dbContext.Users.Include(b => b.Photos).ToListAsync().ConfigureAwait(false);
            return users;
        }

        public async Task SaveAllAsync()
        {
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
