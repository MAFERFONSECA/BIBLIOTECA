using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIBLIOTECA_UAdeO.CLASES
{
    internal class clusuario
    {
        private int id;
        private string nombre;
        private string apaterno;
        private string amaterno;
        private int idtpu;
        private string contrasena;

        public clusuario(int id)
        {
            this.id = id;
        }

        public clusuario()
        {

        }
        public clusuario(int id, string nombre, string apaterno, string amaterno, int idtpu, string contrasena)
        {
            this.id = id;
            this.nombre = nombre;
            this.apaterno = apaterno;
            this.amaterno = amaterno;
            this.idtpu = idtpu;
            this.contrasena = contrasena;
        }



        public string ConsultaSiguienteId()
        {
            return "SELECT MAX(USU_ID) + 1 AS USU_ID FROM USUARIO";
        }

        public string ConsultaInicioSesion(string id, string contraseña)
        {
            return ("SELECT * FROM USUARIO WHERE USU_ID = '" + id + "' AND USU_CONTRASENA = '" + contraseña + "'");
        }

        public string grabar()
        {
            return ("INSERT INTO USUARIO (USU_ID, USU_NOMBRE, USU_APEPATERNO, USU_APEMATERNO, USU_TIPO, USU_CONTRASENA) VALUES ('" + this.id
                + "','" + this.nombre + "', '" + this.apaterno + "', '" + this.amaterno + "', '" + this.idtpu + "', '" + this.contrasena + "')");
        }
        public string consultaindividual()
        {
            return ("select * from VTA_TPU_USUARIO WHERE USU_ID = '" + this.id + "'");
        }

        public string modificar()
        {
            return ("UPDATE USUARIO SET USU_NOMBRE= '" + this.nombre + "', USU_APEPATERNO='" + this.apaterno + "', USU_APEMATERNO='" + this.amaterno + "', USU_TIPO='" + this.idtpu + "',  USU_CONTRASENA='" + this.contrasena + "' WHERE USU_ID= '" + this.id + "'");
        }

        public string eliminar()
        {
            return ("delete from USUARIO WHERE USU_ID = '" + this.id + "'");
        }

        public string consultageneral()
        {
            return ("SELECT U.USU_ID AS ID, T.TPU_DESCRIPCION AS TIPOUSUARIO, U.USU_NOMBRE AS NOMBRE, U.USU_APEPATERNO AS APELLIDOPATERNO, U.USU_APEMATERNO AS APELLIDOMATERNO, U.USU_CONTRASENA AS CONTRASEÑA FROM dbo.TIPO_USUARIO T INNER JOIN dbo.USUARIO U ON T.TPU_ID = U.USU_TIPO"); 
        }

        public string consultaprestamo()
        {
            return ("SELECT * FROM UsuarioPrestar_View");
        }

        public string consultacombo()
        {
            return ("select * from TIPO_USUARIO");
        }

    }
}
