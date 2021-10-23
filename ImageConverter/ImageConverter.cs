using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xabe.FFmpeg;
using Xabe.FFmpeg.Downloader;

namespace MediaConverter
{
    public class ImageConverter : IDisposable
    {
        readonly SemaphoreSlim semaphore;
        readonly CancellationTokenSource cancellationToken = new();

        public string FFmpegPath { get; set; } = "./ffmpeg";
        public ImageConverter(int threadLimit = 2)
        {
            semaphore = new(threadLimit);
            CheckFFMPEG().GetAwaiter();

            AppDomain.CurrentDomain.ProcessExit += new EventHandler(CurrentDomain_ProcessExit);
        }

        private void CurrentDomain_ProcessExit(object? sender, EventArgs e) => this.Dispose();

        private async Task CheckFFMPEG()
        {
            if (FFmpeg.ExecutablesPath is null)
            {
                Console.WriteLine("Downloading FFmpeg...");
                await FFmpegDownloader.GetLatestVersion(FFmpegVersion.Official, FFmpegPath);
                FFmpeg.SetExecutablesPath(FFmpegPath);
                Console.WriteLine($"Download finished!\nPath: {FFmpeg.ExecutablesPath}\n\n");
            }
        }

        public async Task ConvertDir(string dir, string outputPath = "", bool multiThreaded = true, string outputFormat = ".webp", bool lossless = true)
        {
            if (Directory.Exists(dir))
            {
                List<Task> TaskList = new();

                var files = Directory.GetFiles(dir).Where(x => !x.EndsWith(outputFormat)).ToList();
                Console.WriteLine($"Found {files.Count} files");
                foreach (var file in files)
                {
                    TaskList.Add(Convert(file, outputPath, outputFormat, multiThreaded, lossless));
                }

                await Task.WhenAll(TaskList);
            }
            else
            {
                throw new FileNotFoundException();
            }
        }

        public async Task Convert(string inputPath, string outputPath = "", string outputFormat = ".webp", bool multithreaded = true, bool lossless = true, bool overwrite = false)
        {
            Console.WriteLine("Queuing task");
            await semaphore.WaitAsync();
            Console.WriteLine("Starting task!");

            var fileName = Path.GetFileNameWithoutExtension(inputPath);

            if (string.IsNullOrEmpty(outputPath))
            {
                outputPath = Path.ChangeExtension(inputPath, outputFormat);
            }
            else
            {
                outputPath = Path.Combine(outputPath, fileName + outputFormat);
            }
            if (!Directory.Exists(outputPath))
            {
                //Directory.CreateDirectory(outputPath);
            }


            if (File.Exists(outputPath))
            {
                if (!overwrite)
                {
                    semaphore.Release();
                    return;
                }
                var ext = Path.GetExtension(outputPath);
                outputPath = outputPath.Replace(ext, $"(1).{ext}");
            }

            var conversion = await FFmpeg.Conversions.FromSnippet.Convert(inputPath, outputPath);
            conversion.UseMultiThread(multithreaded).AddParameter($"-lossless {(lossless ? 1 : 0)}");
            var result = await conversion.Start(cancellationToken.Token);
            Console.WriteLine($"Converted file! {result.Duration}");

            semaphore.Release();
        }

        public void Dispose()
        {
            Console.Beep();
            cancellationToken.Cancel();
            cancellationToken.Dispose();
            semaphore.Dispose();
        }
    }
}
