using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Negocio.Entidades
{
    public class E_PreguntaObservaciones
    {

        public int IDpreguntaObservaciones { get; set; }
        public int? Idpregunta { get; set; }
        public int? Idcuestionario { get; set; }
        public string Detallepregobservacion { get; set; }
        public int? Ordenobservacion { get; set; }
        public bool? Eliminado { get; set; }
        public int? Respuestas { get; set; }
        public string Leyendainferior { get; set; }



    }
}
