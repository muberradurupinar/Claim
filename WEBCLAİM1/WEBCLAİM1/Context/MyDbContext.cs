using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBCLAİM1.Entity;

namespace WEBCLAİM1.Context
{
    public class MyDbContext: DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
           : base(options)
        {
        }
       public DbSet<User> Users { get; set; }
        


    }
}
