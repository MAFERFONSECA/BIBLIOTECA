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
    /// Lógica de interacción para REGISTROSECCIONES.xaml
    /// </summary>
    public partial class REGISTROSECCIONES : Window
    {
        public REGISTROSECCIONES()
        {
            InitializeComponent();
            ObtenerSiguienteId();
        }

        CLASES.CONEXION c;

        private void ObtenerSiguienteId()
        {
            // Crea un objeto para obtener el siguiente ID
            CLASES.clseccion OCONSULTAR = new CLASES.clseccion();
            // Ejecuta la consulta para obtener el siguiente ID
            c = new CLASES.CONEXION(OCONSULTAR.ConsultaSiguienteId());
            DataSet ds = c.consultar();
            // Verifica si se encontraron resultados
            if (ds.Tables["Tabla"].Rows.Count > 0)
            {
                // Asigna el ID al texto de la caja de texto
                TXTIDSECCION.Text = ds.Tables["Tabla"].Rows[0]["SEC_ID"].ToString();
            }
        }

        private void BTNSALIR_Click(object sender, RoutedEventArgs e)
        {
            SUBMENULIBROS submenuLibros = new SUBMENULIBROS();
            submenuLibros.Show();
            this.Close();
        }

        private void BTNBUSCARSECCION_Click(object sender, RoutedEventArgs e)
        {
            CLASES.clseccion tpu = new clseccion();
            CLASES.CONEXION con = new CLASES.CONEXION();
            if (con.Execute(tpu.consultageneral(), 0) == true)
            {
                if (con.FieldValue != "")
                {
                    TXTIDSECCION.Text = con.FieldValue;
                    buscar();
                }
            }
        }

        private void buscar()
        {
            CLASES.clseccion OCONSULTAR;
            if (String.IsNullOrEmpty(TXTIDSECCION.Text)) MessageBox.Show("Introduzca la Clave de la seccion");
            else
            {   // Usando Constructores
                OCONSULTAR = new CLASES.clseccion(int.Parse(TXTIDSECCION.Text));

                DataSet ds = new DataSet();
                c = new CLASES.CONEXION(OCONSULTAR.consultaindividual());
                ds = c.consultar();
                if (ds.Tables["Tabla"].Rows.Count > 0)
                {
                    TXTNOMBRESECCION.Text = ds.Tables["Tabla"].Rows[0]["SEC_NOMBRE"].ToString();

                }
                else
                    MessageBox.Show("El registro del genero no existe");
            }
        }

        private void BTNGUARDARSECCION_Click(object sender, RoutedEventArgs e)
        {
            // Verificar si la caja de texto del ID está vacía
            if (string.IsNullOrEmpty(TXTIDSECCION.Text))
            {
                MessageBox.Show("El ID no puede estar vacío.");
                ObtenerSiguienteId();
                return; // Salir del método sin continuar
            }
            // Verificar si la caja de texto de descripción está vacía
            if (string.IsNullOrEmpty(TXTNOMBRESECCION.Text))
            {
                MessageBox.Show("La descripción no puede estar vacía");
            }
            else
            {
                // Usando Constructores
                CLASES.clseccion OCONSULTAR = new CLASES.clseccion(int.Parse(TXTIDSECCION.Text), TXTNOMBRESECCION.Text);
                CLASES.clseccion OGRABAR = new CLASES.clseccion(int.Parse(TXTIDSECCION.Text), TXTNOMBRESECCION.Text);
                DataSet ds = new DataSet();
                c = new CLASES.CONEXION(OCONSULTAR.consultaindividual());
                ds = c.consultar();
                if (ds.Tables["Tabla"].Rows.Count > 0)
                {
                    // Verificar si la caja de texto de descripción está vacía
                    if (string.IsNullOrEmpty(TXTNOMBRESECCION.Text))
                    {
                        MessageBox.Show("La descripción no puede estar vacía");
                    }
                    else
                    {
                        c = new CLASES.CONEXION(OGRABAR.modificar());
                        MessageBox.Show(c.EJECUTAR());

                        // Limpiar la caja de texto de descripción
                        TXTNOMBRESECCION.Text = "";

                        // Obtener el siguiente ID disponible y actualizar la caja de texto de ID
                        ObtenerSiguienteId();
                    }
                }
                else
                {
                    c = new CLASES.CONEXION(OGRABAR.grabar());

                    // Mostrar mensaje de operación exitosa solo si se grabó
                    MessageBox.Show(c.EJECUTAR());

                    // Limpiar la caja de texto de descripción
                    TXTNOMBRESECCION.Text = "";

                    // Obtener el siguiente ID disponible y actualizar la caja de texto de ID
                    ObtenerSiguienteId();
                }
            }
        }

        private void BTNELIMINARSECCION_Click(object sender, RoutedEventArgs e)
        {
            // Verificar si la caja de texto del ID está vacía
            if (string.IsNullOrEmpty(TXTIDSECCION.Text))
            {
                MessageBox.Show("El ID no puede estar vacío.");
                ObtenerSiguienteId();
                return; // Salir del método sin continuar
            }

            // Verificar si la caja de texto de descripción está vacía
            if (string.IsNullOrEmpty(TXTNOMBRESECCION.Text))
            {
                MessageBox.Show("La descripción no puede estar vacía");
                return; // Salir del método sin continuar
            }

            try
            {
                // Usando Constructores
                CLASES.clseccion Oeliminar = new CLASES.clseccion(int.Parse(TXTIDSECCION.Text));
                c = new CLASES.CONEXION(Oeliminar.eliminar());
                MessageBox.Show(c.EJECUTAR());

                // Limpiar la caja de texto de descripción
                TXTNOMBRESECCION.Text = "";

                // Obtener el siguiente ID disponible y actualizar la caja de texto de ID
                ObtenerSiguienteId();
            }
            catch (FormatException)
            {
                MessageBox.Show("El ID debe ser un número entero.");
            }
            catch (Exception ex)
            {
                // Mostrar un mensaje genérico de error
                MessageBox.Show("No se puede eliminar la sección porque ya hay un libro registrado con ella.");
            }
            // Limpiar la caja de texto de descripción
            TXTNOMBRESECCION.Text = "";

            // Obtener el siguiente ID disponible y actualizar la caja de texto de ID
            ObtenerSiguienteId();
        }

        private void TXTIDSECCION_KeyDown(object sender, KeyEventArgs e)
        {
            /* Aqui hago que sólo permita números*/
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.OemPeriod)
            {
                if (e.Key == Key.OemPeriod && TXTIDSECCION.Text.IndexOf('.') != -1)
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
                TXTIDSECCION.Text = string.Empty;
                // Obtiene y establece el siguiente ID
                ObtenerSiguienteId();
            }
        }

        private void TXTIDSECCION_TextChanged(object sender, TextChangedEventArgs e)
        {

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
