using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Negocio.Entidades
{
    public class E_TipodeDato
    {
        public int IDtipodato { get; set; }
        public int? IdentificadorTipoDato { get; set; }
        public string TipoHtml { get; set; }
        public string DescripcionTipoDato { get; set; }
        //public string Descripcion { get; set; }
        public bool? Eliminado { get; set; }
    }
}
