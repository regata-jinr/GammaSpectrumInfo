using System;
using System.IO;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Diagnostics;
using System.Linq;
using CanberraDataAccessLib;
using System.Collections.Concurrent;
using System.Collections.Generic;


// TODO: find out how to process files async and as fast as it possible

namespace GSI.Parallel
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Start parallel file processing:");
            IProgress<string> progress = new Progress<string>(file => Console.Write($"{file} "));
            var s =  Stopwatch.StartNew();

            var files = Directory.EnumerateFiles(@"D:\Spectra\2019-11-dji1", "*.cnf", SearchOption.AllDirectories);
            //files = files.TakeLast(200);

            System.Threading.Tasks.Parallel.ForEach(files, (file, state) =>
            {
                ProcessFile(file);
                Console.WriteLine(file);
                //n++;
                //if (n >= 1000)
                //{
                //    Debug.WriteLine(file);
                //    n = 0;
                //    //state.Break();
                //}
            });

            //await files.ForEachAsync(100, async file => await Task.Run(() => ProcessFile(file)), progress);
            //await files.ForEachAsync(100, async file => await Task.Run(() => ProcessFile(file)));


            //foreach (var file in files)
            //{
            //    ProcessFile(file);
            //    //Debug.WriteLine(file);
            //    n++;
            //    if (n >= 200)
            //    {
            //        break;
            //    }
            //}



            //var tasks = files.Select(file => ProcessFileAsync(file)).ToArray();
            //Task.WhenAll(tasks);

            //var block = new ActionBlock<string>(
            //                file => ProcessFileAsync(file),
            //                new ExecutionDataflowBlockOptions 
            //                { MaxDegreeOfParallelism = 50 }
            //            );

            //foreach (var file in files)
            //{
            //    block.Post(file);
            //}

            //block.Complete();
            //block.Completion.Wait();

            s.Stop();
            Console.WriteLine($"Elapsed time: {s.ElapsedMilliseconds / 1000} sec");
        }
        private int n = 0;

        private static void ProcessFile(string file)
        {
            var _spectra = new DataAccess();
            try
            {
                _spectra.Open(file, OpenMode.dReadWrite);

                var ttl = _spectra.Param[ParamCodes.CAM_T_STITLE].ToString();
                var id = _spectra.Param[ParamCodes.CAM_T_SIDENT].ToString();

                if (id.Contains(" "))
                    _spectra.Param[ParamCodes.CAM_T_SIDENT] = id.Replace(" ", "");
                if (ttl.Contains("--") && ttl.Length == 4)
                    _spectra.Param[ParamCodes.CAM_T_STITLE] = $"{id[id.Length - 1]}-{ttl.Replace("--", "")}";

                _spectra.Save("", true);
            }
            catch (Exception ex) { }
            finally
            {
                _spectra.Close();
            }
        }

        //private Task ProcessFileAsync(string file)
        //{
        //}
    }



    public static class EnumerableExtensions
    {
        // Adapted from https://blogs.msdn.microsoft.com/pfxteam/2012/03/05/implementing-a-simple-foreachasync-part-2/
        public static Task ForEachAsync<T>(this IEnumerable<T> source, int degreeOfParallelism, Func<T, Task> body, IProgress<T> progress = null)
        {
            return Task.WhenAll(
                Partitioner.Create(source).GetPartitions(degreeOfParallelism)
                    .Select(partition => Task.Run(async () => {
                        using (partition)
                            while (partition.MoveNext())
                            {
                                await body(partition.Current);
                                progress?.Report(partition.Current);
                            }
                    }))
            );
        }
    }
}
