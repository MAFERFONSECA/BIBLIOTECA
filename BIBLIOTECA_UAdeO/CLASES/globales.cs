using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIBLIOTECA_UAdeO.CLASES
{
    internal class globales
    {
     
        static public string miconexion = "Data Source=MSIMAFER;Initial Catalog=BIBLIOTECA_UADEO;Integrated Security=True";
        

        public globales()
        {

        }


        //NO ELIMINAR ESTA PORQUE SE USA PARA EL FORMULARIO DE ASISTENCIA
        public string ConexionDB()
        {
            return miconexion;
        }
    }
}
