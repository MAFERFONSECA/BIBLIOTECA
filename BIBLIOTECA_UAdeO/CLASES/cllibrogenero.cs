using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIBLIOTECA_UAdeO.CLASES
{
    internal class cllibrogenero
    {
        private int IDLibro;
        private int IDGenero;

        public cllibrogenero(int idlibro, int idgenero)
        {
            IDLibro = idlibro;
            IDGenero = idgenero;
        }

        public string ConsultaIndividual()
        {
            return "SELECT * FROM LIBRO_GENERO WHERE LG_LIBRO = " + IDLibro + "AND LG_GENERO = " + IDGenero;
        }

        public string GuardarLibroGenero()
        {
            return "INSERT INTO LIBRO_GENERO VALUES(" + IDLibro + ", " + IDGenero + ")";
        }

        public string BorrarLibroGenero()
        {
            return "DELETE FROM LIBRO_GENERO WHERE LG_Libro = " + IDLibro + " AND LG_GENERO = " + IDGenero;
        }
    }
}