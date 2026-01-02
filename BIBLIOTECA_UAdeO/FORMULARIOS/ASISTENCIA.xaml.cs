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
    /// Lógica de interacción para ASISTENCIA.xaml
    /// </summary>
    public partial class ASISTENCIA : Window
    {
        private readonly string connectionString; // Cadena de conexión a la base de datos
        private readonly globales globales = new globales();


        public ASISTENCIA()
        {
            InitializeComponent();
            connectionString = globales.ConexionDB();
        }

        private void BTNASISTENCIA_Click(object sender, RoutedEventArgs e)
        {
            // Verificar si la caja de texto del ID está vacía
            if (string.IsNullOrEmpty(TXTID.Text))
            {
                MessageBox.Show("Por favor, ingresa un ID.");
                return; // Salir del método sin continuar
            }

            // Intentar convertir el texto del ID a un entero
            if (!int.TryParse(TXTID.Text, out int idUsuario))
            {
                MessageBox.Show("El ID debe ser un número entero válido.");
                return; // Salir del método sin continuar
            }

            // Consultar la base de datos para obtener el nombre del usuario según el ID proporcionado
            string nombreUsuario = ObtenerNombreUsuarioDesdeBaseDeDatos(idUsuario);

            // Verificar si se encontró un nombre de usuario para el ID proporcionado
            if (nombreUsuario == null)
            {
                MessageBox.Show("No se encontró un usuario con el ID proporcionado.");
                return; // Salir del método sin continuar
            }

            // Mostrar el nombre del usuario en la caja de texto de nombre
            TXTNOMBRE.Text = nombreUsuario;

            // Guardar la asistencia en la base de datos
            GuardarAsistenciaEnBaseDeDatos(idUsuario);

            // Mostrar un mensaje de confirmación de asistencia
            MessageBox.Show($"Asistencia registrada para {nombreUsuario}");

            // Limpiar las cajas de texto después de registrar la asistencia
            TXTID.Text = "";
            TXTNOMBRE.Text = "";
        }


        // Método para guardar la asistencia en la base de datos
        private void GuardarAsistenciaEnBaseDeDatos(int idUsuario)
        {
            // Consulta SQL para insertar un nuevo registro de asistencia
            string query = "INSERT INTO ASISTENCIA (ASI_USUARIO, ASI_FHENTRADA) VALUES (@idUsuario, GETDATE())";

            // Crear la conexión
            using (SqlConnection connection = new SqlConnection(globales.ConexionDB()))
            {
                // Abrir la conexión
                connection.Open();

                // Crear el comando SQL
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Agregar los parámetros a la consulta
                    command.Parameters.AddWithValue("@idUsuario", idUsuario);

                    // Ejecutar la consulta
                    command.ExecuteNonQuery();
                }
            }
        }

        // Método para consultar el nombre del usuario desde la base de datos según su ID
        private string ObtenerNombreUsuarioDesdeBaseDeDatos(int idUsuario)
        {
            string nombreUsuario = null;

            // Consulta SQL para obtener el nombre del usuario según su ID
            string query = "SELECT USU_NOMBRE FROM Usuario WHERE USU_ID = @idUsuario";

            try
            {
                // Crear la conexión
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Abrir la conexión
                    connection.Open();

                    // Crear el comando SQL
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Agregar el parámetro ID de usuario a la consulta
                        command.Parameters.AddWithValue("@idUsuario", idUsuario);

                        // Ejecutar la consulta y obtener el resultado
                        object result = command.ExecuteScalar();

                        // Verificar si se obtuvo un resultado
                        if (result != null)
                        {
                            nombreUsuario = result.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener el nombre del usuario desde la base de datos: {ex.Message}");
            }

            return nombreUsuario;
        }

        private void TXTID_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(TXTID.Text))
            {
                TXTNOMBRE.Text = ""; // Limpiar la caja de texto de nombre
                return;
            }

            if (int.TryParse(TXTID.Text, out int idUsuario))
            {
                string nombreUsuario = ObtenerNombreUsuarioDesdeBaseDeDatos(idUsuario);
                if (nombreUsuario != null)
                {
                    TXTNOMBRE.Text = nombreUsuario; // Mostrar el nombre del usuario en la caja de texto de nombre
                }
                else
                {
                    MessageBox.Show("No se encontró un usuario con el ID proporcionado.");
                    TXTNOMBRE.Text = ""; // Limpiar la caja de texto de nombre
                }
            }
            else
            {
                MessageBox.Show("El ID debe ser un número entero válido.");
                TXTNOMBRE.Text = ""; // Limpiar la caja de texto de nombre
            }
        }

        private void BTNSALIR_Click(object sender, RoutedEventArgs e)
        {
            LOGIN login = new LOGIN();
            login.Show();
            this.Close();
        }
    }
}



