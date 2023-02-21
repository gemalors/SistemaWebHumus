using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Microsoft.OData.Edm;
using Negocio.Entidades;

namespace Negocio.Metodos
{
    public class M_Libros
    {
        DBHumusEntities DB = new DBHumusEntities();





        //Función para crear libro
        public int CrearLibro(int Iduser, string TituloLib, string procesoL, int progresoL, string actualizadoL, string fechacreado, string estadoL,int aceptado)
        {
            
            int x = 3;
       

            try
            {
                
                x = Convert.ToInt32(DB.CrearLibro(Iduser,TituloLib,procesoL,progresoL,actualizadoL,fechacreado,estadoL,aceptado).FirstOrDefault());
            }
            catch (Exception)
            {
                x = 3;
            }
            return x;
        }







        //Función para archivar libro
        public int ArchivarLibro(int Idlibro)
        {

            int x = 0;


            try
            {

                x = Convert.ToInt32(DB.ArchivarLibro(Idlibro).FirstOrDefault());
            }
            catch (Exception)
            {
                x = 3;
            }
            return x;
        }






        //Función para eliminar libro
        public int EliminarLibro(int Idlibro, int iduser)
        {

            int x = 3;


            try
            {

                x = Convert.ToInt32(DB.EliminarLibro(Idlibro,iduser).FirstOrDefault());
            }
            catch (Exception)
            {
                x = 3;
            }
            return x;
        }






        //Función para aceptar o denegar solicitud de aprobación de nuevo libro a publicar
        public int AceptaroDenegarLibro(int Idlibro, int aceptado,string fecha,int Iduser)
        {

            int x = 3;


            try
            {

                x = Convert.ToInt32(DB.AceptaroDenegarLibro(Idlibro,aceptado,fecha,Iduser).FirstOrDefault());
            }
            catch (Exception)
            {
                x = 3;
            }
            return x;
        }





        //Función para agregar libro externo
        public int AgregarLibroExterno(string Titulo, string CodigoISBN, string Editorial, int Idcategoria, string Fechapublica, int idcarrera, string Portada, string Libro, string urlacceso,DateTime fechap)
        {

            int x = 3;


            try
            {

                x = Convert.ToInt32(DB.AgregarLibroExterno(Titulo, CodigoISBN, Editorial, Idcategoria, Fechapublica, idcarrera,Portada,Libro,urlacceso,fechap).FirstOrDefault());
            }
            catch (Exception)
            {
                x = 3;
            }
            return x;
        }





        //Función para editar datos de libro externo
        public int EditarLibroExterno(int Idlibro,string titulo, string codigo, string editorial, int idcategoria, string Fechapublica, int idcarrera, string Portada, string Libro,string urlacceso,DateTime fechap)
        {

            int x = 3;


            try
            {

                x = Convert.ToInt32(DB.EditarLibroExterno(Idlibro, titulo, codigo, editorial, idcategoria, Fechapublica, idcarrera, Portada, Libro,urlacceso,fechap).FirstOrDefault());
            }
            catch (Exception)
            {
                x = 3;
            }
            return x;
        }






        //Función para ver libros x Idusuario
        public List<E_Libros> VerLibroxIdUsuario(int Iduser)
        {
            List<E_Libros> listalibro = new List<E_Libros>();
            
            foreach (var item in DB.ListarLibrosxIdUsuario(Iduser))
            {
                listalibro.Add(new E_Libros()
                {
        
                 IDlibro=item.IDlibro,
                 Titulo=item.Titulo,
                 Idusuario=item.Idusuario,
                    PrimerNombre = item.PrimerNombre,
                    SegundoNombre = item.SegundoNombre,
                    ApellidoPrimero = item.ApellidoPrimero,
                    ApellidoSegundo = item.ApellidoSegundo,
                    Creado = item.Creado,
                ProcesoL = item.ProcesoL,
                ProgresoL = item.ProgresoL,
                Actualizado = item.Actualizado,
                EstadoL = item.EstadoL,
                IdprocesoL = item.IdprocesoL




                });
            }

            return listalibro;
        }





        //Función para editar título de libro
        public int EditarTituloLibro(int IDlibro, string Titulo)
        {
            int r = 3;

            try
            {

                r = Convert.ToInt32(DB.EditarTituloLibro(IDlibro, Titulo).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }





        //Función para editar libro publicado
        public int EditarLibroP(int IDlibro, string procesoL,int progresoL, string actualizadoL, string estadoL, int IdprocesoL,bool estado)
        {
          
            int r = 3;

            try
            {

                r = Convert.ToInt32(DB.EditarLibro(IDlibro, procesoL, progresoL,actualizadoL,estadoL,IdprocesoL,estado).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }






        //Función para ver libros asignados para evaluador x Idusuario
        public List<E_Libros> VerLibroEvaluar(int Id)
        {
            List<E_Libros> listalibroe = new List<E_Libros>();

            foreach (var item in DB.VerLibrosEvaluar(Id))
            {
                listalibroe.Add(new E_Libros()
                {

                    IDlibro = item.IDlibro,
                    Titulo = item.Titulo,
                    PrimerNombre = item.PrimerNombre,
                    SegundoNombre = item.SegundoNombre,
                    ApellidoPrimero = item.ApellidoPrimero,
                    ApellidoSegundo = item.ApellidoSegundo,
                    Creado = item.Creado,
                    ProcesoL = item.ProcesoL,
                    ProgresoL = item.ProgresoL,
                    Actualizado = item.Actualizado,
                    EstadoL = item.EstadoL,
                    IdprocesoL = item.IdprocesoL,
                    IdasignacionEvaluacion=item.IdasignacionEvaluacion,
                    EstadoAsignacion = item.EstadoAsignacion,




                });;
            }

            return listalibroe;
        }







        //Función para ver todos los libros que no han finalizado los procesos editoriales
        public List<E_Libros> ObtenerLibros()
        {
            List<E_Libros> listalibros = new List<E_Libros>();
         
            foreach (var item in DB.VerLibrosEnProceso())
            {
                listalibros.Add(new E_Libros()
                {

                    IDlibro = item.IDlibro,
                    Titulo = item.Titulo,
                    Idusuario = item.Idusuario,
                    PrimerNombre = item.PrimerNombre,
                    SegundoNombre = item.SegundoNombre,
                    ApellidoPrimero = item.ApellidoPrimero,
                    ApellidoSegundo = item.ApellidoSegundo,
                    Creado =item.Creado,
                    ProcesoL=item.ProcesoL,
                    ProgresoL=item.ProgresoL,
                    Actualizado=item.Actualizado,
                    EstadoL=item.EstadoL,
                    IdprocesoL=item.IdprocesoL

                    


            });
            }

            return listalibros;
        }







        //Función para ver lista de libros que tienen solicitudes pendientes de aceptar para iniciar proceso de publicación
        public List<E_Libros> VerSolicitudesLibros()
        {
            List<E_Libros> listalibros = new List<E_Libros>();

            foreach (var item in DB.VerSolicitudesLibros())
            {
                listalibros.Add(new E_Libros()
                {

                    IDlibro = item.IDlibro,
                    Titulo = item.Titulo,
                    Idusuario = item.Idusuario,
                    PrimerNombre = item.PrimerNombre,
                    SegundoNombre = item.SegundoNombre,
                    ApellidoPrimero = item.ApellidoPrimero,
                    ApellidoSegundo = item.ApellidoSegundo,
                    Creado = item.Creado,
                    ProcesoL = item.ProcesoL,
                    ProgresoL = item.ProgresoL,
                    Actualizado = item.Actualizado,
                    EstadoL = item.EstadoL,
                    IdprocesoL = item.IdprocesoL




                });
            }

            return listalibros;
        }






        //Función para ver todos los libros archivados
        public List<E_Libros> ObtenerLibrosA()
        {
            List<E_Libros> listalibros = new List<E_Libros>();

            foreach (var item in DB.VerLibrosArchivados())
            {
                listalibros.Add(new E_Libros()
                {

                    IDlibro = item.IDlibro,
                    Titulo = item.Titulo,
                    Idusuario = item.Idusuario,
                    PrimerNombre = item.PrimerNombre,
                    SegundoNombre = item.SegundoNombre,
                    ApellidoPrimero = item.ApellidoPrimero,
                    ApellidoSegundo = item.ApellidoSegundo,
                    Creado = item.Creado,
                    ProcesoL = item.ProcesoL,
                    ProgresoL = item.ProgresoL,
                    Actualizado = item.Actualizado,
                    EstadoL = item.EstadoL,
                    IdprocesoL = item.IdprocesoL




                });
            }

            return listalibros;
        }






        //Función para ver todos los libros archivados de autor x Idusuario
        public List<E_Libros> ObtenerLibrosArchivadosAutor(int Iduser)
        {
            List<E_Libros> listalibros = new List<E_Libros>();

            foreach (var item in DB.VerLibrosArchivadosAutor(Iduser))
            {
                listalibros.Add(new E_Libros()
                {

                    IDlibro = item.IDlibro,
                    Titulo = item.Titulo,
                    Idusuario = item.Idusuario,
                    PrimerNombre = item.PrimerNombre,
                    SegundoNombre = item.SegundoNombre,
                    ApellidoPrimero = item.ApellidoPrimero,
                    ApellidoSegundo = item.ApellidoSegundo,
                    Creado = item.Creado,
                    ProcesoL = item.ProcesoL,
                    ProgresoL = item.ProgresoL,
                    Actualizado = item.Actualizado,
                    EstadoL = item.EstadoL,
                    IdprocesoL = item.IdprocesoL




                });
            }

            return listalibros;
        }






        //Función para obtener detalles de un libro x Id
        public List<E_Libros> VerDetalleLibro(int IDlibro)
        {
            List<E_Libros> listalibro = new List<E_Libros>();

            foreach (var item in DB.VerDetalleLibro(IDlibro))
            {
                listalibro.Add(new E_Libros()
                {

                   IDlibro=item.IDlibro,
                    Titulo = item.Titulo,
                    CodigoISBN=item.CodigoISBN,
                    Fechapublica=item.Fechapublica,
                    Editorial=item.Editorial,
                    Libro=item.Libro,
                    Portada=item.Portada,
                    Vistas=item.Vistas,
                    Idcategoria=item.Idcategoria,
                    Detallecategoria = item.Detallecategoria,
                    Idcarrera=item.Idcarrera,
                    Creado =item.Creado,
                    Carrera = item.Carrera,
                    UrlAcceso=item.UrlAcceso,
                    Idusuario = item.Idusuario,
                    PrimerNombre = item.PrimerNombre,
                    SegundoNombre = item.SegundoNombre,
                    ApellidoPrimero = item.ApellidoPrimero,
                    ApellidoSegundo = item.ApellidoSegundo,
                    EstadoL = item.EstadoL,
                    IdprocesoL = item.IdprocesoL,
                    ProcesoL=item.ProcesoL


                });
            }

            return listalibro;
        }





        //OK
        //Función para obtener detalles de la cita de un libro x Id
        public List<E_Libros> VerCitaLibro(int IDlibro)
        {
            List<E_Libros> listalibro = new List<E_Libros>();

            foreach (var item in DB.CrearCitaLibro(IDlibro))
            {
                listalibro.Add(new E_Libros()
                {


                    Titulo = item.Titulo,
                    Fechapublica = item.Fechapublica,
                    Editorial = item.Editorial,
                    UrlAcceso=item.UrlAcceso,
                    Autores=item.Autores,
                    TotalAutores=Convert.ToInt32(item.TotalAutores)



                });
            }

            return listalibro;
        }








        //Función para obtener detalles de un libro externo x Id
        public List<E_Libros> VerDetalleLibroExterno(int IDlibro)
        {
            List<E_Libros> listalibro = new List<E_Libros>();

            foreach (var item in DB.VerDetalleLibroExterno(IDlibro))
            {
                listalibro.Add(new E_Libros()
                {

                    IDlibro = item.IDlibro,
                    Titulo = item.Titulo,
                    CodigoISBN = item.CodigoISBN,
                    Fechapublica = item.Fechapublica,
                    Editorial = item.Editorial,
                    Libro = item.Libro,
                    Portada = item.Portada,
                    Vistas = item.Vistas,
                    Idcategoria = item.Idcategoria,
                    Detallecategoria = item.Detallecategoria,
                    UrlAcceso=item.UrlAcceso,
                    Creado = item.Creado,
                    Carrera = item.Carrera,
                    Idcarrera=item.Idcarrera,

                    EstadoL = item.EstadoL,
                    IdprocesoL = item.IdprocesoL


                });
            }

            return listalibro;
        }










        //Función para ver top 10 libros recientes
        public List<E_Libros> TopLibrosRecientes()
        {
            List<E_Libros> listalibros = new List<E_Libros>();

            foreach (var item in DB.VerLibrosTopRecientes())
            {
                listalibros.Add(new E_Libros()
                {

                    IDlibro = item.IDlibro,
                    Titulo = item.Titulo,
                    CodigoISBN=item.CodigoISBN,
                    Fechapublica=item.Fechapublica,
                     UrlAcceso=item.UrlAcceso,
                    Editorial=item.Editorial,
                    Libro=item.Libro,
                    Portada=item.Portada,
                    




                });
            }

            return listalibros;
        }









        //Función para ver todos los libros paginación
        public List<E_Libros> VerTodosLosLibros()
        {
            List<E_Libros> listalibros = new List<E_Libros>();

            foreach (var item in DB.VerTodosLosLibros())
            {
                listalibros.Add(new E_Libros()
                {

                    IDlibro = item.IDlibro,
                    Titulo = item.Titulo,
                    CodigoISBN = item.CodigoISBN,
                    Fechapublica = item.Fechapublica,
                    UrlAcceso = item.UrlAcceso,
                    Editorial = item.Editorial,
                    Libro = item.Libro,
                    Portada = item.Portada,
                    Carrera=item.Carrera,
                    Autores=item.Autores
                    





                });
            }

            return listalibros;
        }






        //Función para ver libros populares paginación
        public List<E_Libros> VerLibrosPopulares()
        {
            List<E_Libros> listalibros = new List<E_Libros>();

            foreach (var item in DB.VerLibrosPopulares())
            {
                listalibros.Add(new E_Libros()
                {

                    IDlibro = item.IDlibro,
                    Titulo = item.Titulo,
                    CodigoISBN = item.CodigoISBN,
                    Fechapublica = item.Fechapublica,
                    UrlAcceso = item.UrlAcceso,
                    Editorial = item.Editorial,
                    Libro = item.Libro,
                    Portada = item.Portada,
               


                });
            }

            return listalibros;
        }





        //Función para ver libros recientes paginación
        public List<E_Libros> VerLibrosRecientes()
        {
            List<E_Libros> listalibros = new List<E_Libros>();

            foreach (var item in DB.VerLibrosRecientes())
            {
                listalibros.Add(new E_Libros()
                {

                    IDlibro = item.IDlibro,
                    Titulo = item.Titulo,
                    CodigoISBN = item.CodigoISBN,
                    Fechapublica = item.Fechapublica,
                    UrlAcceso = item.UrlAcceso,
                    Editorial = item.Editorial,
                    Libro = item.Libro,
                    Portada = item.Portada,
             

                });
            }

            return listalibros;
        }





        //Función para registrar vistas de libro
        public int RegistroVistasLibro(string ip, DateTime fechavistas, int idlibro)
        {

            int x = 3;


            try
            {

                x = Convert.ToInt32(DB.RegistroVistasLibro(ip,fechavistas,idlibro).FirstOrDefault());
            }
            catch (Exception)
            {
                x = 3;
            }
            return x;
        }



        

        //Función para búsqueda filtrada de libros por título
        public  List<E_Libros> BusquedaLibrosTitulo(string buscador)
        {
            List<E_Libros> listalibros = new List<E_Libros>();

            foreach (var item in DB.BusquedaLibrosTitulo(buscador))
            {
                listalibros.Add(new E_Libros()
                {
                    
                    IDlibro = item.IDlibro,
                    Titulo = item.Titulo,
                    CodigoISBN = item.CodigoISBN,
                    Fechapublica = item.Fechapublica,
                    UrlAcceso = item.UrlAcceso,

                    Libro = item.Libro,
                    Portada = item.Portada




                });
            }

            return listalibros;
        }








        //Función para búsqueda filtrada de libros por autores
        public List<E_Libros> BusquedaLibrosAutores(string buscador)
        {
            List<E_Libros> listalibros = new List<E_Libros>();

            foreach (var item in DB.BusquedaLibrosAutores(buscador))
            {
                listalibros.Add(new E_Libros()
                {

                    IDlibro = item.IDlibro,
                    Titulo = item.Titulo,
                    CodigoISBN = item.CodigoISBN,
                    Fechapublica = item.Fechapublica,
                    UrlAcceso = item.UrlAcceso,

                    Libro = item.Libro,
                    Portada = item.Portada




                });
            }

            return listalibros;
        }





        //Función para búsqueda filtrada de libros por código ISBN
        public List<E_Libros> BusquedaLibrosCodigoISBN(string buscador)
        {
            List<E_Libros> listalibros = new List<E_Libros>();

            foreach (var item in DB.BusquedaLibrosCodigoISBN(buscador))
            {
                listalibros.Add(new E_Libros()
                {

                    IDlibro = item.IDlibro,
                    Titulo = item.Titulo,
                    CodigoISBN = item.CodigoISBN,
                    Fechapublica = item.Fechapublica,
                    UrlAcceso = item.UrlAcceso,

                    Libro = item.Libro,
                    Portada = item.Portada




                });
            }

            return listalibros;
        }






        //Función para búsqueda filtrada de libros por todos los parámetros
        public List<E_Libros> BusquedaLibrosTodoParametros(string buscador, int idcarrera, int yearp, int idcate)
        {
            List<E_Libros> listalibros = new List<E_Libros>();

            foreach (var item in DB.BusquedaLibrosTodoParametros(buscador,idcarrera,yearp,idcate))
            {
                listalibros.Add(new E_Libros()
                {

                    IDlibro = item.IDlibro,
                    Titulo = item.Titulo,
                    CodigoISBN = item.CodigoISBN,
                    Fechapublica = item.Fechapublica,
                    UrlAcceso = item.UrlAcceso,

                    Libro = item.Libro,
                    Portada = item.Portada




                });
            }

            return listalibros;
        }






        //Función para búsqueda filtrada de libros por idcarrera y título
        public List<E_Libros> BusquedaLibrosxCarrera(string buscador,int idcarrera)
        {
            List<E_Libros> listalibros = new List<E_Libros>();

            foreach (var item in DB.BusquedaLibrosxCarrera(buscador,idcarrera))
            {
                listalibros.Add(new E_Libros()
                {

                    IDlibro = item.IDlibro,
                    Titulo = item.Titulo,
                    CodigoISBN = item.CodigoISBN,
                    Fechapublica = item.Fechapublica,
                    UrlAcceso = item.UrlAcceso,

                    Libro = item.Libro,
                    Portada = item.Portada




                });
            }

            return listalibros;
        }






        //Función para búsqueda filtrada de libros por idcategoria y título
        public List<E_Libros> BusquedaLibrosxCategoria(string buscador, int idcate)
        {
            List<E_Libros> listalibros = new List<E_Libros>();

            foreach (var item in DB.BusquedaLibrosxCategoria(buscador, idcate))
            {
                listalibros.Add(new E_Libros()
                {

                    IDlibro = item.IDlibro,
                    Titulo = item.Titulo,
                    CodigoISBN = item.CodigoISBN,
                    Fechapublica = item.Fechapublica,
                    UrlAcceso = item.UrlAcceso,

                    Libro = item.Libro,
                    Portada = item.Portada




                });
            }

            return listalibros;
        }






        //Función para búsqueda filtrada de libros por año de publicación y título
        public List<E_Libros> BusquedaLibrosYearPublicacion(string buscador, int yearp)
        {
            List<E_Libros> listalibros = new List<E_Libros>();

            foreach (var item in DB.BusquedaLibrosYearPublicacion(buscador, yearp))
            {
                listalibros.Add(new E_Libros()
                {

                    IDlibro = item.IDlibro,
                    Titulo = item.Titulo,
                    CodigoISBN = item.CodigoISBN,
                    Fechapublica = item.Fechapublica,
                    UrlAcceso = item.UrlAcceso,

                    Libro = item.Libro,
                    Portada = item.Portada




                });
            }

            return listalibros;
        }





        //Función para mostrar todos los libros publicados por idcarrera
        public List<E_Libros> MostrarLibrosxCarrera(int idcarrera)
        {
            List<E_Libros> listalibros = new List<E_Libros>();

            foreach (var item in DB.MostrarLibrosxCarrera(idcarrera))
            {
                listalibros.Add(new E_Libros()
                {

                    IDlibro = item.IDlibro,
                    Titulo = item.Titulo,
                    CodigoISBN = item.CodigoISBN,
                    Fechapublica = item.Fechapublica,
                    UrlAcceso = item.UrlAcceso,
                    Carrera=item.Carrera,
                    Autores=item.Autores,
                    Libro = item.Libro,
                    Portada = item.Portada




                });
            }

            return listalibros;
        }





        //Función para mostrar todos los libros publicados por idcategoria
        public List<E_Libros> MostrarLibrosxCategoria(int idcate)
        {
            List<E_Libros> listalibros = new List<E_Libros>();

            foreach (var item in DB.MostrarLibrosxCategoria(idcate))
            {
                listalibros.Add(new E_Libros()
                {

                    IDlibro = item.IDlibro,
                    Titulo = item.Titulo,
                    CodigoISBN = item.CodigoISBN,
                    Fechapublica = item.Fechapublica,
                    UrlAcceso = item.UrlAcceso,
                    Carrera = item.Carrera,
                    Autores = item.Autores,
                    Libro = item.Libro,
                    Portada = item.Portada




                });
            }

            return listalibros;
        }






        //Función para mostrar todos los libros publicados por año de publicación
        public List<E_Libros> MostrarLibrosxYear(int yearp)
        {
            List<E_Libros> listalibros = new List<E_Libros>();

            foreach (var item in DB.MostrarLibrosxYear(yearp))
            {
                listalibros.Add(new E_Libros()
                {

                    IDlibro = item.IDlibro,
                    Titulo = item.Titulo,
                    CodigoISBN = item.CodigoISBN,
                    Fechapublica = item.Fechapublica,
                    UrlAcceso = item.UrlAcceso,
                    Carrera = item.Carrera,
                    Autores = item.Autores,
                    Libro = item.Libro,
                    Portada = item.Portada




                });
            }

            return listalibros;
        }





        //Función para mostrar todos los libros publicados por período
        public List<E_Libros> MostrarLibrosxPeriodo(int idpe)
        {
            List<E_Libros> listalibros = new List<E_Libros>();

            foreach (var item in DB.MostrarLibrosxPeriodo(idpe))
            {
                listalibros.Add(new E_Libros()
                {

                    IDlibro = item.IDlibro,
                    Titulo = item.Titulo,
                    CodigoISBN = item.CodigoISBN,
                    Fechapublica = item.Fechapublica,
                    UrlAcceso = item.UrlAcceso,
                    Carrera = item.Carrera,
                    Autores = item.Autores,
                    Libro = item.Libro,
                    Portada = item.Portada




                });
            }

            return listalibros;
        }







        //Función para mostrar todos los libros publicados por carrera y período 
        public List<E_Libros> MostrarLibrosxCarreraPeriodo(int idpe,int idc)
        {
            List<E_Libros> listalibros = new List<E_Libros>();

            foreach (var item in DB.MostrarLibrosxCarreraPeriodo(idpe, idc))
            {
                listalibros.Add(new E_Libros()
                {

                    IDlibro = item.IDlibro,
                    Titulo = item.Titulo,
                    CodigoISBN = item.CodigoISBN,
                    Fechapublica = item.Fechapublica,
                    UrlAcceso = item.UrlAcceso,
                    Carrera = item.Carrera,
                    Autores = item.Autores,
                    Libro = item.Libro,
                    Portada = item.Portada




                });
            }

            return listalibros;
        }






        //Función para mostrar todos los libros publicados por carrera y año 
        public List<E_Libros> MostrarLibrosxCarreraYear(int idc,int yearp)
        {
            List<E_Libros> listalibros = new List<E_Libros>();

            foreach (var item in DB.MostrarLibrosxCarreraYear(idc,yearp))
            {
                listalibros.Add(new E_Libros()
                {

                    IDlibro = item.IDlibro,
                    Titulo = item.Titulo,
                    CodigoISBN = item.CodigoISBN,
                    Fechapublica = item.Fechapublica,
                    UrlAcceso = item.UrlAcceso,
                    Carrera = item.Carrera,
                    Autores = item.Autores,
                    Libro = item.Libro,
                    Portada = item.Portada




                });
            }

            return listalibros;
        }





    }

}
