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
    /// Lógica de interacción para INFORMES.xaml
    /// </summary>
    public partial class INFORMES : Window
    {
        public INFORMES()
        {
            InitializeComponent();
        }

        private void BTNSALIR_Click(object sender, RoutedEventArgs e)
        {
            PAGINAPRINCIPAL paginaprincipal = new PAGINAPRINCIPAL();
            paginaprincipal.Show();
            this.Close();
        }

        private void BTNLIBROSINFORME_Click(object sender, RoutedEventArgs e)
        {
            string ruta = @"C:\Users\ANGEL\Documents\4to Semestre\Ingeniería de Software III\BIBLIOTECA_UAdeO\BIBLIOTECA_UAdeO\REPORTES\rptlibro.rpt";
            FORMULARIOS.VISORREPORTES X = new FORMULARIOS.VISORREPORTES(ruta);
            X.Show();
        }

        private void BTNASISTENCIAINFORME_Click(object sender, RoutedEventArgs e)
        {
            string ruta = @"C:\Users\ANGEL\Documents\4to Semestre\Ingeniería de Software III\BIBLIOTECA_UAdeO\BIBLIOTECA_UAdeO\REPORTES\rptasistencia.rpt";
            FORMULARIOS.VISORREPORTES X = new FORMULARIOS.VISORREPORTES(ruta);
            X.Show();
            /*VISORREPORTES RPTAsistencia = new VISORREPORTES();
            RPTAsistencia.Show();*/
        }

        private void BTNPRESTAMOS_Click(object sender, RoutedEventArgs e)
        {
            string ruta = @"C:\Users\ANGEL\Documents\4to Semestre\Ingeniería de Software III\BIBLIOTECA_UAdeO\BIBLIOTECA_UAdeO\REPORTES\rptprestamo.rpt";
            FORMULARIOS.VISORREPORTES X = new FORMULARIOS.VISORREPORTES(ruta);
            X.Show();
        }
    }
}
