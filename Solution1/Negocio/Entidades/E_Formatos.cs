using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Negocio.Entidades
{
    public class E_Formatos
    {
        public int IDformato { get; set; }
        public string Archivo { get; set; }
        public string Detallearchivo { get; set; }
        public int? Idtipoformato { get; set; }
        public bool? Eliminaform { get; set; }
        public string Detalletipoform { get; set; }
        public string DescripcionFormato { get; set; }
        public int? totalfilasFormato { get; set; }
        public int? totalpaginasFormato { get; set; }

      
    }
}
