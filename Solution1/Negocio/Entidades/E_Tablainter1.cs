using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Negocio.Entidades
{
    public class E_Tablainter1
    {
        public int IDtablainter { get; set; }
        public int? Idautor { get; set; }
        public int? Idlibro { get; set; }
        public bool? EliminadoT1 { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string inicialnombre { get; set; }

        public string ApellidoPrimero { get; set; }
        public string ApellidoSegundo { get; set; }
        public string Cedula { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Filial { get; set; }
        public string Titulo { get; set; }


    }
}
