using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Entidades
{
    public class E_Tablainter2
    {

        public int IDtablainter2 { get; set; }
        public int? Idevaluador { get; set; }
        public bool? EliminadoT2 { get; set; }
        public int? Idproceso { get; set; }
        public int? NumEvaluador { get; set; }
        public int? Idusuario { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string ApellidoPrimero { get; set; }
        public string ApellidoSegundo { get; set; }
        public string Cedula { get; set; }
        public string Email { get; set; }

        public string Estado { get; set; }
        public int? Idlibro { get; set; }
        public int? IdasignacionEvaluacion { get; set; }
    }
}
