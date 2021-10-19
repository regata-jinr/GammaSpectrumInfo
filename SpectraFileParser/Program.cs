using System;
using System.IO;
using Regata.Core.DataBase;
using Regata.Core.DataBase.Models;
using Regata.Core.CNF;
using System.Linq;
using System.Threading.Tasks;

namespace GSI.Core.SpectraFileParser
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                Console.WriteLine("Initialise spectra parsing");
                var files = Directory.GetFiles(@"D:\Spectra\WrongSLI1510\WrongSLI1510", "*.cnf").ToList();

                Console.WriteLine($"Files number - {files.Count}");
                foreach (var f in files)
                {
                    await ProcessFile(f);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine("Parsing complete");
        }

        private static async Task ProcessFile(string file)
        {
            Console.WriteLine($"Processing of file '{file}'");
            using (var spectra = new Spectra(file))
            {
                var sd = spectra.SpectrumData;
                //Console.WriteLine(sd);
                var cc = sd.Id.Split('-')[0];
                var cn = sd.Id.Split('-')[1];
                var y = sd.Id.Split('-')[2];
                var ss = sd.Id.Split('-')[3];
                var si = sd.Id.Split('-')[4];
                var sn = sd.Title.Split('-')[1];
                var f = "";
                using (var r = new RegataContext())
                {
                    var ir = r.Irradiations.Where(irr => irr.CountryCode == cc &&
                                                         irr.ClientNumber == cn &&
                                                         irr.Year == y &&
                                                         irr.SetNumber == ss &&
                                                         irr.SetIndex == si &&
                                                         irr.SampleNumber == sn
                                                   ).FirstOrDefault();



                    var mi = new Measurement
                    {
                        CountryCode = sd.Id.Split('-')[0],
                        ClientNumber = sd.Id.Split('-')[1],
                        Year = sd.Id.Split('-')[2],
                        SetNumber = sd.Id.Split('-')[3],
                        SetIndex = sd.Id.Split('-')[4],
                        SampleNumber = sd.Title.Split('-')[1],
                        Type = 0,
                        AcqMode = 2,
                        Height = 20,
                        IrradiationId = ir.Id,
                        DateTimeStart = sd.AcqStartDate,
                        Duration = (int)sd.Duration,
                        DeadTime = sd.DeadTime,
                        DateTimeFinish = sd.AcqStartDate.Value.AddSeconds(sd.Duration),
                        Detector = $"D{Path.GetFileName(file).Substring(0, 1)}",
                        FileSpectra = await GenerateSpectraFileNameFromDBAsync($"D{Path.GetFileName(file).Substring(0, 1)}", 0),
                        Assistant = 133887,
                        Note = null,
                        RegId = 932
                    };

                    //Console.WriteLine($"{mi}-{mi.FileSpectra}-{mi.IrradiationId}");
                    f = mi.FileSpectra;
                    r.Measurements.Add(mi);
                    await r.SaveChangesAsync();
                    //Console.WriteLine($"{file}--->{Path.Combine(Path.GetDirectoryName(file), "new", $"{mi.FileSpectra}.cnf")}");
                }
                File.Copy(file, Path.Combine(Path.GetDirectoryName(file),"new", $"{f}.cnf"), true);

            }


        }

        public static async Task<string> GenerateSpectraFileNameFromDBAsync(string dName, int type)
        {
            return await Task.Run(async () =>
            {
                    using (var r = new RegataContext())
                    {
                        var spectras = r.Measurements.Where(m => (
                                                                   m.FileSpectra.Length == 7 &&
                                                                   m.Type == type &&
                                                                   m.FileSpectra.Substring(0, 1) == dName.Substring(1, 1)
                                                                )
                                                        )
                                                  .Select(m => new
                                                  {
                                                      FileNumber = m.FileSpectra.Substring(2, 5)
                                                  }
                                                         )
                                                  .ToArray();

                        int maxNumber = spectras.Where(s => int.TryParse(s.FileNumber, out _)).Select(s => int.Parse(s.FileNumber)).Max();


                        return $"{dName.Substring(1, 1)}{type}{(++maxNumber).ToString("D5")}";
                    }
             
            });
        }

        private static bool IsNumber(string str)
        {
            return int.TryParse(str, out _);
        }

    }

}
