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
    
    public partial class ObservacionesProceso
    {
        public int IDObservacion { get; set; }
        public string Detalleobservacion { get; set; }
        public string Descripcionobservacion { get; set; }
        public Nullable<bool> Eliminaobservacion { get; set; }
        public Nullable<int> Idproceso { get; set; }
        public string Emisor { get; set; }
        public string Titulo { get; set; }
        public string Fechaobservacion { get; set; }
        public Nullable<bool> VisibleAutor { get; set; }
        public string EstadoObservacion { get; set; }
        public Nullable<int> Idemisor { get; set; }
        public Nullable<bool> RespuestaProceso { get; set; }
        public Nullable<int> Idrequerimiento { get; set; }
        public Nullable<bool> Obligatoria { get; set; }
    
        public virtual Procesos Procesos { get; set; }
    }
}
