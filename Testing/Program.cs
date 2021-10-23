using DataAccess;
using MediaConverter;
using System;
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
                Console.WriteLine($"Can connect: {db.Database.CanConnect()}");
                Console.WriteLine($"Amount of users: {db.DumpUsers.ToList()}");
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
