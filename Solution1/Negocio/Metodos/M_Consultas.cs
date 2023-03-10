using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Negocio.Entidades;


namespace Negocio.Metodos
{
   public class M_Consultas
    {

        DBHumusEntities DB = new DBHumusEntities();





        //Listo - gráfico1
        //Función para ver número de libros publicados por carreras
        public List<E_Libros> ConsultarNumLibrosPublicadosxCarrera()
        {
            List<E_Libros> listalibro = new List<E_Libros>();

            foreach (var item in DB.ConsultarNumLibrosPublicadosxCarrera())
            {
                listalibro.Add(new E_Libros()
                {

                  
                    Carrera = item.Carrera,
                    TotalLibros=item.TotalLibros,
                    LibrosxCarrera=item.LibrosxCarrera,
                    TotalLibrosProceso=item.TotalLibrosProceso,
                    TotalLibrosPublicados=item.TotalLibrosPublicados,
                    TotalCarreras =item.TotalCarreras



                });
            }

            return listalibro;
        }





        //Función para ver número de libros publicados por categorías
        public List<E_Libros> ConsultarNumLibrosPublicadosxCategoria()
        {
            List<E_Libros> listalibro = new List<E_Libros>();

            foreach (var item in DB.ConsultarNumLibrosPublicadosxCategoria())
            {
                listalibro.Add(new E_Libros()
                {


                   Idcategoria=item.IDcategoria,
                   Detallecategoria=item.Detallecategoria,
                   LibrosxCategoria=item.LibrosxCategoria,
                    TotalCategorias=item.TotalCategorias,
                    TotalLibrosPublicados=item.TotalLibrosPublicados,




                });
            }

            return listalibro;
        }








        //Listo - gráfico2
        //Función para listar consultas de libros publicados por período académico
        public List<E_Consultas> ConsultarNumLibrosPublicadosxPeriodo(int idpe)
        {
            List<E_Consultas> lista = new List<E_Consultas>();

            foreach (var item in DB.ConsultarNumLibrosPublicadosxPeriodo(idpe))
            {
                lista.Add(new E_Consultas()
                {
                    IDcarrera = item.IDcarrera,
                    Carrera = item.Carrera,
                    TotalLibros = item.TotalLibros,
                    TotalLibrosPublicadosxp = item.TotalLibrosPublicadosxp,
                    TotalLibrosPublicados = item.TotalLibrosPublicados,
                    TotalLibrosProceso = item.TotalLibrosProceso,
                    LibrosxCarrera = item.LibrosxCarrera,
                    detalleperiodo = item.detalleperiodo



                });
            }

            return lista;
        }





        
        //Listo - gráfico3
        //Función para listar consultas de libros publicados por año
        public List<E_Consultas> ConsultarNumLibrosPublicadosxYear(int idy)
        {
            List<E_Consultas> lista = new List<E_Consultas>();

            foreach (var item in DB.ConsultarNumLibrosPublicadosxYear(idy))
            {
                lista.Add(new E_Consultas()
                {
                    IDcarrera = item.IDcarrera,
                    Carrera = item.Carrera,
                    TotalLibros = item.TotalLibros,
                    TotalLibrosPublicadosxa = item.TotalLibrosPublicadosxa,
                    TotalLibrosPublicados = item.TotalLibrosPublicados,
                    TotalLibrosProceso = item.TotalLibrosProceso,
                    LibrosxCarrera = item.LibrosxCarrera,
                    detalleyear = item.detalleyear,
                




                });
            }

            return lista;
        }







        //Listo - gráfico4
        //Función para listar num de vistas de libros en página web
        public List<E_Consultas> ConsultarNumVistasLibrosxCarrera()
        {
            List<E_Consultas> lista = new List<E_Consultas>();

            foreach (var item in DB.ConsultarNumVistasLibrosxCarrera())
            {
                lista.Add(new E_Consultas()
                {
                    IDcarrera = item.IDcarrera,
                    Carrera = item.Carrera,
                    Totalvistaslibros = item.Totalvistaslibros,
                    Totalvistasxcarrera = item.Totalvistasxcarrera,
                    TotalLibros = item.TotalLibros,
                    TotalLibrosPublicados = item.TotalLibrosPublicados,





                });
            }

            return lista;
        }






        //Listo -  gráfico5
        //Función para consultar num de vistas de libros en página web por año
        public List<E_Consultas> ConsultarNumVistasLibrosxYear(int idy)
        {
            List<E_Consultas> lista = new List<E_Consultas>();

            foreach (var item in DB.ConsultarNumVistasLibrosxYear(idy))
            {
                lista.Add(new E_Consultas()
                {
                    IDcarrera = item.IDcarrera,
                    Carrera = item.Carrera,
                    Totalvistaslibros = item.Totalvistaslibros,
                    Totalvistasxcarrera = item.Totalvistasxcarrera,
                    TotalLibros = item.TotalLibros,
                    Totalvistaslibrosxa = item.Totalvistaslibrosxa,
                    TotalLibrosPublicados = item.TotalLibrosPublicados,
                    detalleyear = item.detalleyear




                });
            }

            return lista;
        }




        //Función para consultar comparativa de num de vistas de libros en página web año actual vs año anterior
        public List<E_Consultas> ConsultarComparativaNumVistasLibrosxYearAnterior()
        {
            List<E_Consultas> lista = new List<E_Consultas>();

            foreach (var item in DB.ConsultarComparativaNumVistasLibrosxYearAnterior())
            {
                lista.Add(new E_Consultas()
                {
                    IDcarrera = item.IDcarrera,
                    Carrera = item.Carrera,
                    Totalvistaslibros = item.Totalvistaslibros,
                    TotalLibros = item.TotalLibros,
                    TotalvistaslibrosANT=item.TotalvistaslibrosANT,
                    TotalvistaslibrosACT=item.TotalvistaslibrosACT,
                    vistasxyearANT=item.vistasxyearANT,
                    vistasxyearACT=item.vistasxyearACT,
                    yearACT=item.yearACT,
                    yearANT=item.yearANT





                });
            }

            return lista;
        }









        //Función para listar consultas de libros publicados por periodo 
        public List<E_Consultas> VerNumLibrosPublicadosxPeriodo(int idc)
        {
            List<E_Consultas> lista = new List<E_Consultas>();

            foreach (var item in DB.VerNumLibrosPublicadosxPeriodo(idc))
            {
                lista.Add(new E_Consultas()
                {
                    IDcarrera = item.IDcarrera,
                    Carrera = item.Carrera,
                    TotalLibros = item.TotalLibros,
                    detalleperiodo = item.detalleperiodo,


                    TotalLibrosPublicados = item.TotalLibrosPublicados,
                    TotalLibrosProceso = item.TotalLibrosProceso,
                    LibrosxCarrera = item.LibrosxCarrera,
                    idperiodo = item.idperiodo,
                    TotalLibrosPublicadosxp = item.TotalLibrosPublicadosxp,





                });
            }

            return lista;
        }







        //Función para listar consultas de libros publicados por periodo 
        public List<E_Consultas> VerNumVistasLibrosxYears(int idc)
        {
            List<E_Consultas> lista = new List<E_Consultas>();

            foreach (var item in DB.VerNumVistasLibrosxYears(idc))
            {
                lista.Add(new E_Consultas()
                {
                    IDcarrera = item.IDcarrera,
                    Carrera = item.Carrera,
                    Totalvistaslibros = item.Totalvistaslibros,
                    Totalvistaslibrosxa=item.Totalvistaslibrosxa,
                    Totalvistasxcarrera = item.Totalvistasxcarrera,
                    detalleyear = item.detalleyear,
                    idyear=item.idyear,


                });
            }

            return lista;
        }





        //Listo
        //Función para ver comparativa de número de libros publicados por carreras del año anterior y año actual
        public List<E_Carreras> ConsultarComparativaYearanterior()
        {
            List<E_Carreras> lista = new List<E_Carreras>();

            foreach (var item in DB.ConsultarComparativaYearanterior())
            {
                lista.Add(new E_Carreras()
                {


                    Carrera = item.Carrera,
                    TotalLibros = item.TotalLibros,
                    //n libros publicados en ese año
                    yanterior = item.yanterior,
                    yactual = item.yactual,
                    yACT=item.yACT,
                    yANT=item.yANT,
                    //n libros x carrera pubicados ese año
                    yLibrosxCarreraANT =item.yLibrosxCarreraANT,
                    yLibrosxCarreraACT=item.yLibrosxCarreraACT,

                    TotalLibrosPublicados = item.TotalLibrosPublicados,
                    TotalCarreras = item.TotalCarreras



                });
            }

            return lista;
        }








        //Función para listar consultas registradas para visualizar estadísticas
        public List<E_Consultas> ListarConsultas()
        {
            List<E_Consultas> lista = new List<E_Consultas>();

            foreach (var item in DB.ListarConsultas())
            {
                lista.Add(new E_Consultas()
                {

                    IDConsulta=item.IDConsulta,
                    DetalleConsulta=item.DetalleConsulta,
                    Identificador=item.Identificador,
                    Grafico=item.Grafico,
                   
                    Parametros=item.Parametros,
                    Idparametro=item.Idparametro



                });
            }

            return lista;
        }







        //Función para editar consulta 
        public int EditarConsulta(int idconsulta,int idparametro)
        {
            int r = 3;
            try
            {
                r = Convert.ToInt32(DB.EditarConsulta(idconsulta,idparametro).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }






        //Función para listar consultas registradas para visualizar estadísticas x ID
        public List<E_Consultas> VerDetalleConsulta(int ide)
        {
            List<E_Consultas> lista = new List<E_Consultas>();

            foreach (var item in DB.DetalleConsultaxId(ide))
            {
                lista.Add(new E_Consultas()
                {

                    IDConsulta = item.IDConsulta,
                    DetalleConsulta = item.DetalleConsulta,
                    Identificador = item.Identificador,
                    Grafico = item.Grafico,
                 
                    Parametros = item.Parametros,
                    Idparametro=item.Idparametro



                });
            }

            return lista;
        }




    






        //Función para listar num de vistas de libros en página web por años
        public List<E_Consultas> VistasLibrosxYears()
        {
            List<E_Consultas> lista = new List<E_Consultas>();

            foreach (var item in DB.VistasLibrosxYears())
            {
                lista.Add(new E_Consultas()
                {
                    year=item.year,
                    Totalvistaslibros = item.Totalvistaslibros,
                    vistasxyear=item.vistasxyear,
          
                    TotalLibros = item.TotalLibros,
                    TotalLibrosPublicados = item.TotalLibrosPublicados,





                });;
            }

            return lista;
        }






        //Función para listar detalles de los 10 libros más vistos en página web
        public List<E_Consultas> VerLibrosVistas()
        {
            List<E_Consultas> lista = new List<E_Consultas>();

            foreach (var item in DB.VerLibrosVistas())
            {
                lista.Add(new E_Consultas()
                {
                    Titulo=item.Titulo,
                    Fechapublica=item.Fechapublica,
                    Carrera=item.Carrera,
                    Autores=item.Autores,
                    nvistas=item.nvistas





                }); ;
            }

            return lista;
        }







        //Función para listar número de libros que se han publicado por años
        public List<E_Consultas> VerLibrospublicadosxYears()
        {
            List<E_Consultas> lista = new List<E_Consultas>();

            foreach (var item in DB.VerLibrospublicadosxYears())
            {
                lista.Add(new E_Consultas()
                {
                   year=item.year,
                    TotalLibrosxyears=item.TotalLibrosxyears



                }); ;
            }

            return lista;
        }







        //Listo
        //Función para listar consultas de libros publicados por año 
        public List<E_Consultas> VerNumLibrosPublicadosxYears(int idc)
        {
            List<E_Consultas> lista = new List<E_Consultas>();

            foreach (var item in DB.VerNumLibrosPublicadosxYears(idc))
            {
                lista.Add(new E_Consultas()
                {
                    IDcarrera = item.IDcarrera,
                    Carrera = item.Carrera,
                    TotalLibros = item.TotalLibros,
                    detalleyear=item.detalleyear,
                    TotalLibrosPublicadosxa=item.TotalLibrosPublicadosxa,
                    TotalLibrosPublicados = item.TotalLibrosPublicados,
                    TotalLibrosProceso = item.TotalLibrosProceso,
                    LibrosxCarrera = item.LibrosxCarrera,
                    idyear=item.idyear,





                });
            }

            return lista;
        }







        //Listo
        //Función para listar consulta comparativa entre periodo actual y anterior de libros publicados
        public List<E_Consultas> ConsultarComparativaPeriodanterior()
        {
            List<E_Consultas> lista = new List<E_Consultas>();

            foreach (var item in DB.ConsultarComparativaPeriodanterior())
            {
                lista.Add(new E_Consultas()
                {
                    IDcarrera = item.IDcarrera,
                    Carrera = item.Carrera,
                    TotalLibros = item.TotalLibros,
                    periodoanterior=item.periodoanterior,
                    periodoactual=item.periodoactual,
                    LibrosxCarreraANT=item.LibrosxCarreraANT,
                    LibrosxCarreraACT=item.LibrosxCarreraACT,
                    periodoACT=item.periodoACT,
                    periodoANT=item.periodoANT,
                    TotalLibrosPublicados = item.TotalLibrosPublicados,
               





                });
            }

            return lista;
        }







    }
}
