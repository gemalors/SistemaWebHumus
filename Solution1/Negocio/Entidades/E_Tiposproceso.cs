using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Negocio.Entidades
{
   public class E_Tiposproceso
    {
        public int IDtiposprocesos { get; set; }
        public int? Numeroprocesos { get; set; }
        public string Detalletipospro { get; set; }
        public bool? Eliminatipoproceso { get; set; }
        public int? Progreso { get; set; }
      
        public bool? Eliminatipospro { get; set; }
        public string Duracionproceso { get; set; }
        public int? Identificador { get; set; }
        public string Descripciontipoproceso { get; set; }


    }
}
