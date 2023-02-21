using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;


namespace Negocio.Entidades
{
    public class E_Consultas
    {
        public int IDConsulta { get; set; }
        public string Carrera { get; set; }
        public int IDcarrera { get; set; }
        public string DetalleConsulta { get; set; }
        public int? Identificador { get; set; }
        public int? Idparametro { get; set; }
        public string Grafico { get; set; }
       
        public bool? Parametros { get; set; }
        public int? TotalLibros { get; set; }
        public int? TotalLibrosPublicadosxp { get; set; }
        public int? TotalLibrosPublicados { get; set; }
        public int? TotalLibrosProceso { get; set; }
        public int? LibrosxCarrera { get; set; }
        public int? TotalLibrosPublicadosxa { get; set; }
        public int? Totalvistaslibros { get; set; }
        public int? Totalvistasxcarrera { get; set; }
        public int? periodoanterior { get; set; }
        public int? periodoactual { get; set; }
        public int? LibrosxCarreraANT { get; set; }
        public int? LibrosxCarreraACT { get; set; }
        public string detalleperiodo { get; set; }
        public int? detalleyear { get; set; }
        public int? vistasxyear { get; set; }
        public int? Totalvistaslibrosxa { get; set; }
        public int? year { get; set; }
        public string periodoACT { get; set; }
        public string periodoANT { get; set; }
        public int? TotalvistaslibrosANT { get; set; }
        public int? TotalvistaslibrosACT { get; set; }
        public int? vistasxyearANT { get; set; }
        public int? vistasxyearACT { get; set; }
        public int? yearACT { get; set; }
        public int? yearANT { get; set; }
        public int? vistasxcarreraANT { get; set; }
        public int? vistasxcarreraACT { get; set; }
        public string iniciosemanaACT { get; set; }
        public string finsemanaACT { get; set; }
        public string semanaANTi { get; set; }
        public string semanaANTf { get; set; }
        public string Titulo { get; set; }
        public string Fechapublica { get; set; }
       
        public string Autores { get; set; }
        public int? nvistas { get; set; }
        public int? TotalLibrosxyears { get; set; }

        



    }
}
