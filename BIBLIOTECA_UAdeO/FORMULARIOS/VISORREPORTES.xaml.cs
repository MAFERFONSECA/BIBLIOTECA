using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BIBLIOTECA_UAdeO.FORMULARIOS
{
    /// <summary>
    /// Lógica de interacción para VISORREPORTES.xaml
    /// </summary>
    public partial class VISORREPORTES : Window
    {
        public VISORREPORTES()
        {
            InitializeComponent();
        }

        private string path;

        public VISORREPORTES(string path)
        {
            InitializeComponent();
            this.path = path;
        }

        private void BTNSALIR_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(this.path);
            rd.Refresh();
            CRVISORREPORTES.ViewerCore.ReportSource = rd;
        }
    }
}
