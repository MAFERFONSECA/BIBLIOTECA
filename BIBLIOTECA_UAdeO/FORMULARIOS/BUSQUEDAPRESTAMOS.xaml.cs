using BIBLIOTECA_UAdeO.CLASES;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Lógica de interacción para BUSQUEDAPRESTAMOS.xaml
    /// </summary>
    public partial class BUSQUEDAPRESTAMOS : Window
    {
        private readonly string Conn;
        private readonly globales Global = new globales();

        public BUSQUEDAPRESTAMOS()
        {
            InitializeComponent();
            Conn = Global.ConexionDB();
            CargarDG();
        }

        private void CargarDG()
        {
            using (SqlConnection ConX = new SqlConnection(Conn))
            {
                CLASES.clprestamo OConsultar = new CLASES.clprestamo();

                string Query = OConsultar.ConsultaGeneral();
                SqlCommand Comm = new SqlCommand(Query, ConX);

                try
                {
                    ConX.Open();
                    SqlDataAdapter Adapt = new SqlDataAdapter(Comm);
                    DataTable DTable = new DataTable();
                    Adapt.Fill(DTable);
                    DT_Prestamos.ItemsSource = DTable.DefaultView;
                }

                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar los datos de los préstamos: \n\n" + ex.Message);
                }
            }
        }

        private void BTNSALIR_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
