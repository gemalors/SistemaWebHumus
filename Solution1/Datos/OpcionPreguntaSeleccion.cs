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
    
    public partial class OpcionPreguntaSeleccion
    {
        public int IDopcionPreguntaSeleccion { get; set; }
        public Nullable<int> Idpregunta { get; set; }
        public string Descripcion { get; set; }
        public Nullable<bool> Eliminado { get; set; }
        public Nullable<int> Identificador { get; set; }
    
        public virtual Preguntas Preguntas { get; set; }
    }
}