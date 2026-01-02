using BIBLIOTECA_UAdeO.CLASES;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
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
    /// Lógica de interacción para REGISTROLIBROS.xaml
    /// </summary>
    /// 
    public partial class REGISTROLIBROS : Window
    {
        private readonly string connectionString;
        private readonly globales globales = new globales();

        public REGISTROLIBROS()
        {
            InitializeComponent();
            ConsultaSiguienteId();
            CargaEditoriales();
            CargaSecciones();
            connectionString = globales.ConexionDB();
            CargarDatosGeneros();
            CargarDatosAutores();
        }

        CLASES.CONEXION c;

        private void ConsultaSiguienteId()
        {
            // Crea un objeto para obtener el siguiente ID
            CLASES.cllibro OCONSULTAR = new CLASES.cllibro();
            // Ejecuta la consulta para obtener el siguiente ID
            c = new CLASES.CONEXION(OCONSULTAR.ConsultaSiguienteId());
            DataSet ds = c.consultar();
            // Verifica si se encontraron resultados
            if (ds.Tables["Tabla"].Rows.Count > 0)
            {
                // Asigna el ID al texto de la caja de texto
                TXT_ID_LIBRO.Text = ds.Tables["Tabla"].Rows[0]["LIB_ID"].ToString();
            }
        }

        private void CargaEditoriales()
        {
            DataSet ds = new DataSet();
            CLASES.cllibro s = new CLASES.cllibro();
            CLASES.CONEXION c = new CLASES.CONEXION(s.ConsultaComboEditorial());
            ds = c.consultar();
            // Asignar la tabla del DataSet como origen de datos del ComboBox
            CMBX_EDITORIAL.ItemsSource = ds.Tables[0].DefaultView;
            // Definir qué columna se mostrará en el ComboBox
            CMBX_EDITORIAL.DisplayMemberPath = ds.Tables[0].Columns["EDI_NOMBRE"].ToString();
            // Definir qué columna se utilizará como valor seleccionado
            CMBX_EDITORIAL.SelectedValuePath = ds.Tables[0].Columns["EDI_ID"].ToString();
        }

        private void CargaSecciones()
        {
            DataSet ds = new DataSet();
            CLASES.cllibro s = new CLASES.cllibro();
            CLASES.CONEXION c = new CLASES.CONEXION(s.ConsultaComboSeccion());
            ds = c.consultar();
            // Asignar la tabla del DataSet como origen de datos del ComboBox
            CMBX_SECCION.ItemsSource = ds.Tables[0].DefaultView;
            // Definir qué columna se mostrará en el ComboBox
            CMBX_SECCION.DisplayMemberPath = ds.Tables[0].Columns["SEC_NOMBRE"].ToString();
            // Definir qué columna se utilizará como valor seleccionado
            CMBX_SECCION.SelectedValuePath = ds.Tables[0].Columns["SEC_ID"].ToString();
        }

        private void CargarDatosGeneros()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                string query = "SELECT GEN_ID AS 'ID', GEN_NOMBRE AS 'Nombre' FROM GENERO";


                SqlCommand command = new SqlCommand(query, connection);

                try
                {

                    connection.Open();


                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();


                    adapter.Fill(dataTable);


                    DATAGRID_GENEROS.ItemsSource = dataTable.DefaultView;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar los datos de los géneros: " + ex.Message);
                }
            }
        }

        private void CargarDatosAutores()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                string query = "SELECT AUT_ID AS 'ID', CONCAT(AUT_NOMBRE, ' ', AUT_APATERNO, ' ', AUT_AMATERNO) AS 'Nombre' FROM AUTOR";


                SqlCommand command = new SqlCommand(query, connection);

                try
                {

                    connection.Open();


                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();


                    adapter.Fill(dataTable);


                    DATAGRID_AUTORES.ItemsSource = dataTable.DefaultView;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar los datos de los autores: " + ex.Message);
                }
            }
        }

        private void BTNSALIR1_Click(object sender, RoutedEventArgs e)
        {
            SUBMENULIBROS submenuLibros = new SUBMENULIBROS();
            submenuLibros.Show();
            this.Close();
        }

        private void CBXI_TITULO_Selected(object sender, RoutedEventArgs e)
        {
            CLASES.cllibro LBR = new CLASES.cllibro();
            CLASES.CONEXION con = new CLASES.CONEXION();

            if(con.Execute(LBR.ConsultaGeneralTitulo(), 0) == true)
            {
                if (con.FieldValue != "")
                {
                    TXT_ID_LIBRO.Text = con.FieldValue;
                    Buscar();
                }
            }

            CMBX_FILTROSBUSQUEDA.SelectedIndex = 0;
        }

        private void CBXI_AUTOR_Selected(object sender, RoutedEventArgs e)
        {
            CLASES.cllibro LBR = new CLASES.cllibro();
            CLASES.CONEXION con = new CLASES.CONEXION();

            if (con.Execute(LBR.ConsultaGeneralAutor(), 0) == true)
            {
                if (con.FieldValue != "")
                {
                    TXT_ID_LIBRO.Text = con.FieldValue;
                    Buscar();
                }
            }

            CMBX_FILTROSBUSQUEDA.SelectedIndex = 0;
        }

        private void CBXI_GENERO_Selected(object sender, RoutedEventArgs e)
        {
            CLASES.cllibro LBR = new CLASES.cllibro();
            CLASES.CONEXION con = new CLASES.CONEXION();

            if (con.Execute(LBR.ConsultaGeneralGenero(), 0) == true)
            {
                if (con.FieldValue != "")
                {
                    TXT_ID_LIBRO.Text = con.FieldValue;
                    Buscar();
                }
            }

            CMBX_FILTROSBUSQUEDA.SelectedIndex = 0;
        }

        private void Buscar()
        {
            CLASES.cllibro OConsultar;

            if(!int.TryParse(TXT_ID_LIBRO.Text, out int IDLibro))
            {
                MessageBox.Show("Introduzca una Clave de Libro válida.");
                return;
            }

            OConsultar = new CLASES.cllibro(IDLibro);

            DataSet ds = new DataSet();
            c = new CLASES.CONEXION(OConsultar.consultaindividual());
            ds = c.consultar();

            if (ds.Tables["Tabla"].Rows.Count > 0)
            {
                TXT_ID_LIBRO.Text = ds.Tables["Tabla"].Rows[0]["LIB_ID"].ToString();

                DataRow row = ds.Tables["Tabla"].Rows[0];

                TXT_TITULO_LIBRO.Text = row["LIB_TITULO"].ToString();
                TXT_APUBLICACION.Text = row["LIB_ANIOPUBLICACION"].ToString();
                TXT_CANTIDAD.Text = row["LIB_CANTIDAD"].ToString();

                int Editorial = Convert.ToInt32(row["LIB_EDITORIAL"]);
                for (int i = 0; i < CMBX_EDITORIAL.Items.Count; i++)
                {
                    DataRowView Item = (DataRowView)CMBX_EDITORIAL.Items[i];
                    int IDEditorial = Convert.ToInt32(Item["EDI_ID"]);
                    if (IDEditorial == Editorial)
                    {
                        CMBX_EDITORIAL.SelectedIndex = i;
                        break;
                    }
                }

                byte Seccion = Convert.ToByte(row["LIB_SECCION"]);
                for (int i = 0; i < CMBX_SECCION.Items.Count; i++)
                {
                    DataRowView Item = (DataRowView)CMBX_SECCION.Items[i];
                    byte IDSeccion = Convert.ToByte(Item["SEC_ID"]);
                    if (IDSeccion == Seccion)
                    {
                        CMBX_SECCION.SelectedIndex = i;
                        break;
                    }
                }
            }

            else MessageBox.Show("No se encontró ningún libro con la clave proporcionada.");
        }

        private void BTN_GUARDAR_LIBRO_Click(object sender, RoutedEventArgs e)
        {
            // Verificar si hay un elemento seleccionado en el ComboBox
            if (CMBX_EDITORIAL.SelectedItem == null || CMBX_SECCION.SelectedItem == null)
            {
                MessageBox.Show("Por favor, seleccione una editorial y una sección.");
                return; // Salir del método sin continuar
            }

            else
            {
                // Obtener los valores seleccionados de los ComboBoxes
                int cveedi = int.Parse(CMBX_EDITORIAL.SelectedValue.ToString());
                int cvesec = int.Parse(CMBX_SECCION.SelectedValue.ToString());

                // Verificar si la caja de texto del ID, Título y año de publicación están vacías
                if (string.IsNullOrEmpty(TXT_ID_LIBRO.Text) || string.IsNullOrEmpty(TXT_TITULO_LIBRO.Text) || string.IsNullOrEmpty(TXT_APUBLICACION.Text))
                {
                    MessageBox.Show("Faltan datos.");
                }
                else
                {
                    // Usando Constructores
                    CLASES.cllibro OCONSULTAR = new CLASES.cllibro(int.Parse(TXT_ID_LIBRO.Text));
                    CLASES.cllibro OGRABAR = new CLASES.cllibro(int.Parse(TXT_ID_LIBRO.Text), TXT_TITULO_LIBRO.Text, int.Parse(TXT_APUBLICACION.Text), cveedi, cvesec, int.Parse(TXT_CANTIDAD.Text));

                    DataSet ds = new DataSet();
                    c = new CLASES.CONEXION(OCONSULTAR.consultaindividual());
                    ds = c.consultar();

                    if (ds.Tables["Tabla"].Rows.Count > 0)
                    {
                        c = new CLASES.CONEXION(OGRABAR.modificar());
                        MessageBox.Show(c.EJECUTAR());
                    }
                    else
                    {
                        c = new CLASES.CONEXION(OGRABAR.grabarlibro());
                        MessageBox.Show(c.EJECUTAR());
                    }

                    // Limpiar las cajas de texto después de guardar
                    TXT_TITULO_LIBRO.Clear();
                    TXT_APUBLICACION.Clear();
                    TXT_CANTIDAD.Text = "0";
                    CMBX_EDITORIAL.SelectedIndex = -1; // Limpiar ComboBoxes
                    CMBX_SECCION.SelectedIndex = -1;

                    // Obtener el siguiente ID disponible y actualizar la caja de texto de ID
                    ConsultaSiguienteId();
                }
            }
        }

        private void TXT_BUSCAR_AUTOR_TextChanged(object sender, TextChangedEventArgs e)
        {
            string term = TXT_BUSCAR_AUTOR.Text.Trim();

            // Filtramos los datos del DataTable
            if (!string.IsNullOrEmpty(term))
            {

                DataView dv = ((DataView)DATAGRID_AUTORES.ItemsSource);
                dv.RowFilter = $"Nombre LIKE '%{term}%'";
            }
            else
            {
                ((DataView)DATAGRID_AUTORES.ItemsSource).RowFilter = string.Empty;
            }
        }

        private void TXT_BUSCAR_GENERO_TextChanged(object sender, TextChangedEventArgs e)
        {
            string term = TXT_BUSCAR_GENERO.Text.Trim();

            // Filtramos los datos del DataTable
            if (!string.IsNullOrEmpty(term))
            {

                DataView dv = ((DataView)DATAGRID_GENEROS.ItemsSource);
                dv.RowFilter = $"Nombre LIKE '%{term}%'";
            }

            else
            {
                ((DataView)DATAGRID_GENEROS.ItemsSource).RowFilter = string.Empty;
            }
        }

        private void DATAGRID_AUTORES_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if(DATAGRID_AUTORES.SelectedItem != null)
            {
                if (String.IsNullOrEmpty(TXT_ID_LIBRO.Text))
                {
                    MessageBox.Show("Ingrese un ID de un libro.");
                    ConsultaSiguienteId();
                }

                else if (String.IsNullOrEmpty(TXT_TITULO_LIBRO.Text)) MessageBox.Show("Ingrese el título del libro.");

                else
                {
                    DataRowView view = (DataRowView)DATAGRID_AUTORES.SelectedItem;
                    string Autor = view.Row.ItemArray[0].ToString();
                    string Nombre = view.Row.ItemArray[1].ToString();

                    string Titulo = TXT_TITULO_LIBRO.Text;

                    int IdLibro = Convert.ToInt32(TXT_ID_LIBRO.Text);
                    int IDAutor = Convert.ToInt32(Autor);

                    CLASES.cllibro BuscarLibro = new CLASES.cllibro(IdLibro);

                    DataSet ds = new DataSet();
                    c = new CLASES.CONEXION(BuscarLibro.consultaindividual());
                    ds = c.consultar();
                    if (ds.Tables["Tabla"].Rows.Count > 0)
                    {
                        CLASES.cllibroautor BuscarLA = new CLASES.cllibroautor(IdLibro, IDAutor);
                        DataSet dsLA = new DataSet();
                        c = new CLASES.CONEXION(BuscarLA.ConsultaIndividual());
                        dsLA = c.consultar();

                        if (dsLA.Tables["Tabla"].Rows.Count > 0)
                        {
                            if (MessageBox.Show($"¿Desea retirar al autor {Nombre} del libro {Titulo}?", "Alerta", MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes)
                            {
                                CLASES.cllibroautor Borrar = new CLASES.cllibroautor(IdLibro, IDAutor);
                                c = new CLASES.CONEXION(Borrar.BorrarLibroAutor());
                                MessageBox.Show(c.EJECUTAR());
                            }

                            else
                            {
                                DATAGRID_AUTORES.SelectedItem = null;
                            }
                        }

                        else
                        {
                            if (MessageBox.Show($"¿Desea agregar al autor {Nombre} al libro {Titulo}?", "Alerta", MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes)
                            {
                                CLASES.cllibroautor Guardar = new CLASES.cllibroautor(IdLibro, IDAutor);
                                c = new CLASES.CONEXION(Guardar.GuardarLibroAutor());
                                MessageBox.Show(c.EJECUTAR());
                            }

                            else
                            {
                                DATAGRID_AUTORES.SelectedItem = null;
                            }
                        }

                    }

                    else
                    {
                        MessageBox.Show("Ingrese un libro existente");
                    }
                }
            }
        }

        private void DATAGRID_GENEROS_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if(DATAGRID_GENEROS.SelectedItem != null)
            {
                if (String.IsNullOrEmpty(TXT_ID_LIBRO.Text))
                {
                    MessageBox.Show("Ingrese un ID de un libro.");
                    ConsultaSiguienteId();
                }

                else if (String.IsNullOrEmpty(TXT_TITULO_LIBRO.Text)) MessageBox.Show("Ingrese el título del libro.");

                else
                {
                    DataRowView view = (DataRowView)DATAGRID_GENEROS.SelectedItem;
                    string Genero = view.Row.ItemArray[0].ToString();
                    string Nombre = view.Row.ItemArray[1].ToString();

                    string Titulo = TXT_TITULO_LIBRO.Text;

                    int IdLibro = Convert.ToInt32(TXT_ID_LIBRO.Text);
                    int IDGenero = Convert.ToInt32(Genero);

                    CLASES.cllibro BuscarLibro = new CLASES.cllibro(IdLibro);

                    DataSet ds = new DataSet();
                    c = new CLASES.CONEXION(BuscarLibro.consultaindividual());
                    ds = c.consultar();
                    if (ds.Tables["Tabla"].Rows.Count > 0)
                    {
                        CLASES.cllibrogenero BuscarLG = new CLASES.cllibrogenero(IdLibro, IDGenero);
                        DataSet dsLA = new DataSet();
                        c = new CLASES.CONEXION(BuscarLG.ConsultaIndividual());
                        dsLA = c.consultar();

                        if (dsLA.Tables["Tabla"].Rows.Count > 0)
                        {
                            if (MessageBox.Show($"¿Desea retirar al libro {Titulo} del género {Nombre}?", "Alerta", MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes)
                            {
                                CLASES.cllibrogenero Borrar = new CLASES.cllibrogenero(IdLibro, IDGenero);
                                c = new CLASES.CONEXION(Borrar.BorrarLibroGenero());
                                MessageBox.Show(c.EJECUTAR());
                            }

                            else
                            {
                                DATAGRID_GENEROS.SelectedItem = null;
                            }
                        }

                        else
                        {
                            if (MessageBox.Show($"¿Desea agregar el libro {Titulo} al género {Nombre}?", "Alerta", MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes)
                            {
                                CLASES.cllibrogenero Guardar = new CLASES.cllibrogenero(IdLibro, IDGenero);
                                c = new CLASES.CONEXION(Guardar.GuardarLibroGenero());
                                MessageBox.Show(c.EJECUTAR());
                            }

                            else
                            {
                                DATAGRID_GENEROS.SelectedItem = null;
                            }
                        }

                    }

                    else
                    {
                        MessageBox.Show("Ingrese un libro existente");
                    }
                }
            }
        }

        private void TXT_ID_LIBRO_KeyDown(object sender, KeyEventArgs e)
        {
            /* Aqui hago que sólo permita números*/
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.OemPeriod)
            {
                if (e.Key == Key.OemPeriod && TXT_ID_LIBRO.Text.IndexOf('.') != -1)
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
                TXT_ID_LIBRO.Text = string.Empty;
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
