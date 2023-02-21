using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Negocio.Entidades
{
   public class E_Notificaciones
    {

        public int IDnotificacion { get; set; }
        public string Notificacion { get; set; }
        public bool? Publicado { get; set; }
        public bool? Estado { get; set; }
        public bool? General { get; set; }
        public string UrlNotificacion { get; set; }
        public string Visible { get; set; }
        public int? Idusuario { get; set; }
        public int? Idtiponotificacion { get; set; }
        public bool? Eliminado { get; set; }
        public string Fechanotifi { get; set; }
        public string DetalleNotificacion { get; set; }
        public string Icono { get; set; }
        public int? TotalNotificacionesnuevas { get; set; }
        public int? TotalNotificacionesrecibidas { get; set; }
        public string DetalleTipoNotificacion { get; set; }

    }
}
