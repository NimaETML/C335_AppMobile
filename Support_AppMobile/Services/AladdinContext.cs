using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Support_AppMobile.Models;

namespace Support_AppMobile.Services
{
    public class AladdinContext : DbContext
    {
        public DbSet<GreaterWish> GreaterWishes { get; set; }

        public AladdinContext()
        {
            //SqLitePCL.Batteries_V2.Init(); // For IOS
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "aladdin.sqlite");
            options.UseSqlite($"Filename={dbPath}");
        }
    }
}
