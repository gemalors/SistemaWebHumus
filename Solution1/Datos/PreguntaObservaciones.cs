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
    
    public partial class PreguntaObservaciones
    {
        public int IDpreguntaObservaciones { get; set; }
        public Nullable<int> Idpregunta { get; set; }
        public string Detallepregobservacion { get; set; }
        public Nullable<int> Ordenobservacion { get; set; }
        public Nullable<bool> Eliminado { get; set; }
        public string Leyendainferior { get; set; }
        public Nullable<int> Respuestas { get; set; }
    
        public virtual Preguntas Preguntas { get; set; }
    }
}
