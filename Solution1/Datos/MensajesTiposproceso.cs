//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Datos
{
    using System;
    using System.Collections.Generic;
    
    public partial class MensajesTiposproceso
    {
        public int IDMensajeTiposproceso { get; set; }
        public string DescripcionMensaje { get; set; }
        public string VisibleMensaje { get; set; }
        public Nullable<bool> EliminadoMensaje { get; set; }
        public Nullable<int> Idestados { get; set; }
    
        public virtual EstadosTiposproceso EstadosTiposproceso { get; set; }
    }
}