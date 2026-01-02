using BIBLIOTECA_UAdeO.CLASES;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
    /// Lógica de interacción para LISTADEASISTENCIA.xaml
    /// </summary>
    public partial class LISTADEASISTENCIA : Window

    {

        private readonly string connectionString;
        private readonly globales globales = new globales();
        public LISTADEASISTENCIA()
        {
            InitializeComponent();
            connectionString = globales.ConexionDB();
            CargarDatosUsuario();


        }

        private void CargarDatosUsuario()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                string query = "SELECT USUARIO.USU_ID AS 'ID', \r\n CONCAT(USUARIO.USU_NOMBRE, ' ', USUARIO.USU_APEPATERNO, ' ', USUARIO.USU_APEMATERNO) AS 'Nombre',\r\n       ASISTENCIA.ASI_FHENTRADA AS 'Fecha y Hora de entrada' \r\nFROM USUARIO \r\nINNER JOIN ASISTENCIA ON ASISTENCIA.ASI_USUARIO = USUARIO.USU_ID\r\nORDER BY ASISTENCIA.ASI_FHENTRADA DESC";

     
                SqlCommand command = new SqlCommand(query, connection);

                try
                {

                    connection.Open();


                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();


                    adapter.Fill(dataTable);


                    miDataGrid.ItemsSource = dataTable.DefaultView;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar los datos de usuario: " + ex.Message);
                }
            }
        }



        private void BTNSALIR_Click(object sender, RoutedEventArgs e)
        {
            PAGINAPRINCIPAL submenuLibros = new PAGINAPRINCIPAL();
            submenuLibros.Show();
            this.Close();
        }

        private void TXTBUSCAR_TextChanged(object sender, TextChangedEventArgs e)
        {

            string term = TXTBUSCAR.Text.Trim();

            // Filtramos los datos del DataTable
            if (!string.IsNullOrEmpty(term))
            {

                DataView dv = ((DataView)miDataGrid.ItemsSource);
                dv.RowFilter = $"Nombre LIKE '%{term}%'"; 
            }
            else
            {
                ((DataView)miDataGrid.ItemsSource).RowFilter = string.Empty;
            }
        }
    }
}
