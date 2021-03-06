﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZaizaiDate.Database.DatabaseContext;
using ZaizaiDate.Database.Entity;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ZaizaiDate.Common.Dto;

namespace ZaizaiDate.Business.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ZaiZaiDateDbContext _dbContext;
        public AuthenticationService(ZaiZaiDateDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AppUser> LoginAsync(IUserLoginDto user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var userCheck = await _dbContext.Users.SingleOrDefaultAsync(a => a.UserName == user.UserName).ConfigureAwait(false);

            if (userCheck == null)
                return null;

            if(!VerifyPasswordHash(user.Password, userCheck.PasswordHash, userCheck.PasswordSalt))
            {
                return null;
            }


            return userCheck;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt);
             
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            for(int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != passwordHash[i])
                    return false;
            }

            return true;
        }

        public async Task<AppUser> RegisterAsync(IRegisterUserDto user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(user.Password, out passwordHash, out passwordSalt);
            AppUser appUser = AppUser.Create(user.UserName);

            appUser.PasswordHash = passwordHash;
            appUser.PasswordSalt = passwordSalt;
            appUser.CreatedDate = DateTimeOffset.UtcNow;
            await _dbContext.Users.AddAsync(appUser).ConfigureAwait(false);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return appUser;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512();

            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        public async Task<bool> UserExists(string username)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(a => a.UserName == username).ConfigureAwait(false);

            return user != null;
        }
    }
}
