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
    /// Lógica de interacción para TIPOUSUARIO.xaml
    /// </summary>
    public partial class TIPOUSUARIO : Window
    {
        public TIPOUSUARIO()
        {
            InitializeComponent();
            // Llama a la función para obtener el siguiente ID disponible
            ObtenerSiguienteId();
        }

        CLASES.CONEXION c;

        private void ObtenerSiguienteId()
        {
            // Crea un objeto para obtener el siguiente ID
            CLASES.clstipousuario OCONSULTAR = new CLASES.clstipousuario();
            // Ejecuta la consulta para obtener el siguiente ID
            c = new CLASES.CONEXION(OCONSULTAR.ConsultaSiguienteId());
            DataSet ds = c.consultar();
            // Verifica si se encontraron resultados
            if (ds.Tables["Tabla"].Rows.Count > 0)
            {
                // Asigna el ID al texto de la caja de texto
                TXTIDTIPOUSUARIO.Text = ds.Tables["Tabla"].Rows[0]["TPU_ID"].ToString();
            }
        }

        private void BTNSALIR_Click(object sender, RoutedEventArgs e)
        {
            USUARIOS usuarios = new USUARIOS();
            usuarios.Show();
            this.Close();
        }

        private void BTNGUARDARTIPOUSUARIO_Click(object sender, RoutedEventArgs e)
        {   // Verificar si la caja de texto del ID está vacía
            if (string.IsNullOrEmpty(TXTIDTIPOUSUARIO.Text))
            {
                MessageBox.Show("El ID no puede estar vacío");

                // Obtener el siguiente ID disponible y establecerlo en la caja de texto del ID
                ObtenerSiguienteId();
            }
            else
            {
                // Verificar si la caja de texto de descripción está vacía
                if (string.IsNullOrEmpty(TXTTIPOUSUARIO.Text))
                {
                    MessageBox.Show("La descripción no puede estar vacía");
                }
                else
                {
                    // Usando Constructores
                    CLASES.clstipousuario OCONSULTAR = new CLASES.clstipousuario(int.Parse(TXTIDTIPOUSUARIO.Text), TXTTIPOUSUARIO.Text);
                    CLASES.clstipousuario OGRABAR = new CLASES.clstipousuario(int.Parse(TXTIDTIPOUSUARIO.Text), TXTTIPOUSUARIO.Text);
                    DataSet ds = new DataSet();
                    c = new CLASES.CONEXION(OCONSULTAR.consultaindividual());
                    ds = c.consultar();
                    if (ds.Tables["Tabla"].Rows.Count > 0)
                    {
                        // Verificar si la caja de texto de descripción está vacía (no es necesario hacerlo aquí, ya se hizo arriba)
                        c = new CLASES.CONEXION(OGRABAR.modificar());
                        MessageBox.Show(c.EJECUTAR());

                        // Limpiar la caja de texto de descripción
                        TXTTIPOUSUARIO.Text = "";

                        // Obtener el siguiente ID disponible y actualizar la caja de texto de ID
                        ObtenerSiguienteId();
                    }
                    else
                    {
                        c = new CLASES.CONEXION(OGRABAR.grabar());

                        // Mostrar mensaje de operación exitosa solo si se grabó
                        MessageBox.Show(c.EJECUTAR());

                        // Limpiar la caja de texto de descripción
                        TXTTIPOUSUARIO.Text = "";

                        // Obtener el siguiente ID disponible y actualizar la caja de texto de ID
                        ObtenerSiguienteId();
                    }
                }
            }


        }

        private void BTNBUSCARTIPOUSUARIO_Click(object sender, RoutedEventArgs e)
        {
            CLASES.clstipousuario tpu = new clstipousuario();
            CLASES.CONEXION con = new CLASES.CONEXION();
            if (con.Execute(tpu.consultageneral(), 0) == true)
            {
                if (con.FieldValue != "")
                {
                    TXTIDTIPOUSUARIO.Text = con.FieldValue;
                    buscar();
                }
            }
        }
        private void buscar()
        {
            CLASES.clstipousuario OCONSULTAR;
            if (String.IsNullOrEmpty(TXTIDTIPOUSUARIO.Text)) MessageBox.Show("Introduzca la Clave del Tipo de Usuario");
            else
            {   // Usando Constructores
                OCONSULTAR = new CLASES.clstipousuario(int.Parse(TXTIDTIPOUSUARIO.Text));

                DataSet ds = new DataSet();
                c = new CLASES.CONEXION(OCONSULTAR.consultaindividual());
                ds = c.consultar();
                if (ds.Tables["Tabla"].Rows.Count > 0)
                {
                    TXTTIPOUSUARIO.Text = ds.Tables["Tabla"].Rows[0]["TPU_DESCRIPCION"].ToString();

                }
                else
                    MessageBox.Show("El registro del Tipo de Uusario no existe");
            }
        }

        private void BTNELIMINARTIPOUSUARIO_Click(object sender, RoutedEventArgs e)
        {
            // Verificar si la caja de texto del ID está vacía
            if (string.IsNullOrEmpty(TXTIDTIPOUSUARIO.Text))
            {
                MessageBox.Show("El ID no puede estar vacío");

                // Obtener el siguiente ID disponible y establecerlo en la caja de texto del ID
                ObtenerSiguienteId();
            }
            // Verificar si la caja de texto de descripción está vacía
            else if (string.IsNullOrEmpty(TXTTIPOUSUARIO.Text))
            {
                MessageBox.Show("La descripción no puede estar vacía");
            }
            else
            {
                try
                {
                    // Usando el ID proporcionado en la caja de texto para eliminar el tipo de usuario
                    string idTipoUsuario = TXTIDTIPOUSUARIO.Text;
                    CLASES.clstipousuario Oeliminar = new CLASES.clstipousuario(int.Parse(idTipoUsuario));
                    string resultadoEliminacion = Oeliminar.eliminar();

                    // Ejecutar la eliminación y mostrar el resultado
                    c = new CLASES.CONEXION(resultadoEliminacion);
                    MessageBox.Show(c.EJECUTAR());

                    // Limpiar la caja de texto de descripción
                    TXTTIPOUSUARIO.Text = "";

                    // Obtener el siguiente ID disponible y actualizar la caja de texto de ID
                    ObtenerSiguienteId();
                }
                catch (FormatException)
                {
                    MessageBox.Show("El ID debe ser un número entero.");
                }
                catch (Exception ex)
                {
                    // Captura específicamente la excepción que indica que el tipo de usuario está enlazado con uno o más usuarios
                    if (ex.Message.Contains("No se puede eliminar el tipo de usuario porque está enlazado con uno o más usuarios."))
                    {
                        MessageBox.Show("No se puede eliminar el tipo de usuario porque está enlazado con uno o más usuarios.");
                    }
                    else
                    {
                        MessageBox.Show("No se puede eliminar el tipo de usuario porque está enlazado con uno o más usuarios.");
                    }
                    // Limpiar la caja de texto de descripción
                    TXTTIPOUSUARIO.Text = "";

                    // Obtener el siguiente ID disponible y actualizar la caja de texto de ID
                    ObtenerSiguienteId();
                }
            }




        }

        private void TXTIDTIPOUSUARIO_KeyDown(object sender, KeyEventArgs e)
        {
            /* Aqui hago que sólo permita números*/
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.OemPeriod)
            {
                if (e.Key == Key.OemPeriod && TXTIDTIPOUSUARIO.Text.IndexOf('.') != -1)
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
                TXTIDTIPOUSUARIO.Text = string.Empty;
                // Obtiene y establece el siguiente ID
                ObtenerSiguienteId();
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
