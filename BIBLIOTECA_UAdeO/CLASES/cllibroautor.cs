using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIBLIOTECA_UAdeO.CLASES
{
    internal class cllibroautor
    {
        private int IDLibro;
        private int IDAutor;

        public cllibroautor(int idlibro, int idautor)
        {
            IDLibro = idlibro;
            IDAutor = idautor;
        }

        public string ConsultaIndividual()
        {
            return "SELECT * FROM LIBRO_AUTOR WHERE LA_LIBRO = " + IDLibro + "AND LA_AUTOR = " + IDAutor;
        }

        public string GuardarLibroAutor()
        {
            return "INSERT INTO LIBRO_AUTOR VALUES(" + IDLibro + ", " + IDAutor + ")";
        }

        public string BorrarLibroAutor()
        {
            return "DELETE FROM LIBRO_AUTOR WHERE LA_Libro = " + IDLibro + " AND LA_AUTOR = " + IDAutor;
        }
    }
}
