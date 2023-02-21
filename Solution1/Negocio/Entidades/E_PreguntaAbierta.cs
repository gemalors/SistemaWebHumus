using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Negocio.Entidades
{
    public class E_PreguntaAbierta
    {
        public int IDpreguntaAbierta { get; set; }
        public int? Idpregunta { get; set; }
        public int? IdentificadorOpcionPregunta { get; set; }
        
        public bool? Eliminado { get; set; }
        public int? Idtipodato { get; set; }
        public int? IDtipodato { get; set; }
        public bool? EspecificarRango { get; set; }
        public string ValorMax { get; set; }
        public string ValorMin { get; set; }
        public int? Idcuestionario { get; set; }
        public int? IdentificadorTipoDato { get; set; }
        public string TipoHtml { get; set; }
        public string Descripcion { get; set; }
        public string DescripcionTipoDato { get; set; }
        

    }
}
