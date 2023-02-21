using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Negocio.Entidades
{
    public class E_VistasLibros
    {

        public int IDvistas { get; set; }
        public string IPvistas { get; set; }
        public DateTime? Fechavistas { get; set; }
        public int? Idlibro { get; set; }
    }
}
