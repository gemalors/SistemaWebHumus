using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Negocio.Entidades
{
   public class E_Preguntas
    {
        public int IDpregunta { get; set; }
        public int? Idtipopregunta { get; set; }
        public int? IdentificadorTipoPregunta { get; set; }
        public string Descripcion { get; set; }
        public bool? Obligatorio { get; set; }
        public int? Orden { get; set; }
        public bool? Eliminado { get; set; }
        public string LeyendaSuperior { get; set; }
        public string LeyendaLateral { get; set; }
        public int? Idcuestionario { get; set; }
        public int? IdpregAnterior { get; set; }
        public string tipopregunta { get; set; }
        public string Htmltipo { get; set; }
        public int IDpreguntaAbierta { get; set; }
        public int IDtipodato { get; set; }
        public bool? EspecificarRango { get; set; }
        public string TiposOpciones { get; set; }
        public string ValorMax { get; set; }
        public string ValorMin { get; set; }
        public int? identificadorTipodato { get; set; }
        public string TipoHtml { get; set; }
        public string descripTipodato { get; set; }
        public int IDopcionPreguntaSeleccion { get; set; }
        public string descripOpcpreg { get; set; }
        
       
        public int IDopcionRespuesta { get; set; }
        public int? ordenOpcsresp { get; set; }
        public string descripOpcsresp { get; set; }

 

    }
}
