using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIBLIOTECA_UAdeO.CLASES
{
    internal class cllibro
    {
        private int id;
        private string titulo;
        private int editorial;
        private int seccion;
        private int aniopublicacion;
        private int cantidad;

        public cllibro(int Id)
        {
            id = Id;
        }

        public cllibro()
        {

        }

        public cllibro(int Id, string Titulo, int Aniopublicacion, int Editorial, int Seccion, int Cantidad)
        {
            id = Id;
            titulo = Titulo;
            aniopublicacion = Aniopublicacion;
            editorial = Editorial;
            seccion = Seccion;
            cantidad = Cantidad;
        }

        public cllibro(int ID, int Cantidad)
        {
            id = ID;
            cantidad = Cantidad;
        }

        public string ConsultaSiguienteId()
        {
            return "SELECT ISNULL(MAX(LIB_ID), 0) + 1 AS LIB_ID FROM LIBRO";
        }

        public string grabarlibro()
        {
            return "INSERT INTO LIBRO VALUES(" + id + ", '" + titulo + "', " + aniopublicacion + ", " + editorial + ", " + seccion + ", " + cantidad + ")";
        }

        public string consultaindividual()
        {
            return "SELECT * FROM LIBRO WHERE LIB_ID = " + id;
        }

        public string ConsultaGeneralTitulo()
        {
            return "SELECT LIBRO.LIB_ID AS 'ID', LIBRO.LIB_TITULO AS 'Título', LIBRO.LIB_ANIOPUBLICACION AS 'Año de publicación', EDITORIAL.EDI_NOMBRE\r\nAS 'Editorial', SECCION.SEC_NOMBRE AS 'Sección', LIBRO.LIB_CANTIDAD AS 'Cantidad' FROM LIBRO INNER JOIN EDITORIAL ON LIBRO.LIB_EDITORIAL = EDITORIAL.EDI_ID INNER JOIN SECCION ON LIBRO.LIB_SECCION = SECCION.SEC_ID";
        }

        public string ConsultaGeneralAutor()
        {
            return "SELECT LIBRO.LIB_ID AS 'ID', LIBRO.LIB_TITULO AS 'Título', LIBRO.LIB_ANIOPUBLICACION AS 'Año de publicación',\r\nEDITORIAL.EDI_NOMBRE AS 'Editorial', SECCION.SEC_NOMBRE AS 'Sección', CONCAT(AUTOR.AUT_NOMBRE, ' ', AUTOR.AUT_APATERNO, ' ', AUTOR.AUT_AMATERNO) AS 'Autor', LIBRO.LIB_CANTIDAD AS 'Cantidad' FROM LIBRO_AUTOR INNER JOIN LIBRO ON LIBRO_AUTOR.LA_LIBRO = LIBRO.LIB_ID INNER JOIN AUTOR ON LIBRO_AUTOR.LA_AUTOR = AUTOR.AUT_ID INNER JOIN EDITORIAL ON LIBRO.LIB_EDITORIAL = EDITORIAL.EDI_ID INNER JOIN SECCION ON LIBRO.LIB_SECCION = SECCION.SEC_ID";
        }

        public string ConsultaGeneralGenero()
        {
            return "SELECT LIBRO.LIB_ID AS 'ID', LIBRO.LIB_TITULO AS 'Título', LIBRO.LIB_ANIOPUBLICACION AS 'Año de publicación', EDITORIAL.EDI_NOMBRE AS 'Editorial', SECCION.SEC_NOMBRE AS 'Sección', GENERO.GEN_NOMBRE AS 'Género', LIBRO.LIB_CANTIDAD AS 'Cantidad' FROM LIBRO_GENERO INNER JOIN LIBRO ON LIBRO_GENERO.LG_LIBRO = LIBRO.LIB_ID INNER JOIN GENERO ON LIBRO_GENERO.LG_GENERO = GENERO.GEN_ID INNER JOIN EDITORIAL ON LIBRO.LIB_EDITORIAL = EDITORIAL.EDI_ID INNER JOIN SECCION ON LIBRO.LIB_SECCION = SECCION.SEC_ID";
        }

        public string modificar()
        {
            return "UPDATE LIBRO SET LIB_TITULO = '" + titulo + "', LIB_ANIOPUBLICACION = " + aniopublicacion + ", LIB_EDITORIAL = " + editorial + ", LIB_SECCION = " + seccion + ", LIB_CANTIDAD = " + cantidad + " WHERE LIB_ID = " + id;
        }

        public string inventariolibros()
        {
            return "UPDATE LIBRO SET LIB_CANTIDAD = " + cantidad + " WHERE LIB_ID = " + id;
        }

        public string ConsultaComboEditorial()
        {
            return ("select * from EDITORIAL");
        }

        public string ConsultaComboSeccion()
        {
            return ("select * from SECCION");
        }
    }
}
