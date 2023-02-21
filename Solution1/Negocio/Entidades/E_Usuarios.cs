using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Negocio.Entidades
{
    public class E_Usuarios
    {
        public int IDusuario { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string ApellidoPrimero { get; set; }
        public string ApellidoSegundo { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Username { get; set; }
        public string Cedula { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Foto { get; set; }
        public int? Idtipousuario { get; set; }
        public bool? Eliminauser { get; set; }
        public bool? Estado { get; set; }
        public string Tipo_usuario { get; set; }

        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Filial { get; set; }
        public string Token_recovery { get; set; }
        public int? Librosfinalizados { get; set; }
        public int? Librosproceso { get; set; }
      


  

    }
}


