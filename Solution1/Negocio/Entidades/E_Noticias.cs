using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;


namespace Negocio.Entidades
{
    public class E_Noticias
    {
        public int IDNoticia { get; set; }
        public string DetalleNoticia { get; set; }
     
        public string DescripcionNoticia { get; set; }
        public bool? EliminaNoticia { get; set; }
        public bool? Estado { get; set; }
        public string ImagenNoticia { get; set; }
        public string Urlacceso { get; set; }
        public string Titulonoticia { get; set; }
        public string Fechapublica { get; set; }
        public DateTime? Fechapublicacion { get; set; }

        public int? totalfilasNoti { get; set; }
        public int? totalpaginasNoti { get; set; }

        

    }
}
