using DataAccess;
using DataAccess.Models;
using MediaConverter;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Testing
{
    class Program
    {
        private static string path = @"C:\Users\David\Pictures\VRChat\8.8.21\raw\VRChat_7680x4320_2021-08-08_23-06-33.993.png";
        private static string dir = @"C:\Users\Dj\Pictures\VRChat\raw";
        private static string output = @"C:\Users\Dj\Pictures\VRChat\Converted\pics";
        static async Task Main(string[] args)
        {
            using (var db = new ImageDumpContext())
            {
                var pendingMigrations = await db.Database.GetPendingMigrationsAsync();

                if (pendingMigrations.Any())
                {
                    Console.WriteLine($"Applying pending migrations...");
                    var watch = new Stopwatch();
                    watch.Start();
                    await db.Database.MigrateAsync();
                    Console.WriteLine($"Done!\nMigrating took {watch.ElapsedMilliseconds}ms");
                    watch.Stop();
                }

                Console.WriteLine($"Can connect: {db.Database.CanConnect()}");
                Console.WriteLine($"Amount of users: {db.DumpUsers.ToList().Count}");

                var usr = new DjDumpUser()
                {
                    DiscordID = 1
                };
                await db.DumpUsers.AddAsync(usr);
                await db.SaveChangesAsync();
            }
        }

        public static async Task Test()
        {
            using (var converter = new ImageConverter(8))
            {
                if (Directory.Exists(dir))
                {
                    await converter.ConvertDir(dir, output);
                }
            }
            //await converter.ConvertToWebp(path);
            Console.WriteLine("Everything started");
            Console.ReadLine();
        }
    }
}
