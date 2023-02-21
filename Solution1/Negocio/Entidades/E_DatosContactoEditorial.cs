using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;


namespace Negocio.Entidades
{
   public class E_DatosContactoEditorial
    {

        public int IDContactoEditorial { get; set; }
        public string Email { get; set; }
        public string Horario { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
    }
}
