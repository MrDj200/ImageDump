using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class ImageDumpContext : DbContext
    {
        public DbSet<DjImage> Images { get; set; }
        public DbSet<DjVRCImage> VRCImages { get; set; }
        public DbSet<DjDumpUser> DumpUsers { get; set; }
        public DbSet<DjDumpGroup> DumpGroups { get; set; }
        public DbSet<DjVRCWorld> VRCWorlds { get; set; }
        public DbSet<DjVRCUser> VRCUsers { get; set; }
        public DbSet<DjBan> UserBans { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=StoreDB;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DjVRCUser>().HasMany(x => x.KnownUsernames).WithOne(x => x.VRCUser);
        }
    }
}
