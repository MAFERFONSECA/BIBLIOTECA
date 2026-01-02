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
    /// Lógica de interacción para LOGIN.xaml
    /// </summary>
    public partial class LOGIN : Window
    {
        private clusuario usuario;
        private globales conexion;
        public LOGIN()
        {
            InitializeComponent();
            usuario = new clusuario();
            conexion = new globales();
        }

        private void BTNENTRARLOGIN_Click(object sender, RoutedEventArgs e)
        {
            string id = TXTIDLOGIN.Text;
            string contraseña = TXTCONTRASENALOGIN.Text;

            string consulta = usuario.ConsultaInicioSesion(id, contraseña);

            string connectionString = conexion.ConexionDB();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(consulta, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        PAGINAPRINCIPAL submenuLibros = new PAGINAPRINCIPAL();
                        submenuLibros.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Credenciales incorrectas. Inténtalo de nuevo.");
                        TXTCONTRASENALOGIN.Clear();
                        TXTIDLOGIN.Clear();
                    }
                }
            }
        }

        private void BTNREGISTROASISTENCIA_Click(object sender, RoutedEventArgs e)
        {
            ASISTENCIA asistencia = new ASISTENCIA();
            asistencia.Show();
            this.Close();
        }
    }
}
