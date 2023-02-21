using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;


namespace Negocio.Entidades
{
  public  class E_InformacionEditorial
    {
        public int IDinformacion { get; set; }
        public string TituloInformacion { get; set; }
        public string EnunciadoInformacion { get; set; }
        public string ImagenInfo { get; set; }
        public string UrlInfo { get; set; }
        public bool? EliminadoInfo { get; set; }
        public int? Pagina { get; set; }

    }
}
