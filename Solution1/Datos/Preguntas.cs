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
    
    public partial class Preguntas
    {
        public Preguntas()
        {
            this.OpcionPreguntaSeleccion = new HashSet<OpcionPreguntaSeleccion>();
            this.OpcionRespuesta = new HashSet<OpcionRespuesta>();
            this.PreguntaAbierta = new HashSet<PreguntaAbierta>();
            this.PreguntaObservaciones = new HashSet<PreguntaObservaciones>();
            this.Respuestas = new HashSet<Respuestas>();
        }
    
        public int IDpregunta { get; set; }
        public Nullable<int> Idtipopregunta { get; set; }
        public string Descripcion { get; set; }
        public Nullable<bool> Obligatorio { get; set; }
        public Nullable<int> Orden { get; set; }
        public Nullable<bool> Eliminado { get; set; }
        public string LeyendaSuperior { get; set; }
        public string LeyendaLateral { get; set; }
        public Nullable<int> Idcuestionario { get; set; }
        public string TiposOpciones { get; set; }
        public Nullable<int> IdpregAnterior { get; set; }
    
        public virtual Cuestionarios Cuestionarios { get; set; }
        public virtual ICollection<OpcionPreguntaSeleccion> OpcionPreguntaSeleccion { get; set; }
        public virtual ICollection<OpcionRespuesta> OpcionRespuesta { get; set; }
        public virtual ICollection<PreguntaAbierta> PreguntaAbierta { get; set; }
        public virtual ICollection<PreguntaObservaciones> PreguntaObservaciones { get; set; }
        public virtual TipoPregunta TipoPregunta { get; set; }
        public virtual ICollection<Respuestas> Respuestas { get; set; }
    }
}
