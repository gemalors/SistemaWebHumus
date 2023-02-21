using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Negocio.Metodos;
using WebApplication1.Properties;
using PagedList;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{

    //message.Subject=Model.Subject;
    //message.Body=ModelBinderAttribute.Body; 


    public class EditorialController : Controller
    {



        M_Libros conexLibro = new M_Libros();
        M_Procesos conexProce = new M_Procesos();
        M_Autores conexAutor = new M_Autores();
        M_Categorias conexCate = new M_Categorias();
        M_DatosContactoEditorial conexDatosEditorial = new M_DatosContactoEditorial();
        M_InformacionEditorial conexInfoWeb = new M_InformacionEditorial();
        M_Formatos conexFormatos = new M_Formatos();
        M_SlidersEditorial conexSliders = new M_SlidersEditorial();
        M_Noticias conexNoticias = new M_Noticias();
        M_Usuarios conexUser = new M_Usuarios();
        M_Documentos conexDoc = new M_Documentos();
        M_Carreras conexcarrera = new M_Carreras();






        //Vista  de inicio- página web- información de editorial
        public ActionResult Inicio()
        {
            try
            {
                ViewBag.Librosrecientes = conexLibro.TopLibrosRecientes();
                ViewBag.InfoEditorial = conexInfoWeb.VerInformacionEDitorial();

                ViewBag.Sliders = conexSliders.VerSliders();
                ViewBag.DatosEditorial = conexDatosEditorial.VerDatosContactoEditorial();
            }
            catch (Exception)
            {
                return View("Error", "Editorial");
            }
           
            return View();
        }






      







        //Vista de detalle de noticia xid
        public ActionResult Noticia(int n)
        {
            try
            {
                ViewBag.NoticiaDetalle = conexNoticias.VerDetalleNoticiasEditorial(n);
                ViewBag.ImagenesNoticia = conexNoticias.VerImagenesdeNoticia(n);
            }
            catch (Exception)
            {
                return View("Error", "Editorial");
            }
            

            return View();
        }






        //Vista de detalle de libro xid
        public ActionResult Titulo(int lib)
        {
            try
            {
                //DateTime fecha = System.DateTime.Now;
                ////para obtener ip cliente
                //string ip = Request.UserHostAddress;

                //int r = conexLibro.RegistroVistasLibro(ip, fecha, lib);
                ViewBag.Autores = conexAutor.VerAutores(lib);
                ViewBag.CitaLibro = conexLibro.VerCitaLibro(lib);
                ViewBag.LibroDetalle = conexLibro.VerDetalleLibro(lib);
            }
            catch (Exception)
            {
                return View("Error","Editorial") ;
            }
           
            return View();
        }






        //Función para agregar nueva vista a libro
        [HttpPost]
        public JsonResult AgregarVista(int lib)
        {
            int r = 0;
            try
            {
                DateTime fecha = System.DateTime.Now;
                ////para obtener ip cliente
                string ip = Request.UserHostAddress;

                 r = conexLibro.RegistroVistasLibro(ip, fecha, lib);
           
              

            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";

            }

            return Json(r, JsonRequestBehavior.AllowGet);
        }







     


        public ActionResult Error()
        {
            return View();
        }







        //Vista de búsqueda filtrada
        public  ActionResult Busqueda(string buscador,int? PageSize,int? idcarrera, int? yearp,int? idcate,int?tipobusca, int? page)
        {
            try
            {
                ViewBag.Carreras = conexcarrera.VerCarreras();
                ViewBag.Categorias = conexCate.Vercategorias();

                var lista = conexLibro.BusquedaLibrosTitulo(buscador);

                PageSize = (PageSize ?? 10);
                page = (page ?? 1);
                tipobusca = (tipobusca ?? 0);
                idcarrera = (idcarrera ?? 0);
                idcate = (idcate ?? 0);
                yearp = (yearp ?? 0);

             
                ViewBag.pagesize = PageSize;
                ViewBag.tbusca = tipobusca;
                ViewBag.buscador = buscador;

               

                if (tipobusca == 1)
                {
                    //if (yearp <= 0)
                    //{
                    //    ViewBag.Alerta = "No se aceptan valores negativos en el parámetro de año de publicación, vuelva a intentarlo con un valor correcto.";
                    //    return View(lista.ToPagedList(page.Value, PageSize.Value));


                    //}
                    lista = conexLibro.BusquedaLibrosTodoParametros(buscador, Convert.ToInt32(idcarrera), Convert.ToInt32(yearp), Convert.ToInt32(idcate));

                    ViewBag.buscador = buscador;
                    ViewBag.idcarrera = idcarrera;
                    ViewBag.idcate = idcate;
                    ViewBag.yp = yearp;


                }
                if (tipobusca == 2)
                {
                    lista = conexLibro.BusquedaLibrosTitulo(buscador);

                    ViewBag.buscador = buscador;
                    ViewBag.idcarrera = 0;
                    ViewBag.idcate = 0;
                    ViewBag.yp = "";

                }
                if (tipobusca == 3)
                {
                    lista = conexLibro.BusquedaLibrosCodigoISBN(buscador);

                    ViewBag.buscador = buscador;
                    ViewBag.idcarrera = 0;
                    ViewBag.idcate = 0;
                    ViewBag.yp = "";

                }
                if (tipobusca == 4)
                {
                    lista = conexLibro.BusquedaLibrosAutores(buscador);

                    ViewBag.buscador = buscador;
                    ViewBag.idcarrera = 0;
                    ViewBag.idcate = 0;
                    ViewBag.yp = "";

                }
                if (tipobusca == 5)
                {
                   
                    //if (yearp <= 0)
                    //{
                    //    ViewBag.Alerta = "No se aceptan valores negativos en el parámetro de año de publicación, vuelva a intentarlo con un valor correcto.";
                    //    return View(lista.ToPagedList(page.Value, PageSize.Value));


                    //}
                    lista = conexLibro.BusquedaLibrosYearPublicacion(buscador, Convert.ToInt32(yearp));
                    ViewBag.buscador = buscador;
                    ViewBag.idcarrera = 0;
                    ViewBag.idcate = 0;
                    ViewBag.yp = yearp;

                }
                if (tipobusca == 6)
                {
                    lista = conexLibro.BusquedaLibrosxCarrera(buscador, Convert.ToInt32(idcarrera));
                    ViewBag.buscador = buscador;
                    ViewBag.idcarrera = idcarrera;
                    ViewBag.idcate = 0;
                    ViewBag.yp = "";

                }
                if (tipobusca == 7)
                {
                    lista = conexLibro.BusquedaLibrosxCategoria(buscador, Convert.ToInt32(idcate));
                    ViewBag.buscador = buscador;
                    ViewBag.idcarrera = 0;
                    ViewBag.idcate = idcate;
                    ViewBag.yp = "";

                }
                if (tipobusca == 8)
                {
                    lista = conexLibro.MostrarLibrosxCarrera(Convert.ToInt32(idcarrera));
                    ViewBag.buscador = "";
                    ViewBag.idcarrera = idcarrera;
                    ViewBag.idcate = 0;
                    ViewBag.yp = "";

                }
                if (tipobusca == 9)
                {
                    lista = conexLibro.MostrarLibrosxCategoria(Convert.ToInt32(idcate));
                    ViewBag.buscador = "";
                    ViewBag.idcarrera = 0;
                    ViewBag.idcate = idcate;
                    ViewBag.yp = "";

                }
                if (tipobusca == 10)
                {
                    lista = conexLibro.MostrarLibrosxYear(Convert.ToInt32(yearp));
                    ViewBag.buscador = "";
                    ViewBag.idcarrera = 0;
                    ViewBag.idcate = 0;
                    ViewBag.yp = yearp;

                }

                return View(lista.ToPagedList(page.Value, PageSize.Value));


            }
            catch (Exception)
            {
                return View("Error", "Editorial");
            }
           

           
        
        }






        //Vista de noticias
        public ActionResult Noticias(int? PageSize,int? page)
        {

           
            try
            {
                ViewBag.Carreras = conexcarrera.VerCarreras();
                ViewBag.Categorias = conexCate.Vercategorias();

                var lista = conexNoticias.VerNoticiasEditorial();

                PageSize = (PageSize ?? 12);
                page = (page ?? 1);
            


                ViewBag.pagesize = PageSize;
          


             
           

                return View(lista.ToPagedList(page.Value, PageSize.Value));


            }
            catch (Exception)
            {
                return View("Error", "Editorial");
            }




        }






        //Vista de libros
        public ActionResult Titulos(int? PageSize, int? page,int? tipobusca)
        {
            try
            {


                PageSize = (PageSize ?? 20);
                page = (page ?? 1);
                tipobusca = (tipobusca ?? 1);

                var lista = conexLibro.VerTodosLosLibros();

                ViewBag.pagesize = PageSize;
                ViewBag.page = page;
                ViewBag.tipo = tipobusca;

                //todos los libros
                if (tipobusca == 1)
                {
                    lista = conexLibro.VerTodosLosLibros();


                }
                // libros populares
                if (tipobusca == 2)
                {
                    lista = conexLibro.VerLibrosPopulares();


                }
                //libros recientes
                if (tipobusca == 3)
                {

                   lista = conexLibro.VerLibrosRecientes();
                }


             
           

                return View(lista.ToPagedList(page.Value, PageSize.Value));


            }
            catch (Exception)
            {
                return View("Error", "Editorial");
            }




        }







        //Vista de información de procesos editoriales - página web
        public ActionResult Procesos()
        {

            try
            {
                ViewBag.Formatos = conexFormatos.ConsultarFormatos();
                ViewBag.InfoEditorial = conexInfoWeb.VerInformacionEDitorial();
            }
            catch (Exception)
            {
                return View("Error", "Editorial");
            }
         
          
           
            return View();
        }




      



        //Vista  de contacto- página web- formulario de contacto
        public ActionResult Contacto()
        {
            try
            {
                ViewBag.DatosEditorial = conexDatosEditorial.VerDatosContactoEditorial();
            }
            catch (Exception)
            {
                return View("Error", "Editorial");
            }
           
            return View();
        }







        //Función de envío de mensaje desde página web al correo de la Editorial
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EnviarMensaje(string name,string message, string email)
        {
           

            try
            {

                

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
               
                mail.From = new MailAddress("edicionesHU@gmail.com");
                mail.To.Add("edicionesHU@gmail.com");
                mail.Subject = "Mensaje desde Sitio Web";
                //mail.Attachments.Add(new Attachment(path));
              

                mail.IsBodyHtml = true;
                string htmlBody1 = BodyEmail(message, email, name);



                mail.Body = htmlBody1;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("edicionesHU@gmail.com", "mwat most cwuu zpfc");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
           
            }
            catch(Exception )
            {
                //throw new Exception(ex.Message);
                return RedirectToAction("Error","Editorial");
            }



            return RedirectToAction("Contacto");
            //return RedirectToRoute("http:/Files/");


        }

       

       






        //Función para el diseño del cuerpo del mensaje de correo enviado.
        private string BodyEmail(string message, string email, string name)
        {
            string date = DateTime.Now.ToString("MM-dd-yyyy");
            string year = date.Substring(6, 4);

            string htmlBody = "<table width='100%' bgcolor='#F5F5F5' border='0' cellpadding='0' cellspacing='0'><tbody><tr><td><table class='m_ - 8535930574907526684content' align='center' border='1' cellpadding='0' cellspacing='0' style='border-top:10px solid #348DE4;border-left:1px solid #C0C2C4 ;border-right:1px solid #C0C2C4 ; border-bottom:5px solid #348DE4;border-collapse:collapse;margin-top:30px;width:100%;max-width:600px' bgcolor='#fff'><tbody><tr><td class='m_ - 8535930574907526684header' style='padding: 20px 30px 20px 30px'>" +
                                "<table align='center' border='0' cellpadding='0' cellspacing='0' style='bgcolor = '#fff'><tbody><tr><td valign='top'><a href='https://localhost:44329/Editorial/Inicio' class='h1 text-secondary'><img src='https://raw.githubusercontent.com/gemalors/Imagenes/main/nlog2copia.png' class='light-logo'  border='0'/></a></td><td style='font-size:0;line-height:0' width='220px'>&nbsp;</td> <td align='right' width='300px'> <a href='https://localhost:44329/Usuario/Login'  style='font-size:18px;letter-spacing:0.02rem; color:#0056bf;font-family:Arial,Helvetica,sans-serif;text-decoration:None'  target='_blank'>Sistema web Humus</a></td></tr></tbody></table></td></tr>" +
            "<tr><td class='m_- 8535930574907526684innerpadding' style='padding: 30px 30px 30px 30px; border-top:1px solid #F2F5F9'><table><tbody><tr><td><br><br><span class='m_-8535930574907526684text-body text-justify' style='line-height:1.5; font-family:Arial,Helvetica,sans-serif; font-size:15px'>" + message + "</span></td></tr><tr><td style = 'font-size:0;line-height:0' height= '20px'>&nbsp;</td></tr><tr><td></td></tr></tbody></table></td></tr>" +
                              "<tr><td class='m_- 8535930574907526684innerpadding' style='padding: 30px 30px 30px 30px;  border-top:1px solid #F2F5F9'><table><tbody><tr></tr><tr><td><table align = 'center' border = '0' cellpadding = '0' cellspacing ='0'style = 'padding-top:30px'><tbody><tr><td width='548px' valign='top' style = 'font-family:Arial,Helvetica,sans-serif;font-size:16px;line-height:1.6'><span style = 'font-size:15px;color:#655F5D;font-family:Arial,Helvetica,sans-serif'> Emisor:<a style = 'color:#0056bf;text-decoration: none;'> " + name + "</a></span><br><span style = 'font-size:15px;color:#655F5D;font-family:Arial,Helvetica,sans-serif'> Correo electrónico: <a href = 'mailto:" + email + "' target = '_blank' style = 'color:#0056bf;text-decoration: none;'>" + email + "</a></span><br>" +
                              "</td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr><tr><td><table class='m_-8535930574907526684content' align='center' style='width:100%;max-width:600px'><tbody><tr><td bgcolor = '#F5F5F5' class='m_-8535930574907526684innerpadding'style='padding:5px 30px 30px 30px'><table align='center' border='0' cellpadding='0' cellspacing='0'><tbody><tr><td style ='color:#90949c;font-size:12px;line-height:1.4;font-family:Arial,Helvetica,sans-serif'><br><br><span>Editorial Humus, Coordinación General de Investigación.</span><br><span> Escuela Superior Politécnica Agropecuaria de Manabí Manuel Félix López.</span><br><span>Ecuador - Manabí - Bolivar.</span><br> ® "+year+" Editorial Humus, Inc.<br></td>" +
                              " </tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table>";


            return htmlBody.ToString();
        }



   
       




        

        

    }



}








//Versión 1 para envío de correo
//SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
//smtp.Credentials = new NetworkCredential("edicionesHU@gmail.com", "2021HUMUS");
//smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
//smtp.EnableSsl = true;
//smtp.UseDefaultCredentials = false;

//MailMessage mail = new MailMessage();
//mail.From = new MailAddress("edicionesHU@gmail.com", "Editorial HUMUS");
//mail.To.Add(new MailAddress("monserratlors@gmail.com"));
//mail.Subject = "Mensaje de Bienvenida";
//mail.IsBodyHtml = false;
//mail.Body = message;
//smtp.Send(mail);
//string body1 = "Mensaje de: "+name+"\n\n\n"+message+"\n\n\n"+"Email de contacto: "+email;

//string body1 = "<body>" +
//    "<h1 class='text-cyan'>Editorial Humus</h1>" +
//    "<h5 class='text-muted'>Usuario: " + name + "</h5>" +
//    "<span class='text-muted'>Email de contacto: " + email + "</span>" +
//    "<span class='text-muted'> " + message + "</span>" +
//    "<br/><br/><span></span>";




// Versión 2 para envío de correo
//    var fromAddress = new MailAddress("edicionesHU@gmail.com");
//    var fromPassword = "H)M)$202i";
//    var toAddress = new MailAddress("edicionesHU@gmail.com");

//    string subject = "Mensaje desde Sitio Web";
//    string body = body1;

//    System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient
//    {
//        Host = "smtp.gmail.com",
//        Port = 587,
//        EnableSsl = true,
//        DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
//        UseDefaultCredentials = false,

//        Credentials = new NetworkCredential(fromAddress.Address, fromPassword)

//    };

//    using (var message1 = new MailMessage(fromAddress, toAddress)
//    {
//        Subject = subject,
//        Body = body,
//        IsBodyHtml = true,
//}
//    )



//        smtp.Send(message1);
