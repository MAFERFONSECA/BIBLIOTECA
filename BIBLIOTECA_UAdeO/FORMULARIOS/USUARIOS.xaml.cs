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
    /// Lógica de interacción para USUARIOS.xaml
    /// </summary>
    public partial class USUARIOS : Window
    {
        public USUARIOS()
        {
            InitializeComponent();
        }

        private void BTNSOLICITANTES_Click(object sender, RoutedEventArgs e)
        {
            REGISTROUSUARIOS registrousuario = new REGISTROUSUARIOS();
            registrousuario.Show();
            this.Close();
        }

        private void BTNTIPOUSUARIO_Click(object sender, RoutedEventArgs e)
        {
            TIPOUSUARIO tipousuario = new TIPOUSUARIO();
            tipousuario.Show();
            this.Close();
        }

        private void BTNSALIR_Click(object sender, RoutedEventArgs e)
        {
            PAGINAPRINCIPAL paginaprincipal = new PAGINAPRINCIPAL();
            paginaprincipal.Show();
            this.Close();
        }
    }
}
