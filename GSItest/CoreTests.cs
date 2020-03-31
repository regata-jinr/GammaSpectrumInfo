using System;
using Xunit;

namespace GSI.Core.Test
{
    public class CoreTests
    {
        [Fact]
        public void CheckParsingProcess()
        {
            // File	SampleSet	SampleId	Operator	Type	Weight	Error	Units	Height	Duration, s	Dead time	Build up type	Irradiation begin date	Irradiation end date	Description	Read successfuly	Error message
            // 1006379 RU - 33 - 19 - 88 - j   j - 01        SLI - 2   0.2053  0   gram    20  900 0.21    IRRAD   19.03.2020 9:50 19.03.2020 9:53 Vergel_K.N.Journal_18 True

            var spectra = new Spectra(@"D:\Spectra\2020\03\kji\1006379");

            Assert.Equal("1006379", spectra.viewModel.File);
            Assert.Equal("RU-33-19-88-j", spectra.viewModel.Id);
            Assert.Equal("j-01", spectra.viewModel.Title);
            Assert.True(string.IsNullOrEmpty(spectra.viewModel.CollectorName));
            Assert.Equal("SLI-2", spectra.viewModel.Type);
            Assert.Equal(0.2053, spectra.viewModel.Quantity, 2);
            Assert.Equal(0, spectra.viewModel.Uncertainty, 2);
            Assert.Equal("gram", spectra.viewModel.Units);
            Assert.Equal(20, spectra.viewModel.Geometry, 2);
            Assert.Equal(900, spectra.viewModel.Duration, 2);
            Assert.Equal(0.21, (double)spectra.viewModel.DeadTime, 2);
            Assert.Equal("IRRAD", spectra.viewModel.BuildUpType);
            Assert.Equal(DateTime.Parse("19.03.2020 9:50"), spectra.viewModel.BeginDate, TimeSpan.FromMinutes(1));
            Assert.Equal(DateTime.Parse("19.03.2020 9:53"), spectra.viewModel.EndDate, TimeSpan.FromMinutes(1));
            Assert.Equal("Vergel_K.N.Journal_18", spectra.viewModel.Description);
            Assert.True(spectra.ReadSuccess);
            Assert.True(string.IsNullOrEmpty(spectra.ErrorMessage));
        }
    }
}
