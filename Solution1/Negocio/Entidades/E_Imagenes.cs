using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Negocio.Entidades
{
  public class E_Imagenes
    {


        public int IDimagen { get; set; }
        public string Imagendetalle { get; set; }
        public bool? Eliminado { get; set; }
        public int? Idnoticia { get; set; }
    }
}
