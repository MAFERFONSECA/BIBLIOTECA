using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIBLIOTECA_UAdeO.CLASES
{
    internal class clseditorial
    {
        private int _id;
        private string _name;

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }

        public clseditorial()
        {

        }

        public clseditorial(int id, string name)
        {
            _id = id;
            _name = name;
        }

        public clseditorial(int id)
        {
            _id = id;
        }


        public string ConsultarSiguienteID()
        {
            return "SELECT MAX(EDI_ID) + 1 AS EDI_ID FROM EDITORIAL";
        }

        public string grabar()
        {
            return ("INSERT INTO EDITORIAL VALUES('" + _id + "', '" + _name + "')");
        }

        public string modificar()
        {
            return ("update EDITORIAL SET EDI_NOMBRE = '" + _name + "' WHERE EDI_ID = '" + _id + "'");
        }
        public string consultaindividual()
        {
            return ("SELECT * FROM EDITORIAL WHERE EDI_ID = '" + _id + "'");
        }
        public string consultageneral()
        {
            return ("SELECT   EDI_ID AS ID,  EDI_NOMBRE AS NOMBRE  FROM   EDITORIAL\r\n");
        }
        public string eliminar()
        {
            return ("delete from EDITORIAL WHERE EDI_ID = '" + _id + "'");

        }
    }
}
