using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GSI.Core;

namespace GSI.UI
{
    // TODO: parse selected spectra to list of object
    // TODO: bind list to data source of dgv
    // TODO: add csvhelper
    // TODO: add labels.json for language settings
    // TODO: add tests
    // TODO: add lib for install that will check dotnetcore runtime installation

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var spectra = new Spectra(@"D:\Spectra\2020\01\dji-1\7107916.cnf");


        }

    }
}
