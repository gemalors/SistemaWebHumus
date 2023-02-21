using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Negocio.Entidades
{
   public  class E_RequerimientosProcesos
    {

        public int IDrequerimiento { get; set; }
        public string Detallerequerimiento { get; set; }
        public string Descripcionrequerimiento { get; set; }
        public int? Idtipoproceso { get; set; }
        public bool? Eliminarequerimiento { get; set; }
        public bool? Visibleautor { get; set; }
        public bool? Visibleevaluador { get; set; }
        public bool? Visibleadmin { get; set; }
        public string Detalletipospro { get; set; }
        public string Emisor { get; set; }
        public string respuesta { get; set; }
        public int? Progreso { get; set; }
        public int? Numeroprocesos { get; set; }
        public string Duracionproceso { get; set; }
        public string Tipodato { get; set; }
        public int? IDtiposprocesos { get; set; }
        public int? IDtipodato { get; set; }
        public int? Identificador { get; set; }

        public string Descripciontipoproceso { get; set; }

        public bool? Obligatorio { get; set; }
        public int? Obligatorios { get; set; }
        public bool? ResultadoProceso { get; set; }


    }
}
