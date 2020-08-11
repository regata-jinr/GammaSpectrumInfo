using System;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace GSI.Core.SpectraFileParser
{
    class Program
    {
        private const string conStr = @"Server=RUMLAB\REGATALOCAL;Database=NAA_DB_TEST;Trusted_Connection=True;";
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Initialise spectra parsing");
                var files = Directory.GetFiles(@"D:\Spectra", "*.cnf", SearchOption.AllDirectories);
                //files = files.Take(100).ToArray();
                Console.WriteLine($"Total files number - {files.Length}");

                files.AsParallel().ForAll(f => ProcessFile(f));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine("Parsing complete");
        }
      
        private static void ProcessFile(string file)
        {
            //Console.WriteLine($"Processing of file '{file}'");
            var spectra = new GSI.Core.Spectra(file);
            var sk = spectra.Sample.Id.Split('-');
            if (sk.Length != 5) return;

            using (var ic = new InfoContext(conStr))
            {
                var oper = spectra.Sample.Description.Split('_')[0].ToLower();
                var assist = ic.Empls.Where(e => e.LastName.ToLower() == oper).FirstOrDefault();
                int? pid = null;
                if (assist != null)
                    pid = assist.PersonalId;

                ic.Add(new MeasurementInfo
                {
                    CountryCode = sk[0],
                    ClientNumber = sk[1],
                    Year = sk[2],
                    SetNumber = sk[3],
                    SetIndex = sk[4],
                    SampleNumber = spectra.Sample.Title.Split('-')[1],
                    Type = spectra.Sample.Type,
                    AcqMode = spectra.Sample.AcqMod,
                    Height = spectra.Sample.Geometry,
                    DateTimeStart = spectra.Sample.AcqStartDate,
                    Duration = (int)spectra.Sample.Duration,
                    DeadTime = spectra.DeadTime,
                    DateTimeFinish = null,
                    FileSpectra = Path.GetFileNameWithoutExtension(file),
                    Detector = $"D{Path.GetFileName(file).Substring(0, 1)}",
                    Token = null,
                    Assistant = pid,
                    Note = null
                });
                ic.SaveChanges();
            }
        }
    }

    static class ObjectHelper
    {
        public static void Dump<T>(this T x, string prem)
        {
            Console.WriteLine($"{prem} {x}");
        }
    }

    public class MeasurementInfo 
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int? IrradiationId { get; set; }
        [Required]
        public int? MRegId { get; set; }
        [Required]
        public string CountryCode { get; set; }
        [Required]
        public string ClientNumber { get; set; }
        [Required]
        public string Year { get; set; }
        [Required]
        public string SetNumber { get; set; }
        [Required]
        public string SetIndex { get; set; }
        [Required]
        public string SampleNumber { get; set; }
        [Required]
        public string Type { get; set; }
        public string AcqMode { get; set; }
        public float? Height { get; set; }
        public DateTime? DateTimeStart { get; set; }
        public int? Duration { get; set; }
        public float? DeadTime { get; set; }
        public DateTime? DateTimeFinish { get; set; }
        public string FileSpectra { get; set; }
        public string Detector { get; set; }
        public string Token { get; set; }
        public int? Assistant { get; set; }
        public string Note { get; set; }


        [NotMapped]
        public string SetKey => $"{CountryCode}-{ClientNumber}-{Year}-{SetNumber}-{SetIndex}";

        [NotMapped]
        public string SampleKey => $"{SetIndex}-{SampleNumber}";
        public override string ToString() => $"{SetKey}-{SampleNumber}";
    }

    [Table("Staff")]
    public class Staff
    {
        public int PersonalId { get; set; }
        [Required]
        public string LastName { get; set; }
    }


    public class InfoContext : DbContext
    {
        public DbSet<MeasurementInfo> Measurements { get; set; }
        public DbSet<Staff> Empls { get; set; }

        private readonly string _conString;

        public InfoContext(string conStr) : base()
        {
            _conString = conStr;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_conString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MeasurementInfo>()
                .HasIndex(c => c.FileSpectra)
                .IsUnique();

            modelBuilder.Entity<Staff>()
               .HasKey(c => c.PersonalId);

        }
    }
}

