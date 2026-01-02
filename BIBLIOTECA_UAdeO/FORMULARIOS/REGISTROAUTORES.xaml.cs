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
    /// Lógica de interacción para REGISTROAUTORES.xaml
    /// </summary>
    public partial class REGISTROAUTORES : Window
    {
        public REGISTROAUTORES()
        {
            InitializeComponent();
            ObtenerSiguienteID();
        }

        CLASES.CONEXION c;

        private void ObtenerSiguienteID()
        {
            CLASES.clsautor OCONSULTAR = new CLASES.clsautor();  // Crea un objeto para obtener el siguiente ID
            c = new CLASES.CONEXION(OCONSULTAR.ConsultarSiguienteID());  // Ejecuta la consulta para obtener el siguiente ID
            DataSet ds = c.consultar();

            if (ds.Tables["Tabla"].Rows.Count > 0)  // Verifica si se encontraron resultados
            {
                TXT_ID_AUTOR.Text = ds.Tables["Tabla"].Rows[0]["AUT_ID"].ToString();  // Asigna el ID al texto de la caja de texto
            }
        }

        private void BTNSALIR_Click(object sender, RoutedEventArgs e)
        {
            SUBMENULIBROS submenuLibros = new SUBMENULIBROS();
            submenuLibros.Show();
            this.Close();
        }

        private void BTNBUSCARAUTOR_Click(object sender, RoutedEventArgs e)
        {
            CLASES.clsautor TPU = new CLASES.clsautor();
            CLASES.CONEXION Con = new CLASES.CONEXION();

            if (Con.Execute(TPU.consultageneral(), 0) == true)
            {
                if (Con.FieldValue != "")
                {
                    TXT_ID_AUTOR.Text = Con.FieldValue;
                    buscar();
                }
            }
        }

        private void BTN_GUARDAR_AUTOR_Click(object sender, RoutedEventArgs e)
        {

            // Verificar si la caja de texto del ID está vacía
            if (string.IsNullOrEmpty(TXT_ID_AUTOR.Text))
            {
                MessageBox.Show("El ID no puede estar vacío.");
                ObtenerSiguienteID();
                return; // Salir del método sin continuar
            }

            // Verificar si la caja de texto de descripción está vacía
            if (string.IsNullOrEmpty(TXT_NOMBRE_AUTOR.Text) || string.IsNullOrEmpty(TXT_APATERNO_AUTOR.Text))
            {
                MessageBox.Show("El nombre y el apellido paterno no pueden estar vacíos.");
            }

            else
            {
                // Usando Constructores
                CLASES.clsautor OCONSULTAR = new CLASES.clsautor(int.Parse(TXT_ID_AUTOR.Text), TXT_NOMBRE_AUTOR.Text, TXT_APATERNO_AUTOR.Text, TXT_AMATERNO_AUTOR.Text);
                CLASES.clsautor OGRABAR = new CLASES.clsautor(int.Parse(TXT_ID_AUTOR.Text), TXT_NOMBRE_AUTOR.Text, TXT_APATERNO_AUTOR.Text, TXT_AMATERNO_AUTOR.Text);
                DataSet ds = new DataSet();
                c = new CLASES.CONEXION(OCONSULTAR.consultaindividual());
                ds = c.consultar();

                if (ds.Tables["Tabla"].Rows.Count > 0)
                {
                    // Verificar si la caja de texto de descripción está vacía
                    if (string.IsNullOrEmpty(TXT_NOMBRE_AUTOR.Text) || string.IsNullOrEmpty(TXT_APATERNO_AUTOR.Text))
                    {
                        MessageBox.Show("La descripción no puede estar vacía");
                    }

                    else
                    {
                        c = new CLASES.CONEXION(OGRABAR.modificar());
                        MessageBox.Show(c.EJECUTAR());

                        // Limpiar la caja de texto de descripción
                        TXT_NOMBRE_AUTOR.Text = "";
                        TXT_APATERNO_AUTOR.Clear();
                        TXT_AMATERNO_AUTOR.Clear();

                        // Obtener el siguiente ID disponible y actualizar la caja de texto de ID
                        ObtenerSiguienteID();
                    }
                }

                else
                {
                    c = new CLASES.CONEXION(OGRABAR.grabar());

                    // Mostrar mensaje de operación exitosa solo si se grabó
                    MessageBox.Show(c.EJECUTAR());

                    // Limpiar la caja de texto de descripción
                    TXT_NOMBRE_AUTOR.Text = "";
                    TXT_APATERNO_AUTOR.Clear();
                    TXT_AMATERNO_AUTOR.Clear();

                    // Obtener el siguiente ID disponible y actualizar la caja de texto de ID
                    ObtenerSiguienteID();
                }
            }
        }

        private void buscar()
        {
            CLASES.clsautor OCONSULTAR;
            if (String.IsNullOrEmpty(TXT_ID_AUTOR.Text)) MessageBox.Show("Introduzca la Clave del autor.");
            else
            {   // Usando Constructores
                OCONSULTAR = new CLASES.clsautor(int.Parse(TXT_ID_AUTOR.Text));

                DataSet ds = new DataSet();
                c = new CLASES.CONEXION(OCONSULTAR.consultaindividual());
                ds = c.consultar();
                if (ds.Tables["Tabla"].Rows.Count > 0)
                {
                    TXT_NOMBRE_AUTOR.Text = ds.Tables["Tabla"].Rows[0]["AUT_NOMBRE"].ToString();
                    TXT_APATERNO_AUTOR.Text = ds.Tables["Tabla"].Rows[0]["AUT_APATERNO"].ToString();
                    TXT_AMATERNO_AUTOR.Text = ds.Tables["Tabla"].Rows[0]["AUT_AMATERNO"].ToString();
                }
                else
                    MessageBox.Show("El registro del autor no existe.");
            }
        }

        private void BTN_ELIMINAR_AUTOR_Click(object sender, RoutedEventArgs e)
        {
            // Verificar si la caja de texto del ID está vacía
            if (string.IsNullOrEmpty(TXT_ID_AUTOR.Text))
            {
                MessageBox.Show("El ID no puede estar vacío.");
                ObtenerSiguienteID();
                return; // Salir del método sin continuar
            }

            // Verificar si la caja de texto de descripción está vacía
            if (string.IsNullOrEmpty(TXT_NOMBRE_AUTOR.Text) || string.IsNullOrEmpty(TXT_APATERNO_AUTOR.Text))
            {
                MessageBox.Show("El autor debe tener nombre y un apellido");
                return; // Salir del método sin continuar
            }

            try
            {
                // Usando Constructores
                CLASES.clsautor Oeliminar = new CLASES.clsautor(int.Parse(TXT_ID_AUTOR.Text));
                c = new CLASES.CONEXION(Oeliminar.eliminar());
                MessageBox.Show(c.EJECUTAR());

                // Limpiar la caja de texto de descripción
                TXT_NOMBRE_AUTOR.Clear();
                TXT_APATERNO_AUTOR.Clear();
                TXT_AMATERNO_AUTOR.Clear();

                // Obtener el siguiente ID disponible y actualizar la caja de texto de ID
                ObtenerSiguienteID();
            }
            catch (FormatException)
            {
                MessageBox.Show("El ID debe ser un número entero.");
            }
            catch (Exception ex)
            {
                // Mostrar un mensaje genérico de error
                MessageBox.Show("No se puede eliminar el autor porque ya hay un libro registrado con el.");
            }
            // Limpiar la caja de texto de descripción
            TXT_NOMBRE_AUTOR.Text = "";
            TXT_APATERNO_AUTOR.Text = "";
            TXT_AMATERNO_AUTOR.Text = "";

            // Obtener el siguiente ID disponible y actualizar la caja de texto de ID
            ObtenerSiguienteID();
        }

        private void TXT_ID_AUTOR_KeyDown(object sender, KeyEventArgs e)
        {
            /* Aqui hago que sólo permita números*/
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.OemPeriod)
            {
                if (e.Key == Key.OemPeriod && TXT_ID_AUTOR.Text.IndexOf('.') != -1)
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
                TXT_ID_AUTOR.Text = string.Empty;
                // Obtiene y establece el siguiente ID
                ObtenerSiguienteID();
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
