using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Negocio.Entidades
{
   public class E_Carreras
    {

        public int IDcarrera { get; set; }
        public string Carrera { get; set; }
        public bool? Eliminado { get; set; }



        public int? TotalLibros { get; set; }
        public int? TotalCarreras { get; set; }
        public int? TotalLibrosProceso { get; set; }
        public int? TotalLibrosPublicados { get; set; }
        //n libros publicados en ese año
        public int? yanterior { get; set; }
        public int? yactual { get; set; }
        //n libros x carrera pubicados ese año
        public int? yLibrosxCarreraANT { get; set; }
        public int? yLibrosxCarreraACT { get; set; }
        public int? yACT { get; set; }
        public int? yANT { get; set; }
     

   



    }
}
