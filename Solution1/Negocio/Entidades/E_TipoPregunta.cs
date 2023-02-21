using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Negocio.Entidades
{
   public class E_TipoPregunta
    {
        public int IDtipopregunta { get; set; }
        public int? IdentificadorTipoPregunta { get; set; }
        public string tipopregunta { get; set; }
        public bool? Eliminado { get; set; }
        
    }
}
