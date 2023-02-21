using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Negocio.Entidades
{
    public class E_InformeRespuestas
    {
        public int IDinformeRespuestas { get; set; }
        public string FechaRegistro { get; set; }
        public int? Idusuario { get; set; }
        public string FechaFinalizado { get; set; }
        public bool? Finalizado { get; set; }
        public bool? Eliminado { get; set; }

    }
}
