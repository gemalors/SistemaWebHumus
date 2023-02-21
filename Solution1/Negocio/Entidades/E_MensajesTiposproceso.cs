using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;


namespace Negocio.Entidades
{
  public class E_MensajesTiposproceso
    {

        public int IDMensajeTiposproceso { get; set; }
        public string DescripcionMensaje { get; set; }
        public string VisibleMensaje { get; set; }
        public bool? EliminadoMensaje { get; set; }
        public int? Idestados { get; set; }
        public int? Idtipoproceso { get; set; }
        public int? IdentificadorEstados { get; set; }
        public string DetalleEstados { get; set; }
        public string Detalletipospro { get; set; }




    }
}
