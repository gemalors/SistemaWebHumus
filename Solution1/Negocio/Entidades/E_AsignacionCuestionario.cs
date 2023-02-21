using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Negocio.Entidades
{
   public class E_AsignacionCuestionario
    {
        public int IDAsignacionCuestionario { get; set; }
        public string Fechafinalizado { get; set; }
        public string FechaAsignado { get; set; }
        public string FechaAsignacion { get; set; }
        public string Fechacreado { get; set; }
        public int? Idcuestionario { get; set; }
        public int? Idtipousuario { get; set; }
        public bool? Eliminado { get; set; }
        public bool? Estado { get; set; }
        public bool? Finalizado { get; set; }
        public int? Idpersona { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPrimero { get; set; }
        public string ApellidoSegundo { get; set; }
        public string Email { get; set; }
        public string Detalleusuario { get; set; }
        public string TipoCuestionario { get; set; }
        public string UsuarioAsignado { get; set; }
        public string Descripcion { get; set; }
        public int? NumEvaluador { get; set; }
        public bool? finalasignacion { get; set; }
        

    }
}
