using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Negocio.Metodos;
using Negocio.Entidades;
using System.Web.Routing;
using Microsoft.OData.Edm;
using Rotativa;
using System.IO;
using System.Net.Mail;

namespace WebApplication1.Controllers
{
    public class LibroController : Controller
    {
        //string urlDomain = "https://localhost:44329/";

        M_Libros conexLibro = new M_Libros();
        M_Procesos conexProce = new M_Procesos();
        M_Autores conexAutor = new M_Autores();
        M_Categorias conexCate = new M_Categorias();
        M_Usuarios conexUser = new M_Usuarios();
        M_Documentos conexDocs = new M_Documentos();
        M_Evaluadores conexEvaluador = new M_Evaluadores();
        M_Notificaciones conexNotificacion = new M_Notificaciones();
        M_Carreras conexcarrera = new M_Carreras();
        M_EstadosTiposproceso conexEstado = new M_EstadosTiposproceso();
     
        M_MensajesTiposproceso conexMensaje = new M_MensajesTiposproceso();

        //para convertir una vista html en pdf

        ////public ActionResult InformeLibro(int IDLibro)
        ////{
        ////    ViewBag.Procesos = conexProce.VerProceso(IDLibro);
        ////    ViewBag.Autores = conexAutor.VerAutores(IDLibro);
        ////    var listalibro = conexLibro.VerDetalleLibro(IDLibro);
        ////    return View(listalibro);
        ////}


        ////public ActionResult Imprime(int IDLibro)
        ////{   
        //// return new ActionAsPdf("InformeLibro", new {IDLibro=IDLibro }) { FileName="Informe-de-Libro-publicado-"+IDLibro+".pdf"};
        ////}


        ////public ActionResult Imprime(int IDLibro)
        ////{   
        //// return new Rotativa.ViewAsPdf("InformeLibro", new {IDLibro=IDLibro }) { FileName="Informe-de-Libro-publicado-"+IDLibro+".pdf"};
        ////}





        //Función para registrar libro externo //
        [HttpPost]
        public ActionResult RegistrarLibroExterno(string Titulo, string CodigoISBN, DateTime Fechapublica, string Editorial, int idcarrera, int Idcategoria, HttpPostedFileBase Portada, HttpPostedFileBase Libro)
        {
            int r = 0;
           
            string rutap = Server.MapPath("~/Files/ImagesLibros/");
            string rutal = Server.MapPath("~/Files/Libros/");
            string Extension = GetExtension(Libro.FileName);
            string fecha = Fechapublica.ToString("dd/MM/yyyy");
      

            string urlacceso = "";

            try
            {
               string NombreDoc = Titulo + "-" + "Libro." + Extension;
               string Filename ="Portada_"+Titulo.ToString() + ".png";
                Portada.SaveAs(rutap + Filename);
                Filename = "https://localhost:44329/Files/ImagesLibros/" + Filename;

                if (Extension == "txt" || Extension == "doc" || Extension == "docx" || Extension == "pdf" || Extension == "rtf")
                {

                    Libro.SaveAs(rutal + NombreDoc);
                    NombreDoc = " https://localhost:44329/Files/Libros/" + NombreDoc;

                    r = conexLibro.AgregarLibroExterno(Titulo, CodigoISBN, Editorial, Idcategoria, fecha, idcarrera, Filename, NombreDoc,urlacceso,Fechapublica);
                    urlacceso = "https://localhost:44329/Editorial/Titulo?lib=" + r + "";
                    int x = conexLibro.EditarLibroExterno(r, Titulo, CodigoISBN, Editorial, Idcategoria, fecha, idcarrera, Filename, NombreDoc, urlacceso,Fechapublica);

                    TempData["OK"] = "Datos de libro guardados correctamente";

                }
                else
                {
                    TempData["ERROR2"] = "Formato de archivo no aceptado";
                }

               
            }
            catch (Exception)
            { 
                TempData["ERROR"] = "Algo salió mal";
                //return View();
            }


            return RedirectToAction("AgregarLibro", new RouteValueDictionary(new { controller = "Libro", action = "AgregarLibro", IDlibro = r }));
        }







        //Función para editar libro externo
        [HttpPost]
        public ActionResult EditarLibroxPublicar(int Idlibro,string titulo, string codigo, DateTime fecha, string editorial, int idcarrera, HttpPostedFileBase Portada, HttpPostedFileBase Libro, int idcategoria)
        {
            int r;
           
            string fechap = fecha.ToString("dd/MM/yyyy");
            string rutap = Server.MapPath("~/Files/ImagesLibros/");
            string rutal = Server.MapPath("~/Files/Libros/");
           
            string Filename = "";
            string NombreDoc = "";

           
            try
            {
                foreach (var item in conexLibro.VerDetalleLibroExterno(Idlibro))
                {
                    if (Libro == null)
                    {
                        NombreDoc = item.Libro;
                        if (NombreDoc == null)
                        {
                            TempData["ERROR2"] = "No se ha agregado el archivo del libro, vuelva a intentarlo";
                            return RedirectToAction("AgregarLibro", new RouteValueDictionary(new { controller = "Libro", action = "AgregarLibro", IDlibro = Idlibro }));
                        }
                       

                    }
                    else
                    {
                        string Extension = GetExtension(Libro.FileName);
                        NombreDoc = titulo + "-" + "Libro." + Extension;

                        if (Extension == "txt" || Extension == "doc" || Extension == "docx" || Extension == "pdf" || Extension == "rtf")
                        {

                            Libro.SaveAs(rutal + NombreDoc);
                            NombreDoc = "https://localhost:44329/Files/Libros/" + NombreDoc;
                        }
                        else
                        {

                            TempData["ERROR2"] = "Formato de archivo no aceptado";
                        }

                    }



                    if (Portada == null)
                    {
                        Filename = item.Portada;
                        if (Filename == null)
                        {
                            TempData["ERROR2"] = "No se ha agregado el archivo de portada del libro, vuelva a intentarlo";
                            return RedirectToAction("AgregarLibro", new RouteValueDictionary(new { controller = "Libro", action = "AgregarLibro", IDlibro = Idlibro }));
                        }

                    }
                    else
                    {
                        Filename = "Portada_" + titulo.ToString() + ".png";
                        Portada.SaveAs(rutap + Filename);
                        Filename = "https://localhost:44329/Files/ImagesLibros/" + Filename;
                    }


                }

                string urlacceso = "https://localhost:44329/Editorial/Titulo?lib=" + Idlibro + "";

                r = conexLibro.EditarLibroExterno(Idlibro,titulo, codigo, editorial, idcategoria, fechap, idcarrera, Filename, NombreDoc,urlacceso,fecha);



                if (r == 1)
                {
                    TempData["OK"] = "Datos de libro editados correctamente";
                }
               
            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";
                //return View();
            }


            return RedirectToAction("AgregarLibro", new RouteValueDictionary(new { controller = "Libro", action = "AgregarLibro", IDlibro = Idlibro }));
        }







        //Función para guardar datos o editar datos del libro antes de publicarlo
        [HttpPost]
        public ActionResult GuardarDatosPublicacion(int Idlibro,int Idproceso, string titulo, string codigo, DateTime fecha, string editorial, int idcarrera, HttpPostedFileBase Portada, HttpPostedFileBase Libro, int idcategoria)
        {
            int r = 0;
            string fechap = fecha.ToString("dd/MM/yyyy");
            string rutap = Server.MapPath("~/Files/ImagesLibros/");
            string rutal = Server.MapPath("~/Files/Libros/");

       


            string Filename = "";
            string NombreDoc = "";


            try
            {
                foreach (var item in conexLibro.VerDetalleLibro(Idlibro))
                {
                    if (Libro == null)
                    {
                        NombreDoc = item.Libro;

                        if (NombreDoc == null)
                        {
                            TempData["ERROR2"] = "No se ha agregado archivo de libro, vuelva a intentarlo";
                            return RedirectToAction("PublicacionLibro", new RouteValueDictionary(new { controller = "Libro", action = "PublicacionLibro", Idlibro = Idlibro, Idproceso = Idproceso,Op=0 }));
                        }
                    
                    }
                    else
                    {
                        string Extension = GetExtension(Libro.FileName);
                        NombreDoc = titulo + "-" + "Libro." + Extension;

                        if (Extension == "txt" || Extension == "doc" || Extension == "docx" || Extension == "pdf" || Extension == "rtf")
                        {

                            Libro.SaveAs(rutal + NombreDoc);
                            NombreDoc = "https://localhost:44329/Files/Libros/" + NombreDoc;
                        }
                        else
                        {

                            TempData["ERROR2"] = "Formato de archivo no aceptado";
                        }

                    }
                    if (Portada == null)
                    {
                        Filename = item.Portada;
                        if (Filename == null)
                        {
                            TempData["ERROR2"] = "No se ha agregado archivo para la portada de libro, vuelva a intentarlo";
                            return RedirectToAction("PublicacionLibro", new RouteValueDictionary(new { controller = "Libro", action = "PublicacionLibro", Idlibro = Idlibro, Idproceso = Idproceso,Op=0 }));
                        }
                    
                    }
                    else
                    {
                        Filename = "Portada_" + titulo.ToString() + ".png";
                        Portada.SaveAs(rutap + Filename);
                        Filename = "https://localhost:44329/Files/ImagesLibros/" + Filename;
                    }


                }

                string urlacceso ="https://localhost:44329/Editorial/Titulo?lib=" + Idlibro +"";

                r = conexLibro.EditarLibroExterno(Idlibro, titulo, codigo, editorial, idcategoria, fechap, idcarrera, Filename, NombreDoc,urlacceso,fecha);


                if (r == 1)
                {
                    TempData["OK"] = "Datos de libro registrados correctamente";
                }

            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";
                //return View();
            }


            return RedirectToAction("PublicacionLibro", new RouteValueDictionary(new { controller = "Libro", action = "PublicacionLibro", Idlibro = Idlibro, Idproceso=Idproceso,Op=0 }));
        }






        //Vista para agregar libro externo
        public ActionResult AgregarLibro(int IDlibro)
        {
            if (Session["Type"] == null)
            {
                TempData["ERRORD"] = "No tiene acceso a esta sección del sistema. Inicia sesión";
                return RedirectToAction("Login", "Usuario");
            }


            if (Session["Type"].ToString() != "Administrador")
            {

                TempData["ERRORD"] = "No tiene acceso a esta sección del sistema. Inicia sesión";
                return RedirectToAction("Login", "Usuario");

            }


            int ntotal = 0;
            int nnuevas = 0;

            int Id = Convert.ToInt32(Session["Id"]);
            ViewBag.Notificaciones = conexNotificacion.VerNotificacionesxIduser(Id);

            foreach (var item in ViewBag.Notificaciones)
            {
                ntotal = Convert.ToInt32(item.TotalNotificacionesrecibidas);
                nnuevas = Convert.ToInt32(item.TotalNotificacionesnuevas);
                break;
            }
            ViewBag.TotalNotificaciones = ntotal;
            ViewBag.NuevasNotificaciones = nnuevas;

            ViewBag.Autores = conexAutor.VerAutores(IDlibro);
           
            ViewBag.CitaLibro = conexLibro.VerCitaLibro(IDlibro);


            ViewBag.Carreras = conexcarrera.VerCarreras();
            //"Libro Publicado"


            ViewBag.Usuario = conexUser.BuscarUsuario(Session["Username"].ToString());
            ViewBag.Idlibro1 = IDlibro;
            ViewBag.Categorias = conexCate.Vercategorias();
            ViewBag.LibroDetalle = conexLibro.VerDetalleLibroExterno(IDlibro);
            foreach(var item in ViewBag.LibroDetalle)
            {
                ViewBag.Estado = item.EstadoL;
            }
            ViewBag.Autores = conexAutor.VerAutores(IDlibro);
            return View();
        }






        //Vista para publicar libro
        public ActionResult PublicacionLibro(int Idlibro, int Idproceso)
        {
            if (Session["Type"] == null)
            {
                TempData["ERRORD"] = "No tiene acceso a esta sección del sistema. Inicia sesión";
                return RedirectToAction("Login", "Usuario");
            }


            if (Session["Type"].ToString() != "Administrador")
            {

                TempData["ERRORD"] = "No tiene acceso a esta sección del sistema. Inicia sesión";
                return RedirectToAction("Login", "Usuario");

            }

            int ntotal = 0;
            int nnuevas = 0;

            int Id = Convert.ToInt32(Session["Id"]);
            ViewBag.Notificaciones = conexNotificacion.VerNotificacionesxIduser(Id);

            foreach (var item in ViewBag.Notificaciones)
            {
                ntotal = Convert.ToInt32(item.TotalNotificacionesrecibidas);
                nnuevas = Convert.ToInt32(item.TotalNotificacionesnuevas);
                break;
            }
            ViewBag.TotalNotificaciones = ntotal;
            ViewBag.NuevasNotificaciones = nnuevas;

            ViewBag.Proceso = conexProce.VerProcesoxIdp(Idlibro, Idproceso);
            ViewBag.Carreras = conexcarrera.VerCarreras();
            ViewBag.Libro = conexLibro.VerDetalleLibro(Idlibro);

            ViewBag.Idlibro = Idlibro;
            ViewBag.Usuario = conexUser.BuscarUsuario(Session["Username"].ToString());
            ViewBag.Idp = Idproceso;

            ViewBag.CitaLibro = conexLibro.VerCitaLibro(Idlibro);
          
           


            ViewBag.Categorias = conexCate.Vercategorias();
            ViewBag.LibroDetalle = conexLibro.VerDetalleLibro(Idlibro);
            ViewBag.Autores = conexAutor.VerAutores(Idlibro);
            return View();
        }






        //Función para publicar libro externo
        public ActionResult PublicarLibroExterno(int IDlibro)
        {
            ViewBag.Autores = conexAutor.VerAutores(IDlibro);

                if (ViewBag.Autores.Count == 0)
                {
                  TempData["ERROR2"] = "Falta agregar la información correspondiente de autor(es) del libro.";
                  return RedirectToAction("AgregarLibro", new RouteValueDictionary(new { controller = "Libro", action = "AgregarLibro", IDlibro = IDlibro }));
                }


            foreach (var item in conexLibro.VerDetalleLibro(IDlibro))
            {
                if (item.Titulo==null || item.Libro == null || item.Portada == null || item.Fechapublica == null || item.CodigoISBN == null || item.Editorial == null || item.Idcategoria == null || item.Idcarrera == null)
                {
                    TempData["ERROR2"] = "Faltan datos por registrar para completar la publicación del libro. Registre todos los datos solicitados antes de publicar el libro.";
                    return RedirectToAction("AgregarLibro", new RouteValueDictionary(new { controller = "Libro", action = "AgregarLibro", IDlibro = IDlibro }));
                }
            }


            try
            {
                string fechactualizada = System.DateTime.Now.ToString("dd/MM/yyyy" + " - " + "H:mm tt");
                int Idproceso = 0;
                string estado = "Libro publicado";
                bool est = true;
                int progresoL = 100;
              
                string procesoL = "Proceso finalizado";
       
             
                int el = conexLibro.EditarLibroP(IDlibro, procesoL, progresoL, fechactualizada, estado, Idproceso,est);


                if (el==1)
                {
                    TempData["OK"] = "Libro publicado exitósamente";
                }
            }
            catch(Exception)   
            {
                TempData["ERROR"] = "Algo salió mal";
                //return View();
            }


            return RedirectToAction("AgregarLibro", new RouteValueDictionary(new { controller = "Libro", action = "AgregarLibro", IDlibro = IDlibro }));
        }








        //Lista de libros - vista autor
        public ActionResult Libros()
        {

            if (Session["Type"] == null)
            {
                TempData["ERRORD"] = "No tiene acceso a esta sección del sistema. Inicia sesión";
                return RedirectToAction("Login", "Usuario");
            }


            if (Session["Type"].ToString() != "Autor")
            {
                TempData["ERRORD"] = "No tiene acceso a esta sección del sistema. Inicia sesión";
                return RedirectToAction("Login", "Usuario");

            }


            int ntotal = 0;
            int nnuevas = 0;

            int Id = Convert.ToInt32(Session["Id"]);
            ViewBag.Notificaciones = conexNotificacion.VerNotificacionesxIduser(Id);

            foreach (var item in ViewBag.Notificaciones)
            {
                ntotal = Convert.ToInt32(item.TotalNotificacionesrecibidas);
                nnuevas = Convert.ToInt32(item.TotalNotificacionesnuevas);
                break;
            }
            ViewBag.TotalNotificaciones = ntotal;
            ViewBag.NuevasNotificaciones = nnuevas;

            
            var listalibro = conexLibro.VerLibroxIdUsuario(Convert.ToInt32(Session["Id"]));
            ViewBag.Usuario = conexUser.BuscarUsuario(Session["Username"].ToString());
            return View(listalibro);
        }






        //Lista de libros - vista Admin
        public ActionResult ListaLibros()
        {
            if (Session["Type"] == null)
            {
                TempData["ERRORD"] = "No tiene acceso a esta sección del sistema. Inicia sesión";
                return RedirectToAction("Login", "Usuario");
            }


            if (Session["Type"].ToString() != "Administrador")
            {

                TempData["ERRORD"] = "No tiene acceso a esta sección del sistema. Inicia sesión";
                return RedirectToAction("Login", "Usuario");

            }

            int ntotal = 0;
            int nnuevas = 0;

            int Id = Convert.ToInt32(Session["Id"]);
            ViewBag.Notificaciones = conexNotificacion.VerNotificacionesxIduser(Id);

            foreach (var item in ViewBag.Notificaciones)
            {
                ntotal = Convert.ToInt32(item.TotalNotificacionesrecibidas);
                nnuevas = Convert.ToInt32(item.TotalNotificacionesnuevas);
                break;
            }
            ViewBag.TotalNotificaciones = ntotal;
            ViewBag.NuevasNotificaciones = nnuevas;

            
            ViewBag.Usuario = conexUser.BuscarUsuario(Session["Username"].ToString());
            var listalibros = conexLibro.ObtenerLibros();
            return View(listalibros);
        }






        //Lista de solicitudes de nuevos libros a publicar - vista Admin
        public ActionResult SolicitudesLibros()
        {
            if (Session["Type"] == null)
            {
                TempData["ERRORD"] = "No tiene acceso a esta sección del sistema. Inicia sesión";
                return RedirectToAction("Login", "Usuario");
            }


            if (Session["Type"].ToString() != "Administrador")
            {

                TempData["ERRORD"] = "No tiene acceso a esta sección del sistema. Inicia sesión";
                return RedirectToAction("Login", "Usuario");

            }

            int ntotal = 0;
            int nnuevas = 0;

            int Id = Convert.ToInt32(Session["Id"]);
            ViewBag.Notificaciones = conexNotificacion.VerNotificacionesxIduser(Id);

            foreach (var item in ViewBag.Notificaciones)
            {
                ntotal = Convert.ToInt32(item.TotalNotificacionesrecibidas);
                nnuevas = Convert.ToInt32(item.TotalNotificacionesnuevas);
                break;
            }
            ViewBag.TotalNotificaciones = ntotal;
            ViewBag.NuevasNotificaciones = nnuevas;


            ViewBag.Usuario = conexUser.BuscarUsuario(Session["Username"].ToString());
            var listalibros = conexLibro.VerSolicitudesLibros();
            return View(listalibros);
        }







        //Función para solicitar publicar un nuevo libro
        [HttpPost]
        public JsonResult SolcitarPublicarNuevoLibro(int idemisor)
        {
            int r = 0;
            bool general = false;
            int l = 0;
            string nombres = "";
            string fecha = System.DateTime.Now.ToString("dd/MM/yyyy" + " - " + "H:mm tt");
            string url1 = "Libro/SolicitudesLibros";
            string visible1 = "Administrador";
            string visible2 = "Autor";
            string detallenotificacion = "Solicitud para publicar nuevo libro.";


            try
            {
               
                    foreach (var item in conexUser.VerDetalleUsuario(idemisor))
                    {
                        nombres = item.PrimerNombre + " " + item.ApellidoPrimero+" "+item.ApellidoSegundo;
                    }



                    l = conexLibro.CrearLibro(idemisor, "Libro pendiente", "Solicitud en revisión", 0, fecha, fecha, "Pendiente aceptación", 0);



                    string notifi1 = nombres + " ha solicitado iniciar los procesos correspondientes para publicar un nuevo libro.";
                  
                    string notifi2 = "Se ha enviado una solicitud para iniciar los procesos correspondientes para publicar un nuevo libro, próximamente se le notificará si su solicitud fue aceptada o denegada.";
                  
                 

                    int n2 = conexNotificacion.InsertarNotificacion(notifi2, "#", visible2, idemisor, 3, fecha, detallenotificacion, general);

                    foreach (var item in conexUser.BuscarUsuarioxTipoUsuario(visible1))
                    {
                        int n = conexNotificacion.InsertarNotificacion(notifi1, url1, visible1, item.IDusuario, 3, fecha, detallenotificacion, general);
                        break;

                    }

      
                SendEmailNotificacionEditorial(notifi1, detallenotificacion);

                TempData["OKS"] = "OK";


            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";
            }

            return Json(r, JsonRequestBehavior.AllowGet);
        }








        //Lista de libros - vista evaluador
        public ActionResult LibrosEvaluar()
        {
            if (Session["Type"] == null)
            {
                TempData["ERRORD"] = "No tiene acceso a esta sección del sistema. Inicia sesión";
                return RedirectToAction("Login", "Usuario");
            }


            if (Session["Type"].ToString() != "Evaluador")
            {

                TempData["ERRORD"] = "No tiene acceso a esta sección del sistema. Inicia sesión";
                return RedirectToAction("Login", "Usuario");

            }
            int ntotal = 0;
            int nnuevas = 0;

            int Id = Convert.ToInt32(Session["Id"]);
            ViewBag.Notificaciones = conexNotificacion.VerNotificacionesxIduser(Id);

            foreach (var item in ViewBag.Notificaciones)
            {
                ntotal = Convert.ToInt32(item.TotalNotificacionesrecibidas);
                nnuevas = Convert.ToInt32(item.TotalNotificacionesnuevas);
                break;
            }

            ViewBag.TotalNotificaciones = ntotal;
            ViewBag.NuevasNotificaciones = nnuevas;

            ViewBag.Usuario = conexUser.BuscarUsuario(Session["Username"].ToString());
            var listalibros = conexLibro.VerLibroEvaluar(Convert.ToInt32(Session["Id"]));
            ViewBag.Libros = listalibros;
            return View(listalibros);
        }





        //Lista de libros archivados - vista admin//
        public ActionResult LibrosArchivados()
        {

            if (Session["Type"] == null)
            {
                TempData["ERRORD"] = "No tiene acceso a esta sección del sistema. Inicia sesión";
                return RedirectToAction("Login", "Usuario");
            }


            if (Session["Type"].ToString() != "Administrador")
            {

                TempData["ERRORD"] = "No tiene acceso a esta sección del sistema. Inicia sesión";
                return RedirectToAction("Login", "Usuario");

            }
            int ntotal = 0;
            int nnuevas = 0;

            int Id = Convert.ToInt32(Session["Id"]);
            ViewBag.Notificaciones = conexNotificacion.VerNotificacionesxIduser(Id);

            foreach (var item in ViewBag.Notificaciones)
            {
                ntotal = Convert.ToInt32(item.TotalNotificacionesrecibidas);
                nnuevas = Convert.ToInt32(item.TotalNotificacionesnuevas);
                break;

            }


            ViewBag.TotalNotificaciones = ntotal;
            ViewBag.NuevasNotificaciones = nnuevas;

            ViewBag.Usuario = conexUser.BuscarUsuario(Session["Username"].ToString());
            var listalibros = conexLibro.ObtenerLibrosA();
            return View(listalibros);
        }






        //Lista de libros archivados - vista autor//
        public ActionResult ListaLibrosArchivados()
        {

            if (Session["Type"] == null)
            {
                TempData["ERRORD"] = "No tiene acceso a esta sección del sistema. Inicia sesión";
                return RedirectToAction("Login", "Usuario");
            }


            if (Session["Type"].ToString() != "Autor")
            {

                TempData["ERRORD"] = "No tiene acceso a esta sección del sistema. Inicia sesión";
                return RedirectToAction("Login", "Usuario");

            }
            int ntotal = 0;
            int nnuevas = 0;

            int Id = Convert.ToInt32(Session["Id"]);
            ViewBag.Notificaciones = conexNotificacion.VerNotificacionesxIduser(Id);

            foreach (var item in ViewBag.Notificaciones)
            {
                ntotal = Convert.ToInt32(item.TotalNotificacionesrecibidas);
                nnuevas = Convert.ToInt32(item.TotalNotificacionesnuevas);
                break;
            }

            ViewBag.TotalNotificaciones = ntotal;
            ViewBag.NuevasNotificaciones = nnuevas;

            int Iduser = Convert.ToInt32(Session["Id"]);
            ViewBag.Usuario = conexUser.BuscarUsuario(Session["Username"].ToString());
            var listalibros = conexLibro.ObtenerLibrosArchivadosAutor(Iduser);
            return View(listalibros);
        }





        //Función para obtener la extensión de un archivo
        private string GetExtension(string attachment_name)
        {
            var index_point = attachment_name.IndexOf(".") + 1;
            return attachment_name.Substring(index_point);
        }

       





        //Función para publicar libro una vez terminados todos los procesos
        public ActionResult Publicar(int Idlibro, int Idproceso)
        {
            
            string fechactualizada = System.DateTime.Now.ToString("dd/MM/yyyy" + " - " + "H:mm tt");
            bool general = false;
            string estado = "Libro publicado";
            string estadop = "";
            string titulol = "";
            int progresoL = 100;
            int Idpanterior = 0;
            string procesoL = "";
            int Iduser = 0;
            bool est = true;
            int op = 0;
            int Idtipoproceso = 0;
            int n1, r, el, n2,a = 0;
            string detallen = "Libro publicado";
            string urlnotificacion = "Proceso/VerProcesoAutor?Idlibro=" + Idlibro + "&Idproceso=" + Idproceso + "";
            string visiblen = "Autor";
            string urlnotificacion1 = "Proceso/VerProcesoAdmin?Idlibro=" + Idlibro + "&Idproceso=" + Idproceso + "";
            string visiblen1 = "Administrador";
            string asunto = "";
            string notificacion = "";
            string notificacion1 = "";
            int identificadorE = 0;

            foreach (var item in conexLibro.VerDetalleLibro(Idlibro))
            {
                if(item.Libro==null || item.Portada==null || item.Fechapublica==null || item.CodigoISBN==null || item.Editorial==null || item.Idcategoria==null || item.Idcarrera == null)
                {
                    TempData["ERROR2"] = "Faltan datos por registrar para completar la publicación del libro. Registre todos los datos solicitados antes de publicar el libro.";
                    return RedirectToAction("PublicacionLibro", new RouteValueDictionary(new { controller = "Libro", action = "PublicacionLibro", Idlibro = Idlibro, Idproceso = Idproceso }));
                }
            }

            foreach (var item in conexProce.VerProcesoxIdp(Idlibro, Idproceso))
            {
                Idpanterior = Convert.ToInt32(item.Idprocanterior);
                procesoL = item.Detalletipospro;
                Iduser = Convert.ToInt32(item.Idusuario);
                titulol = item.Titulo;
                Idtipoproceso = Convert.ToInt32(item.Idtipoproceso);
            }
      

        

            foreach(var item in conexEstado.ListarEstadoxIdentificador(Idtipoproceso, 8))
            {
                estadop = item.DetalleEstados;
                identificadorE = item.IdentificadorEstados;
            }


      
             r = conexProce.EditarProceso1(Idproceso, fechactualizada, estadop,Idpanterior,op,identificadorE);
            el = conexLibro.EditarLibroP(Idlibro, procesoL, progresoL, fechactualizada, estado, Idproceso,est);


            if (r > 0)
            {
                //crear notificación autor

                 asunto = "Libro " + titulol + " publicado.";
                 notificacion = "Todos los procesos correspondientes a la publicación del libro "+titulol+ " han finalizado correctamente, este libro ya se encuentra publicado en el repositorio web de la Editorial Humus. Su libro será movido a la sección de libros archivados en donde podrá acceder a toda la información registrada durante la ejecución de los respectivos procesos para la publicación de este libro.";
                 n2 = conexNotificacion.InsertarNotificacion(notificacion, urlnotificacion, visiblen, Iduser, 2, fechactualizada, detallen,general);
         
                //enviar correo autor
                SendEmail(notificacion, Iduser, asunto);


                //notificación admin
                 notificacion1 = "Se ha publicado el libro " + titulol + ", ahora se encuentra publicado en el repositorio web de la Editorial Humus. Se recomienda mover el libro a la sección de libros archivados en donde podrá acceder a toda la información registrada durante la ejecución de los respectivos procesos para la publicación de este libro.";

                foreach (var item in conexUser.BuscarUsuarioxTipoUsuario(visiblen1))
                {
                    n1 = conexNotificacion.InsertarNotificacion(notificacion1, urlnotificacion1, visiblen1, item.IDusuario, 2, fechactualizada, detallen, general);
                    break;
                }
                //enviar correo a Editorial
                SendEmailNotificacionEditorial(notificacion1, asunto);


                a = conexUser.EditarNumLibrosFinalizados(Iduser);
            

                TempData["OK"] = "Libro publicado exitósamente";
            }
            else
            {
                TempData["ERROR"] = "Algo salió mal";
                //return View();
            }
            return RedirectToAction("PublicacionLibro", new RouteValueDictionary(new { controller = "Libro", action = "PublicacionLibro", Idlibro = Idlibro,Idproceso=Idproceso }));
        }








        //Función para envío de correo de notificación sin archivos adjuntos 
        public void SendEmail(string notificacion, int Iduser, string detallenotificacion)
        {


            string date = DateTime.Now.ToString("MM-dd-yyyy");
            string year = date.Substring(6, 4);
            string emaildestino = "";
            string email = "edicionesHU@gmail.com";
            try
            {
                foreach (var item in conexUser.VerDetalleUsuario(Iduser))
                {

                    emaildestino = item.Email;

                }

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("edicionesHU@gmail.com");
                mail.To.Add(emaildestino);
                mail.Subject = detallenotificacion;

                mail.IsBodyHtml = true;

                string htmlBody = "<table width='100%' bgcolor='#F5F5F5' border='0' cellpadding='0' cellspacing='0'><tbody><tr><td><table class='m_ - 8535930574907526684content' align='center' border='1' cellpadding='0' cellspacing='0' style='border-top:10px solid #348DE4;border-left:1px solid #C0C2C4 ;border-right:1px solid #C0C2C4 ; border-bottom:5px solid #348DE4;border-collapse:collapse;margin-top:30px;width:100%;max-width:600px' bgcolor='#fff'><tbody><tr><td class='m_ - 8535930574907526684header' style='padding: 20px 30px 20px 30px'>" +
                              "<table align='center' border='0' cellpadding='0' cellspacing='0' style='bgcolor = '#fff'><tbody><tr><td valign='top'><a href='https://localhost:44329/Editorial/Inicio' class='h1 text-secondary'><img src='https://raw.githubusercontent.com/gemalors/Imagenes/main/nlog2copia.png' class='light-logo'   border='0'/></a></td><td style='font-size:0;line-height:0' width='220px'>&nbsp;</td> <td align='right' width='300px'> <a href='https://localhost:44329/Usuario/Login'  style='font-size:18px;letter-spacing:0.02rem; color:#0056bf;font-family:Arial,Helvetica,sans-serif;text-decoration:None'  target='_blank'>Sistema web Humus</a></td></tr></tbody></table></td></tr>" +
                              "<tr><td class='m_- 8535930574907526684innerpadding' style='padding: 30px 30px 30px 30px; border-top:1px solid #F2F5F9'><table><tbody><tr><td><br><br><span class='m_-8535930574907526684text-body text-justify' style='line-height:1.5; font-family:Arial,Helvetica,sans-serif; font-size:15px'>" + notificacion + "</span></td></tr><tr><td style = 'font-size:0;line-height:0' height= '20px'>&nbsp;</td></tr><tr><td></td></tr></tbody></table></td></tr>" +
                            "<tr><td class='m_- 8535930574907526684innerpadding' style='padding: 30px 30px 30px 30px;  border-top:1px solid #F2F5F9'><table><tbody><tr></tr><tr><td><table align = 'center' border = '0' cellpadding = '0' cellspacing ='0'style = 'padding-top:30px'><tbody><tr><td width='548px' valign='top' style = 'font-family:Arial,Helvetica,sans-serif;font-size:16px;line-height:1.6'><span style = 'font-size:15px;color:#655F5D;font-family:Arial,Helvetica,sans-serif'> Emisor:<a style = 'color:#0056bf;text-decoration: none;'> Editorial Humus</a></span><br><span style = 'font-size:15px;color:#655F5D;font-family:Arial,Helvetica,sans-serif'> Correo electrónico: <a href = 'mailto:" + email + "' target = '_blank' style = 'color:#0056bf;text-decoration: none;'>" + email + "</a></span><br>" +
                            "</td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr><tr><td><table class='m_-8535930574907526684content' align='center' style='width:100%;max-width:600px'><tbody><tr><td bgcolor = '#F5F5F5' class='m_-8535930574907526684innerpadding'style='padding:5px 30px 30px 30px'><table align='center' border='0' cellpadding='0' cellspacing='0'><tbody><tr><td style ='color:#90949c;font-size:12px;line-height:1.4;font-family:Arial,Helvetica,sans-serif'><br><br><span>Editorial Humus, Coordinación General de Investigación.</span><br><span> Escuela Superior Politécnica Agropecuaria de Manabí Manuel Félix López.</span><br><span>Ecuador - Manabí - Bolivar.</span><br> ® "+year+" Editorial Humus, Inc.<br></td>" +
                            " </tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table>";




                mail.Body = htmlBody;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("edicionesHU@gmail.com", "mwat most cwuu zpfc");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);



            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }





        }







        //Función para eliminar autores de libro//
        [HttpPost]
        public JsonResult EliminarAutor(int idautor)
        {
            int r = 0;
            try
            {
                r = conexAutor.EliminarAutor(idautor);
                if (r == 1)
                {
                    TempData["OKEA"] = "OK";


                }

            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";

            }

            return Json(r, JsonRequestBehavior.AllowGet);
        }









     
        //Función para editar título de libro
        [HttpPost]
        public JsonResult EditarTitulo(int idlibro,string titulo,string tituloant)
        {
            int r = 0;
            int n1 = 0;
            bool general = false;
            try
            {
                r = conexLibro.EditarTituloLibro(idlibro,titulo);
                if (r == 1)
                {
                    string nombresenvia = Session["Nombres"].ToString();
                    string notificacion = nombresenvia + " ha editado el título de su libro "+tituloant+" a "+titulo+".";
                    string urlnotificacion = "Libro/ListaLibros";
                    string visible = "Administrador";
               
                    string asunto = "Libro: "+titulo+" actualizado";
                    string fecha= System.DateTime.Now.ToString("dd/MM/yyyy" + "-" + "H:mm tt");
                    foreach(var item in conexUser.BuscarUsuarioxTipoUsuario(visible))
                    {
                        n1 = conexNotificacion.InsertarNotificacion(notificacion,urlnotificacion,visible,item.IDusuario,2,fecha,asunto,general);
                        break;
                    }
                    //enviar correo a editorial notificando cambio de titulo de libro
                    SendEmailNotificacionEditorial(notificacion, asunto);

                    TempData["OK"] = "Título de libro editado";
                }
              
                else
                {
                    TempData["ERROR"] = "Algo salió mal";
                }

            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";

            }

            return Json(r, JsonRequestBehavior.AllowGet);
        }






        //Función para envío de correo de notificación a Editorial sin archivos adjuntos 
        public void SendEmailNotificacionEditorial(string notificacion, string detallenotificacion)
        {


            string date = DateTime.Now.ToString("MM-dd-yyyy");
            string year = date.Substring(6, 4);
            string email = "edicionesHU@gmail.com";

            string emaildestino = email;
            try
            {


                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("edicionesHU@gmail.com");
                mail.To.Add(emaildestino);
                mail.Subject = detallenotificacion;

                mail.IsBodyHtml = true;

                string htmlBody = "<table width='100%' bgcolor='#F5F5F5' border='0' cellpadding='0' cellspacing='0'><tbody><tr><td><table class='m_ - 8535930574907526684content' align='center' border='1' cellpadding='0' cellspacing='0' style='border-top:10px solid #348DE4;border-left:1px solid #C0C2C4 ;border-right:1px solid #C0C2C4 ; border-bottom:5px solid #348DE4;border-collapse:collapse;margin-top:30px;width:100%;max-width:600px' bgcolor='#fff'><tbody><tr><td class='m_ - 8535930574907526684header' style='padding: 20px 30px 20px 30px'>" +
                              "<table align='center' border='0' cellpadding='0' cellspacing='0' style='bgcolor = '#fff'><tbody><tr><td valign='top'><a href='https://localhost:44329/Editorial/Inicio' class='h1 text-secondary'><img src='https://raw.githubusercontent.com/gemalors/Imagenes/main/nlog2copia.png' class='light-logo'  border='0'/></a></td><td style='font-size:0;line-height:0' width='220px'>&nbsp;</td> <td align='right' width='300px'> <a href='https://localhost:44329/Usuario/Login'  style='font-size:18px;letter-spacing:0.02rem; color:#0056bf;font-family:Arial,Helvetica,sans-serif;text-decoration:None'  target='_blank'>Sistema web Humus</a></td></tr></tbody></table></td></tr>" +
                              "<tr><td class='m_- 8535930574907526684innerpadding' style='padding: 30px 30px 30px 30px; border-top:1px solid #F2F5F9'><table><tbody><tr><td><br><br><span class='m_-8535930574907526684text-body text-justify' style='line-height:1.5; font-family:Arial,Helvetica,sans-serif; font-size:15px'>" + notificacion + "</span></td></tr><tr><td style = 'font-size:0;line-height:0' height= '20px'>&nbsp;</td></tr><tr><td></td></tr></tbody></table></td></tr>" +
                            "<tr><td class='m_- 8535930574907526684innerpadding' style='padding: 30px 30px 30px 30px;  border-top:1px solid #F2F5F9'><table><tbody><tr></tr><tr><td><table align = 'center' border = '0' cellpadding = '0' cellspacing ='0'style = 'padding-top:30px'><tbody><tr><td width='548px' valign='top' style = 'font-family:Arial,Helvetica,sans-serif;font-size:16px;line-height:1.6'><span style = 'font-size:15px;color:#655F5D;font-family:Arial,Helvetica,sans-serif'> Emisor:<a style = 'color:#0056bf;text-decoration: none;'> Editorial Humus</a></span><br><span style = 'font-size:15px;color:#655F5D;font-family:Arial,Helvetica,sans-serif'> Correo electrónico: <a href = 'mailto:" + email + "' target = '_blank' style = 'color:#0056bf;text-decoration: none;'>" + email + "</a></span><br>" +
                            "</td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr><tr><td><table class='m_-8535930574907526684content' align='center' style='width:100%;max-width:600px'><tbody><tr><td bgcolor = '#F5F5F5' class='m_-8535930574907526684innerpadding'style='padding:5px 30px 30px 30px'><table align='center' border='0' cellpadding='0' cellspacing='0'><tbody><tr><td style ='color:#90949c;font-size:12px;line-height:1.4;font-family:Arial,Helvetica,sans-serif'><br><br><span>Editorial Humus, Coordinación General de Investigación.</span><br><span> Escuela Superior Politécnica Agropecuaria de Manabí Manuel Félix López.</span><br><span>Ecuador - Manabí - Bolivar.</span><br> ® "+year+" Editorial Humus, Inc.<br></td>" +
                            " </tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table>";




                mail.Body = htmlBody;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("edicionesHU@gmail.com", "mwat most cwuu zpfc");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);



            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }





        }








        //Función para archivar libro
        public ActionResult ArchivarLibro(int Idlibro)
        {
            int r = 0;
 
            try
            {
                r = conexLibro.ArchivarLibro(Idlibro);
               


                if (r == 1)
                {
                    TempData["OK"] = "Libro archivado";
                }

                else
                {
                    TempData["ERROR"] = "Algo salió mal";
                }

            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";

            }

            return RedirectToAction("ListaLibros");
        }







        //Función para eliminar libro//
        [HttpPost]
        public JsonResult EliminarLibro(int Idlibro,int Iduser)
        {
            int r = 0;
            try
            {
                r = conexLibro.EliminarLibro(Idlibro,Iduser);
                if (r == 1)
                {
                    
                    TempData["OKL"] = "OK";
                }
                else
                {
                    TempData["ERROR"] = "Algo salió mal";
                }
            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";

            }

            return Json(r, JsonRequestBehavior.AllowGet);
        }



    





        //Función para aceptar o denegar solicitud de nuevo libro
        [HttpPost]
        public JsonResult AceptaroDenegarLibro(int Idlibro,int aceptado,int Iduser)
        {
            int r = 0;
            string detalle = "";
            string notificacion = "";
            string url = "#";
            string visible = "Autor";
            string fecha = System.DateTime.Now.ToString("dd/MM/yyyy" + " - " + "H:mm tt");
            bool general = false;
            string Nombre1 = "";
            string Nombre2 = "";
            string ApellidoP = "";
            string ApellidoS = "";
            string Email = "";
            string Username = "";
            string Telefono = "";
            string Direccion = "";
            string Filial = "";
            int y, n = 0;

            try
            {
                r = conexLibro.AceptaroDenegarLibro(Idlibro,aceptado,fecha,Iduser);
                //insertar autor

              
                

                if (r == 1)
                {

                    foreach (var item in conexUser.VerDetalleUsuario(Iduser))
                    {
                        Nombre1 = item.PrimerNombre;
                        Nombre2 = item.SegundoNombre;
                        ApellidoP = item.ApellidoPrimero;
                        ApellidoS = item.ApellidoSegundo;
                        Email = item.Email;
                        Username = item.Username;
                        Telefono = item.Telefono;
                        Direccion = item.Direccion;
                        Filial = item.Filial;
                    }

                    y = conexAutor.InsertarAutor(Idlibro, Nombre1,Nombre2, ApellidoP,ApellidoS, Username, Email, Telefono, Direccion, Filial);



                    //crear notificación para autor 
                    //enviar correo con notificación
                    detalle = "Solicitud de libro aceptada";
                    notificacion = "La solicitud para publicar un nuevo libro ha sido aceptada, ya puede iniciar con los procesos correspondientes para la publicación de su libro.";
                    url = "Libro/Libros";


                    n = conexNotificacion.InsertarNotificacion(notificacion, url, visible, Iduser, 2, fecha, detalle, general);

                    SendEmail(notificacion, Iduser, detalle);



                    TempData["OKA"] = "Libro aceptado";
                }
                else if (r == 2)
                {
                    detalle = "Solicitud de libro no aceptada";
                    notificacion = "La solicitud para publicar un nuevo libro no ha sido aceptada. Vuelva a intentarlo en otro momento.";
                  
                    n = conexNotificacion.InsertarNotificacion(notificacion, url, visible, Iduser, 2, fecha, detalle, general);

                    SendEmail(notificacion, Iduser, detalle);


                    TempData["OKE"] = "Libro no aceptado";
                }
                else
                {
                    TempData["ERROR"] = "Algo salió mal";
                }

            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";

            }

            return Json(r, JsonRequestBehavior.AllowGet);
        }




    }
}
