using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
