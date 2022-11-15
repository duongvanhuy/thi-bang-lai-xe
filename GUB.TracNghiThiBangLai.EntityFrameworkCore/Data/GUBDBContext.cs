using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GUB.TracNghiemThiBangLai.Entities;
using Microsoft.EntityFrameworkCore;

namespace GUB.TracNghiThiBangLai.EntityFrameworkCore.Data
{
    public class GUBDBContext : DbContext
    {
        public GUBDBContext(DbContextOptions<GUBDBContext> options) : base(options)
        {
        }
        
        public DbSet<Question> Questions { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Computer> Computers { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
