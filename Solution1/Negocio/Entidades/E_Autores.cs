using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Negocio.Entidades
{
   public class E_Autores
    {

        public int IDautor { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string inicialnombre { get; set; }
        public string ApellidoPrimero { get; set; }
        public string ApellidoSegundo { get; set; }
        public string Cedula { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public bool? Eliminautor { get; set; }
    }
}
