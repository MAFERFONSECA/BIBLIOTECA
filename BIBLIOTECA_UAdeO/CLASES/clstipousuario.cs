using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIBLIOTECA_UAdeO.CLASES
{
    internal class clstipousuario
    {
        //   PROPIEDADES - CAMPOS - COMO ES
        private int _id;
        private string _name;

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }

        //   CONSTRUCTORES
        public clstipousuario() //consultar todos los datos
        {
        }
        public clstipousuario(int id, string name) // grabar y modificar
        {
            _id = id;
            _name = name;

        }
        public clstipousuario(int id)  // consulta individual
        {
            _id = id;
        }










        // LO QUE HACE METODOS - ACCIONES 

        // MÉTODO PARA CONSULTAR EL SIGUIENTE ID
        public string ConsultaSiguienteId()
        {
            return "SELECT MAX(TPU_ID) + 1 AS TPU_ID FROM TIPO_USUARIO";
        }

        public string grabar()
        {
            return ("INSERT INTO TIPO_USUARIO VALUES('" + _id + "', '" + _name + "')");
        }

        public string modificar()
        {
            return ("update TIPO_USUARIO SET TPU_DESCRIPCION = '" + _name + "' WHERE TPU_ID = '" + _id + "'");
        }

        public string consultaindividual()
        {
            return ("SELECT * FROM TIPO_USUARIO WHERE TPU_ID = '" + _id + "'");
        }

        public string consultageneral()
        {
            return ("SELECT TPU_ID AS 'ID', TPU_DESCRIPCION AS 'DESCRIPCION' FROM TIPO_USUARIO");
        }

        public string eliminar()
        {
            return ("delete from TIPO_USUARIO WHERE TPU_ID = '" + _id + "'");
        }
    }
}
