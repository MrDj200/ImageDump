using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=StoreDB;");
        }
    }
}
