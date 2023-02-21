using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Negocio.Entidades
{
    public class E_ObservacionesProceso
    {
        public int IDObservacion { get; set; }
        public string Detalleobservacion { get; set; }
        public string Descripcionobservacion { get; set; }
        public bool? Eliminaobservacion { get; set; }
        public int? Idproceso { get; set; }
        public string Emisor { get; set; }
        public string Titulo { get; set; }
        public string Fechaobservacion { get; set; }
        public bool? VisibleAutor { get; set; }
        public string EstadoObservacion { get; set; }
        public int? Idemisor { get; set; }
        public bool? RespuestaProceso { get; set; }
        public bool? Obligatoria { get; set; }
        public int? Idrequerimiento { get; set; }

    }
}
