using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Negocio.Entidades
{
    public class E_EstadosTiposproceso
    {
        public int IDEstadosTiposproceso { get; set; }
        public int IdentificadorEstados { get; set; }
        public string DetalleEstados { get; set; }
        public bool? EliminadoEstados { get; set; }
        public int? Idtipoproceso { get; set; }
        public bool? VRequerimientosAutor { get; set; }
        public bool? VEnviarProcesoRevision { get; set; }
        public bool? VEnviarRevisionReversado { get; set; }
        public bool? VRequerimientosAdmin { get; set; }
        public bool? VEnviarResultados { get; set; }
        public bool? VIniciarSiguienteProceso { get; set; }
        public bool? VAsignarEvaluadores { get; set; }
        public bool? VEnviarEvaluacionPares { get; set; }
        public bool? VPublicacionLibro { get; set; }

    }
}
