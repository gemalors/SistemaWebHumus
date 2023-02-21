using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Negocio.Entidades
{
   public class E_Documentos
    {

        public int IDdocumento { get; set; }
        public string Documento { get; set; }
        public bool? Visiblevaluador { get; set; }
        public string Detalledocu { get; set; }
        public int? Idproceso { get; set; }
        public int? Idrequerimiento { get; set; }

        public bool? Visibleautor { get; set; }
        public bool? Visibleadmin { get; set; }
        public string EstadoDocu { get; set; }

        public string InicioFecha { get; set; }
        public string Fecha{ get; set; }
        public string ActualizadoFecha { get; set; }
        public string Estado { get; set; }
        public int? Idtipoproceso { get; set; }
        public string Detalletipospro { get; set; }
        public string Emisor { get; set; }
        public int? Progreso { get; set; }
        public int? Idlibro { get; set; }
        public int? Idemisor { get; set; }
        public string Titulo { get; set; }
        public int? totalfilas { get; set; }
        public int? totalpaginas { get; set; }

        public bool? DocObligatorio { get; set; }


    }
}
