using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
//using Microsoft.OData.Edm;

namespace Negocio.Entidades
{
   public class E_Cuestionarios
    {
        public int IDcuestionario { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool? Eliminado { get; set; }
        public string UsuarioAsignado { get; set; }
        public string FechaAsignado { get; set; }
        public string Fechacreado { get; set; }
        
        public bool? Estado { get; set; }
        public int IDlibro { get; set; }
        public string Titulo { get; set; }

        public int IDpregunta { get; set; }
        public int? Idtipopregunta { get; set; }
        public int? NumeroCuestionario { get; set; }
        public int? Idtipousuario { get; set; }
        
        public int? Idpreg { get; set; }
        public string Descripcion_pregunta { get; set; }
        public bool? Obligatorio { get; set; }
        public int? Orden { get; set; }
        public string LeyendaSuperior { get; set; }
        public string LeyendaLateral { get; set; }

        
        public int IDpreguntaAbierta { get; set; }
      
        public int? Idtipodato { get; set; }

        public int IDopcionPreguntaSeleccion { get; set; }
        public string Descrip_opcion_preg_seleccion { get; set; }
        public bool? Opcionesrespuesta { get; set; }

        public int IDopcionRespuesta { get; set; }
        public int? Opc_resp_orden { get; set; }
        public string Descrip_Opc_Resp { get; set; }

        public int TipodeDato_Id { get; set; }
        public int? Identificador { get; set; }
        public string TipoHtml { get; set; }
        public string Descrip_TipodeDato { get; set; }
        public bool? Finalizado { get; set; }
        public int Idtipopreg { get; set; }
        public int? Tipopreg_identificador { get; set; }
        public string Descrip_Tipopreg { get; set; }

        public string TipoCuestionario { get; set; }
       

    }
}
