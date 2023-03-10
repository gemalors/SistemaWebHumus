using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Negocio.Metodos;
using Negocio.Entidades;
using SimpleCrypto;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web.Routing;

namespace WebApplication1.Controllers
{
    public class UsuarioController : Controller
    {
        string urlDomain = "https://localhost:44329/";
     

        M_Usuarios conexUser = new M_Usuarios();
        M_Libros conexLibro = new M_Libros();
        M_Evaluadores conexEvaluador = new M_Evaluadores();
        M_Autores conexAutor =new M_Autores();
        M_Tiposproceso conexTipospro = new M_Tiposproceso();
        M_Notificaciones conexNotificacion = new M_Notificaciones();
       


        //Vista login
        public ActionResult Login()
        {
            try
            {
                if (Session["Type"] != null)
                {
                    if (Session["Type"].ToString() == "Autor")
                    {

                        return RedirectToAction("IndexAutor", "Usuario");

                    }
                    else if (Session["Type"].ToString() == "Evaluador")
                    {

                        return RedirectToAction("IndexEvaluador", "Usuario");
                    }
                    else if (Session["Type"].ToString() == "Administrador")
                    {

                        return RedirectToAction("IndexAdmin", "Usuario");

                    }

                }
            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";
                return RedirectToAction("Login", "Usuario");
                //return RedirectToAction("Error","Web");
            }
            

            return View();
        }






        //Función de autenticación - login
        [HttpPost]
        public ActionResult Logeo(string Username, string password)
        {
            try
            {

           
            ICryptoService cryptoService = new PBKDF2();

            foreach (var item in conexUser.BuscarUsuario(Username))
            {
                password = cryptoService.Compute(password, item.Salt);

                if (item.Foto != null)
                {
                    Session["Foto"] = item.Foto;
                }
            }

            foreach (var item in conexUser.LoginUsuarios(Username, password))
            {
                Session["Id"] = item.IDusuario;
                Session["Username"] = item.Username;
                Session["Type"] = item.Tipo_usuario;
                Session["Nombres"] = item.PrimerNombre + " " + item.ApellidoPrimero;


            }

               
            }
            catch (Exception)
            {
               
                TempData["ERROR"] = "Algo salió mal";
                return RedirectToAction("Login", "Usuario");
            }


            if (Session["Type"] == null)
            {

                TempData["ERROR"] = "Usuario o contraseña incorrecta";
                return RedirectToAction("Login", "Usuario");

            }
            else if (Session["Type"].ToString() == "Autor")
            {

                return RedirectToAction("IndexAutor", "Usuario");

            }
            else if (Session["Type"].ToString() == "Evaluador")
            {

                return RedirectToAction("IndexEvaluador", "Usuario");
            }
            else if (Session["Type"].ToString() == "Administrador")
            {

                return RedirectToAction("IndexAdmin", "Usuario");

            }

            return RedirectToAction("Login", "Usuario");

        }







        //Vista recuperar contraseña//
        public ActionResult ForgotPass()
        {
            return View();
        }








        //Función verificar email para recuperar contraseña//
        [HttpPost]
        public ActionResult VerificarEmail(string email)
        {
         
            try
            {

                    string token = GetSha256(Guid.NewGuid().ToString());

                    int x = conexUser.EditarToken(email, token);

                    if (x == 1)
                    {
                        SendEmail(email, token);
                        TempData["EXISTEE"] = "ok";
                        return RedirectToAction("Login", "Usuario");

                    }
                    else if (x == 2)
                    {
                        TempData["ERROR"] = "error";
                        return RedirectToAction("ForgotPass", "Usuario");
                    }

                


            }

            catch (Exception)
            {
                //    throw new Exception(ex.Message);
                TempData["ERROR"] = "Algo salió mal";
                return RedirectToAction("Login", "Usuario");
            }


            return RedirectToAction("ForgotPass", "Usuario");
        }







        //Función para generar token para recuperar contraseña
        private string GetSha256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return str.ToString();
        }







        //Envio de correo para recuperación de contraseña//
        private void SendEmail(string emaildest, string token)
        {
            try
            {
                string date = DateTime.Now.ToString("MM-dd-yyyy");
                string year = date.Substring(6, 4);
                string emailorigen = "edicionesHU@gmail.com";
                string url = urlDomain + "/Usuario/RecoveryPass/?token=" + token;

                string mensaje = "Has solicitado recuperar tu contraseña. Da click en el siguiente enlace para cambiar tu contraseña.";
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
             
                mail.From = new MailAddress("edicionesHU@gmail.com");
                mail.To.Add(emaildest);
                mail.Subject = "Solicitud de recuperación de contraseña";
          
                mail.IsBodyHtml = true;
          


                string htmlBody = "<table width='100%' bgcolor='#F5F5F5' border='0' cellpadding='0' cellspacing='0'><tbody><tr><td><table class='m_ - 8535930574907526684content' align='center' border='1' cellpadding='0' cellspacing='0' style='border-top:10px solid #348DE4;border-left:1px solid #C0C2C4 ;border-right:1px solid #C0C2C4 ; border-bottom:5px solid #348DE4;border-collapse:collapse;margin-top:30px;width:100%;max-width:600px' bgcolor='#fff'><tbody><tr><td class='m_ - 8535930574907526684header' style='padding: 20px 30px 20px 30px'>" +
                                "<table align='center' border='0' cellpadding='0' cellspacing='0' style='bgcolor = '#fff'><tbody><tr><td valign='top'><a href='https://localhost:44329/Editorial/Inicio' class='h1 text-secondary'><img src='https://raw.githubusercontent.com/gemalors/Imagenes/main/nlog2copia.png' class='light-logo'   border='0'/></a></td><td style='font-size:0;line-height:0' width='220px'>&nbsp;</td> <td align='right' width='300px'> <a href='https://localhost:44329/Usuario/Login'  style='font-size:18px;letter-spacing:0.02rem; color:#0056bf;font-family:Arial,Helvetica,sans-serif;text-decoration:None'  target='_blank'>Sistema web Humus</a></td></tr></tbody></table></td></tr>" +
                                "<tr><td class='m_- 8535930574907526684innerpadding' style='padding: 30px 30px 30px 30px; border-top:1px solid #F2F5F9'><table><tbody><tr><td><br><br><span class='m_-8535930574907526684text-body text-justify' style='line-height:1.5; font-family:Arial,Helvetica,sans-serif; font-size:15px'>" + mensaje + "</span><br/><br/><br/><a class='m_ - 8535930574907526684text - body' style='line-height:1.5; font-family:Arial,Helvetica,sans-serif; font-size:15px' href=" + url + ">Click aquí para recuperar contraseña</a> </td></tr><tr><td style = 'font-size:0;line-height:0' height= '20px'>&nbsp;</td></tr><tr><td></td></tr></tbody></table></td></tr>" +
                              "<tr><td class='m_- 8535930574907526684innerpadding' style='padding: 30px 30px 30px 30px;  border-top:1px solid #F2F5F9'><table><tbody><tr></tr><tr><td><table align = 'center' border = '0' cellpadding = '0' cellspacing ='0'style = 'padding-top:30px'><tbody><tr><td width='548px' valign='top' style = 'font-family:Arial,Helvetica,sans-serif;font-size:16px;line-height:1.6'><span style = 'font-size:15px;color:#655F5D;font-family:Arial,Helvetica,sans-serif'> Emisor:<a style = 'color:#0056bf;text-decoration: none;'> Editorial Humus</a></span><br><span style = 'font-size:15px;color:#655F5D;font-family:Arial,Helvetica,sans-serif'> Correo electrónico: <a href = 'mailto:" + emailorigen + "' target = '_blank' style = 'color:#0056bf;text-decoration: none;'>" + emailorigen + "</a></span><br>" +
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






        //Función de actualización de contraseña//
        [HttpPost]
        public ActionResult RecuperarP(string Pass, string Passc, string token)
        {

            try
            {

                foreach (var item in conexUser.VerificarToken(token))
                {

                    if (Pass == Passc)
                    {
                        int r = conexUser.RecuperarPass(token, Pass);
                        if (r == 1)
                        {
                            string email = item.Email;
                            token = null;
                            int x = conexUser.EditarToken(email, token);
                            TempData["EXISTE2"] = "Ok";
                            return RedirectToAction("Login", "Usuario");
                        }
                        else
                        {

                            TempData["ERROR"] = "Algo salió mal";
                            return RedirectToAction("RecoveryPass", new RouteValueDictionary(new { controller = "Usuario", action = "RecoveryPass", token = token }));
                        }
                    }
                    else
                    {
                        TempData["ERROR3"] = "Contraseñas no coinciden. Vuelva a intentarlo!";
                        return RedirectToAction("RecoveryPass", new RouteValueDictionary(new { controller = "Usuario", action = "RecoveryPass", token = token }));
                    }


                }
   

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
             
            }


            TempData["ERROR"] = "Tu token ha expirado!";

            return RedirectToAction("Login", "Usuario");


        }







        //Vista de actualización de contraseña//
        public ActionResult RecoveryPass(string token)
        {
            ViewBag.token = token;
            return View();
        }






        [HttpPost]
        //Función para cerrar sesión//
        public ActionResult Logout()
        {
            int r = 0;
            Session.Remove("Id");
            Session.Remove("Username");
            Session.Remove("Type");
            Session.Remove("Nombres");
            return Json(r, JsonRequestBehavior.AllowGet);
            //return RedirectToAction("Login", "Usuario");

        }








        //Vista index-administrador//
        public ActionResult IndexAdmin()
        {
            if (Session["Type"] == null)
            {
                TempData["ERRORD"] = "No tiene acceso a esta sección del sistema. Inicia sesión ";
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

            return View();

        }





        //Vista de notificaciones de administrador
        public ActionResult Notificaciones()
        {
            if (Session["Type"] == null)
            {
                TempData["ERRORD"] = "No tiene acceso a esta sección del sistema. Inicia sesión ";
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
            int ne = conexNotificacion.CambiarEstadoNotificacion(Id);
            ViewBag.TiposNotificaciones = conexNotificacion.VerTiposNotificaciones();
            var listaNotificaciones = conexNotificacion.VerTdodoNotificacionesxIduser(Id);
            ViewBag.NotificacionesRegistradas = conexNotificacion.VerNotificacionesRegistradas();
            foreach(var item in listaNotificaciones)
            {
                ntotal = Convert.ToInt32(item.TotalNotificacionesrecibidas);
                nnuevas = Convert.ToInt32(item.TotalNotificacionesnuevas);
                break;
            }
            ViewBag.TotalNotificaciones = ntotal;
            ViewBag.NuevasNotificaciones = nnuevas;
            ViewBag.Notificaciones = conexNotificacion.VerNotificacionesxIduser(Id);
            ViewBag.Usuario = conexUser.BuscarUsuario(Session["Username"].ToString());
            return View(listaNotificaciones);

        }






        //Vista de notificaciones de autor
        public ActionResult NotificacionesAutor()
        {
            if (Session["Type"] == null)
            {
                TempData["ERRORD"] = "No tiene acceso a esta sección del sistema. Inicia sesión ";
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

            int ne = conexNotificacion.CambiarEstadoNotificacion(Id);
            var listaNotificaciones = conexNotificacion.VerTdodoNotificacionesxIduser(Id);
            foreach (var item in listaNotificaciones)
            {
                ntotal = Convert.ToInt32(item.TotalNotificacionesrecibidas);
                nnuevas = Convert.ToInt32(item.TotalNotificacionesnuevas);
                break;
            }
            ViewBag.TotalNotificaciones = ntotal;
            ViewBag.NuevasNotificaciones = nnuevas;
            ViewBag.Notificaciones = conexNotificacion.VerNotificacionesxIduser(Id);
           
            ViewBag.Usuario = conexUser.BuscarUsuario(Session["Username"].ToString());
            return View(listaNotificaciones);

        }





        //Vista de notificaciones de evaluador
        public ActionResult NotificacionesEvaluador()
        {
            if (Session["Type"] == null)
            {
                TempData["ERRORD"] = "No tiene acceso a esta sección del sistema. Inicia sesión ";
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

            int ne = conexNotificacion.CambiarEstadoNotificacion(Id);
            var listaNotificaciones = conexNotificacion.VerTdodoNotificacionesxIduser(Id);
            foreach (var item in listaNotificaciones)
            {
                ntotal = Convert.ToInt32(item.TotalNotificacionesrecibidas);
                nnuevas = Convert.ToInt32(item.TotalNotificacionesnuevas);
                break;
            }
            ViewBag.TotalNotificaciones = ntotal;
            ViewBag.NuevasNotificaciones = nnuevas;
            ViewBag.Notificaciones = conexNotificacion.VerNotificacionesxIduser(Id);
           
            ViewBag.Usuario = conexUser.BuscarUsuario(Session["Username"].ToString());
            return View(listaNotificaciones);

        }





        //Vista index-autor//
        public ActionResult IndexAutor()
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

            ViewBag.Usuario = conexUser.BuscarUsuario(Session["Username"].ToString());
            return View();

        }





        //Vista index-evaluador//
        public ActionResult IndexEvaluador()
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
          
            return View();

        }





        //vista de lista de usuarios 
        public ActionResult ListaUsuarios()
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
            var listausers = conexUser.VerUsuarios();
            ViewBag.Tipos = conexUser.VerTiposUsuarios();

            return View(listausers);
        }

       




        //Vista de registro de usuarios//
        public ActionResult Registro()
        {
            string busca = "";

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

            int Idu = Convert.ToInt32(Session["Id"]);
            ViewBag.Notificaciones = conexNotificacion.VerNotificacionesxIduser(Idu);

            foreach (var item in ViewBag.Notificaciones)
            {
                ntotal = Convert.ToInt32(item.TotalNotificacionesrecibidas);
                nnuevas = Convert.ToInt32(item.TotalNotificacionesnuevas);
                break;
            }
            ViewBag.TotalNotificaciones = ntotal;
            ViewBag.NuevasNotificaciones = nnuevas;


            //ViewBag.DetalleUsuario = conexUser.VerDetalleUsuario(Id);
         
            //var lista = conexUser.BuscarUsuario(busca);
          

           


            ViewBag.Usuario = conexUser.BuscarUsuario(Session["Username"].ToString());
            ViewBag.Tipos = conexUser.VerTiposUsuarios();
            return View();
        }





        [HttpGet]
        public JsonResult ObtenerDatos(string ced)
        {


            var lista = conexUser.BuscarUsuario(ced);


            return Json(lista, JsonRequestBehavior.AllowGet);
        }




        // POST: Usuario/Crear usuario
        [HttpPost]
        public ActionResult Create(string Nombre1,string Nombre2, string ApellidoP,string ApellidoS, string Email, string Username, string Telefono, string Direccion, int IdTipousuario)
        {
            string password = Username;
            bool general = false;
            int r = 0;

            int c = ValidarCedula(Username);

            if (c == 2)
            {
                TempData["EXISTE"] = "Número de cédula no válido, verifique que el número este correctamente digitado y vuelva a intentarlo";
                return RedirectToAction("Registro", new RouteValueDictionary(new { controller = "Usuario", action = "Registro", Id = r }));

            }

            if (Telefono == "")
            {
                Telefono = "Sin actualizar";
            }
            if (Direccion == "")
            {
                Direccion = "Sin actualizar";
            }
           
            string Foto = null;
            string Filial = "Sin actualizar";
            string notificacion = "";
            string urlnotificacion = "";
            string visible = "";
            string detallenotificacion = "";
            int Iduser = 0;

            int libproceso = 0;

            try
            {

                if (IdTipousuario == 4)
                {
                    libproceso = 1;
                    Filial = "Escuela Superior Politécnica Agropecuaria de Manabí Manuel Felix López";
                }


                r = conexUser.Crearusuarios(Nombre1,Nombre2, ApellidoP,ApellidoS, Email, Username, password, Foto, IdTipousuario, Telefono, Direccion, Filial, libproceso);

                


                if (r == -1)
                {
                    TempData["EXISTE"] = "Este usuario ya existe";
                    return RedirectToAction("Registro", new RouteValueDictionary(new { controller = "Usuario", action = "Registro", Id = r }));

                }
                if(r==0)
                {
                    TempData["ERROR"] = "Algo salió mal";
                    return RedirectToAction("Registro", new RouteValueDictionary(new { controller = "Usuario", action = "Registro", Id = r }));

                }
                if(r>0)
                {

                    //enviar correo  con credenciales a usuario--
                    
                    SendEmailNotificacion("Bienvenido(a) a la Editorial Humus, tu credencial de acceso para usuario y contraseña es: " + Username + ". Puede cambiar estas credenciales cuando usted lo desee.", r, Nombre1 + " te damos la bienvenida");


                    if (IdTipousuario == 4)
                    {
                      
                        string titulo = "Libro sin iniciar";
                        string fechacreado = System.DateTime.Now.ToString("dd/MM/yyyy" + " - " + "H:mm tt");
                        string procesoL = "Sin iniciar";
                        string actualizadoL = System.DateTime.Now.ToString("dd/MM/yyyy" + " - " + "H:mm tt");
                        string estadoL = "Pendiente";
                        int progresoL = 0;
                        int aceptado = 1;

                        int x = conexLibro.CrearLibro(r, titulo, procesoL, progresoL, actualizadoL, fechacreado, estadoL,aceptado);
                 
                      
                        int y = conexAutor.InsertarAutor(x, Nombre1,Nombre2, ApellidoP,ApellidoS, Username, Email, Telefono, Direccion, Filial);

                        //crear notificación para autor
                        detallenotificacion = "Nuevo libro registrado";
                        notificacion = "Se ha registrado un nuevo libro. Antes de iniciar los procesos correspondientes proceda a editar el título del libro; y posteriormente envíe la documentación solicitada.";
                        urlnotificacion = "Libro/Libros";
                        visible = "Autor";
                        Iduser = r;
                   
                      
                        int n1 = conexNotificacion.InsertarNotificacion(notificacion,urlnotificacion,visible,Iduser,2,fechacreado,detallenotificacion,general);
                        
                        //enviar correo de notificación a autor--

                        SendEmailNotificacion(notificacion, Iduser,detallenotificacion);



                        //crear notificación para admin
                        notificacion = "Se ha registrado un nuevo libro para "+Nombre1+" "+ApellidoP+" "+ApellidoS+". Está pendiente el inicio del proceso correspondiente por parte del autor.";
                        string urlnotificacion2 = "Libro/ListaLibros";
                        string visible2 = "Administrador";
                  

                        //notificación para usuarios tipo administrador
                        foreach (var item in conexUser.BuscarUsuarioxTipoUsuario(visible2))
                        {

                            int n2 = conexNotificacion.InsertarNotificacion(notificacion, urlnotificacion2, visible2, item.IDusuario, 2, fechacreado, detallenotificacion,general);
                            break;
                        }
                        //enviar correo a editorial...
                        SendEmailNotificacionEditorial(notificacion, detallenotificacion);



                    }



                  
                    TempData["OK"] = "Usuario registrado";

                }
            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";
                return RedirectToAction("Registro", new RouteValueDictionary(new { controller = "Usuario", action = "Registro", Id = 0 }));
            }

           
            return RedirectToAction("ListaUsuarios");
        }









        // POST: Función para editar datos de usuario x Admin/
        [HttpPost]
        public ActionResult Edit(int Iduser, string Nombre1, string Nombre2, string ApellidoP,string ApellidoS, string Email, string Username, string password, string Telefono, string Direccion, string Foto,string Filial)
        {

            try
            {
                int r = conexUser.ModificarUsuarios(Iduser, Nombre1,Nombre2, ApellidoP,ApellidoS, Email, Username, password, Telefono, Direccion, Foto,Filial);
                if (r == 1)
                {
                    TempData["OK"] = "Datos de usuario han sido editados";
                    //return RedirectToAction("Index","controller");
                }
            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";
                //return View();
            }

            return RedirectToAction("ListaUsuarios");
        }







        //Función para registrar notificación
        [HttpPost]
        public ActionResult RegistroNotificacion(string detallenotifi, string descripnotifi, string visible)
        {

            int idtiponotifi = 6;
            string urlnotifi = "#";
            string fecha= System.DateTime.Now.ToString("dd/MM/yyyy" + " - " + "H:mm tt");
            bool general = true;
            try
            {
               

                int r = conexNotificacion.InsertarNotificacion(descripnotifi,urlnotifi,visible,0,idtiponotifi,fecha,detallenotifi,general);
                if (r == 1)
                {
                    TempData["OK"] = "Notificación registrada";
                    //return RedirectToAction("Index","controller");
                }
            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";
                //return View();
            }

            return RedirectToAction("Notificaciones");
        }







        //Función para editar notificación
        [HttpPost]
        public ActionResult EditNotificacion(int Idnotifi, string detallenotifi1, string notifi, string visible)
        {
            int idtiponotifi1 = 6;
            string urlnotifi = "#";
            string fecha1 = System.DateTime.Now.ToString("dd/MM/yyyy" + " - " + "H:mm tt");
            //string fecha1 = fecha.ToString("dd/MM/yyyy");
            bool general = true;
            try
            {


                int r = conexNotificacion.EditarNotificacion(Idnotifi,notifi, urlnotifi, visible, 0, idtiponotifi1, fecha1, detallenotifi1,general);
                if (r == 1)
                {
                    TempData["OK"] = "Notificación editada";
                    //return RedirectToAction("Index","controller");
                }
            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";
                //return View();
            }

            return RedirectToAction("Notificaciones");
        }






        //Función para cambiar estado de usuario (habilitar/deshabilitar)//
        [HttpPost]
        public JsonResult CambiarEstadoUsuario(int Iduser)
        {
            int r = 0;
            try
            {
                 r = conexUser.DeshabilitarUsuario(Iduser);
                if (r == 1)
                {
                    TempData["OK"] = "Estado de usuario ha sido modificado";
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







        //Función para eliminar usuario//
        [HttpPost]
        public JsonResult Delete(int Iduser)
        {
            int r = 0;
            try
            {
                 r = conexUser.EliminarUsuarios(Iduser);
                if (r == 1)
                {
                    TempData["OKE"] = "OK";
                }
                else
                {
                    TempData["ERROR"] = "Algo salió mal";
                }
            }
            catch(Exception)
            {
                TempData["ERROR"] = "Algo salió mal";

            }

            return Json(r, JsonRequestBehavior.AllowGet);
        }







        // Vista editar perfil de autor//
        public ActionResult EditperfilAutor()
        {
            if (Session["Type"] == null)
            {
                TempData["ERRORD"] = "No tiene acceso a esta sección del sistema";
                return RedirectToAction("Login", "Usuario");
            }

            if (Session["Type"].ToString() != "Autor")
            {

                TempData["ERRORD"] = "No tiene acceso a esta sección del sistema";
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


            var listausers = conexUser.BuscarUsuario(Session["Username"].ToString());

            ViewBag.Usuario = conexUser.BuscarUsuario(Session["Username"].ToString());
            return View(listausers);
        }






        // Vista editar perfil administrador//
        public ActionResult EditperfilAdmin()
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

            var listausers = conexUser.BuscarUsuario(Session["Username"].ToString());
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
            return View(listausers);
        }







        // Vista editar perfil evaluador//
        public ActionResult EditperfilEvaluador()
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

            var listausers = conexUser.BuscarUsuario(Session["Username"].ToString());
    

            ViewBag.Usuario = conexUser.BuscarUsuario(Session["Username"].ToString());
        
            return View(listausers);
        }






     //Función para eliminar foto de usuario
        public ActionResult EliminarFoto(int Iduser)
        {
            try
            {
                int r = conexUser.EliminarFoto(Iduser);
                if (r == 1)
                {

                    TempData["OK"] = "Foto eliminada";

                }
            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";
            }
            
           
            if (Session["Type"].ToString() == "Autor")
            {

                return RedirectToAction("EditperfilAutor", "Usuario");

            }
            else if (Session["Type"].ToString() == "Administrador")
            {

                return RedirectToAction("EditperfilAdmin", "Usuario");
            }
            else
            {
                return RedirectToAction("EditperfilEvaluador", "Usuario");
            }

        }












        //Función de editar el perfil de usuarios//
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPerfil(int Iduser, string Nombre1,string Nombre2, string ApellidoP,string ApellidoS, string Email, string Filial, string Telefono, string Direccion, string Pass, HttpPostedFileBase Foto)
        {

            string Filename = null;
            string ruta = Server.MapPath("~/Recursos/Plantilla/assets/images/users/");

            if (Session["Foto"] == null && Foto == null)
            {
                Filename = null;

            }
            if (Session["Foto"] != null && Foto == null)
            {
                Filename = Session["Foto"].ToString();
            }

            if (Session["Foto"] == null && Foto != null)
            {
                Filename = Session["Username"].ToString() + ".png";
            }

            if (Session["Foto"] != null && Foto != null)
            {
                Filename = Session["Username"].ToString() + ".png";
            }

            try
            {
                int r = conexUser.EditarPerfil(Iduser, Nombre1,Nombre2, ApellidoP,ApellidoS, Email,Filial, Telefono, Direccion, Pass, Filename);
                if (r == 1)
                {
                    if (Foto != null)
                    {
                        Foto.SaveAs(ruta + Filename);
                    }

                    TempData["OK"] = "Datos actualizados exitósamente";

                }
            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";
            }

            

            if (Session["Type"].ToString() == "Autor")
            {

                return RedirectToAction("EditperfilAutor", "Usuario");

            }
            else if (Session["Type"].ToString() == "Administrador")
            {

                return RedirectToAction("EditperfilAdmin", "Usuario");
            }
            else 
            {
                return RedirectToAction("EditperfilEvaluador", "Usuario");
            }

        }







        //Función para eliminar notificación//
        [HttpPost]
        public JsonResult EliminarNotificacion(int Idnotifi)
        {
          
            int r = 0;
            try
            {
                r = conexNotificacion.EliminarNotificacion(Idnotifi);
                if (r == 1)
                {
                    TempData["OKE1N"]="OK"; 
                }
             
            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";

            }

            return Json(r, JsonRequestBehavior.AllowGet);
        }





        [HttpPost]
        public JsonResult EliminarTodoNotificaciones(int Iduser)
        {

            int r = 0;
            try
            {
                r = conexNotificacion.EliminarTodoNotificaciones(Iduser);
                if (r == 1)
                {

                    TempData["OKN"] = "Notificaciones eliminadas";

                }

            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";

            }

            return Json(r, JsonRequestBehavior.AllowGet);
        }




      


        //Función para publicar notificación//
        [HttpPost]
        public JsonResult PublicarNotificacion(int Idnotifi,string visible,string detallen,string notifica)
        {

            int r = 0;
            try
            {
                r = conexNotificacion.PublicarNotificacion(Idnotifi,visible);

                if (r != 3)
                {
                    TempData["OK"] = "Notificación publicada correctamente.";

                    foreach (var item in conexUser.BuscarUsuarioxTipoUsuario(visible))
                    {
                        //enviar correo                  
                        SendEmailNotificacion(notifica, item.IDusuario, detallen);
                    }
                }
              
                
                

            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";

            }

            return Json(r, JsonRequestBehavior.AllowGet);
        }







        //Función para validar némero de cédula
        public int ValidarCedula(string ced)
        {

            int r = 0;
            try
            {

                int[] valores = new int[9];
                //string ced = "0500681697";
                char[] chars = ced.ToCharArray();

                int r1 = 0;
                int suma = 0;
                int verif = 0;
                int res = 0;
                int sum2 = 0;
                int n = 0;


                for (int ctr = 0; ctr < 9; ctr++)
                {
                    r1 = ctr % 2;
                    if (r1 == 0)
                    {
                        valores[ctr] = (int)Char.GetNumericValue(chars[ctr]) * 2;
                        if (valores[ctr] > 9)
                        {
                            valores[ctr] = valores[ctr] - 9;
                        }

                    }
                    else
                    {
                        valores[ctr] = (int)Char.GetNumericValue(chars[ctr]) * 1;

                    }

                    suma = suma + valores[ctr];

                }

                verif = suma + 10;
                res = verif % 10;
                sum2 = verif - res;

                n = sum2 - suma;
                if (n == 10)
                {
                    n = 0;
                }

               

                if ((int)Char.GetNumericValue(chars[9]) == n)
                {
                    r = 1;
                    //Console.WriteLine("cédula valida {0}:{1}", chars[9], n);
                }
                else
                {
                    r = 2;
                    //Console.WriteLine("cédula no valida {0}:{1}", chars[9], n);
                }



            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }



            return r;

        }








        //Función para envío de correo de notificación sin archivos adjuntos - envío de datos de credenciales al crear usuarios
        public void SendEmailNotificacion(string notificacion, int Iduser, string detallenotificacion)
        {


            string date = DateTime.Now.ToString("MM-dd-yyyy");
            string year = date.Substring(6, 4);
            string emaildestino = "";
            string email = "edicionesHU@gmail.com";
            try
            {
                foreach(var item in conexUser.VerDetalleUsuario(Iduser))
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








        //Función para envío de correo de notificación a Editorial sin archivos adjuntos - envío de datos de credenciales al crear usuarios
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





    }




}
