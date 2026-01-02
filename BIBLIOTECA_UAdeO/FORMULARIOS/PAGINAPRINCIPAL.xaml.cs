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
    /// Lógica de interacción para PAGINAPRINCIPAL.xaml
    /// </summary>
    public partial class PAGINAPRINCIPAL : Window
    {
        public PAGINAPRINCIPAL()
        {
            InitializeComponent();
        }

        private void BTNLIBROS_Click(object sender, RoutedEventArgs e)
        {
            SUBMENULIBROS submenuLibros = new SUBMENULIBROS();
            submenuLibros.Show();
            this.Close();
        }

        private void BTNSOLICITANTES_Click(object sender, RoutedEventArgs e)
        {
            USUARIOS solicitantes = new USUARIOS();
            solicitantes.Show();
            this.Close();
        }

        private void BTNPRESTAMOS_Click(object sender, RoutedEventArgs e)
        {
            PRESTAMOS prestamos = new PRESTAMOS();
            prestamos.Show();
            this.Close();
        }

        private void BTNINFORMES_Click(object sender, RoutedEventArgs e)
        {
            INFORMES informes = new INFORMES();
            informes.Show();
            this.Close();
        }

        private void BTNCERRARSESION_Click(object sender, RoutedEventArgs e)
        {
            LOGIN login = new LOGIN();
            login.Show();
            this.Close();
        }

        private void BTNLISTADEASISTENCIA_Click(object sender, RoutedEventArgs e)
        {
            LISTADEASISTENCIA login = new LISTADEASISTENCIA();
            login.Show();
            this.Close();
        }
    }
}
