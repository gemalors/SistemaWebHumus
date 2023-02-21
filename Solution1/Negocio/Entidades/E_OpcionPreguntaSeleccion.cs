using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Negocio.Entidades
{
    public class E_OpcionPreguntaSeleccion
    {
        public int IDopcionPreguntaSeleccion { get; set; }
        public int? Idpregunta { get; set; }
      
        public string descripOpcpreg { get; set; }
        public bool? Eliminado { get; set; }
        public int? Idcuestionario { get; set; }
        public int? IdentificadorOpcionPregunta { get; set; }
        
       

    }
}
