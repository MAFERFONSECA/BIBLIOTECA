using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIBLIOTECA_UAdeO.CLASES
{
    internal class CONEXION
    {
        private string sentencia1;
        private SqlConnection conn = new SqlConnection();
        private SqlCommand cmd;

        public CONEXION(string sentencia1)
        {
            this.sentencia1 = sentencia1;
        }
        public CONEXION() { }


        public string EJECUTAR()
        {
            conn.ConnectionString = CLASES.globales.miconexion;
            if (conn.State != ConnectionState.Open)
            {
                conn.Close();
                conn.Open();
            }
            cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sentencia1;
            cmd.ExecuteNonQuery(); // sirve para guardar, modificar y eliminar
            conn.Close();
            return "Operación exitosa";
        }
        public DataSet consultar()
        {
            DataSet datos = new DataSet();
            try
            {
                conn = new SqlConnection(CLASES.globales.miconexion);
                if (conn.State != ConnectionState.Open)
                {
                    conn.Close();
                    conn.Open();
                }
                SqlDataAdapter resp = new SqlDataAdapter(sentencia1, conn);
                resp.Fill(datos, "Tabla");
                conn.Close();
                return datos;
            }
            catch (Exception ex)
            {



            }

            return datos;
        }

        private string mFieldValue;  //Cadena con el ID del item seleccionado

        internal string FieldValue  //Devuelve el ID del item seleccionado
        {
            get { return mFieldValue; }
        }

        internal bool Execute(string SQL, int ColumnNumberToRetrive)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(SQL, CLASES.globales.miconexion);
            da.Fill(ds, "Table");

            FORMULARIOS.SearchForm frmSearchForm = new FORMULARIOS.SearchForm();
            frmSearchForm.mColNumber = ColumnNumberToRetrive;
            frmSearchForm.mDS = ds;
            ds = null;
            bool? resultado = frmSearchForm.ShowDialog();
            if (resultado.HasValue && resultado.Value)
            {
                mFieldValue = frmSearchForm.ReturnValue;
                return true;
            }
            else
            {
                return false;
            }

        }

        private string mFieldCant; //Cadena con la cantidad de libros escogida, creada exclusivamente para el formulario de buscar libro para prestar

        internal string FieldCant
        {
            get { return mFieldCant; } //Devuelve la cantidad de libros
        }

        internal bool ExecLibro(string SQL, int ColumnNumberToRetrive)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(SQL, CLASES.globales.miconexion);
            da.Fill(ds, "Table");

            FORMULARIOS.BUSCARLIBROPRESTAR Libro = new FORMULARIOS.BUSCARLIBROPRESTAR();
            Libro.mColNumber = ColumnNumberToRetrive;
            Libro.mDS = ds;
            ds = null;
            bool? resultado = Libro.ShowDialog();

            if (resultado.HasValue && resultado.Value)
            {
                mFieldValue = Libro.ReturnValue;  //ID del libro
                mFieldCant = Libro.ReturnCant;  //Cantidad de libros
                return true;
            }

            else
            {
                return false;
            }
        }
    }
}
