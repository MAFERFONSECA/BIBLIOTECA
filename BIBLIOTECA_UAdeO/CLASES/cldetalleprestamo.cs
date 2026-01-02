using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIBLIOTECA_UAdeO.CLASES
{
    public class cldetalleprestamo
    {
        private int Prestamo;
        private int Libro;
        private byte Cantidad;

        public cldetalleprestamo()
        {

        }

        public cldetalleprestamo(int prestamo)
        {
            Prestamo = prestamo;
        }

        public cldetalleprestamo(int prestamo, int libro, byte cantidad)
        {
            Prestamo = prestamo;
            Libro = libro;
            Cantidad = cantidad;
        }

        public string ElegirDETPDevolver()
        {
            return "SELECT * FROM DETALLE_PRESTAMO WHERE DETP_PRESTAMO = " + Prestamo;
        }

        public string GuardarDetalle()
        {
            return "INSERT INTO DETALLE_PRESTAMO (DETP_PRESTAMO, DETP_LIBRO, DETP_CANTIDAD) VALUES(" + Prestamo + ", " + Libro + ", " + Cantidad + ")";
        }
    }
}
