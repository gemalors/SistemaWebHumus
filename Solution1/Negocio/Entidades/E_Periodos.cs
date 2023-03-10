using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;


namespace Negocio.Entidades
{
   public class E_Periodos
    {


        public int IDperiodo { get; set; }
      
        public string Detalleperiodo { get; set; }
        public int? Ordenperiodo { get; set; }
        public DateTime? Fechainicio { get; set; }
        public DateTime? Fechafin { get; set; }


    }
}
