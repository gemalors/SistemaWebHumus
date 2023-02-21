using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Negocio.Entidades
{
    public class E_OpcionesRespuesta
    {

        public int IDrespuestaopcion { get; set; }
        public int? OrdenOpcionRespuesta { get; set; }
        public int? IdentificadorTipoPregunta { get; set; }
        public string DescripcionOpcionRespuesta { get; set; }
        public bool? Eliminado { get; set; }
        public int? Idpreg { get; set; }
        public int IDpregunta { get; set; }
      
        public int? Idcuestionario { get; set; }
        public int IDtipopregunta { get; set; }
       
        public int IDopcionPreguntaSeleccion { get; set; }

        



    }
}
