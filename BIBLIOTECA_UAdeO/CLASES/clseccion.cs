using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIBLIOTECA_UAdeO.CLASES
{
    internal class clseccion
    {
        //   PROPIEDADES - CAMPOS - COMO ES
        private int _id;
        private string _name;

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }

        //   CONSTRUCTORES
        public clseccion() //consultar todos los datos
        {
        }
        public clseccion(int id, string name) // grabar y modificar
        {
            _id = id;
            _name = name;

        }
        public clseccion(int id)  // consulta individual
        {
            _id = id;
        }


        // LO QUE HACE METODOS - ACCIONES 

        // MÉTODO PARA CONSULTAR EL SIGUIENTE ID
        public string ConsultaSiguienteId()
        {
            return "SELECT MAX(SEC_ID) + 1 AS SEC_ID FROM SECCION";
        }


        public string grabar()
        {
            return ("INSERT INTO SECCION VALUES('" + _id + "', '" + _name + "')");
        }
        public string modificar()
        {
            return ("update SECCION SET SEC_NOMBRE = '" + _name + "' WHERE SEC_ID = '" + _id + "'");
        }
        public string consultaindividual()
        {
            return ("SELECT * FROM SECCION WHERE SEC_ID = '" + _id + "'");
        }
        public string consultageneral()
        {
            return ("SELECT  SEC_ID AS ID,   SEC_NOMBRE AS NOMBRE FROM    SECCION\r\n");
        }
        public string eliminar()
        {
            return ("delete from SECCION WHERE SEC_ID = '" + _id + "'");

        }
    }
}
