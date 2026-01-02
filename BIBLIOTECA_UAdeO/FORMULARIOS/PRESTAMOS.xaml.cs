using BIBLIOTECA_UAdeO.CLASES;
using CrystalDecisions.Shared;
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
    /// Lógica de interacción para PRESTAMOS.xaml
    /// </summary>
    public partial class PRESTAMOS : Window
    {
        private readonly string connectionString;
        private readonly globales globales = new globales();
        

        public PRESTAMOS()
        {
            InitializeComponent();
            ConsultaSiguienteID();
            connectionString = globales.ConexionDB();
            ConsultaTablaExistente();  //Borra la tabla temporal en caso de tener registros para evitar conflictos
        }

        CLASES.CONEXION c;
        int IDPrestamo;
        int IDSolicitante = 0;
        string NombreSolicitante;

        int IDLibro;
        byte CantidadLibro;

        int BorraTabla;  //Variable que toma valores para el switch del método BorrarTablaTemp
        bool Realizado;  //Variable booleana para confirmar la realización de un préstamo

        private void ConsultaSiguienteID()
        {
            // Crea un objeto para obtener el siguiente ID
            CLASES.clprestamo OConsultar = new CLASES.clprestamo();
            // Ejecuta la consulta para obtener el siguiente ID
            c = new CLASES.CONEXION(OConsultar.ConsultaSiguenteID());
            DataSet ds = c.consultar();
            // Verifica si se encontraron resultados
            if (ds.Tables["Tabla"].Rows.Count > 0)
            {
                // Asigna el ID al texto de la caja de texto
                IDPrestamo = Convert.ToInt32(ds.Tables["Tabla"].Rows[0]["ID"].ToString());
                LBL_PRESTAMOID.Content = IDPrestamo;
            }
        }

        private void ConsultaTablaExistente()  //Método para consultar si la tabla temporal tenía datos
        {
            CLASES.cldetalleprestamotemp OConsultar = new CLASES.cldetalleprestamotemp();

            DataSet ds = new DataSet();
            c = new CLASES.CONEXION(OConsultar.ConsultaGeneral());
            ds = c.consultar();

            if (ds.Tables["Tabla"].Rows.Count > 0)  //Si tiene datos, los elimina para evitar conflictos
            {
                BorraTabla = 1;
                Realizado = false;  //Asigna falso al valor booleano
                BorrarTablaTemp(BorraTabla, Realizado);
            }

            CargarDG();
        }

        private void BorrarTablaTemp(int borrar, bool realizado)  //El método para borrar la tabla temporal se creó aparte para poder borrarla también en caso de que se cambie el solicitante y/o el préstamo como tal no se efectúe
        {
            CLASES.cldetalleprestamotemp OConsultar = new CLASES.cldetalleprestamotemp();
            CLASES.cldetalleprestamotemp OBorrar = new CLASES.cldetalleprestamotemp();

            DataSet ds = new DataSet();
            c = new CLASES.CONEXION(OConsultar.ConsultaGeneral());
            ds = c.consultar();

            if (ds.Tables["Tabla"].Rows.Count > 0)
            {
                foreach (DataTable DT in ds.Tables)  //El DataSet DS se retoma justo aquí; por cada DataTable del DataSet DS
                {
                    foreach (DataRow DR in DT.Rows)  //Por cada fila del DataTable
                    {
                        int IDLibro = Convert.ToInt32(DR.ItemArray[2]);  //ID del libro
                        byte CantDevolver = Convert.ToByte(DR.ItemArray[3]);  //Cantidad de libros que se van a prestar

                        CLASES.cllibro ElegLibro = new CLASES.cllibro(IDLibro);
                        DataSet DSCant = new DataSet();
                        c = new CLASES.CONEXION(ElegLibro.consultaindividual());
                        DSCant = c.consultar();

                        if (DSCant.Tables["Tabla"].Rows.Count > 0 && realizado == false) //Si encuentra el libro con el ID, y la variable booleana "realizado" es falsa, se devuelven los libros que se iban a prestar al inventario
                        {
                            byte CantActual = Convert.ToByte(DSCant.Tables["Tabla"].Rows[0]["LIB_CANTIDAD"]);  //Obtiene la cantidad de libros en el inventario
                            int Cantidad = CantActual + CantDevolver;  //Suma los libros en inventario con los que se devolverán

                            CLASES.cllibro DevLibro = new CLASES.cllibro(IDLibro, Cantidad);
                            c = new CLASES.CONEXION(DevLibro.inventariolibros());
                            string Devuelto = c.EJECUTAR();  //Esta vez lo puse como una variable string porque, bueno... sería muy fastidioso ver un MessageBox cada vez que actualice una cantidad de un libro
                        }
                    }
                }

                c = new CLASES.CONEXION(OBorrar.BorrarTablaTemporal());

                switch (borrar)
                {
                    case 0:
                        MessageBox.Show(c.EJECUTAR());  //Si solo se cancela el préstamo, mostrará el MessageBox
                        break;

                    case 1:
                        string Eliminado = c.EJECUTAR();  //Si se cambia de solicitante en medio del proceso, no mostrará MessageBox
                        break;

                    default:
                        MessageBox.Show("");
                        break;
                }
            }

            IDSolicitante = 0;
            NombreSolicitante = null;
            CargarDG();
        }

        private void CargarDG()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                CLASES.cldetalleprestamotemp DetalleTemp = new CLASES.cldetalleprestamotemp();
                string query = DetalleTemp.ConsultaDG();
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();

                    adapter.Fill(dataTable);

                    DTGRIDPRESTAMOACTUAL.ItemsSource = dataTable.DefaultView;
                }

                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar los datos del préstamo: \n" + ex.Message);
                }
            }
        }

        private void BTNBUSCARLECTOR_Click(object sender, RoutedEventArgs e)
        {
            if (IDSolicitante != 0)
            {
                BorraTabla = 1;  //La variable BorraTabla toma valor para usarse en el switch del método BorrarTablaTemp
                Realizado = false;
                IDSolicitante = 0;
                NombreSolicitante = null;
                TXT_IDSOLICITANTE.Text = "Por elegir...";
                TXT_NOMBRESOLICITANTE.Clear();
                BorrarTablaTemp(BorraTabla, Realizado);
            }

            CLASES.clusuario alu = new clusuario();
            CLASES.CONEXION con = new CLASES.CONEXION();
            if (con.Execute(alu.consultaprestamo(), 0) == true)
            {
                if (con.FieldValue != "")
                {
                    TXT_IDSOLICITANTE.Text = con.FieldValue;
                    buscarlector();
                }
            }
        }

        private void buscarlector()
        {
            CLASES.clusuario OCONSULTAR;

            if (!int.TryParse(TXT_IDSOLICITANTE.Text, out int idUsuario))
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
                IDSolicitante = Convert.ToInt32(ds.Tables["Tabla"].Rows[0]["USU_ID"].ToString());
                TXT_IDSOLICITANTE.Text = Convert.ToString(IDSolicitante);

                string Nombre = ds.Tables["Tabla"].Rows[0]["USU_NOMBRE"].ToString();
                string ApePat = ds.Tables["Tabla"].Rows[0]["USU_APEPATERNO"].ToString();
                string ApeMat = ds.Tables["Tabla"].Rows[0]["USU_APEMATERNO"].ToString();

                NombreSolicitante = Nombre + " " + ApePat + " " + ApeMat;

                TXT_NOMBRESOLICITANTE.Text = NombreSolicitante;
            }

            else
            {
                MessageBox.Show("No se encontró ningún usuario con la Clave proporcionada.");
            }
        }

        private void BTNBUSCARLIBRO_Click(object sender, RoutedEventArgs e)
        {
            if (IDSolicitante != 0)
            {
                CLASES.cllibro LBR = new CLASES.cllibro();
                CLASES.CONEXION con = new CLASES.CONEXION();

                if (con.ExecLibro(LBR.ConsultaGeneralTitulo(), 0) == true)
                {
                    if (con.FieldValue != "")
                    {
                        IDLibro = Convert.ToInt32(con.FieldValue);
                        CantidadLibro = Convert.ToByte(con.FieldCant);
                        IngresarLibro(IDLibro, CantidadLibro);
                    }
                }
            }

            else MessageBox.Show("Elija un lector para prestar libros");
        }

        private void IngresarLibro(int ID, byte cantidad)
        {
            CLASES.cllibro ElegLibro = new CLASES.cllibro(ID);
            DataSet DSCant = new DataSet();
            c = new CLASES.CONEXION(ElegLibro.consultaindividual());  //Consulta un libro
            DSCant = c.consultar();

            if (DSCant.Tables["Tabla"].Rows.Count > 0)  //Si encuentra el libro
            {
                byte CantActual = Convert.ToByte(DSCant.Tables["Tabla"].Rows[0]["LIB_CANTIDAD"]);  //Obtiene la cantidad de libros en el inventario
                int Cantidad = CantActual - cantidad;  //Resta la cantidad de libros prestados a los del inventario

                CLASES.cllibro PrestaLibro = new CLASES.cllibro(IDLibro, Cantidad);
                c = new CLASES.CONEXION(PrestaLibro.inventariolibros());  //Actualiza la cantidad de libros
                string Prestado = c.EJECUTAR();  //Esta vez lo puse como una variable string porque, bueno... sería muy fastidioso ver un MessageBox cada vez que actualice una cantidad de un libro

                CLASES.cldetalleprestamotemp OGuardar = new CLASES.cldetalleprestamotemp(IDPrestamo, IDSolicitante, ID, cantidad);
                c = new CLASES.CONEXION(OGuardar.InsertarLibros());
                MessageBox.Show(c.EJECUTAR());

                CargarDG();
            }
        }

        private void BTNGUARDARPRESTAMO_Click(object sender, RoutedEventArgs e)
        {
            CLASES.cldetalleprestamotemp OConsultar = new CLASES.cldetalleprestamotemp();
            DataSet DS = new DataSet();
            c = new CLASES.CONEXION(OConsultar.ConsultaGeneral());
            DS = c.consultar();

            if (DS.Tables["Tabla"].Rows.Count > 0)  //El DataSet DS se retomará mucho más adelante, en la línea 315
            {
                DateTime Hoy = DateTime.Now;  //Fecha del momento
                int Anio = Hoy.Year;  //Año
                int Mes = Hoy.Month;  //Mes
                int Dia = Hoy.Day;  //Día
                int Hora = Hoy.Hour;  //Hora
                int Minuto = Hoy.Minute;  //Minuto
                int Segundo = Hoy.Second;  //Segundo
                int Milisegundo = Hoy.Millisecond;  //Milisegundo

                string sMes;  //Cadena con el numero de mes
                string sDia; //Cadena con el numero de día
                string sHora;  //Cadena con el numero de hora
                string sMinuto;  //Cadena con el numero de minuto
                string sSegundo;  //Cadena con el numero de segundo
                string sMilisegundo;  //Cadena con el numero de milisegundo

                if (Mes < 10) sMes = $"0{Mes}";  //Si el numero de mes es menor a 10 se le pone un 0 a la izquierda
                else sMes = Convert.ToString(Mes);

                if (Dia < 10) sDia = $"0{Dia}";  //Si el numero de día es menor a 10 se le pone un 0 a la izquierda
                else sDia = Convert.ToString(Dia);  //Si no, se convierte directamente

                if (Hora < 10) sHora = $"0{Hora}";  //Si el numero de hora es menor a 10 se le pone un 0 a la izquierda
                else sHora = Convert.ToString(Hora);  //Si no, se convierte directamente

                if (Minuto < 10) sMinuto = $"0{Minuto}";  //Si el numero de minuto es menor a 10 se le pone un 0 a la izquierda
                else sMinuto = Convert.ToString(Minuto);  //Si no, se convierte directamente

                if (Segundo < 10) sSegundo = $"0{Segundo}";  //Si el numero de segundo es menor a 10 se le pone un 0 a la izquierda
                else sSegundo = Convert.ToString(Segundo);  //Si no, se convierte directamente

                if (Milisegundo < 10) sMilisegundo = $"00{Milisegundo}";  //Si el numero de milisegundo es menor a 10 se le ponen dos 0's a la izquierda
                else if (Milisegundo < 100 && Milisegundo >= 10) sMilisegundo = $"0{Milisegundo}"; //Si el numero de mes es menor a 100, pero mayor o igual a 10, se le pone un 0 a la izquierda
                else sMilisegundo = Convert.ToString(Milisegundo);  //Si no, se convierte directamente

                string FechaIni = $"{Anio}-{sMes}-{sDia} {sHora}:{sMinuto}:{sSegundo}.{sMilisegundo}";
                //Cadena con la fecha actual

                Dia = Dia + 7;

                if((Mes == 1 && Dia > 31) || (Mes == 3 && Dia > 31) || (Mes == 5 && Dia > 31) ||
                    (Mes == 7 && Dia > 31) || (Mes == 8 && Dia > 31) || (Mes == 10 && Dia > 31))
                {  //Si el mes es enero, marzo, mayo, julio, agosto u octubre y el Día de devolución es mayor a 31
                    Mes++;  //Se suma 1 al número de mes
                    Dia = Dia - 31;  //Se le resta 31 al número de días
                }

                else if((Mes == 4 && Dia > 30) || (Mes == 6 && Dia > 30) || (Mes == 9 && Dia > 30) ||
                    (Mes == 11 && Dia > 30))
                {  //Si el mes es abril, junio, septiembre o noviembre y el Día de devolución es mayor a 30
                    Mes++;
                    Dia = Dia - 30;
                }

                else if(Mes == 12 && Dia > 31) //Si es diciembre y el Día de devolución es mayor a 31
                {
                    Anio++;
                    Mes = 1;
                    Dia = Dia - 31;
                }

                else if (Mes == 2 && Anio % 4 == 0 && Dia > 29)
                {  //Si es febrero en año bisiesto y el día de devoución es mayor a 29
                    Mes++;
                    Dia = Dia - 29;
                }

                else if (Mes == 2 && Dia > 28) //Si es un febrero cualquiera y el día de devolución es mayor a 28
                {
                    Mes++;
                    Dia = Dia - 28;
                }

                string sAnioDev = Convert.ToString(Anio);
                string sMesDev;
                string sDiaDev;

                if (Mes < 10) sMesDev = $"0{Mes}";
                else sMesDev = Convert.ToString(Mes);

                if (Dia < 10) sDiaDev = $"0{Dia}";
                else sDiaDev = Convert.ToString(Dia);

                string FechaDev = $"{sAnioDev}-{sMesDev}-{sDiaDev} {sHora}:{sMinuto}:{sSegundo}.{sMilisegundo}";
                //Cadena con la fecha de devolución, programada para ser una semana después de la solicitud del préstamo

                CLASES.clprestamo OGuardar = new CLASES.clprestamo(IDPrestamo, FechaIni, FechaDev, IDSolicitante);
                c = new CLASES.CONEXION(OGuardar.GuardarPrestamo());  //Guarda el préstamo
                MessageBox.Show(c.EJECUTAR());

                foreach(DataTable DT in DS.Tables)  //El DataSet DS se retoma justo aquí; por cada DataTable del DataSet DS
                {
                    foreach (DataRow DR in DT.Rows)  //Por cada fila del DataTable
                    {
                        int Prestamo = Convert.ToInt32(DR.ItemArray[0]);  //ID del préstamo
                        int IDLibro = Convert.ToInt32(DR.ItemArray[2]);  //ID del libro
                        byte CantPrestar = Convert.ToByte(DR.ItemArray[3]);  //Cantidad de libros que se van a prestar

                        CLASES.cldetalleprestamo GDetalle = new CLASES.cldetalleprestamo(Prestamo, IDLibro, CantPrestar);
                        c = new CLASES.CONEXION(GDetalle.GuardarDetalle());  //Guarda los detalles
                        string Detalle = c.EJECUTAR();
                    }
                }

                BorraTabla = 1;
                Realizado = true;
                BorrarTablaTemp(BorraTabla, Realizado);

                TXT_IDSOLICITANTE.Text = "Por elegir...";
                TXT_NOMBRESOLICITANTE.Clear();
                ConsultaSiguienteID();
            }

            else MessageBox.Show("Sin datos.");
        }

        private void BTNCANCELAR_Click(object sender, RoutedEventArgs e)
        {
            BorraTabla = 0;
            Realizado = false;
            BorrarTablaTemp(BorraTabla, Realizado); //La variable BorraTabla toma valor para usarse en el switch del método BorrarTablaTemp
            TXT_IDSOLICITANTE.Text = "Por elegir...";
            TXT_NOMBRESOLICITANTE.Clear();
        }

        private void BTNDEVOLUCION_Click(object sender, RoutedEventArgs e)
        {
            CONFIRMARDEVOLUCION ConfDev = new CONFIRMARDEVOLUCION();
            ConfDev.Show();
        }

        private void BTNBUSCARPRESTAMO_Click(object sender, RoutedEventArgs e)
        {
            BUSQUEDAPRESTAMOS BP = new BUSQUEDAPRESTAMOS();
            BP.Show();
        }

        private void BTNSALIR_Click(object sender, RoutedEventArgs e)
        {
            BorraTabla = 1;
            Realizado = false;
            BorrarTablaTemp(BorraTabla, Realizado);
            PAGINAPRINCIPAL paginaprincipal = new PAGINAPRINCIPAL();
            paginaprincipal.Show();
            this.Close();
        }
    }
}
