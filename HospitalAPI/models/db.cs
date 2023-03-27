using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.models
{
    public class db:DbContext
    {
        public DbSet<user> users { get; set; }
        public DbSet<position> positions { get; set; }
        public DbSet<work> work { get; set; }
        public DbSet<category> category { get; set; }
        public DbSet<Post> posts { get; set; }
        protected override  void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Server=MOHAMEDYASSER\SQLEXPRESS;Database=hospitalDB;TrustServerCertificate=True;Trusted_Connection=True");
        }
    }
}
