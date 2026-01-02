using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIBLIOTECA_UAdeO.CLASES
{
    public class cldetalleprestamotemp
    {
        private int IDPrestamo;
        private int Usuario;
        private int Libro;
        private byte Cantidad;

        public cldetalleprestamotemp() { }

        public cldetalleprestamotemp(int idprestamo, int usuario, int libro, byte cantidad)
        {
            IDPrestamo = idprestamo;
            Usuario = usuario;
            Libro = libro;
            Cantidad = cantidad;
        }

        public string ConsultaDG()
        {
            return "SELECT * FROM DetallePrestamoTemp_View";
        }

        public string ConsultaGeneral()
        {
            return "SELECT * FROM DETALLEPRESTAMOTEMP";
        }

        public string BorrarTablaTemporal()
        {
            return "DELETE FROM DETALLEPRESTAMOTEMP";
        }

        public string InsertarLibros()
        {
            return "INSERT INTO DETALLEPRESTAMOTEMP VALUES(" + IDPrestamo + ", " + Usuario + ", " + Libro + ", " + Cantidad + ")";
        }
    }
}
