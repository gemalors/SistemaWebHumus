using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Negocio.Entidades
{
  public  class E_SlidersEditorial
    {
        public int IDSlider { get; set; }
        
        public string ImagenSlider { get; set; }
        public string Urlslider { get; set; }
        public bool? EliminadoSlider { get; set; }
       
    }
}
