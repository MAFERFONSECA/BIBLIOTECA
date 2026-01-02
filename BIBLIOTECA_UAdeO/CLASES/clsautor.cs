using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIBLIOTECA_UAdeO.CLASES
{
    internal class clsautor
    {
        private int _id;
        private string _name;
        private string _apellidopat;
        private string _apellidomat;

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string ApellidoPat { get => _apellidopat; set => _apellidopat = value; }
        public string ApellidoMat { get => _apellidomat; set => _apellidomat = value; }

        public clsautor()
        { 
            
        }

        public clsautor(int id, string name, string apepat, string apemat)
        {
            _id = id;
            _name = name;
            _apellidopat = apepat;
            _apellidomat = apemat;
        }

        public clsautor(int id, string name, string apepat)
        {
            _id = id;
            _name = name;
            _apellidopat = apepat;
        }

        public clsautor(int id)
        {
            _id = id;
        }


        public string ConsultarSiguienteID()
        {
            return "SELECT MAX(AUT_ID) + 1 AS AUT_ID FROM AUTOR";
        }

        public string grabar() //Cuando el autor tiene dos apellidos
        {
            return ("INSERT INTO AUTOR VALUES('" + _id + "', '" + _name + "', '" + _apellidopat + "', '" + _apellidomat + "')");
        }

        public string modificar()
        {
            return ("update AUTOR SET AUT_NOMBRE = '" + _name + "', AUT_APATERNO = '" + _apellidopat +"', AUT_AMATERNO = '" + _apellidomat + "' WHERE AUT_ID = '" + _id + "'");
        }
        public string consultaindividual()
        {
            return ("SELECT * FROM AUTOR WHERE AUT_ID = '" + _id + "'");
        }
        public string consultageneral()
        {
            return ("SELECT   AUT_ID AS ID,   AUT_NOMBRE AS NOMBRE,   AUT_APATERNO AS APATERNO,   AUT_AMATERNO AS AMATERNO FROM    AUTOR");
        }
        public string eliminar()
        {
            return ("delete from AUTOR WHERE AUT_ID = '" + _id + "'");
        }
    }
}
