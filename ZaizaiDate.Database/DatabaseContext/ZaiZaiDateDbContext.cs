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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder is null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }

            modelBuilder.Entity<AppUser>(eb =>
            {
                eb.Property(b => b.UserName).HasMaxLength(256).IsRequired();
                eb.Property(b => b.Gender).HasMaxLength(40);
                eb.Property(b => b.Introduction).HasMaxLength(2000);
                eb.Property(b => b.LookingFor).HasMaxLength(2000); ;
                eb.Property(b => b.Interests).HasMaxLength(2000);
                eb.Property(b => b.City).HasMaxLength(200);
                eb.Property(b => b.Country).HasMaxLength(200);
                eb.Property(b => b.Timestamp).IsRowVersion();
            });

            modelBuilder.Entity<Photo>(eb =>
            {
                eb.Property(b => b.Url).HasMaxLength(2000); 
                eb.Property(b => b.Description).HasMaxLength(2000); 
                eb.Property(b => b.Timestamp).IsRowVersion();
            });
        }
    }
}
