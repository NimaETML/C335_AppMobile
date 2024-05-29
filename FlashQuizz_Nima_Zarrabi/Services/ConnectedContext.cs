using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FlashQuizz_Nima_Zarrabi.Models;

namespace FlashQuizz_Nima_Zarrabi.Services
{
    public class ConnectedContext : DbContext
    {
        public DbSet<Set> Sets { get; set; }

        public ConnectedContext()
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
