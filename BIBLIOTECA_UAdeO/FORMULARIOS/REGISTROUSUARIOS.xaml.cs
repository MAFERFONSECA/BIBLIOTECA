using BIBLIOTECA_UAdeO.CLASES;
using System;
using System.Collections.Generic;
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
    /// Lógica de interacción para REGISTROUSUARIOS.xaml
    /// </summary>
    public partial class REGISTROUSUARIOS : Window
    {
        public REGISTROUSUARIOS()
        {
            InitializeComponent();
            TXTCONTRASEÑARUSUARIO.IsEnabled = false;
            cargatpusuarios();
            ConsultaSiguienteId();
        }

        private void cargatpusuarios()
        {
            DataSet ds = new DataSet();
            CLASES.clusuario s = new CLASES.clusuario();
            CLASES.CONEXION c = new CLASES.CONEXION(s.consultacombo());
            ds = c.consultar();
            // Asignar la tabla del DataSet como origen de datos del ComboBox
            CMBTIPORUSUARIO.ItemsSource = ds.Tables[0].DefaultView;
            // Definir qué columna se mostrará en el ComboBox
            CMBTIPORUSUARIO.DisplayMemberPath = ds.Tables[0].Columns["TPU_DESCRIPCION"].ToString();
            // Definir qué columna se utilizará como valor seleccionado
            CMBTIPORUSUARIO.SelectedValuePath = ds.Tables[0].Columns["TPU_ID"].ToString();

        }

        private void ConsultaSiguienteId()
        {
            // Crea un objeto para obtener el siguiente ID
            CLASES.clusuario OCONSULTAR = new CLASES.clusuario();
            // Ejecuta la consulta para obtener el siguiente ID
            c = new CLASES.CONEXION(OCONSULTAR.ConsultaSiguienteId());
            DataSet ds = c.consultar();
            // Verifica si se encontraron resultados
            if (ds.Tables["Tabla"].Rows.Count > 0)
            {
                // Asigna el ID al texto de la caja de texto
                TXTIDRUSUARIO.Text = ds.Tables["Tabla"].Rows[0]["USU_ID"].ToString();
            }
        }



        private void BTNSALIR_Click(object sender, RoutedEventArgs e)
        {
            USUARIOS usuarios = new USUARIOS();
            usuarios.Show();
            this.Close();
        }

        private void CMBTIPORUSUARIO_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CMBTIPORUSUARIO.SelectedIndex == 0) // Suponiendo que "ADMINISTRADOR" es el primer elemento en el ComboBox
            {
                // Habilitar la caja de texto
                TXTCONTRASEÑARUSUARIO.IsEnabled = true;
            }
            else
            {
                // Si la selección no es "ADMINISTRADOR", deshabilitar la caja de texto
                TXTCONTRASEÑARUSUARIO.IsEnabled = false;
                // Limpiar el contenido de la caja de texto
                TXTCONTRASEÑARUSUARIO.Clear();
            }
        }

        private void BTNBUSCARUSUARIO_Click(object sender, RoutedEventArgs e)
        {
            CLASES.clusuario alu = new clusuario();
            CLASES.CONEXION con = new CLASES.CONEXION();
            if (con.Execute(alu.consultageneral(), 0) == true)
            {
                if (con.FieldValue != "")
                {
                    TXTIDRUSUARIO.Text = con.FieldValue;
                    buscar();
                }
            }
        }
        CLASES.CONEXION c;
        private void BTNGUARDARUSUARIO_Click(object sender, RoutedEventArgs e)
        {
            // Verificar si la caja de texto del ID está vacía
            if (string.IsNullOrEmpty(TXTIDRUSUARIO.Text))
            {
                MessageBox.Show("El ID no puede estar vacío");
                ConsultaSiguienteId();
                return; // Salir del método sin continuar
            }

            // Verificar si hay un elemento seleccionado en el ComboBox
            if (CMBTIPORUSUARIO.SelectedItem == null)
            {
                MessageBox.Show("Por favor, seleccione un tipo de usuario.");
                return; // Salir del método sin continuar
            }

            // Obtener el valor seleccionado del ComboBox
            int cvetpu = int.Parse(CMBTIPORUSUARIO.SelectedValue.ToString());

            // Verificar si la caja de texto del nombre y apellido paterno están vacías
            if (string.IsNullOrEmpty(TXTNOMBRERUSUARIO.Text) || string.IsNullOrEmpty(TXTAPATERNORUSUARIO.Text))
            {
                MessageBox.Show("El nombre y el apellido paterno no pueden estar vacíos.");
            }
            else
            {
                // Usando Constructores
                CLASES.clusuario OGRABAR;

                if (!string.IsNullOrEmpty(TXTIDRUSUARIO.Text))
                {
                    // Modificar usuario existente
                    OGRABAR = new CLASES.clusuario(int.Parse(TXTIDRUSUARIO.Text), TXTNOMBRERUSUARIO.Text, TXTAPATERNORUSUARIO.Text, TXTAMATERNORUSUARIO.Text, cvetpu, TXTCONTRASEÑARUSUARIO.Text);
                }
                else
                {
                    // Nuevo usuario
                    OGRABAR = new CLASES.clusuario(int.Parse(TXTIDRUSUARIO.Text), TXTNOMBRERUSUARIO.Text, TXTAPATERNORUSUARIO.Text, TXTAMATERNORUSUARIO.Text, cvetpu, TXTCONTRASEÑARUSUARIO.Text);
                }

                DataSet ds = new DataSet();
                c = new CLASES.CONEXION(OGRABAR.consultaindividual());
                ds = c.consultar();

                if (ds.Tables["Tabla"].Rows.Count > 0)
                {
                    c = new CLASES.CONEXION(OGRABAR.modificar());
                    MessageBox.Show(c.EJECUTAR());
                }
                else
                {
                    c = new CLASES.CONEXION(OGRABAR.grabar());
                    MessageBox.Show(c.EJECUTAR());
                }

                // Limpiar las cajas de texto después de guardar
                TXTNOMBRERUSUARIO.Text = "";
                TXTAPATERNORUSUARIO.Clear();
                TXTAMATERNORUSUARIO.Clear();
                TXTCONTRASEÑARUSUARIO.Clear();
                CMBTIPORUSUARIO.SelectedIndex = -1; // Limpiar ComboBox

                // Obtener el siguiente ID disponible y actualizar la caja de texto de ID
                ConsultaSiguienteId();
            }

        }

        private void buscar()
        {
            CLASES.clusuario OCONSULTAR;

            if (!int.TryParse(TXTIDRUSUARIO.Text, out int idUsuario))
            {
                MessageBox.Show("Introduzca una Clave de Usuario válida.");
                return;
            }

            OCONSULTAR = new CLASES.clusuario(idUsuario);

            DataSet ds = new DataSet();
            c = new CLASES.CONEXION(OCONSULTAR.consultaindividual());
            ds = c.consultar();

            if (ds.Tables["Tabla"].Rows.Count > 0)
            {
                // Asignar el ID de usuario a la caja de texto TXTIDRUSUARIO
                TXTIDRUSUARIO.Text = ds.Tables["Tabla"].Rows[0]["USU_ID"].ToString();

                DataRow row = ds.Tables["Tabla"].Rows[0];

                // Asignar los valores a los demás controles
                TXTNOMBRERUSUARIO.Text = row["USU_NOMBRE"].ToString();
                TXTAPATERNORUSUARIO.Text = row["USU_APEPATERNO"].ToString();
                TXTAMATERNORUSUARIO.Text = row["USU_APEMATERNO"].ToString();
                TXTCONTRASEÑARUSUARIO.Text = row["USU_CONTRASENA"].ToString();

                // Obtener el ID del tipo de usuario y seleccionar en el ComboBox
                int idTipoUsuario = Convert.ToInt32(row["USU_TIPO"]);
                for (int i = 0; i < CMBTIPORUSUARIO.Items.Count; i++)
                {
                    DataRowView item = (DataRowView)CMBTIPORUSUARIO.Items[i];
                    int tipoUsuarioId = Convert.ToInt32(item["TPU_ID"]);
                    if (tipoUsuarioId == idTipoUsuario)
                    {
                        CMBTIPORUSUARIO.SelectedIndex = i;
                        break;
                    }
                }
            }
            else
            {
                MessageBox.Show("No se encontró ningún usuario con la Clave proporcionada.");
            }
        }

        private void TXTIDRUSUARIO_KeyDown(object sender, KeyEventArgs e)
        {
            /* Aqui hago que sólo permita números*/
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.OemPeriod)
            {
                if (e.Key == Key.OemPeriod && TXTIDRUSUARIO.Text.IndexOf('.') != -1)
                {
                    e.Handled = true;
                    return;
                }
                else
                {
                    e.Handled = false;
                }
            }
            else
            {
                MessageBox.Show("Solo admite números");
                // Limpia la caja de texto
                TXTIDRUSUARIO.Text = string.Empty;
                // Obtiene y establece el siguiente ID
                ConsultaSiguienteId();
            }
        }





        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Verifica si el texto ingresado no es un número
            if (!IsNumber(e.Text))
            {
                e.Handled = true; // Detiene el evento, evitando que el texto no numérico se agregue al TextBox
            }
        }

        // Función para verificar si una cadena es un número
        private bool IsNumber(string text)
        {
            return int.TryParse(text, out _); // Intenta convertir la cadena en un número entero
        }
    }
}
