using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIBLIOTECA_UAdeO.CLASES
{
    public class clprestamo
    {
        private int ID;
        private string FechaInicio;
        private string FechaDevolucion;
        private int Usuario;

        public clprestamo() { }

        public clprestamo(int id)
        {
            ID = id;
        }

        public clprestamo (int id,  string fechainicio, string fechadevolucion, int usuario)
        {
            ID = id;
            FechaInicio = fechainicio;
            FechaDevolucion = fechadevolucion;
            Usuario = usuario;
        }

        public string ConsultaSiguenteID()
        {
            return "SELECT ISNULL(MAX(PRE_ID), 0) + 1 AS 'ID' FROM PRESTAMO";
        }

        public string GuardarPrestamo()
        {
            return "INSERT INTO PRESTAMO VALUES (" + ID + ", '" + FechaInicio + "', '" + FechaDevolucion + "', 0, " + Usuario + ")";
        }

        public string ConsultaIndividual()
        {
            return "SELECT * FROM PRESTAMO WHERE PRE_ID = " + ID;
        }

        public string ConsultaGeneral()
        {
            return "SELECT * FROM Prestamos_View";
        }

        public string CGSinDevolver()
        {
            return "SELECT * FROM PrestamoSinDevolver_View";
        }

        public string Devolver()
        {
            return "UPDATE PRESTAMO SET PRE_ESTADODEVOLUCION = 1 WHERE PRE_ID = " + ID;
        }
    }
}
