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
    /// Lógica de interacción para CONFIRMARDEVOLUCION.xaml
    /// </summary>
    public partial class CONFIRMARDEVOLUCION : Window
    {
        private readonly string Conn;
        private readonly globales Global = new globales();

        public CONFIRMARDEVOLUCION()
        {
            InitializeComponent();
            Conn = Global.ConexionDB();
            CargarDG();
        }

        //int IDSol = 0;
        //string NombreSol;

        CLASES.CONEXION C;

        private void CargarDG()
        {
            using (SqlConnection ConX = new SqlConnection(Conn))
            {
                CLASES.clprestamo Pre = new CLASES.clprestamo();

                string Query = Pre.CGSinDevolver();
                SqlCommand Comm = new SqlCommand(Query, ConX);

                try
                {
                    ConX.Open();
                    SqlDataAdapter Adapt = new SqlDataAdapter(Comm);
                    DataTable DTable = new DataTable();
                    Adapt.Fill(DTable);
                    DATAGRIDDEVOLVER.ItemsSource = DTable.DefaultView;
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

        private void TXTBUSCAR_TextChanged(object sender, TextChangedEventArgs e)
        {
            string Buscar = TXTBUSCAR.Text.Trim();

            // Filtramos los datos del DataTable
            if (!string.IsNullOrEmpty(Buscar))
            {
                DataView dv = ((DataView)DATAGRIDDEVOLVER.ItemsSource);
                dv.RowFilter = $"Solicitante LIKE '%{Buscar}%'";
            }

            else
            {
                ((DataView)DATAGRIDDEVOLVER.ItemsSource).RowFilter = string.Empty;
            }
        }

        private void BTNCONFIRMARDEVOLUCION_Click(object sender, RoutedEventArgs e)
        {
            if (DATAGRIDDEVOLVER.SelectedItem != null)
            {
                DataRowView view = (DataRowView)DATAGRIDDEVOLVER.SelectedItem;
                int IDPre = Convert.ToInt32(view.Row.ItemArray[0].ToString());
                string NombreSol = view.Row.ItemArray[1].ToString();
                string FechaIni = view.Row.ItemArray[2].ToString();
                string FechaDev = view.Row.ItemArray[3].ToString();

                CLASES.clprestamo FindDev = new CLASES.clprestamo(IDPre);

                DataSet ds = new DataSet();
                C = new CLASES.CONEXION(FindDev.ConsultaIndividual());
                ds = C.consultar();

                if (ds.Tables["Tabla"].Rows.Count > 0)
                {
                    if (MessageBox.Show($"¿Desea devolver el préstamo No. {IDPre} solicitado por {NombreSol} el día {FechaIni} para ser devuelto el día {FechaDev}?", "Alerta", MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes)
                    {
                        CLASES.clprestamo Dev = new CLASES.clprestamo(IDPre);
                        C = new CLASES.CONEXION(Dev.Devolver());
                        MessageBox.Show(C.EJECUTAR());
                        RestablecerLibros(IDPre); //Toma el ID del préstamo para hacer las respectivas devoluciones
                        CargarDG();
                    }

                    else
                    {
                        DATAGRIDDEVOLVER.SelectedItem = null;
                        return;
                    }
                }

                else
                {
                    MessageBox.Show("Préstamo no encontrado");
                    DATAGRIDDEVOLVER.SelectedItem = null;
                }
            }

            else MessageBox.Show("Elija un préstamo a devolver.");
        }

        private void RestablecerLibros(int idprestamo)
        {
            CLASES.cldetalleprestamo BuscarLibros = new CLASES.cldetalleprestamo(idprestamo);
            DataSet DSL = new DataSet();
            C = new CLASES.CONEXION(BuscarLibros.ElegirDETPDevolver());  //Elige los detalles de prestamo con el id del préstamo devuelto
            DSL = C.consultar();

            if (DSL.Tables["Tabla"].Rows.Count > 0) //Si detecta registros
            {
                foreach(DataTable DT in DSL.Tables)  //Por cada DataTable en el DataSet...
                {
                    foreach(DataRow DR in DT.Rows)  //Por cada fila del DataTable
                    {
                        int IDLibro = Convert.ToInt32(DR.ItemArray[2]);  //ID del libro
                        byte CantDev = Convert.ToByte(DR.ItemArray[3]);  //Cantidad de libros prestados que se devolverán

                        CLASES.cllibro ElegLibro = new CLASES.cllibro(IDLibro);
                        DataSet DSCant = new DataSet();
                        C = new CLASES.CONEXION(ElegLibro.consultaindividual());
                        DSCant = C.consultar();

                        if (DSCant.Tables["Tabla"].Rows.Count > 0)
                        {
                            byte CantActual = Convert.ToByte(DSCant.Tables["Tabla"].Rows[0]["LIB_CANTIDAD"]);  //Obtiene la cantidad de libros en el inventario
                            int Cantidad = CantActual + CantDev;  //Suma los libros en inventario con los que se devolverán

                            CLASES.cllibro DevLibro = new CLASES.cllibro(IDLibro, Cantidad);
                            C = new CLASES.CONEXION(DevLibro.inventariolibros());
                            string Devuelto = C.EJECUTAR();  //Esta vez lo puse como una variable string porque, bueno... sería muy fastidioso ver un MessageBox cada vez que actualice una cantidad de un libro
                        }
                    }
                }
            }
        }
    }
}
