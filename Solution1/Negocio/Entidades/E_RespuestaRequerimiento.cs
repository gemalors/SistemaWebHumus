using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;


namespace Negocio.Entidades
{
  public  class E_RespuestaRequerimiento
    {

        public int IDrespuestaRequerimiento { get; set; }
      
        public string DetalleResp { get; set; }
        public int?  ProgresoResp { get; set; }
        public int? Idrequerimiento { get; set; }
        public int? Idtipoproceso { get; set; }

        public int? Identificador { get; set; }
        public int? TipodatoIdentificador { get; set; }
        public string Detallerequerimiento { get; set; }
        public bool? Obligatorio { get; set; }
        public string Detalletipospro { get; set; }
        public string respuesta { get; set; }
        public bool? ResultadoProceso { get; set; }

        public int? IdrespSeleccion { get; set; }
        
    }
}
