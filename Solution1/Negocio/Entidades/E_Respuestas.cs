using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Negocio.Entidades
{
    public class E_Respuestas
    {
        public int IDrespuesta { get; set; }
        public int? IdinformeRespuestas { get; set; }
        public int? Idpregunta { get; set; }
       
        public int? Idcuestionario { get; set; }
        public int? IdrespuestaLogica { get; set; }
        public string DescripcionRespuestaAbierta { get; set; }
        public bool? Eliminado { get; set; }
        public int? Respuestas { get; set; }
        public int? Idusuario { get; set; }
        public string Fecha_registros { get; set; }
        public string DescripcionOpcionRespuesta { get; set; }
        public string descripOpcpreg { get; set; }
        public int? Idopcionpreg { get; set; }
        public int? Idresp { get; set; }
        public string titulorespabierta { get; set; }

        public int? Idobservacionpreg { get; set; }
        public int? Idasignacion { get; set; }


    }
}
