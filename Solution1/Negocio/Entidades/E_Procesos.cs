using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Negocio.Entidades
{
    public class E_Procesos
    {
        public int IDproceso { get; set; }
        public string InicioFecha { get; set; }
        public string FinFecha { get; set; }
        public string Estado_Proceso{ get; set; }
        public string Titulo { get; set; }
        public bool? Eliminaproceso { get; set; }
        public int? Idlibro { get; set; }
        public int? ProgresoL { get; set; }
        public string Duracionproceso { get; set; }
        public string Detalledocumento { get; set; }
        public int? Tipo_Proceso { get; set; }
      
        public int? Numeroprocesos { get; set; }
        public string ActualizadoFecha { get; set; }
        public int? Idtipoproceso { get; set; }
        public string Detalletipospro { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string ApellidoPrimero { get; set; }
        public string ApellidoSegundo { get; set; }
        public int? Idusuario { get; set; }
        public bool? Estado { get; set; }
        public int? Progreso { get; set; }
        public int? Opresultado { get; set; }
        public int? Idprocanterior { get; set; }
        public int? Identificador { get; set; }
        public string Descripciontipoproceso { get; set; }
        public int? TipodatoIdentificador { get; set; }
        public bool? Obligatorio { get; set; }
        public string DetalleEstados { get; set; }
        public int? IdentificaEstado { get; set; }
        public int? Idestado { get; set; }
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
