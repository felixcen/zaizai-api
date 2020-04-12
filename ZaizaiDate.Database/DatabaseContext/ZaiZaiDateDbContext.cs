using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ZaizaiDate.Database.Entity;

namespace ZaizaiDate.Database.DatabaseContext
{
    public class ZaiZaiDateDbContext : DbContext
    {
        public ZaiZaiDateDbContext(DbContextOptions options) : base(options)
        {
        }

        protected ZaiZaiDateDbContext()
        {
        }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<Photo> Photos { get; set; }
    }
}
