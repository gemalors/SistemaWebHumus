using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Negocio.Metodos;

namespace WebApplication1.Controllers
{
    public class WebController : Controller
    {
       
        M_Usuarios conexUser = new M_Usuarios();
        M_DatosContactoEditorial conexDatosEditorial = new M_DatosContactoEditorial();
        M_InformacionEditorial conexInfoWeb = new M_InformacionEditorial();
        M_Formatos conexFormatos = new M_Formatos();
        M_SlidersEditorial conexSliders = new M_SlidersEditorial();
        M_Noticias conexNoticias = new M_Noticias();
        M_Notificaciones conexNotificacion = new M_Notificaciones();
       

       



        //Vista para editar  o agregar información de la página web
        public ActionResult EditarInfoWeb()
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

            ViewBag.Sliders = conexSliders.VerSliders();
            ViewBag.Noticias = conexNoticias.VerNoticiasEditorial();
            ViewBag.InfoEditorial = conexInfoWeb.VerInformacionEDitorial();
            ViewBag.Formatos = conexFormatos.ConsultarFormatos();
            ViewBag.Tipoformatos = conexFormatos.VerTiposformatos();
            ViewBag.DatosEditorial = conexDatosEditorial.VerDatosContactoEditorial();
            ViewBag.Usuario = conexUser.BuscarUsuario(Session["Username"].ToString());
   
            return View();
        }



       



        //Vista de error
        public ActionResult Error()
        {
           

            return View();
        }







        //Vista de detalle de noticia registrada
        public ActionResult DetalleNoticia(int Idn)
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

            ViewBag.NoticiaDetalle = conexNoticias.VerDetalleNoticiasEditorial(Idn);
            ViewBag.ImagenesNoticia = conexNoticias.VerImagenesdeNoticia(Idn);
            ViewBag.Usuario = conexUser.BuscarUsuario(Session["Username"].ToString());
       
            return View();
        }






        //Función para registrar imágenes de noticia
        [HttpPost]
        public ActionResult RegistroImagen(int Idn1, HttpPostedFileBase imagenN)
        {
            string fecha1 = System.DateTime.Now.ToString("dd-MM-yyyy" + "H-mm");
            int r = 0;
            string rutap = Server.MapPath("~/Files/ImagenNoticias/");

            try
            {

                string Filename = "ImgNoticia_" + fecha1 + ".png";
                imagenN.SaveAs(rutap + Filename);


                r = conexNoticias.AgregarImagenNoticia(Filename, Idn1);

                if (r == 1)
                {
                    TempData["OK"] = "Imagen agregada correctamente";
                }
                else
                {
                    TempData["ERROR"] = "Algo salió mal";
                }
            }
            catch
            {
                return RedirectToAction("EditarInfoWeb");

            }
            return RedirectToAction("EditarInfoWeb");
        }





        //Función para publicar noticia
        public ActionResult PublicarNoticia(int Idnoticia)
        {
            DateTime fechap = System.DateTime.Now;
            string fecha1 = fechap.ToString("dd/MM/yyyy");
            int r = 0;
         
            string urlacceso = "https://localhost:44329/Editorial/Noticia?n="+Idnoticia+"";

            try
            {

               
                r = conexNoticias.PublicarNoticia(Idnoticia, fecha1,fechap,urlacceso);

                if (r == 1)
                {
                    TempData["OK"] = "Noticia publicada correctamente";
                }
                else
                {
                    TempData["ERROR"] = "Algo salió mal";
                }
            }
            catch
            {
                return RedirectToAction("EditarInfoWeb");

            }
            return RedirectToAction("EditarInfoWeb");
        }





        //Función para editar imágenes de noticia
        [HttpPost]
        public ActionResult EditarImagen(int IdImg,int idn, HttpPostedFileBase imagenN)
        {
            string fecha1 = System.DateTime.Now.ToString("dd-MM-yyyy" + "H-mm");
            int r = 0;
            string rutap = Server.MapPath("~/Files/ImagenNoticias/");

            try
            {

                string Filename = "ImgNoticia_" + fecha1 + ".png";
                imagenN.SaveAs(rutap + Filename);


                r = conexNoticias.EditarImagenNoticia(IdImg, Filename);

                if (r == 1)
                {
                    TempData["OK"] = "Imagen editada correctamente";
                }
                else
                {
                    TempData["ERROR"] = "Algo salió mal";
                }
            }
            catch
            {
                return RedirectToAction("DetalleNoticia", new RouteValueDictionary(new { controller = "Web", action = "DetalleNoticia", Idn = idn }));
            

            }
            return RedirectToAction("DetalleNoticia", new RouteValueDictionary(new { controller = "Web", action = "DetalleNoticia", Idn = idn}));
         
        }





        //Función para eliminar imágenes de noticia
        public JsonResult EliminarImagen(int IdImg)
        {
            int r = 0;
            try
            {
                r = conexNoticias.EliminarImagenNoticia(IdImg);

                if (r == 1)
                {
                    TempData["OKE"] = "OK";


                }

            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";

            }

            return Json(r, JsonRequestBehavior.AllowGet);
        }






        //Función para editar datos de contacto de la editorial en página web
        [HttpPost]
        public ActionResult EditarDatosContacto(int Iddatos, string email, string horario, string direccion, string telefono)
        {
            int r = 0;
            try
            {
                r = conexDatosEditorial.EditarDatosContacto(Iddatos, email, horario, direccion, telefono);

                if (r == 1)
                {
                    TempData["OK"] = "Datos de contacto editados correctamente";
                }
                else
                {
                    TempData["ERROR"] = "Algo salió mal";
                }
            }
            catch
            {
                return RedirectToAction("EditarInfoWeb");
         
            }
            return RedirectToAction("EditarInfoWeb");
        }






        //Función para registrar datos de slider en página web
        [HttpPost]
        public ActionResult RegistroSlider(string url, HttpPostedFileBase imagen)
        {
            string fecha1 = System.DateTime.Now.ToString("dd-MM-yyyy" + "H-mm");
            int r = 0;
            string rutap = Server.MapPath("~/Files/ImagenSliders/");

            if (url == null)
            {
                url = "#";
            }
            try
            {

                string Filename = "ImgSlider_" + fecha1 + ".png";
               
                imagen.SaveAs(rutap + Filename);

              


                r = conexSliders.AgregarSlider(Filename,url);

                if (r ==1)
                {
                    TempData["OK"] = "Slider publicado correctamente";
                }
                else
                {
                    TempData["ERROR"] = "Algo salió mal";
                }
            }
            catch
            {
                return RedirectToAction("EditarInfoWeb");

            }
            return RedirectToAction("EditarInfoWeb");
        }






        //Función para editar datos de slider en página web
        [HttpPost]
        public ActionResult EditarSlider(int Id1, string url1, HttpPostedFileBase imagen, string imagen1)
        {
            string fecha1 = System.DateTime.Now.ToString("dd-MM-yyyy" + "H-mm");
            int r = 0;
            string rutap = Server.MapPath("~/Files/ImagenSliders/");
            string Filename;

            if (url1 == null)
            {
                url1 = "#";
            }
            try
            {
                if (imagen == null)
                {
                    Filename = imagen1;
                    if(Filename=="" || Filename == null)
                    {
                        TempData["ERROR"] = "Es necesario que agregue el archivo correspondiente a la imagen";
                        return RedirectToAction("EditarInfoWeb");
                    }

                }
                else
                {

                    Filename = "ImgSlider_" +fecha1 + ".png";
                    imagen.SaveAs(rutap + Filename);
                 
                   

                }

                


                r = conexSliders.EditarSlider(Id1, Filename, url1);

                if (r ==1)
                {
                    TempData["OK"] = "Datos editados correctamente";
                }
                else
                {
                    TempData["ERROR"] = "Algo salió mal";
                }
            }
            catch
            {
                return RedirectToAction("EditarInfoWeb");

            }
            return RedirectToAction("EditarInfoWeb");
        }





      
        //Función para eliminar slider en página web
        public JsonResult EliminarSlider(int Idslider)
        {
            int r = 0;
            try
            {
                r = conexSliders.EliminarSlider(Idslider);

                if (r == 1)
                {
                    TempData["OKE"] = "OK";


                }

            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";

            }

            return Json(r, JsonRequestBehavior.AllowGet);
        }





        //Función para registrar datos de noticia en página web
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult RegistroNoticia(string titulon, string detallen,string descripn, HttpPostedFileBase imagen)
        {
           
            string fecha1 = System.DateTime.Now.ToString("dd-MM-yyyy"+ "H-mm");
            int r = 0;
            string rutap = Server.MapPath("~/Files/ImagenNoticias/");

            try
            {

                string Filename = "ImgNoticia_" + fecha1 + ".png";
                imagen.SaveAs(rutap + Filename);
                Filename = "https://localhost:44329/Files/ImagenNoticias/" + Filename;

                r = conexNoticias.AgregarNoticia(detallen,descripn,Filename,titulon);

                if (r == 1)
                {
                    TempData["OK"] = "Noticia registrada correctamente";
                }
                else
                {
                    TempData["ERROR"] = "Algo salió mal";
                }
            }
            catch
            {
                return RedirectToAction("EditarInfoWeb");

            }
            return RedirectToAction("EditarInfoWeb");
        }







        //Función para editar datos de noticia en página web
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditarNoticia(int Idn, string titulon1, string detallen1, string descripn1, HttpPostedFileBase imagen, string imagen2n)
        {
          
            string fecha1 = System.DateTime.Now.ToString("dd-MM-yyyy" + "H-mm");
            int r = 0;
            string rutap = Server.MapPath("~/Files/ImagenNoticias/");
            string Filename;
            try
            {
                if (imagen == null)
                {
                    Filename = imagen2n;
                    if (Filename == "" || Filename == null)
                    {
                        TempData["ERROR"] = "Es necesario que agregue el archivo correspondiente a la imagen";
                        return RedirectToAction("EditarInfoWeb");
                    }

                }
                else
                {
                    Filename = "ImgNoticia_" + fecha1 + ".png";
                   
                    imagen.SaveAs(rutap + Filename);
                    Filename = "https://localhost:44329/Files/ImagenNoticias/" + Filename;

                }




                r = conexNoticias.EditarNoticia(Idn,detallen1,descripn1,Filename,titulon1);

                if (r == 1)
                {
                    TempData["OK"] = "Noticia editada correctamente";
                }
                else
                {
                    TempData["ERROR"] = "Algo salió mal";
                }
            }
            catch
            {
                return RedirectToAction("EditarInfoWeb");

            }
            return RedirectToAction("EditarInfoWeb");
        }






        //Función para eliminar noticia en página web
        public JsonResult EliminarNoticia(int Idnoticia)
        {
            int r = 0;
            try
            {
                r = conexNoticias.EliminarNoticia(Idnoticia);

                if (r == 1)
                {
                    TempData["OKE1"] = "OK";


                }

            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";

            }

            return Json(r, JsonRequestBehavior.AllowGet);
        }






        //Función para registrar datos de información de la Editorial en página web
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult RegistroInfoEditorial(string tituloinfo, string descripinfo, HttpPostedFileBase imageninfo, string url,int pagina)
        {
            string fecha1 = System.DateTime.Now.ToString("dd-MM-yyyy" + "H-mm");
            int r = 0;
            string rutap = Server.MapPath("~/Files/ImagenInfoEditorial/");

            try
            {

                string Filename = "ImgInfo_" + fecha1 + ".png";
                imageninfo.SaveAs(rutap + Filename);
            

                r = conexInfoWeb.AgregarInformacionEditorial(tituloinfo, descripinfo, Filename, url,pagina);

                if (r == 1)
                {
                    TempData["OK"] = "Información publicada correctamente";
                }
                else
                {
                    TempData["ERROR"] = "Algo salió mal";
                }
            }
            catch
            {
                return RedirectToAction("EditarInfoWeb");

            }
            return RedirectToAction("EditarInfoWeb");
        }






        //Función para editar datos de información de la Editorial en página web
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditarInfoEditorial(int idinfo1, string tituloinfo1, string descripinfo1, HttpPostedFileBase imageninfo2, string imageninfo1,int pagina1)
        {
            string fecha1 = System.DateTime.Now.ToString("dd-MM-yyyy" + "H-mm");
            int r = 0;
            string url = null;
            string rutap = Server.MapPath("~/Files/ImagenInfoEditorial/");
            string Filename;
            try
            {
                if (imageninfo2 == null)
                {
                    Filename = imageninfo1;
                    if (Filename == "" || Filename == null)
                    {
                        TempData["ERROR"] = "Es necesario que agregue el archivo correspondiente a la imagen";
                        return RedirectToAction("EditarInfoWeb");
                    }

                }
                else
                {

                    Filename = "ImgInfo_" + fecha1 + ".png";
                    imageninfo2.SaveAs(rutap + Filename);
               


                }




                r = conexInfoWeb.EditarInformacionEditorial(idinfo1, tituloinfo1, descripinfo1, Filename, url,pagina1);

                if (r == 1)
                {
                    TempData["OK"] = "Información editada correctamente";
                }
                else
                {
                    TempData["ERROR"] = "Algo salió mal";
                }
            }
            catch
            {
                return RedirectToAction("EditarInfoWeb");

            }
            return RedirectToAction("EditarInfoWeb");
        }





        //Función para eliminar información de la Editorial en página web
        public JsonResult EliminarInfoEditorial(int Idinfo)
        {
            int r = 0;
            try
            {
                r = conexInfoWeb.EliminarInformacionEditorial(Idinfo);

                if (r == 1)
                {
                    TempData["OKE2"] = "OK";


                }

            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";

            }

            return Json(r, JsonRequestBehavior.AllowGet);
        }





        //Función para registrar tipo de formatos para página web
        [HttpPost]
        public ActionResult RegistroTipoFormato(string detalletp)
        {
           
            int r = 0;
          

            try
            {


                r = conexFormatos.CrearTipoformatos(detalletp);

                if (r !=0)
                {
                    TempData["OK"] = "Tipo de formato registrado correctamente";
                }
                else
                {
                    TempData["ERROR"] = "Algo salió mal";
                }
            }
            catch
            {
                return RedirectToAction("EditarInfoWeb");

            }
            return RedirectToAction("EditarInfoWeb");
        }





        //Función para editar tipo de formatos para página web
        [HttpPost]
        public ActionResult EditarTipoFormato(int Idtp, string detalletp1)
        {
           
            int r = 0;
         
        
            try
            {
                
                r = conexFormatos.EditarTipoformatos(Idtp,detalletp1);

                if (r == 1)
                {
                    TempData["OK"] = "Datos editados correctamente";
                }
                else
                {
                    TempData["ERROR"] = "Algo salió mal";
                }
            }
            catch
            {
                return RedirectToAction("EditarInfoWeb");

            }
            return RedirectToAction("EditarInfoWeb");
        }






        //Función para eliminar tipo de formatos para página web
        public JsonResult EliminarTipoformato(int Idtp)
        {
            int r = 0;
            try
            {
                r = conexFormatos.EliminarTipoformatos(Idtp);

                if (r == 1)
                {
                    TempData["OKE3"] = "OK";


                }

            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";

            }

            return Json(r, JsonRequestBehavior.AllowGet);
        }






        //Función para obtener extensión de un archivo
        private string GetExtension(string attachment_name)
        {
            var index_point = attachment_name.IndexOf(".") + 1;
            return attachment_name.Substring(index_point);
        }





        //Función para registrar formatos para página web
        [HttpPost]
        public ActionResult RegistroFormato(string detallef, string descripcionf,int idtipo, HttpPostedFileBase archivof)
        {
           
            int r = 0;
            string rutap = Server.MapPath("~/Files/FormatosWeb/");

            try
            {

                string Extension = GetExtension(archivof.FileName);
                string NombreDoc ="Formato_de_" + detallef + "." + Extension;

               
                if (Extension == "txt" || Extension == "doc" || Extension == "docx" || Extension == "pdf" || Extension == "rtf")
                {

                    archivof.SaveAs(rutap + NombreDoc);
                    NombreDoc = "https://localhost:44329/Files/FormatosWeb/" + NombreDoc;
                    r =conexFormatos.CrearFormatos(NombreDoc,detallef,idtipo,descripcionf);

                   

                }
                else
                {
                    TempData["ERROR"] = "Formato de archivo no aceptado";
                }



                if (r == 1)
                {
                    TempData["OK"] = "Formato publicado correctamente";
                }
                else
                {
                    TempData["ERROR"] = "Algo salió mal";
                }
            }
            catch
            {
                return RedirectToAction("EditarInfoWeb");

            }
            return RedirectToAction("EditarInfoWeb");
        }






        //Función para editar formatos para página web
        [HttpPost]
        public ActionResult EditarFormato(int Idf, string detallef1, string descripcionf1, int idtipof, string archivo1, HttpPostedFileBase archivof)
        {
            
            int r = 0;
            string rutap = Server.MapPath("~/Files/FormatosWeb/");
            string NombreDoc;
            try
            {

                
                if (archivof == null)
                {
                    NombreDoc = archivo1;
                    if (NombreDoc == "" || NombreDoc == null)
                    {
                        TempData["ERROR"] = "Es necesario que agregue el archivo correspondiente";
                        return RedirectToAction("EditarInfoWeb");
                    }
                    else
                    {
                      
                        r = conexFormatos.EditarFormatos(Idf, NombreDoc, detallef1, idtipof, descripcionf1);

                    }

                }
                else
                {

                    string Extension = GetExtension(archivof.FileName);
                    NombreDoc = "Formato_de_" + detallef1 + "." + Extension;



                    if (Extension == "txt" || Extension == "doc" || Extension == "docx" || Extension == "pdf" || Extension == "rtf")
                    {

                        archivof.SaveAs(rutap + NombreDoc);
                        NombreDoc = "https://localhost:44329/Files/FormatosWeb/" + NombreDoc;
                        r = conexFormatos.EditarFormatos(Idf, NombreDoc, detallef1, idtipof, descripcionf1);



                    }
                    else
                    {
                        TempData["ERROR"] = "Formato de archivo no aceptado";
                    }



                }



                if (r == 1)
                {
                    TempData["OK"] = "Formato editado correctamente";
                }
                else
                {
                    TempData["ERROR"] = "Algo salió mal";
                }
            }
            catch
            {
                return RedirectToAction("EditarInfoWeb");

            }
            return RedirectToAction("EditarInfoWeb");
        }








        //Función para eliminar formatos para página web
        public JsonResult EliminarFormato(int Idf)
        {
            int r = 0;
            try
            {
                r = conexFormatos.EliminarFormato(Idf);

                if (r == 1)
                {
                    TempData["OKE4"] = "OK";


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

