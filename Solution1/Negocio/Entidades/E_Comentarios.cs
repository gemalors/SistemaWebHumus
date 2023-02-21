using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Negocio.Entidades
{
   public class E_Comentarios
    {

        public int IDcomentario { get; set; }
        public string Comentario { get; set; }
        public bool? Eliminacoment { get; set; }
        public string Fecha { get; set; }
        public int? Idproceso { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string Foto { get; set; }
        public string ApellidoPrimero { get; set; }
        public string ApellidoSegundo { get; set; }
        public int? Idusuario { get; set; }
        public int? Idlibro { get; set; }

    }
}
