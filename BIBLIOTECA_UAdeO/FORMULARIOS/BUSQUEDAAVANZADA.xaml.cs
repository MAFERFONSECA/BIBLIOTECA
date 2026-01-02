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
    /// Lógica de interacción para BUSQUEDAAVANZADA.xaml
    /// </summary>
    public partial class BUSQUEDAAVANZADA : Window
    {
        public BUSQUEDAAVANZADA()
        {
            InitializeComponent();
        }

        private void BTNSALIR_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
