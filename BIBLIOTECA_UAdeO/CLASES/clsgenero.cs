using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIBLIOTECA_UAdeO.CLASES
{
    internal class clsgenero
    {
        //   PROPIEDADES - CAMPOS - COMO ES
        private int _id;
        private string _name;

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }

        //   CONSTRUCTORES
        public clsgenero() //consultar todos los datos
        {
        }
        public clsgenero(int id, string name) // grabar y modificar
        {
            _id = id;
            _name = name;

        }
        public clsgenero(int id)  // consulta individual
        {
            _id = id;
        }


        // LO QUE HACE METODOS - ACCIONES 

        // MÉTODO PARA CONSULTAR EL SIGUIENTE ID
        public string ConsultaSiguienteId()
        {
            return "SELECT MAX(GEN_ID) + 1 AS GEN_ID FROM GENERO";
        }


        public string grabar()
        {
            return ("INSERT INTO GENERO VALUES('" + _id + "', '" + _name + "')");
        }
        public string modificar()
        {
            return ("update GENERO SET GEN_NOMBRE = '" + _name + "' WHERE GEN_ID = '" + _id + "'");
        }
        public string consultaindividual()
        {
            return ("SELECT * FROM GENERO WHERE GEN_ID = '" + _id + "'");
        }
        public string consultageneral()
        {
            return ("SELECT   GEN_ID AS ID,   GEN_NOMBRE AS NOMBRE FROM    GENERO");
        }
        public string eliminar()
        {
            return ("delete from GENERO WHERE GEN_ID = '" + _id + "'");

        }
    }
}
