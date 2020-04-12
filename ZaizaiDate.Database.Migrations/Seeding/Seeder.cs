using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using ZaizaiDate.Database.DatabaseContext;
using ZaizaiDate.Database.Entity;

namespace ZaizaiDate.Database.Migrations.Seeding
{
    public static class Seeder
    {
        private const string seedingFilePath = "ZaizaiDate.Database.Migrations.Seeding.zaizai_seeding.json";

        public static void SeedUsers(ZaiZaiDateDbContext dbContext)
        {
            if (dbContext is null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            if (dbContext.Users.Any())
                return;

            var userData = ReadUsersFile();
            var users = JsonConvert.DeserializeObject<List<AppUser>>(userData);

            foreach(var user in users)
            {

                byte[] passwordHash, passwordSalt;
                CreatePasswordHash("pwd12345", out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.UserName = user.UserName.ToLowerInvariant();
                user.CreatedDate = DateTimeOffset.UtcNow;
                dbContext.Users.Add(user);

            }
            dbContext.SaveChanges();
        }
        
        private static string ReadUsersFile()
        {
            using (var stream = typeof(Seeder).GetTypeInfo().Assembly.GetManifestResourceStream(seedingFilePath)) 
            using (var reader = new StreamReader(stream, Encoding.UTF8))
                return reader.ReadToEnd();
   
        }
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512();

            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }
}
