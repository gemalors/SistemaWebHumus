using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Datos;


namespace Negocio.Entidades
{
    public class E_Libros
    {
        public int IDlibro { get; set; }
        public int? Numcapitulos { get; set; }
        public string Titulo { get; set; }
        public string CodigoISBN { get; set; }
        public string Fechapublica { get; set; }
        public DateTime Fechapublicacion { get; set; }
        public string Editorial { get; set; }
        public string Libro { get; set; }
        public string Portada { get; set; }
        public int? Vistas { get; set; }
        public bool? Eliminalibros { get; set; }
        public int? Idcategoria { get; set; }
        public int? Idusuario { get; set; }
        public bool? Estado { get; set; }

        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string ApellidoPrimero { get; set; }
        public string ApellidoSegundo { get; set; }

        public string Detallecategoria { get; set; }
        public string Creado { get; set; }
        public string ProcesoL { get; set; }
        public int? ProgresoL { get; set; }
        public string Actualizado { get; set; }
        public string EstadoL { get; set; }
        public int? IdprocesoL { get; set; }
        public string UrlAcceso { get; set; }
        public string Autores { get; set; }
        public string Carrera { get; set; }
        public int TotalAutores { get; set; }
        public int? IDautor { get; set; }
        public int? Idcarrera { get; set; }
        public int? Idlibro { get; set; }

        public int? IdasignacionEvaluacion { get; set; }
        public int? TotalLibros { get; set; }
        public int? LibrosxCarrera { get; set; }
        public int? TotalCarreras { get; set; }

        public int? TotalLibrosProceso { get; set; }
        public int? TotalLibrosPublicados { get; set; }
        public int? TotalLibEPublicados { get; set; }
        public int? TotalLibIPublicados { get; set; }
        public int? LibrosxCarreraE { get; set; }
        public string EstadoAsignacion { get; set; }
        public int? LibrosxCategoria { get; set; }
        public int? TotalCategorias { get; set; }


 





    }
}
