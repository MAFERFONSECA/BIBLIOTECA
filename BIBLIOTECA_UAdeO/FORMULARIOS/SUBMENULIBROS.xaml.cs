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
    /// Lógica de interacción para SUBMENULIBROS.xaml
    /// </summary>
    public partial class SUBMENULIBROS : Window
    {
        public SUBMENULIBROS()
        {
            InitializeComponent();
        }

        private void BTNRLIBROS_Click(object sender, RoutedEventArgs e)
        {
            REGISTROLIBROS registrolibros = new REGISTROLIBROS();
            registrolibros.Show();
            this.Close();
        }

        private void BTNRAUTORES_Click(object sender, RoutedEventArgs e)
        {
            REGISTROAUTORES registroautores = new REGISTROAUTORES();
            registroautores.Show();
            this.Close();
        }

        private void BTNRGENEROS_Click(object sender, RoutedEventArgs e)
        {
            REGISTROGENEROS registrogeneros = new REGISTROGENEROS();
            registrogeneros.Show();
            this.Close();
        }

        private void BTNREDITORIAL_Click(object sender, RoutedEventArgs e)
        {
            REGISTROEDITORIALES registroeditoriales = new REGISTROEDITORIALES();
            registroeditoriales.Show();
            this.Close();
        }

        private void BTNRSECCION_Click(object sender, RoutedEventArgs e)
        {
            REGISTROSECCIONES registrosecciones = new REGISTROSECCIONES();
            registrosecciones.Show();
            this.Close();
        }

        private void BTNREGRESAR_Click(object sender, RoutedEventArgs e)
        {
            PAGINAPRINCIPAL paginaprincipal = new PAGINAPRINCIPAL();
            paginaprincipal.Show();
            this.Close();
        }
    }
}
