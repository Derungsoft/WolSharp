using Derungsoft.WolSharp.Samples.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Derungsoft.WolSharp.Samples.Server
{
    public class WolSharpDbContext : DbContext
    {
        public  DbSet<Device> Devices { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=WolSharp.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Device>()
                .ToTable(nameof(Device));

            base.OnModelCreating(modelBuilder);
        }
    }
}