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
    
    public partial class Tablainter1
    {
        public int IDtablainter { get; set; }
        public Nullable<int> Idautor { get; set; }
        public Nullable<int> Idlibro { get; set; }
        public Nullable<bool> EliminadoT1 { get; set; }
    
        public virtual Autores Autores { get; set; }
        public virtual Libros Libros { get; set; }
    }
}
