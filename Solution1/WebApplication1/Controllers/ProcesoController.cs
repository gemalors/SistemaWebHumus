using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Negocio.Metodos;
using Negocio.Entidades;
using Microsoft.VisualBasic.FileIO;
using System.Web.Routing;
using System.IO;
using Rotativa;
using System.Net.Mail;

namespace WebApplication1.Controllers
{
    public class ProcesoController : Controller
    {
        M_Cuestionarios conexCuestionario = new M_Cuestionarios();
        M_Libros conexLibro = new M_Libros();
        M_Procesos conexProceso = new M_Procesos();
        M_Autores conexAutor = new M_Autores();
        M_Evaluadores conexEvaluador = new M_Evaluadores();
        M_Comentarios conexComent = new M_Comentarios();
        M_Documentos conexDocs = new M_Documentos();
        M_Usuarios conexUser = new M_Usuarios();
        M_Tiposproceso conexTipoPro = new M_Tiposproceso();
        M_RequerimientosProcesos conexRequerimientoPro = new M_RequerimientosProcesos();
        M_RespuestaRequerimiento conexRespuestaReq = new M_RespuestaRequerimiento();
        M_Preguntas conexPreg = new M_Preguntas();
        M_ObservacionesProceso conexObser = new M_ObservacionesProceso();
        M_Notificaciones conexNotificacion = new M_Notificaciones();
        M_EstadosTiposproceso conexEstado = new M_EstadosTiposproceso();
        M_MensajesTiposproceso conexMensaje=new M_MensajesTiposproceso();





        //Vista para gestionar opciones de tipo de procesos
        public ActionResult OpcionesProceso(int Idproceso, int Id, int t)
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
            ViewBag.Idp = Idproceso;


            int ntotal = 0;
            int nnuevas = 0;
            int Iduser = Convert.ToInt32(Session["Id"]);




            ViewBag.Notificaciones = conexNotificacion.VerNotificacionesxIduser(Iduser);

            foreach (var item in ViewBag.Notificaciones)
            {
                ntotal = Convert.ToInt32(item.TotalNotificacionesrecibidas);
                nnuevas = Convert.ToInt32(item.TotalNotificacionesnuevas);
                break;
            }
            ViewBag.TotalNotificaciones = ntotal;
            ViewBag.NuevasNotificaciones = nnuevas;


        
            ViewBag.tipo = t;
            ViewBag.Id = Id;

            if (t == 1) {
                foreach (var item in conexRequerimientoPro.ListarRequerimientosProcesos(Idproceso))
                {
                    if (item.IDrequerimiento == Id)
                    {
                        ViewBag.Requerimiento = item.Detallerequerimiento;
                        ViewBag.respuesta = item.respuesta;
                        break;
                    }
                }

                //opciones de respuesta de requerimientos

                ViewBag.OpcionesRespuestas = conexRespuestaReq.ListarRespuestasRequerimiento(Id);


            }



            if (t == 2) 
            { 

            //mensajes para estados de procesos

            ViewBag.Mensajes = conexMensaje.ListarMensajeEstadoTiposProcesos(Id);

            foreach (var item in conexEstado.ListarEstadoTiposProcesos(Idproceso))
            {
                if (Id == item.IDEstadosTiposproceso)
                {
                    ViewBag.Estado = item.DetalleEstados;
                    break;
                }

            }

            }


            ViewBag.TipoNotificacion = conexNotificacion.VerTiposNotificaciones();
            ViewBag.Usuario = conexUser.BuscarUsuario(Session["Username"].ToString());
            return View();
        }







        //Vista para gestionar información de tipos de procesos
        public ActionResult InformacionProcesos(int Idproceso)
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
            ViewBag.Idp = Idproceso;
            int ntotal = 0;
            int nnuevas = 0;
            int Iduser = Convert.ToInt32(Session["Id"]);



            ViewBag.Notificaciones = conexNotificacion.VerNotificacionesxIduser(Iduser);

            foreach (var item in ViewBag.Notificaciones)
            {
                ntotal = Convert.ToInt32(item.TotalNotificacionesrecibidas);
                nnuevas = Convert.ToInt32(item.TotalNotificacionesnuevas);
                break;
            }
            ViewBag.TotalNotificaciones = ntotal;
            ViewBag.NuevasNotificaciones = nnuevas;


            ViewBag.Tipodato = conexPreg.ListarTipodeDatoPreguntaAbierta();
            ViewBag.Tipos = conexTipoPro.ListarTiposProcesos();


            foreach (var item in ViewBag.Tipos)
            {
                if (item.IDtiposprocesos == Idproceso)
                {
                    ViewBag.proceso = item.Detalletipospro;
                    break;
                }
            }

           
            ViewBag.Estados = conexEstado.ListarEstadoTiposProcesos(Idproceso);
            ViewBag.Requerimientos = conexRequerimientoPro.ListarRequerimientosProcesos(Idproceso);
            ViewBag.Tipos = conexTipoPro.ListarTiposProcesos();
            ViewBag.Usuario = conexUser.BuscarUsuario(Session["Username"].ToString());
            return View();
        }









        //Vista de proceso de libro admin
        public ActionResult VerProcesoAdmin(int Idlibro, int Idproceso)
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



         
            string visible = Session["Type"].ToString();
            int idestado = 0;
            int ntotal = 0;
            int nnuevas = 0;
            int Idtipoproc= 0;
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




            var listaprocess = conexProceso.VerProcesoxIdp(Idlibro, Idproceso);

            foreach(var item in listaprocess)
            {
                Idtipoproc = Convert.ToInt32(item.Idtipoproceso);
                idestado = Convert.ToInt32(item.Idestado);
        
            }

            ViewBag.Evaluadores = conexEvaluador.VerEvaluadores(Idlibro);



            ViewBag.LibroDetalle = conexLibro.VerDetalleLibro(Idlibro);
            ViewBag.Idp = Idproceso;
      
            ViewBag.Libro = Idlibro;

            ViewBag.MensajeProceso = conexMensaje.VerMensajeEstadoProceso(idestado, Idtipoproc, visible);

            ViewBag.Observaciones = conexObser.VerObservaciones(Idproceso);


            ViewBag.SeleccionRespuesta = conexRespuestaReq.ListarRespuestasRequerimientoxTipoProceso(Idtipoproc, visible);
           
            ViewBag.Requerimientoproceso = conexRequerimientoPro.ListarRequerimientosProcesoxId(Idtipoproc, visible);
            ViewBag.Usuario = conexUser.BuscarUsuario(Session["Username"].ToString());
         
            ViewBag.Documentosenviados = conexDocs.VerDocumentosenviados(Idproceso,Id);
            ViewBag.Documentosrecibidos = conexDocs.VerDocumentosrecibidos(Idproceso, Id);
            ViewBag.Autores = conexAutor.VerAutores(Idlibro);
           
            ViewBag.Comentarios = conexComent.VerComentarios(Idproceso);
            return View(listaprocess);
            
        }






        //Vista de proceso de libro autor
        public ActionResult VerProcesoAutor(int Idlibro, int Idproceso)
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
            ViewBag.LibroDetalle = conexLibro.VerDetalleLibro(Idlibro);
            int Id = Convert.ToInt32(Session["Id"]);

            foreach (var item in ViewBag.LibroDetalle)
            {
                if (Id != item.Idusuario)
                {
                    TempData["ERRORD"] = "Algo salió mal";
                    return RedirectToAction("Login", "Usuario");
                }
            }

            int ntotal = 0;
            int nnuevas = 0;
            int Idpanterior = 0;
            int idestado = 0;
            int Idtipoproc = 0;
            var listaprocess = conexProceso.VerProcesoxIdp(Idlibro, Idproceso);
            string visible = Session["Type"].ToString();


            foreach (var item in listaprocess)
            {
              
                Idpanterior = Convert.ToInt32(item.Idprocanterior);
                Idtipoproc = Convert.ToInt32(item.Idtipoproceso);
                idestado =Convert.ToInt32(item.Idestado);
            }



           
            ViewBag.Notificaciones = conexNotificacion.VerNotificacionesxIduser(Id);

            foreach (var item in ViewBag.Notificaciones)
            {
                ntotal = Convert.ToInt32(item.TotalNotificacionesrecibidas);
                nnuevas = Convert.ToInt32(item.TotalNotificacionesnuevas);
                break;
            }
            ViewBag.TotalNotificaciones = ntotal;
            ViewBag.NuevasNotificaciones = nnuevas;


            
           
         
            ViewBag.Observaciones = conexObser.VerObservaciones(Idproceso);
            ViewBag.SeleccionRespuesta = conexRespuestaReq.ListarRespuestasRequerimientoxTipoProceso(Idtipoproc,visible);
            ViewBag.Idpanterior = Idpanterior;
            ViewBag.MensajeProceso = conexMensaje.VerMensajeEstadoProceso(idestado, Idtipoproc, visible);

            ViewBag.Requerimientoproceso = conexRequerimientoPro.ListarRequerimientosProcesoxId(Idtipoproc,visible);
            ViewBag.Usuario = conexUser.BuscarUsuario(Session["Username"].ToString());

            ViewBag.Documentosenviados = conexDocs.VerDocumentosenviados(Idproceso, Id);
            ViewBag.Documentosrecibidos = conexDocs.VerDocumentosAutor(Idproceso, Id);

            ViewBag.Idlibro = Idlibro;
            ViewBag.Idproceso = Idproceso;
            ViewBag.Autores = conexAutor.VerAutores(Idlibro);
            ViewBag.Comentarios = conexComent.VerComentarios(Idproceso);
            return View(listaprocess);

        
        }







        //Vista para gestionar procesos
        public ActionResult GestionarProcesos()
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
   
            ViewBag.Tipos = conexTipoPro.ListarTiposProcesos();
            ViewBag.Usuario = conexUser.BuscarUsuario(Session["Username"].ToString());
            return View();
        }








        //Función para dar inicio a proceso //
        public ActionResult IniciarProceso(int IDlibro)
         {
            string procesoL = "";
            int Idtipoproceso=0;
            int progresoL=0;
            int n1,el,p=0;
            bool est = false;
            bool general = false;
            string titulo = "";
            string estado = "";//estado inicial -> Pendiente
            int identificadorE = 0;
            string nombresenvia = Session["Nombres"].ToString();
            string urlnotificacion = "";
            string visible = "Administrador";
            string detalle = "Proceso iniciado";
            string asunto ="";
            string fecha = System.DateTime.Now.ToString("dd/MM/yyyy" + " - " + "H:mm tt");
            string notificacion = "";



            try
            {
                foreach (var item in conexLibro.VerDetalleLibro(IDlibro))
                {
                if(item.Titulo == "Libro sin iniciar")
                {
                    TempData["ERROR2"] = "Edite el título del libro para poder iniciar el proceso correctamente";
                    return RedirectToAction("Libros", "Libro");
                    }
                    else
                    {
                        titulo = item.Titulo;
                    }
                }


                foreach (var item1 in conexTipoPro.ListarTiposProcesosxnum(1))
                {

                    procesoL = item1.Detalletipospro;
                    Idtipoproceso = item1.IDtiposprocesos;
                    progresoL = Convert.ToInt32(item1.Progreso);


                }


                foreach (var item in conexEstado.ListarEstadoxIdentificador(Idtipoproceso, 1))
                {

                    estado = item.DetalleEstados;
                    identificadorE = item.IdentificadorEstados;

                }




                p = conexProceso.CrearProceso(IDlibro, fecha, estado, Idtipoproceso,identificadorE);
                el = conexLibro.EditarLibroP(IDlibro, procesoL, progresoL, fecha, estado, p,est);




                // crear notificación
                asunto = detalle + " - [Libro: " + titulo + "]";

                notificacion = nombresenvia + " ha iniciado el proceso de "+procesoL+" para el libro titulado: "+titulo+".";
                urlnotificacion = "Proceso/VerProcesoAdmin?Idlibro="+IDlibro+"&Idproceso="+p+ "";


              
                foreach (var item in conexUser.BuscarUsuarioxTipoUsuario(visible))
                {
                    n1 = conexNotificacion.InsertarNotificacion(notificacion, urlnotificacion, visible, item.IDusuario, 1, fecha, detalle,general);
                    break;
                }
                //enviar correo a Editorial
                SendEmailNotificacionEditorial(notificacion,asunto);

                TempData["OK"] = "Proceso iniciado correctamente.";


            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";
            }
           
           
            return RedirectToAction("VerProcesoAutor", new RouteValueDictionary(new { controller = "Proceso", action = "VerProcesoAutor", IDlibro = IDlibro, Idproceso = p }));
            //return RedirectToAction("VerProceso", "Proceso");
        }







        //Vista de lista de evaluadores
        public ActionResult Evaluadores(int IDlibro, int Idproceso)
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
            ViewBag.listevaluadores = conexEvaluador.ListaEvaluadores();


            ViewBag.Idlibro = IDlibro;

            ViewBag.Idproceso = Idproceso;
            return View();
        }








        //Función para registrar evaluador desde la vista de evaluadores
        [HttpPost]
        public ActionResult RegistroEvaluador(int IDlibro, int Idproceso, string Nombre1,string Nombre2, string ApellidoP,string ApellidoS, string Email, string Cedula)
        {
            int IdTipousuario = 7;
            string Foto = null;
            string Telefono = "Sin actualizar";
            string Direccion = "Sin actualizar";
            string Filial = "Sin actualizar";
            int libproceso = 0;
            int IdE = 0;


            try
            {
                IdE = conexUser.Crearusuarios(Nombre1,Nombre2, ApellidoP,ApellidoS, Email, Cedula, Cedula, Foto, IdTipousuario, Telefono, Direccion, Filial,libproceso);
     
                if (IdE >0)
                {

                    //enviar correo  con credenciales a usuario--

                    SendEmail("Bienvenido(a) a la Editorial Humus, tu credencial de acceso para usuario y contraseña es: " + Cedula + ". Puede cambiar estas credenciales cuando usted lo desee.", IdE, Nombre1 + " te damos la bienvenida");

                    TempData["OK"] = "Evaluador registrado correctamente";
                    return RedirectToAction("Evaluadores", new RouteValueDictionary(new { controller = "Proceso", action = "Evaluadores", IDlibro = IDlibro, Idproceso = Idproceso }));



                }
                else if(IdE==-1)
                {
                    TempData["ERROR2"] = "Usuario ya existe";
                 
                }
            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";
            }


            return RedirectToAction("Evaluadores", new RouteValueDictionary(new { controller = "Proceso", action = "Evaluadores", IDlibro = IDlibro, Idproceso = Idproceso }));
        }




        



        //Función para enviar mensaje al correo electrónico de usuarios
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult EnviarMensaje(int Iduser,string asunto,string mensaje)
        {
            int r = 0;
            string notificacion = "";
            string detalle = "";
            
            try
            {
                detalle = asunto;
                notificacion = mensaje;
                SendEmail(notificacion, Iduser, detalle);
                TempData["OKM"] = "OK";

            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";
            }

            return Json(r, JsonRequestBehavior.AllowGet);
        }








        
        //Función para enviar mensaje al correo electrónico de la Editorial Humus 
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult EnviarMensajeEditorial(int idemisor,string asunto, string mensaje)
        {
            int r = 1;
          
         
            try
            {
                if (mensaje == "")
                {
                    TempData["ERROR2"] = "Falta agregar mensaje, vuelva a intentarlo.";

                }
                else
                {


                   string detalle = asunto;
                   string notificacion = mensaje;
                    SendEmailEditorial(notificacion, idemisor, detalle);
                    TempData["OKM"] = "OK";



                }

            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";
            }

            return Json(r, JsonRequestBehavior.AllowGet);
        }








        //Función para envío de correo de notificación sin archivos adjuntos - dirigido a usuarios del sistema 
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







        
        //Función para envío de correo de notificación sin archivos adjuntos 
        public void SendEmailEditorial(string notificacion, int idemisor, string detallenotificacion)
        {


            string date = DateTime.Now.ToString("MM-dd-yyyy");
            string year = date.Substring(6, 4);
            string emaildestino = "";
            string nombres = "";
            string email = "edicionesHU@gmail.com";
            try
            {
                foreach (var item in conexUser.VerDetalleUsuario(idemisor))
                {

                    emaildestino = item.Email;
                    nombres = item.PrimerNombre + " " + item.ApellidoPrimero+" "+item.ApellidoSegundo;

                }

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("edicionesHU@gmail.com");
                mail.To.Add(email);
                mail.Subject = detallenotificacion;

                mail.IsBodyHtml = true;

                string htmlBody = "<table width='100%' bgcolor='#F5F5F5' border='0' cellpadding='0' cellspacing='0'><tbody><tr><td><table class='m_ - 8535930574907526684content' align='center' border='1' cellpadding='0' cellspacing='0' style='border-top:10px solid #348DE4;border-left:1px solid #C0C2C4 ;border-right:1px solid #C0C2C4 ; border-bottom:5px solid #348DE4;border-collapse:collapse;margin-top:30px;width:100%;max-width:600px' bgcolor='#fff'><tbody><tr><td class='m_ - 8535930574907526684header' style='padding: 20px 30px 20px 30px'>" +
                              "<table align='center' border='0' cellpadding='0' cellspacing='0' style='bgcolor = '#fff'><tbody><tr><td valign='top'><a href='https://localhost:44329/Editorial/Inicio' class='h1 text-secondary'><img src='https://raw.githubusercontent.com/gemalors/Imagenes/main/nlog2copia.png' class='light-logo'   border='0'/></a></td><td style='font-size:0;line-height:0' width='220px'>&nbsp;</td> <td align='right' width='300px'> <a href='https://localhost:44329/Usuario/Login'  style='font-size:18px;letter-spacing:0.02rem; color:#0056bf;font-family:Arial,Helvetica,sans-serif;text-decoration:None'  target='_blank'>Sistema web Humus</a></td></tr></tbody></table></td></tr>" +
                              "<tr><td class='m_- 8535930574907526684innerpadding' style='padding: 30px 30px 30px 30px; border-top:1px solid #F2F5F9'><table><tbody><tr><td><br><br><span class='m_-8535930574907526684text-body text-justify' style='line-height:1.5; font-family:Arial,Helvetica,sans-serif; font-size:15px'>" + notificacion + "</span></td></tr><tr><td style = 'font-size:0;line-height:0' height= '20px'>&nbsp;</td></tr><tr><td></td></tr></tbody></table></td></tr>" +
                            "<tr><td class='m_- 8535930574907526684innerpadding' style='padding: 30px 30px 30px 30px;  border-top:1px solid #F2F5F9'><table><tbody><tr></tr><tr><td><table align = 'center' border = '0' cellpadding = '0' cellspacing ='0'style = 'padding-top:30px'><tbody><tr><td width='548px' valign='top' style = 'font-family:Arial,Helvetica,sans-serif;font-size:16px;line-height:1.6'><span style = 'font-size:15px;color:#655F5D;font-family:Arial,Helvetica,sans-serif'> Emisor:<a style = 'color:#0056bf;text-decoration: none;'>"+nombres+"</a></span><br><span style = 'font-size:15px;color:#655F5D;font-family:Arial,Helvetica,sans-serif'> Correo electrónico: <a href = 'mailto:" + emaildestino + "' target = '_blank' style = 'color:#0056bf;text-decoration: none;'>" + emaildestino + "</a></span><br>" +
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






     
        //Función para envío de correo de notificación con archivos adjuntos dirigido a la Editorial
        public void SendEmailDocumentosEditorial(string notificacion, string detallenotificacion, int Idproceso, int idemisor)
        {


            string path = "";
            string date = DateTime.Now.ToString("MM-dd-yyyy");
            string year = date.Substring(6, 4);

            string email = "edicionesHU@gmail.com";
            string emaildestino = email;
            try
            {

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("edicionesHU@gmail.com");



                foreach (var item1 in conexDocs.VerDocumentosenviados(Idproceso, idemisor))
                {
                    if (item1.EstadoDocu == "Enviado" && item1.Visibleadmin==true)
                    {
                        path = @"D:\Copia-Sistema\SistemaHumus\Solution1\WebApplication1\Files\Documents\" + item1.Documento;
                        mail.Attachments.Add(new Attachment(path));
                    }
                }



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






       
        //Función para envío de correo de notificación con archivos adjuntos dirigidos a autores
        public void SendEmailDocumentos(string notificacion, int Iduser, string detallenotificacion, int Idproceso, int idemisor)
        {


            string path = "";
            string emaildestino = "";
            string email = "edicionesHU@gmail.com";
            try
            {
                string date = DateTime.Now.ToString("MM-dd-yyyy");
                string year = date.Substring(6, 4);
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("edicionesHU@gmail.com");



                foreach (var item1 in conexDocs.VerDocumentosenviados(Idproceso, idemisor))
                {
                    if (item1.EstadoDocu == "Enviado" && item1.Visibleautor==true)
                    {
                        path = @"D:\Copia-Sistema\SistemaHumus\Solution1\WebApplication1\Files\Documents\" + item1.Documento;
                        mail.Attachments.Add(new Attachment(path));
                    }
                }


                foreach (var item in conexUser.VerDetalleUsuario(Iduser))
                {

                    emaildestino = item.Email;

                }


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






        
        //Función para envío de correo de notificación con archivos adjuntos en el proceso de evaluacion de pares académicos 
        public void SendEmailDocumentos2(string notificacion, int Iduser, string detallenotificacion, int Idproceso, int idemisor)
        {

            string date = DateTime.Now.ToString("MM-dd-yyyy");
            string year = date.Substring(6, 4);
            string path = "";
            string emaildestino = "";
            string email = "edicionesHU@gmail.com";
            try
            {

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("edicionesHU@gmail.com");



                foreach (var item1 in conexDocs.VerDocumentosrecibidos(Idproceso, idemisor))
                {
                    if (item1.Idemisor != Iduser && item1.EstadoDocu == "Enviado" && item1.Visibleautor == true)
                    {
                        path = @"D:\Copia-Sistema\SistemaHumus\Solution1\WebApplication1\Files\Documents\" + item1.Documento;
                        mail.Attachments.Add(new Attachment(path));
                    }
                }

                foreach (var item1 in conexDocs.VerDocumentosenviados(Idproceso, idemisor))
                {
                    if (item1.EstadoDocu == "Enviado" && item1.Visibleautor == true)
                    {
                        path = @"D:\Copia-Sistema\SistemaHumus\Solution1\WebApplication1\Files\Documents\" + item1.Documento;
                        mail.Attachments.Add(new Attachment(path));
                    }
                }

                foreach (var item in conexUser.VerDetalleUsuario(Iduser))
                {

                    emaildestino = item.Email;

                }


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







   
        //Función2  para envío de correo de notificación con archivos adjuntos - enviar borrador de libro a evaluadores
        public void SendEmailDocumentos3(string notificacion, int Iduser, string detallenotificacion, int Idproceso,int idemisor)
        {

            string date = DateTime.Now.ToString("MM-dd-yyyy");
            string year = date.Substring(6, 4);
            string path = "";
            string emaildestino = "";
            string email = "edicionesHU@gmail.com";
            try
            {

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("edicionesHU@gmail.com");



                foreach (var item1 in conexDocs.VerDocumentosenviados(Idproceso,idemisor))
                {
                    if (item1.EstadoDocu == "Pendiente a evaluar" && item1.Visiblevaluador==true)
                    {
                        path = @"D:\Copia-Sistema\SistemaHumus\Solution1\WebApplication1\Files\Documents\" + item1.Documento;
                        mail.Attachments.Add(new Attachment(path));
                    }
                }


                foreach (var item in conexUser.VerDetalleUsuario(Iduser))
                {

                    emaildestino = item.Email;

                }


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









        //Función para asignar evaluador para un libro
        public ActionResult AddEvaluador(int IDlibro, int Idproceso, int IDevaluador, int Libevaluando)
        {
            int num = 1;
            bool general = false;
            string fecha1 = System.DateTime.Now.ToString("dd/MM/yyyy");
            string titulol = "";
            string procesoa = "";
            string estadoasignacion = "Pendiente";
            int Idtab2, a, e,n = 0;
            string fecha = System.DateTime.Now.ToString("dd/MM/yyyy" + " - " + "H:mm tt");
            string detalle = "Libro asignado para evaluar";
            string url = "Libro/LibrosEvaluar";
            string visible = "Evaluador";
            string asunto = "";
            string notificacion = "";
         


            try
            {


            ViewBag.Evaluadores = conexEvaluador.VerEvaluadores(IDlibro);


            foreach(var item in ViewBag.Evaluadores)
            {
                if (IDevaluador == item.Idevaluador)
                {
                    TempData["ERROR2"] = " Este evaluador ya ha sido asignado para este libro!!!";
                    return RedirectToAction("Evaluadores", new RouteValueDictionary(new { controller = "Proceso", action = "Evaluadores", IDlibro = IDlibro, Idproceso = Idproceso}));
                }
            
            }

            if (ViewBag.Evaluadores.Count==2)
            {
                TempData["ERROR2"] = " Ya se ha completado el número de evaluadores permitidos para este libro!!!";
                return RedirectToAction("Evaluadores", new RouteValueDictionary(new { controller = "Proceso", action = "Evaluadores", IDlibro = IDlibro, Idproceso = Idproceso }));
            }


            if (Libevaluando == 2)
            {
                TempData["ERROR2"] = " Ya se ha completado el límite de libros permitidos para este evaluador!!!";
                return RedirectToAction("Evaluadores", new RouteValueDictionary(new { controller = "Proceso", action = "Evaluadores", IDlibro = IDlibro, Idproceso = Idproceso}));
            }


            if (ViewBag.Evaluadores.Count == 1)
                {
                    num = 2;
                }

              

                Idtab2 = conexEvaluador.AsignarEvaluador(IDlibro, Idproceso,IDevaluador,estadoasignacion,num);
                a = conexCuestionario.InsertarAsignacion(1, IDevaluador,fecha1);
                e = conexEvaluador.EditarEstadoAsignacionEvaluador2(Idproceso,estadoasignacion,a,IDevaluador);
             



                foreach (var item3 in conexProceso.VerProcesoxIdp(IDlibro, Idproceso))
                {
                    titulol = item3.Titulo;
                    procesoa = item3.Detalletipospro;
                    //nombresa = item3.Nombres + " " + item3.Apellidos;

                }

              
               


            if (Idtab2 != 0 && a!=0 && e==2)
            {
                    //crear notificación para evaluador asignado
                    //enviar correo con notificación

                     asunto = "Asignación de evaluación - [Libro: "+ titulol + "]";
                     notificacion = "Se le ha asignado el libro " + titulol + " para la evaluación correspondiente al proceso de "+procesoa+". Pronto se le enviará la documentación respectiva para que pueda iniciar la evaluación asignada accediendo al sistema web de la Editorial Humus.";
                  

                     n = conexNotificacion.InsertarNotificacion(notificacion, url, visible, IDevaluador, 2, fecha, detalle,general);

                    SendEmail(notificacion, IDevaluador, asunto);


                TempData["OK"] = "Evaluador asignado correctamente";
                return RedirectToAction("Evaluadores", new RouteValueDictionary(new { controller = "Proceso", action = "Evaluadores", IDlibro = IDlibro, Idproceso = Idproceso }));
             }
             else
             {
                    TempData["ERROR"] = "Algo salió mal";

              }

            }
            catch(Exception)
            {
                TempData["ERROR"] = "Algo salió mal";
                //return View();
            }

            return RedirectToAction("Evaluadores", new RouteValueDictionary(new { controller = "Proceso", action = "Evaluadores", IDlibro = IDlibro, Idproceso = Idproceso }));
            //return RedirectToAction("Evaluadores", new RouteValueDictionary(new { controller = "Proceso", action = "Evaluadores", IDlibro = IDlibro, Idproceso = Idproceso }));
        }






        //Función para editar tipo de proceso
        [HttpPost]
        public ActionResult EditarTipoProceso(int Idtipoproceso, int numero,string detalletipospro, string descripcion, int progreso, string duracionproceso)
        {
            try
            {
                if ( progreso <= 0 || numero <=0)
                {
                    TempData["ERROR2"] = "No se aceptan valores negativos, vuelva a intentarlo e ingrese los valores correctos.";
                    return RedirectToAction("GestionarProcesos", "Proceso");
                }

                int r = conexTipoPro.EditarTiposprocesos(Idtipoproceso, detalletipospro, progreso,duracionproceso, descripcion,numero);
                if (r ==1)
                {
                    TempData["OK"] = "Datos de tipo de proceso editados correctamente";

                    //return RedirectToAction("Index","controller");
                }

                else
                {
                    TempData["ERROR"] = "Algo salió mal";
                    //return View();
                }
            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";
            }

            return RedirectToAction("GestionarProcesos", "Proceso");


        }







        //Función para editar requerimiento de proceso
        [HttpPost]
        public ActionResult EditRequerimientoProceso(int Idp1, int Idrequerimiento1, string detallerequerimiento1, string descripcionrequerimiento1,int tipodato1, string emisor, string resp,bool obligatorio,bool resultproceso, bool visibleadmin, bool visibleautor, bool visibleE)
        {
            try
            {
                int r = conexRequerimientoPro.EditarRequerimientosProcesos(Idrequerimiento1, detallerequerimiento1, Idp1, tipodato1, descripcionrequerimiento1, emisor, resp,visibleadmin, visibleE,visibleautor,obligatorio,resultproceso);
                if (r ==2)
                {
                    TempData["OK"] = "Datos de requerimiento editados correctamente";

                    //return RedirectToAction("Index","controller");
                }

                else
                {
                    TempData["ERROR"] = "Algo salió mal";
                    //return View();
                }

            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";
            }

            return RedirectToAction("InformacionProcesos", new RouteValueDictionary(new { controller = "Proceso", action = "InformacionProcesos", Idproceso = Idp1 }));
        }









        //Función para agregar requerimiento de proceso
        [HttpPost]
        public ActionResult AddRequerimientoProceso(int Idp,string detallerequerimiento, string descripcionrequerimiento, int idtipodato,string resp, bool obligatorio, string emisor,bool resultproceso, bool visiblead, bool visibleautor, bool visibleE)
        {

            int r = 0;
            try
            {
                r = conexRequerimientoPro.AgregarRequerimientosProcesos(detallerequerimiento, Idp, idtipodato, descripcionrequerimiento, emisor, resp, visiblead, visibleE, visibleautor,obligatorio,resultproceso);

                if (r != 0)
                {
                    TempData["OK"] = "Datos de requerimiento de tipo de proceso registrados correctamente";

                    //return RedirectToAction("Index","controller");
                }

                else
                {
                    TempData["ERROR"] = "Algo salió mal";
                    //return View();
                }
            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";
            }

            return RedirectToAction("InformacionProcesos", new RouteValueDictionary(new { controller = "Proceso", action = "InformacionProcesos", Idproceso = Idp }));
        }









        //Función para agregar estados de proceso
        [HttpPost]
        public ActionResult AddEstadosProceso(int Idp,int identificador, string detalle,bool vra,bool er,bool epr,bool vrad,bool eresult,bool inp,bool ae,bool epares,bool pl)
        {

            int r = 0;
            try
            {
                if (identificador <= 0)
                {
                    TempData["ERROR2"] = "No se aceptan valores negativos, vuelva a intentarlo e ingrese un valor correcto.";
                    return RedirectToAction("InformacionProcesos", new RouteValueDictionary(new { controller = "Proceso", action = "InformacionProcesos", Idproceso = Idp }));
                }

                r = conexEstado.AgregarEstadoTiposProcesos(identificador, detalle, Idp, vra, er, epr, vrad, eresult, inp, ae, epares, pl);

                if (r ==1)
                {
                    TempData["OK"] = "Estado de proceso registrado correctamente";

                    //return RedirectToAction("Index","controller");
                }
                if (r == 2)
                {
                    TempData["ERROR2"] = "El número de identificador de estado ya existe para este proceso, vuelva a intentarlo con un valor diferente.";

                    //return RedirectToAction("Index","controller");
                }
                if(r==3)
                {
                    TempData["ERROR"] = "Algo salió mal";
                    //return View();
                }
            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";
            }

            return RedirectToAction("InformacionProcesos", new RouteValueDictionary(new { controller = "Proceso", action = "InformacionProcesos", Idproceso = Idp }));
        }










        //Función para editar estados de proceso
        [HttpPost]
        public ActionResult EditarEstadosProceso(int Idp, int idestado, string detalle, bool vra, bool er, bool epr, bool vrad, bool eresult, bool inp, bool ae, bool epares, bool pl)
        {

            int r = 0;
            try
            {
                r = conexEstado.EditarEstadoTiposProcesos(idestado, detalle, vra, er, epr, vrad, eresult, inp, ae, epares, pl);

                if (r == 2)
                {
                    TempData["OK"] = "Estado de proceso editado correctamente";

                    //return RedirectToAction("Index","controller");
                }

                else
                {
                    TempData["ERROR"] = "Algo salió mal";
                    //return View();
                }
            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";
            }

            return RedirectToAction("InformacionProcesos", new RouteValueDictionary(new { controller = "Proceso", action = "InformacionProcesos", Idproceso = Idp }));
        }








      





        //Función para agregar opción de respuesta de requerimiento de proceso
        [HttpPost]
        public ActionResult AddOpcionRespuesta(int Idp,int Idrequerimiento,int t,string detalle, int progreso)
        {

            try
            {
                int r = conexRespuestaReq.AgregarRespuestaRequerimiento(Idrequerimiento, detalle,progreso);

                if (r ==1)
                {
                    TempData["OK"] = "Opción de respuesta de requerimiento registrada correctamente";

                    //return RedirectToAction("Index","controller");
                }

                else
                {
                    TempData["ERROR"] = "Algo salió mal";
                    //return View();
                }
            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";
            }

            //return RedirectToAction("GestionarProcesos", "Proceso");
            return RedirectToAction("OpcionesProceso", new RouteValueDictionary(new { controller = "Proceso", action = "OpcionesProceso", Idproceso = Idp,Id= Idrequerimiento ,t=t}));
        }





        //Función para editar opción de respuesta de requerimiento de proceso
        [HttpPost]
        public ActionResult EditarOpcionRespuesta(int Idp, int Idrequerimiento, int t, int idrespuesta, string detalle1, int progreso1)
        {

            try
            {
                int r = conexRespuestaReq.EditarRespuestaRequerimiento(idrespuesta, detalle1, progreso1);

                if (r == 2)
                {
                    TempData["OK"] = "Opción de respuesta de requerimiento editada correctamente";

                    //return RedirectToAction("Index","controller");
                }

                else
                {
                    TempData["ERROR"] = "Algo salió mal";
                    //return View();
                }
            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";
            }

            //return RedirectToAction("GestionarProcesos", "Proceso");
            return RedirectToAction("OpcionesProceso", new RouteValueDictionary(new { controller = "Proceso", action = "OpcionesProceso", Idproceso = Idp, Id = Idrequerimiento, t = t }));
        }








        //Función para agregar mensajes a estados de proceso
        [HttpPost]
        public ActionResult AddMensajeEstados(int Idp, int Idestado, int t, string descripcion, string visible)
        {

            try
            {
                int r = conexMensaje.AgregarMensajeEstadoTiposProcesos(Idestado, descripcion, visible);

                if (r == 1)
                {
                    TempData["OK"] = "Mensaje de estado registrado correctamente";

                    //return RedirectToAction("Index","controller");
                }

                else
                {
                    TempData["ERROR"] = "Algo salió mal";
                    //return View();
                }
            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";
            }

            //return RedirectToAction("GestionarProcesos", "Proceso");
            return RedirectToAction("OpcionesProceso", new RouteValueDictionary(new { controller = "Proceso", action = "OpcionesProceso", Idproceso = Idp, Id = Idestado, t = t }));
        }





        //Función para editar mensajes a estados de proceso
        [HttpPost]
        public ActionResult EditarMensajeEstados(int Idp, int Idestado, int t, int idmensaje,string descripcion1, string visible1)
        {

            try
            {
                int r = conexMensaje.EditarMensajeEstadoTiposProcesos(idmensaje, descripcion1, visible1);

                if (r == 2)
                {
                    TempData["OK"] = "Mensaje de estado editado correctamente";

                    //return RedirectToAction("Index","controller");
                }

                else
                {
                    TempData["ERROR"] = "Algo salió mal";
                    //return View();
                }
            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";
            }

            //return RedirectToAction("GestionarProcesos", "Proceso");
            return RedirectToAction("OpcionesProceso", new RouteValueDictionary(new { controller = "Proceso", action = "OpcionesProceso", Idproceso = Idp, Id = Idestado, t = t }));
        }







        //Función para eliminar opción de respuesta de requerimiento de proceso
        [HttpPost]
        public JsonResult EliminarOpcionRespuesta(int idrespuesta)
        { int r = 0;
            try
            {
                r = conexRespuestaReq.EliminarRespuestaRequerimiento(idrespuesta);
                if (r == 1)
                {
                    TempData["OKEOR"] = "OK";
                }

            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";
            }

            return Json(r, JsonRequestBehavior.AllowGet);
        }




        //Función para eliminar mensaje de estado de proceso
        [HttpPost]
        public JsonResult EliminarMensajeEstados(int idmensaje)
        {
            int r = 0;
            try
            {
                r = conexMensaje.EliminarMensajeEstadoTiposProcesos(idmensaje);
                if (r == 2)
                {
                    TempData["OKMensaje"] = "OK";
                }

            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";
            }

            return Json(r, JsonRequestBehavior.AllowGet);
        }











        //Función para eliminar requerimiento de proceso
        [HttpPost]
        public JsonResult EliminarRequerimientoProceso(int Idrequerimiento)
        {
            int r = 0;
            try
            {
                r = conexRequerimientoPro.EliminarRequerimientosProcesos(Idrequerimiento);
                if (r == 1)
                {
                    TempData["OKER"] = "OK";
                }
            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";
            }
            
            return Json(r, JsonRequestBehavior.AllowGet);
        }







        //Función para agregar datos de autores de libro
        [HttpPost]
        public ActionResult AddAutor(int IDlibro, int Idproceso, string Nombre1,string Nombre2, string ApellidoP,string ApellidoS, string Cedula, string Direccion, string Telefono, string Email, string Filial)
        {
            try
            {
                int Idtab1 = conexAutor.InsertarAutor(IDlibro, Nombre1,Nombre2, ApellidoP,ApellidoS, Cedula, Email, Telefono, Direccion, Filial);
                if (Idtab1 != 0)
                {
                    TempData["OK"] = "Datos de autor registrados correctamente";

                    //return RedirectToAction("Index","controller");
                }

                else
                {
                    TempData["ERROR"] = "Algo salió mal";
                    //return View();
                }
            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";
            }
            if (Session["Type"].ToString() == "Autor")
            {

                return RedirectToAction("VerProcesoAutor", new RouteValueDictionary(new { controller = "Proceso", action = "VerProcesoAutor", IDlibro = IDlibro, Idproceso = Idproceso }));
            }

            if (Session["Type"].ToString() == "Administrador")
            {
                return RedirectToAction("AgregarLibro", new RouteValueDictionary(new { controller = "Libro", action = "AgregarLibro", IDlibro = IDlibro }));
            }
            return RedirectToAction("AgregarLibro", new RouteValueDictionary(new { controller = "Libro", action = "AgregarLibro", IDlibro = IDlibro }));
        }

    






        //Función para editar datos de autores de libro
        [HttpPost]
        public ActionResult EditAutor(int IDlibro, int Idproceso,int Idautor, string Nombre1,string Nombre2, string ApellidoP,string ApellidoS, string Cedula, string Direccion, string Telefono, string Email, string Filial)
        {
            try
            {

                int r = conexAutor.EditarAutor(Idautor, Nombre1,Nombre2, ApellidoP,ApellidoS, Cedula, Email, Telefono, Direccion, Filial);
                if (r != 3)
                {
                    TempData["OK"] = "Datos de autor editados correctamente";

                    //return RedirectToAction("Index","controller");
                }

                else
                {
                    TempData["ERROR"] = "Algo salió mal";
                    //return View();
                }
            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";
            }

             if (Session["Type"].ToString() == "Autor")
            {

                return RedirectToAction("VerProcesoAutor", new RouteValueDictionary(new { controller = "Proceso", action = "VerProcesoAutor", IDlibro = IDlibro, Idproceso = Idproceso }));

            }
            
            if (Session["Type"].ToString() == "Administrador")
            {
                return RedirectToAction("AgregarLibro", new RouteValueDictionary(new { controller = "Libro", action = "AgregarLibro", IDlibro = IDlibro }));
            }


            return RedirectToAction("AgregarLibro", new RouteValueDictionary(new { controller = "Libro", action = "AgregarLibro", IDlibro = IDlibro }));
        

    }






        //Función para obtener la extensión de un archivo
        private string GetExtension(string attachment_name)
        {
            var index_point = attachment_name.IndexOf(".") + 1;
            return attachment_name.Substring(index_point);
        }




         


       
        //Función para guardar requerimientos de proceso de autor
        [HttpPost]
        public ActionResult GuardarRequerimientodeprocesoAutor(int Idtipoproceso, int IDlibro, int Idproceso,int identificadorE, int Idpanterior, int Idrequerimiento, string estadopro, HttpPostedFileBase Documento, string observacion, int op)
        {
            try
            {
       
                int visibleautor=0;
                bool obligatorio = false;
                bool resultp = false;
             

                int visibleadmin=0;
                int visiblevaluador = 0;
                string fecha = System.DateTime.Now.ToString("dd/MM/yyyy" + " - " + "H:mm tt");
                string emisor = Session["Nombres"].ToString();
                int Id =Convert.ToInt32(Session["Id"]);
                string estadoDoc = "Pendiente";
                int idemisor = Convert.ToInt32(Session["Id"]);
                string detalleobservacion = "";
                string DetalleDoc = "";
                string Detalle = "";
                int cont = 0;


                ViewBag.DocumentosEnviados = conexDocs.VerDocumentosenviados(Idproceso, idemisor);
                ViewBag.Requerimientos = conexRequerimientoPro.ListarRequerimientosProcesoxId(Idtipoproceso, "Autor");
                ViewBag.ObservacionesEnviadas = conexObser.VerObservacionesEnviadas(Idproceso, Id);


                foreach(var item in ViewBag.Requerimientos)
                {
                    ViewBag.Obligatorios = item.Obligatorios;
                    break;
                }

                foreach (var item4 in ViewBag.DocumentosEnviados)
                {
                    if (item4.EstadoDocu == "Pendiente" && item4.DocObligatorio==true)
                    {
                        cont++;
                    }

                }

               
                foreach (var item4 in ViewBag.ObservacionesEnviadas)
                {
                    if (item4.EstadoObservacion == "Pendiente" && item4.RespuestaProceso == true && item4.Obligatoria==true)
                    {
                        cont++;
                    }

                }




                foreach (var item3 in ViewBag.DocumentosEnviados)
                {
                    if(Idrequerimiento==item3.Idrequerimiento && item3.EstadoDocu == "Pendiente")
                    {
                        TempData["ERROR2"] = "El documento correspondiente a este requerimiento ya se ha registrado, intente guardar el documento correcto.";
                    
                    return RedirectToAction("VerProcesoAutor", new RouteValueDictionary(new { controller = "Proceso", action = "VerProcesoAutor", Idlibro = IDlibro, Idproceso = Idproceso }));

                }
                }


                foreach (var item4 in ViewBag.ObservacionesEnviadas)
                {
                    if (item4.EstadoObservacion == "Pendiente" && item4.Idrequerimiento == Idrequerimiento)
                    {
                        TempData["ERROR2"] = "La respuesta correspondiente a este requerimiento ya se ha registrado.";

                        return RedirectToAction("VerProcesoAutor", new RouteValueDictionary(new { controller = "Proceso", action = "VerProcesoAutor", Idlibro = IDlibro, Idproceso = Idproceso }));

                    }

                }





                foreach (var item in ViewBag.Requerimientos)
                {

                    if (Idrequerimiento == item.IDrequerimiento)
                    {
                        Detalle = item.Detallerequerimiento;
                        visibleadmin = Convert.ToInt32(item.Visibleadmin);
                        visibleautor = Convert.ToInt32(item.Visibleautor);
                        visiblevaluador = Convert.ToInt32(item.Visibleevaluador);
                        obligatorio = item.Obligatorio;
                        resultp = item.ResultadoProceso;
                        break;


                    }


                }


                if (op != 0)
                {

                    foreach (var item3 in conexRespuestaReq.ListarRespuestasRequerimientoxIdRequerimiento(op, Idrequerimiento))
                    {
                        DetalleDoc = item3.DetalleResp;
                        detalleobservacion = item3.DetalleResp;
                    }
                }
                else
                {
                    DetalleDoc = Detalle;
                }


                if (resultp == true)
                {
                  
                    int ep = conexProceso.EditarProceso1(Idproceso, fecha, estadopro, Idpanterior, op, identificadorE);
                }




                if (Documento != null)
                {



                    string fecha1 = System.DateTime.Now.ToString("dd-MM-yyyy" + " " + "H-mm");
                    string Extension = GetExtension(Documento.FileName);
                    string NombreDoc = IDlibro + "_" + fecha1 +"_" + Detalle + "." + Extension;
                    string ruta = Server.MapPath("~/Files/Documents/");
                    


                   

                    if (Extension == "txt" || Extension == "doc" || Extension == "docx" || Extension == "pdf" || Extension == "rtf")
                    {

                        Documento.SaveAs(ruta + NombreDoc);
                        int d = conexDocs.InsertarDocumento(Idproceso, NombreDoc, DetalleDoc, visibleautor, emisor, fecha, estadoDoc, visibleadmin, visiblevaluador,idemisor,Idrequerimiento,obligatorio);
                     
                        TempData["OK"] = "Documento guardado exitósamente";

                    }
                    else
                    {
                        TempData["ERROR2"] = "Formato de archivo no aceptado";
                    }

                }
               
       
                             
                if (observacion != null)
                {

                    int r = conexObser.AgregarObservacion(Idproceso, observacion, detalleobservacion, emisor, Detalle, fecha,true,"Pendiente",Id,true,Idrequerimiento,obligatorio);

                    TempData["OK"] = "Observación con respuesta registrada correctamente";

                }


                if (op != 0 && observacion == null && Documento==null)
                {
                    int r = conexObser.AgregarObservacion(Idproceso, detalleobservacion, "", emisor, Detalle, fecha, true, "Pendiente", Id, true, Idrequerimiento, obligatorio);

                    TempData["OK"] = "Observación con respuesta registrada correctamente";
                }



            }

            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";
            }

            return RedirectToAction("VerProcesoAutor", new RouteValueDictionary(new { controller = "Proceso", action = "VerProcesoAutor", Idlibro = IDlibro, Idproceso = Idproceso}));
            
            
        }







        //Función para guardar requerimientos de proceso de admin
        [HttpPost]
        public ActionResult GuardarRequerimientodeprocesoAdmin(int IDlibro, int Idproceso,int identificadorE, int Idpanterior,int Idrequerimiento, int Idtipoproceso, string estadopro,HttpPostedFileBase Documento, string resp, int op)
        {
            try
            {
                string estadoDoc = "Pendiente";
                string DetalleDoc = "";
                string Detalle = "";
                bool obligatorio = false;
                bool resultp = false;
                int visibleautor = 0;
                int visiblevaluador = 0;
                int visibleadmin = 0;
    
         
                string emisor = Session["Nombres"].ToString();
                string fechactualizada = System.DateTime.Now.ToString("dd/MM/yyyy" + " - " + "H:mm tt");

                string detalleobservacion = "";
                int idemisor = Convert.ToInt32(Session["Id"]);
                string visiblereq = Session["Type"].ToString();
                int cont = 0;
           
           

                ViewBag.DocumentosEnviados = conexDocs.VerDocumentosenviados(Idproceso, idemisor);
                ViewBag.Requerimientos = conexRequerimientoPro.ListarRequerimientosProcesoxId(Idtipoproceso, visiblereq);
                ViewBag.ObservacionesEnviadas = conexObser.VerObservacionesEnviadas(Idproceso, idemisor);



                foreach (var item in ViewBag.Requerimientos)
                {
                    ViewBag.Obligatorios = item.Obligatorios;
                    break;
                }

            

                foreach (var item4 in ViewBag.DocumentosEnviados)
                {
                    if (item4.EstadoDocu == "Pendiente" && item4.DocObligatorio==true)
                    {
                        cont++;
                    }

                }

                foreach (var item4 in ViewBag.ObservacionesEnviadas)
                {
                    if (item4.EstadoObservacion == "Pendiente" && item4.RespuestaProceso == true && item4.Obligatoria == true)
                    {
                        cont++;
                    }

                }



            

                foreach (var item3 in ViewBag.DocumentosEnviados)
                {
                    if (Idrequerimiento == item3.Idrequerimiento && item3.EstadoDocu == "Pendiente")
                    {
                        TempData["ERROR2"] = "El documento correspondiente a este requerimiento ya se ha registrado, intente guardar el documento correcto.";

                        return RedirectToAction("VerProcesoAdmin", new RouteValueDictionary(new { controller = "Proceso", action = "VerProcesoAdmin", Idlibro = IDlibro, Idproceso = Idproceso }));

                    }
                }


                foreach (var item4 in ViewBag.ObservacionesEnviadas)
                {
                    if (item4.EstadoObservacion == "Pendiente" && item4.Idrequerimiento == Idrequerimiento)
                    {
                        TempData["ERROR2"] = "La respuesta correspondiente a este requerimiento ya se ha registrado.";

                        return RedirectToAction("VerProcesoAdmin", new RouteValueDictionary(new { controller = "Proceso", action = "VerProcesoAdmin", Idlibro = IDlibro, Idproceso = Idproceso }));

                    }

                }



                foreach (var item in ViewBag.Requerimientos)
                {

                    if (Idrequerimiento == item.IDrequerimiento)
                    {
                        Detalle = item.Detallerequerimiento;
                        visibleadmin = Convert.ToInt32(item.Visibleadmin);
                        visibleautor = Convert.ToInt32(item.Visibleautor);
                        visiblevaluador = Convert.ToInt32(item.Visibleevaluador);
                        obligatorio = item.Obligatorio;
                        resultp = item.ResultadoProceso;
                        break;


                    }


                }


                if (op != 0)
                {

                    foreach (var item3 in conexRespuestaReq.ListarRespuestasRequerimientoxIdRequerimiento(op, Idrequerimiento))
                    {
                        DetalleDoc = item3.DetalleResp;
                        detalleobservacion = item3.DetalleResp;
                    }
                }
                else
                {
                    DetalleDoc = Detalle;
                }

            

                if (resultp == true)
                {

                    int ep = conexProceso.EditarProceso1(Idproceso, fechactualizada, estadopro, Idpanterior, op, identificadorE);
                }




                if (Documento != null)
                  {


                        string fecha = System.DateTime.Now.ToString("dd-MM-yyyy H-mm");
                        string Extension = GetExtension(Documento.FileName);
                        string NombreDoc = IDlibro + "_" + fecha + "_" + Detalle + "." + Extension;

                        string ruta = Server.MapPath("~/Files/Documents/");

                        if (Extension == "txt" || Extension == "doc" || Extension == "docx" || Extension == "pdf" || Extension == "rtf")
                        {

                            Documento.SaveAs(ruta + NombreDoc);
                            int d = conexDocs.InsertarDocumento(Idproceso, NombreDoc, DetalleDoc, visibleautor, emisor, fechactualizada, estadoDoc, visibleadmin, visiblevaluador, idemisor, Idrequerimiento,obligatorio);

                       
                           

                            TempData["OK"] = "Documento guardado correctamente";

                        }
                        else
                        {
                            TempData["ERROR2"] = "Formato de archivo no aceptado";
                        }

                }
                
                    
                   


                if (resp != null)
                {
                    int r = conexObser.AgregarObservacion(Idproceso, resp, detalleobservacion, emisor, Detalle, fechactualizada, true, "Pendiente", idemisor, true, Idrequerimiento, obligatorio);

                    TempData["OK"] = "Observación con respuesta registrada correctamente";
                }


                if (op!=0 && resp == null && Documento==null)
                {
                    int r = conexObser.AgregarObservacion(Idproceso, detalleobservacion, "", emisor, Detalle, fechactualizada, true, "Pendiente", idemisor, true, Idrequerimiento, obligatorio);

                    TempData["OK"] = "Observación con respuesta registrada correctamente";
                }

            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";
            }


            return RedirectToAction("VerProcesoAdmin", new RouteValueDictionary(new { controller = "Proceso", action = "VerProcesoAdmin", Idlibro = IDlibro, Idproceso = Idproceso }));

        }






        
        //Función para enviar resultados de procesos a autor 
        public ActionResult EnviarResultados(int IDlibro, int Idproceso,int op, int progresoL, int Idpanterior)
        {
            try
            {

                bool general = false;
                string procesoa = "";
                bool est = false;
                string titulo = "";
                int Idu = 0;
                string detalleopcionrespuesta = "";
                int identificadorE = 9; //cuando se envian los resultados
                string emisor = Session["Nombres"].ToString();
                string fechactualizada = System.DateTime.Now.ToString("dd/MM/yyyy" + " - " + "H:mm tt");
                int idemisor = Convert.ToInt32(Session["Id"]);
                string visiblereq = Session["Type"].ToString();
                int Idtipoproce = 0;
                int Idreq = 0;
                int progreso = progresoL;
                string estado = "";
                int cont = 0;
                //int conte = 0;
                string estadomin = "";
                int e, ne, a, nAutor, r, el, eob = 0;
                string notificacion2 = "";
                string notificacion3 = "";
                string notificacion1 = ""; 
                int ed = 0;
                //int idestado = 0;
                int identificador = 0;
                string detallenotificacion = "Proceso actualizado";
                string asunto = "";
                string urlevaluador = "Libro/LibrosEvaluar";
                string visible3 = "Evaluador";
                string visible2 = "Autor";
                string urlAutor = "Proceso/VerProcesoAutor?Idlibro=" + IDlibro + "&Idproceso=" + Idproceso + "";




                foreach (var item2 in conexProceso.VerProcesoxIdp(IDlibro, Idproceso))
                {
                    titulo = item2.Titulo;
                    Idu = Convert.ToInt32(item2.Idusuario);
                    procesoa = item2.Detalletipospro;
                    Idtipoproce = Convert.ToInt32(item2.Idtipoproceso);
                    identificador = Convert.ToInt32(item2.Identificador);
                    if (item2.Identificador != 1)
                    {
                        Idpanterior = Convert.ToInt32(item2.Idprocanterior);
                    }

                }

                asunto = detallenotificacion + " - [Libro: " + titulo + "]";

                ViewBag.Requerimientos = conexRequerimientoPro.ListarRequerimientosProcesoxId(Idtipoproce, visiblereq);
                ViewBag.Documentosenviados = conexDocs.VerDocumentosenviados(Idproceso, idemisor);
                ViewBag.DocumentosRecibidos = conexDocs.VerDocumentosrecibidos(Idproceso, idemisor);
                ViewBag.ObservacionesEnviadas = conexObser.VerObservacionesEnviadas(Idproceso, idemisor);



                foreach (var item in ViewBag.Requerimientos)
                {
                    ViewBag.Obligatorios = item.Obligatorios;

                    if (item.respuesta == "Si" && item.ResultadoProceso == true)
                    {
                        Idreq = item.IDrequerimiento;
                        break;
                    }
                   
                }



                foreach (var item4 in ViewBag.DocumentosEnviados)
                {
                    if (item4.EstadoDocu == "Pendiente" && item4.DocObligatorio == true)
                    {
                        cont++;
                    }

                }


                foreach (var item4 in ViewBag.ObservacionesEnviadas)
                {
                    if (item4.EstadoObservacion == "Pendiente" && item4.RespuestaProceso == true && item4.Obligatoria == true)
                    {
                        cont++;
                    }

                }

              

                if (cont < ViewBag.Obligatorios || cont == 0)
                {
                    TempData["ERROR2"] = "Faltan documentos por agregar o respuesta requerida !!!.";
                    return RedirectToAction("VerProcesoAdmin", new RouteValueDictionary(new { controller = "Proceso", action = "VerProcesoAdmin", Idlibro = IDlibro, Idproceso = Idproceso }));

                }



                if (cont == ViewBag.Obligatorios)
                {

                    if (op != 0)
                    {

                        foreach (var item2 in conexRespuestaReq.ListarRespuestasRequerimientoxIdRequerimiento(op, Idreq))
                        {
                            progreso = progreso + Convert.ToInt32(item2.ProgresoResp);
                            detalleopcionrespuesta = item2.DetalleResp; //estado

                        }
                        //detalleproceso = detalleopcionrespuesta;
                    }



                    if (op == 0)
                    {
                        foreach (var item in conexEstado.ListarEstadoxIdentificador(Idtipoproce, 10))
                        {
                            identificadorE = item.IdentificadorEstados;
                            estado = item.DetalleEstados;
                        }
                    }
                    else
                    {
                        
                        foreach (var item in conexEstado.ListarEstadoxDetalleRespuesta(Idtipoproce, detalleopcionrespuesta))
                        {
                            identificadorE = item.IdentificadorEstados;
                            estado = item.DetalleEstados;
                        }
                            
                    }




                    estadomin = estado.ToLower(); ;


                    if (identificador == 4)
                    {

                        foreach (var iteme in conexEvaluador.VerEvaluadores(IDlibro))
                        {

                            if (identificadorE == 5)
                            {
                               
                                 e = conexEvaluador.EditarEstadoAsignacionEvaluador(Idproceso, Convert.ToInt32(iteme.Idevaluador), "Pendiente");
                                 notificacion2 = "Libro " + estadomin + ",se ha asignado el libro " + titulo + " para volver a realizar la evaluación correspondiente al proceso de " + procesoa + ". Próximamente se enviará el documento respectivo para evaluar.";
                                 ne = conexNotificacion.InsertarNotificacion(notificacion2, urlevaluador, visible3, Convert.ToInt32(iteme.Idevaluador), 1, fechactualizada, detallenotificacion, general);

                                SendEmail(notificacion2, Convert.ToInt32(iteme.Idevaluador), asunto);
                            }
                            if (identificadorE == 4 || identificadorE == 6)
                            {
                                a = conexUser.EditarNumLibrosFinalizados(Convert.ToInt32(iteme.Idevaluador));
                                notificacion3 = "Libro "+estadomin+", el proceso de " + procesoa + " para el libro " + titulo + " ha finalizado. Gracias por su colaboración en la evaluación de este libro.";

                                ne = conexNotificacion.InsertarNotificacion(notificacion3, urlevaluador, visible3, Convert.ToInt32(iteme.Idevaluador), 1, fechactualizada, detallenotificacion, general);

                               SendEmail(notificacion3, Convert.ToInt32(iteme.Idevaluador), asunto);
                            }
                           
                           

                        }




                        //cambiar estado a documentos recibidos por evaluadores
                        //foreach (var itemd in conexDocs.VerDocumentosrecibidos(Idproceso, idemisor))
                        //{
                        //    if (itemd.Idemisor != Idu && itemd.EstadoDocu == "Pendiente revisar")
                        //    {

                        //        ed = conexDocs.EditarDocumento(itemd.IDdocumento, Idproceso, "Enviado");
                        //    }
                        //}


                        ed = conexDocs.EditarEstadoDocumentosEnviadosxEvaluadores(idemisor, Idu, Idproceso, "Pendiente revisar", "Enviado");
                        //posible elimianr esto
                        //ed = conexDocs.EditarEstadoDocumentosEnviadosxEvaluadores(idemisor, Idu, Idproceso, "Enviado en espera", "Enviado");
                        ed = conexDocs.EditarEstadoDocumentosEnviados(Idu, Idproceso, "Pendiente a evaluar", "Revisado");

                    }





                    //notificación para autores   
                    notificacion1 = "Se ha actualizado información sobre el proceso de " + procesoa + " correspondiente al libro titulado " + titulo + ".";        
                    nAutor = conexNotificacion.InsertarNotificacion(notificacion1, urlAutor, visible2, Idu, 1, fechactualizada, detallenotificacion, general);




                    ///editar estado de documentos enviados por autor 
                    ed = conexDocs.EditarEstadoDocumentosEnviados(Idu, Idproceso, "Enviado", "Revisado");




                    //editar proceso y libro
                    r = conexProceso.EditarProceso1(Idproceso, fechactualizada, estado, Idpanterior, 0, identificadorE);
                    el = conexLibro.EditarLibroP(IDlibro, procesoa, progreso, fechactualizada, estado, Idproceso, est);
                       
                    
                    

                    //cambiar estado de documentos enviados por admin
                    ed = conexDocs.EditarEstadoDocumentosEnviados(idemisor, Idproceso, "Enviado", "Revisado");




                    //cambiar estado a documentos agregados por admin para que sean visibles por autor
                    eob = conexObser.EditarEstadoObservacionesEnviadas(idemisor, Idproceso, "Pendiente", "Enviado");
                    ed = conexDocs.EditarEstadoDocumentosEnviados(idemisor, Idproceso, "Pendiente", "Enviado");




                    if (identificador == 4)
                    {
                        SendEmailDocumentos2(notificacion1, Idu, asunto, Idproceso, idemisor);
                    }
                    else
                    {
                        SendEmailDocumentos(notificacion1, Idu, asunto, Idproceso, idemisor);
                    }





                    TempData["OK"] = "Resultados enviados correctamente.";

                    

                }

            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";
            }


            return RedirectToAction("VerProcesoAdmin", new RouteValueDictionary(new { controller = "Proceso", action = "VerProcesoAdmin", Idlibro = IDlibro, Idproceso = Idproceso }));

        }






        
        //Función para guardar requerimientos de proceso de evaluador
        [HttpPost]
        public ActionResult GuardarRequerimientodeprocesoEvaluador(int IDlibro, int Idproceso,int Idtipoproceso, int Ida,int Idrequerimiento, HttpPostedFileBase Documento, string resp, int op)
        {
            try { 
            
                string estadoDoc = "Pendiente";
                string emisor = Session["Nombres"].ToString();
                int ID = Convert.ToInt32(Session["Id"]);
                string visible= Session["Type"].ToString();
                int visibleadmin = 0;
                int visibleautor = 0;
                int visiblevaluador = 0;
                bool obligatorio=false;
                int cont = 0;
                int d = 0;
                int Num = 0;
                string Detalle = "";
                string DetalleDoc = "";
    
                string detalleobservacion = "";
                string fecha = System.DateTime.Now.ToString("dd/MM/yyyy" + " - " + "H:mm tt");
           


                ViewBag.DocumentosEnviados = conexDocs.VerDocumentosenviados(Idproceso, ID);
                ViewBag.Requerimientos = conexRequerimientoPro.ListarRequerimientosProcesoxId(Idtipoproceso, visible);
                ViewBag.ObservacionesEnviadas = conexObser.VerObservacionesEnviadas(Idproceso, ID);


                foreach (var item in ViewBag.Requerimientos)
                {
                    ViewBag.Obligatorios = item.Obligatorios;
                    break;
                }


                foreach (var item4 in ViewBag.DocumentosEnviados)
                {
                    if (item4.EstadoDocu == "Pendiente" && item4.DocObligatorio == true)
                    {
                        cont++;
                    }

                }


                foreach (var item4 in ViewBag.ObservacionesEnviadas)
                {
                    if (item4.EstadoObservacion == "Pendiente" && item4.RespuestaProceso == true && item4.Obligatoria == true)
                    {
                        cont++;
                    }

                }




                foreach (var item3 in ViewBag.DocumentosEnviados)
                {
                    if (Idrequerimiento == item3.Idrequerimiento && item3.EstadoDocu == "Pendiente")
                    {
                        TempData["ERROR2"] = "El documento correspondiente a este requerimiento ya se ha registrado, intente guardar el documento correcto; si ya lo hizo proceda a finalizar la evaluación.";

                        return RedirectToAction("VerProcesoEvaluador", new RouteValueDictionary(new { controller = "Proceso", action = "VerProcesoEvaluador", Idlibro = IDlibro, Idproceso = Idproceso, Ida = Ida }));

                    }
                }


                foreach (var item4 in ViewBag.ObservacionesEnviadas)
                {
                    if (item4.EstadoObservacion == "Pendiente" && item4.Idrequerimiento == Idrequerimiento)
                    {
                        TempData["ERROR2"] = "La respuesta correspondiente a este requerimiento ya se ha registrado.";

                        return RedirectToAction("VerProcesoEvaluador", new RouteValueDictionary(new { controller = "Proceso", action = "VerProcesoEvaluador", Idlibro = IDlibro, Idproceso = Idproceso, Ida = Ida }));

                    }

                }




                foreach (var item in ViewBag.Requerimientos)
                {

                    if (Idrequerimiento == item.IDrequerimiento)
                    {
                        Detalle = item.Detallerequerimiento;
                        visibleadmin = Convert.ToInt32(item.Visibleadmin);
                        visibleautor = Convert.ToInt32(item.Visibleautor);
                        visiblevaluador = Convert.ToInt32(item.Visibleevaluador);
                        obligatorio = item.Obligatorio;

                        break;


                    }
                }




                if (op != 0)
                {
                    foreach (var item3 in conexRespuestaReq.ListarRespuestasRequerimientoxIdRequerimiento(op, Idrequerimiento))
                    {
                        DetalleDoc = item3.DetalleResp;
                        detalleobservacion = item3.DetalleResp;
                    }
                }
                else
                {
                    DetalleDoc = Detalle;
                }





                foreach (var item in conexEvaluador.VerDetalleAsignacionEvaluador(Idproceso, ID))
                {
                    Num = Convert.ToInt32(item.NumEvaluador);
                }


                emisor = emisor + " (Evaluador " + Num + ")";



                if (Documento != null)
                {

                 

                   
                    string fecha1 = System.DateTime.Now.ToString("dd-MM-yyyy" + " " + "H-mm");
                  
                    string Extension = GetExtension(Documento.FileName);
                    
                   
                    string NombreDoc = IDlibro+"_"+fecha1+ "_" + Detalle + "." + Extension;

                    string ruta = Server.MapPath("~/Files/Documents/");

                if (Extension == "txt" || Extension == "doc" || Extension == "docx" || Extension == "pdf" || Extension == "rtf")
                {

                    if (Extension == "doc" || Extension=="docx")
                    {
                      
                        emisor = "Evaluador "+Num;
                     
                        DetalleDoc = "Observaciones registradas";
                       
                    }
                     

                    Documento.SaveAs(ruta + NombreDoc);
                    d = conexDocs.InsertarDocumento(Idproceso, NombreDoc, DetalleDoc, visibleautor, emisor, fecha, estadoDoc,visibleadmin, visiblevaluador, ID,Idrequerimiento,obligatorio);

                     


                   TempData["OK"] = "Documento guardado correctamente";

                }
                else
                {
                    TempData["ERROR2"] = "Formato de archivo no aceptado";
                }

                }
               



              


                if (resp != null)
                {
                    emisor = "Evaluador " + Num;


                    int r = conexObser.AgregarObservacion(Idproceso, resp, detalleobservacion, emisor, Detalle, fecha, false, "Pendiente", ID, true, Idrequerimiento, obligatorio);

                    TempData["OK"] = "Observación con respuesta registrada correctamente";

                }


                if (op != 0 && resp == null && Documento==null)
                {
                    int r = conexObser.AgregarObservacion(Idproceso, detalleobservacion, "", emisor, Detalle, fecha, false, "Pendiente", ID, true, Idrequerimiento, obligatorio);

                    TempData["OK"] = "Observación con respuesta registrada correctamente";
                }




            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";
            }



            return RedirectToAction("VerProcesoEvaluador", new RouteValueDictionary(new { controller = "Proceso", action = "VerProcesoEvaluador", Idlibro = IDlibro, Idproceso = Idproceso,Ida=Ida }));

        }






        //Vista de proceso de libro evaluador
        public ActionResult VerProcesoEvaluador(int IDlibro, int Idproceso,int Ida)
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

            int Idc = 0;
            int idestado = 0;
            int Idtipoproc = 0;
            string visible = Session["Type"].ToString();
            int ntotal = 0;
            int nnuevas = 0;
            int Id = Convert.ToInt32(Session["Id"]);
           


            foreach (var item in conexCuestionario.VerDetalleAsignacion(Ida))
            {

                Idc = Convert.ToInt32(item.Idcuestionario);
                if (item.Idpersona != Id)
                {
                    TempData["ERROR"] = "Algo salió mal";
                    return RedirectToAction("LibrosEvaluar", "Libro");
                }
            }


           
            var listaprocess = conexProceso.VerProcesoxIdp(IDlibro, Idproceso);
            foreach(var item in listaprocess)
            {
                Idtipoproc = Convert.ToInt32(item.Idtipoproceso);
                idestado = Convert.ToInt32(item.Idestado);
            }



           
            ViewBag.Notificaciones = conexNotificacion.VerNotificacionesxIduser(Id);

            foreach (var item in ViewBag.Notificaciones)
            {
                ntotal = Convert.ToInt32(item.TotalNotificacionesrecibidas);
                nnuevas = Convert.ToInt32(item.TotalNotificacionesnuevas);
                break;
            }
            ViewBag.TotalNotificaciones = ntotal;
            ViewBag.NuevasNotificaciones = nnuevas;


          
            
          
            ViewBag.Idasignacion = Ida;
            ViewBag.MensajeProceso = conexMensaje.VerMensajeEstadoProceso(idestado, Idtipoproc, visible);
            ViewBag.SeleccionRespuesta = conexRespuestaReq.ListarRespuestasRequerimientoxTipoProceso(Idtipoproc, visible);
            ViewBag.MensajeProceso = conexMensaje.VerMensajeEstadoProceso(idestado, Idtipoproc, visible);
            ViewBag.Requerimientoproceso = conexRequerimientoPro.ListarRequerimientosProcesoxId(Idtipoproc, visible);
            ViewBag.DetalleAsignacionEvaluador = conexEvaluador.VerDetalleAsignacionEvaluador(Idproceso,Id);


         

            ViewBag.Idc = Idc;
            ViewBag.Usuario = conexUser.BuscarUsuario(Session["Username"].ToString());
            
           
            ViewBag.Documentosenviados = conexDocs.VerDocumentosenviados(Idproceso,Id);
            ViewBag.Documentosrecibidos = conexDocs.VerDocumentosrecibidos(Idproceso,Id);
            
       
            return View(listaprocess);

        }







        //Función para agregar proceso
        [HttpPost]
        public ActionResult AddProceso(int identificador,int Numero, string detalletipospro, string descripcion, int progreso,string duracionproceso)
        {
            try
            {
                if(Numero<=0 || progreso <= 0 || identificador<=0)
                {
                    TempData["ERROR2"] = "No se aceptan valores negativos, vuelva a intentarlo e ingrese los valores correctos.";
                    return RedirectToAction("GestionarProcesos", "Proceso");
                }


                int r = conexTipoPro.AgregarTiposprocesos(detalletipospro, progreso, duracionproceso, Numero,identificador,descripcion);

                if (r == 1)
                {
                    TempData["OK"] = "Tipo de proceso registrado";
                }
                else if (r == 2)
                {
                    TempData["ERROR2"] = "El identificador para este tipo de proceso ya existe, intente con un valor diferente.";
                }
                else if (r == 4)
                {
                    TempData["ERROR2"] = "El número de este tipo de proceso ya existe, intente con un valor diferente.";
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

            return RedirectToAction("GestionarProcesos", "Proceso");
        }







   
        //Función para enviar proceso a revisión por autor
        public ActionResult EnviarRevision1(int IDlibro, int Idproceso, int Op, int progresoL)
        {
            try
            {
                int r, el,ed,eob,n1;
                bool general = false;
                int cont = 0;
                int Idpanterior = 0;
                bool est = false;
                string emisor= Session["Nombres"].ToString();
                int idemisor = Convert.ToInt32(Session["Id"]);
                string visible = Session["Type"].ToString();
                int identificador = 0;
                string procesoL = "";
                int Idtipoproceso = 0;
                string fechactualizada = System.DateTime.Now.ToString("dd/MM/yyyy" + " - " + "H:mm tt");
                string estado = "";//En revisión
                string titulo = "";
                int identificadorE = 2;
                int progreso = progresoL;
                string detalleopcionrespuesta = "";
                int Idreq = 0;
                //int idestado = 0;
                string notificacion = "";
                string urlnotificacion = "Proceso/VerProcesoAdmin?Idlibro=" + IDlibro + "&Idproceso=" + Idproceso + "";
                string visiblen = "Administrador";
                string detallenotificacion = "Proceso actualizado";
                string asunto = "";



                foreach (var item2 in conexProceso.VerProcesoxIdp(IDlibro, Idproceso))
                {
                    procesoL = item2.Detalletipospro;
                    Idtipoproceso = Convert.ToInt32(item2.Idtipoproceso);
                    identificador = Convert.ToInt32(item2.Identificador);
                    titulo = item2.Titulo;


                    if (item2.Identificador != 1)
                    {
                        Idpanterior = Convert.ToInt32(item2.Idprocanterior);
                    }

                }

                asunto = detallenotificacion + " - [Libro: " + titulo + "]";
                ViewBag.Requerimientos = conexRequerimientoPro.ListarRequerimientosProcesoxId(Idtipoproceso, visible);
                ViewBag.Documentosenviados = conexDocs.VerDocumentosenviados(Idproceso, idemisor);
                ViewBag.ObservacionesEnviadas = conexObser.VerObservacionesEnviadas(Idproceso, idemisor);



                foreach (var item in ViewBag.Requerimientos)
                {
                    ViewBag.Obligatorios = item.Obligatorios;
                    if (item.respuesta == "Si" && item.ResultadoProceso == true)
                    {
                        Idreq = item.IDrequerimiento;
                        break;
                    }
          
                }



                foreach (var item4 in ViewBag.DocumentosEnviados)
                {
                    if (item4.EstadoDocu == "Pendiente" && item4.DocObligatorio==true)
                    {
                        cont++;
                    }

                }


                foreach (var item4 in ViewBag.ObservacionesEnviadas)
                {
                    if (item4.EstadoObservacion == "Pendiente" && item4.RespuestaProceso == true && item4.Obligatoria == true)
                    {
                        cont++;
                    }

                }


             

                if (cont < ViewBag.Obligatorios || cont == 0)
                {
                    TempData["ERROR2"] = "Faltan documentos por agregar o respuesta requerida !!!.";
                    return RedirectToAction("VerProcesoAutor", new RouteValueDictionary(new { controller = "Proceso", action = "VerProcesoAutor", Idlibro = IDlibro, Idproceso = Idproceso }));

                }





                if (cont == ViewBag.Obligatorios)
                {

                    if (Op != 0)
                    {

                        foreach (var item2 in conexRespuestaReq.ListarRespuestasRequerimientoxIdRequerimiento(Op, Idreq))
                        {
                            progreso = progreso + Convert.ToInt32(item2.ProgresoResp);
                            detalleopcionrespuesta = item2.DetalleResp; //estado

                        }
                        //detalleproceso = detalleopcionrespuesta;
                    }





                    if (Op == 0)
                    {
                        foreach (var item in conexEstado.ListarEstadoxIdentificador(Idtipoproceso, 2))
                        {
                            identificadorE = item.IdentificadorEstados;
                            estado = item.DetalleEstados;
                        }
                    }
                    else
                    {

                        foreach (var item in conexEstado.ListarEstadoxDetalleRespuesta(Idtipoproceso, detalleopcionrespuesta))
                        {
                            identificadorE = item.IdentificadorEstados;
                            estado = item.DetalleEstados;
                        }

                    }



                    
                    r = conexProceso.EditarProceso1(Idproceso, fechactualizada, estado, Idpanterior,0,identificadorE);
                    el = conexLibro.EditarLibroP(IDlibro, procesoL, progreso, fechactualizada, estado, Idproceso, est);
                   


               

                    ed = conexDocs.EditarEstadoDocumentosEnviados(idemisor, Idproceso, "Pendiente", "Enviado");
                    eob = conexObser.EditarEstadoObservacionesEnviadas(idemisor, Idproceso, "Pendiente", "Enviado");




                    if (r ==1 && el==1)
                    {
                        TempData["OK"] = "El proceso ha sido enviado a revisión";
                      

                        //agregar notificación

                        notificacion = emisor + " ha actualizado información sobre el proceso de " + procesoL + " correspondiente al libro titulado " + titulo + ".";
                       

                        foreach (var item in conexUser.BuscarUsuarioxTipoUsuario(visiblen))
                        {
                            n1 = conexNotificacion.InsertarNotificacion(notificacion, urlnotificacion, visiblen, item.IDusuario, 1, fechactualizada, detallenotificacion,general);
                            break;
                        }

                        //enviar correo a editorial
                        SendEmailDocumentosEditorial(notificacion, asunto, Idproceso, idemisor);
                       }

                    else
                    {
                        TempData["ERROR"] = "Algo salió mal";
                        //return View();
                    }


                }





            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";
            }
            return RedirectToAction("VerProcesoAutor", new RouteValueDictionary(new { controller = "Proceso", action = "VerProcesoAutor", Idlibro = IDlibro, Idproceso = Idproceso }));

        }








        //Función para enviar proceso a revisión por autor cuando el libro ha sido regresado
        public ActionResult EnviarRevision2(int IDlibro, int Idproceso, int progresoL)
        {
            try
            {
                int r, el,ed,n1,Op=0;
                bool general = false;
                int cont = 0;
                int Idpanterior = 0;
                bool est = false;
                string emisor = Session["Nombres"].ToString();
                int idemisor = Convert.ToInt32(Session["Id"]);
                string visible = Session["Type"].ToString();
                int identificadorE = 2;
                string procesoL = "";
                int Idtipoproceso = 0;
                string fechactualizada = System.DateTime.Now.ToString("dd/MM/yyyy" + " - " + "H:mm tt");
                string estado = "";
                string titulol = "";
                string notificacion = "";
                string detallen = "Proceso actualizado";
                string asunto = "";
                string urlnotificacion = "Proceso/VerProcesoAdmin?Idlibro=" + IDlibro + "&Idproceso=" + Idproceso + "";
                string visiblen = "Administrador";




                ViewBag.Documentosenviados = conexDocs.VerDocumentosenviados(Idproceso, idemisor);

                foreach (var item2 in conexProceso.VerProcesoxIdp(IDlibro, Idproceso))
                {
                    procesoL = item2.Detalletipospro;
                    Idtipoproceso = Convert.ToInt32(item2.Idtipoproceso);
                  
                    titulol = item2.Titulo;

                    if (item2.Identificador != 1)
                    {
                        Idpanterior = Convert.ToInt32(item2.Idprocanterior);
                    }

                }


                asunto = detallen + " - [Libro: " + titulol + "]";

                foreach (var item1 in ViewBag.Documentosenviados)
                {
                    if (item1.EstadoDocu == "Pendiente" && item1.Detalledocu=="Libro corregido")
                    {
                        cont++;
                    }
                }




                if (cont == 0)
                {
                    TempData["ERROR2"] = "No se ha guardado el documento solicitado, vuelva a intentarlo.";
                    return RedirectToAction("VerProcesoAutor", new RouteValueDictionary(new { controller = "Proceso", action = "VerProcesoAutor", Idlibro = IDlibro, Idproceso = Idproceso }));

                }
                else
                {

              

                    foreach (var item in conexEstado.ListarEstadoxIdentificador(Idtipoproceso,2))
                    {
                        
                                identificadorE = item.IdentificadorEstados;
                                estado = item.DetalleEstados;
                         
                    }


                    r = conexProceso.EditarProceso1(Idproceso, fechactualizada, estado, Idpanterior, Op, identificadorE);
                    el = conexLibro.EditarLibroP(IDlibro, procesoL, progresoL, fechactualizada, estado, Idproceso, est);
                  

                    

                    ed = conexDocs.EditarEstadoDocumentosEnviados(idemisor, Idproceso, "Pendiente", "Enviado");


                    if (r == 1 && el == 1)
                    {
                        TempData["OK"] = "El proceso ha sido enviado a revisión";

                        //crear notificación
                        notificacion = emisor + " ha actualizado información sobre el proceso de " + procesoL + ". Se ha enviado a revisión las correcciones pendientes correspondiente al libro titulado " + titulol + ".";

                        foreach (var item in conexUser.BuscarUsuarioxTipoUsuario(visiblen))
                        {
                            n1 = conexNotificacion.InsertarNotificacion(notificacion, urlnotificacion, visiblen, item.IDusuario, 1, fechactualizada, detallen, general);
                            break;
                        }

                        //enviar correo a editorial
                        SendEmailDocumentosEditorial(notificacion, asunto, Idproceso, idemisor);


                    }

                


                }
               



            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";
            }
            return RedirectToAction("VerProcesoAutor", new RouteValueDictionary(new { controller = "Proceso", action = "VerProcesoAutor", Idlibro = IDlibro, Idproceso = Idproceso }));

        }






        

        //Función para agregar documento corregido cuando el libro ha sido regresado
        [HttpPost]
        public ActionResult GuardarDocCorregido(int IDlibro, int Idproceso, string Detalle, HttpPostedFileBase Documento)
        {
            try
            {
                bool obligatorio = true;
                int visibleautor = 0;
                int visibleadmin = 1;
                int visiblevaluador = 0;
                string emisor = Session["Nombres"].ToString();
                string estadoDoc = "Pendiente";
                string Detalledoc = "Libro corregido";
                string fechactualizada = System.DateTime.Now.ToString("dd/MM/yyyy" + " - " + "H:mm tt");
                string fecha = System.DateTime.Now.ToString("dd-MM-yyyy" + " " + "H-mm");
                string Extension = GetExtension(Documento.FileName);
                string NombreDoc = IDlibro + "_" + fecha + "_" + Detalledoc + "." + Extension;
                string ruta = Server.MapPath("~/Files/Documents/");
                int idemisor = Convert.ToInt32(Session["Id"]);
                ViewBag.Documentosenviados = conexDocs.VerDocumentosenviados(Idproceso, idemisor);


                foreach (var item1 in ViewBag.Documentosenviados)
                {
                    if (item1.EstadoDocu == "Pendiente" && item1.Detalledocu == "Libro corregido")
                    {
                        TempData["INFO"] = "El documento solicitado ya se encuentra guardado, proceda a enviar el proceso a revisión.";
                        return RedirectToAction("VerProcesoAutor", new RouteValueDictionary(new { controller = "Proceso", action = "VerProcesoAutor", Idlibro = IDlibro, Idproceso = Idproceso }));

                    }
                }



                if (Extension == "txt" || Extension == "doc" || Extension == "docx" || Extension == "pdf" || Extension == "rtf")
                {

                    Documento.SaveAs(ruta + NombreDoc);
                    int d = conexDocs.InsertarDocumento(Idproceso, NombreDoc, Detalledoc, visibleautor, emisor, fechactualizada, estadoDoc, visibleadmin, visiblevaluador, idemisor, 0,obligatorio);

                    TempData["OK"] = "Documento guardado exitósamente";

                }
                else
                {
                    TempData["ERROR2"] = "Formato de archivo no aceptado";
                }



            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";
            }

            return RedirectToAction("VerProcesoAutor", new RouteValueDictionary(new { controller = "Proceso", action = "VerProcesoAutor", Idlibro = IDlibro, Idproceso = Idproceso }));

        }









        
        //Función para enviar proceso a evaluar por pares académicos
        public ActionResult EnviaraEvaluarPares(int IDlibro, int Idproceso, int Idpanterior, string procesoL, int progresoL)
        {
            try
            {
                int Idu = 0;
                string titulol = "";
                string procesoa = "";
                bool est = false;
                bool general = false;
                int idemisor = Convert.ToInt32(Session["Id"]);
                int ed,r,el,n1,n2 = 0;
                int cont = 0;
                string fechactualizada = System.DateTime.Now.ToString("dd/MM/yyyy" + " - " + "H:mm tt");
                int Idtipoproceso = 0;
                int idestado = 0;
                string estado = "";
                int identificadorE = 7;
                string visibleE = "Evaluador";
                string visibleA = "Autor";
                string detallenotificacionE = "Evaluación pendiente del libro:";
                string asuntoE = "";
                string detallenotificacionA = "Proceso actualizado";
                string asuntoA = "";
                string notificacion2 = "";
                string urlA = "Proceso/VerProcesoAutor?Idlibro=" + IDlibro + "&Idproceso=" + Idproceso + "";
                string urlE = "";
                string notificacion1 = "";

               

                ViewBag.Evaluadores = conexEvaluador.VerEvaluadores(IDlibro);


                if (ViewBag.Evaluadores.Count == 0)
                {
                    TempData["ERROR2"] = "No se ha agregado la información de evaluador(es) aún !!!.";
                    return RedirectToAction("VerProcesoAdmin", new RouteValueDictionary(new { controller = "Proceso", action = "VerProcesoAdmin", IDlibro = IDlibro, Idproceso = Idproceso }));
                }

                if (ViewBag.Evaluadores.Count < 2)
                {
                    TempData["ERROR2"] = "Falta agregar un evaluador !!!.";
                    return RedirectToAction("VerProcesoAdmin", new RouteValueDictionary(new { controller = "Proceso", action = "VerProcesoAdmin", IDlibro = IDlibro, Idproceso = Idproceso }));
                }




                foreach (var item1 in conexProceso.VerProcesoxIdp(IDlibro, Idproceso))
                {
                    titulol = item1.Titulo;
                    procesoa = item1.Detalletipospro;
                    Idu = Convert.ToInt32(item1.Idusuario);
                    Idtipoproceso =Convert.ToInt32(item1.Idtipoproceso);
                }


                asuntoE = detallenotificacionE + " " + titulol + "";
                asuntoA = detallenotificacionA + " - [Libro: " + titulol + "]";


                //documentos enviados por autor
                ViewBag.Docenviadosautor = conexDocs.VerDocumentosenviados(Idproceso, Idu);




                foreach (var item in ViewBag.Docenviadosautor)
                {
                    if (item.EstadoDocu == "Enviado")
                    {
                        cont++;
                    }
                }



                if (cont == 1)
                {
                 


                 ed = conexDocs.EditarEstadoDocumentosEnviados(Idu, Idproceso, "Enviado", "Pendiente a evaluar");

                  

                    foreach (var item in conexEstado.ListarEstadoxIdentificador(Idtipoproceso,7))
                    {
                        estado = item.DetalleEstados;
                        idestado = item.IDEstadosTiposproceso;
                        identificadorE = item.IdentificadorEstados;
                              
                    }



                //editar proceso y libro
                 r = conexProceso.EditarProceso1(Idproceso, fechactualizada, estado, Idpanterior, 0, identificadorE);
                 el = conexLibro.EditarLibroP(IDlibro, procesoL, progresoL, fechactualizada, estado, Idproceso, est);



                    //notificación para autores
                    notificacion1 = "Como parte del proceso de " + procesoa + ". Su libro " + titulol + " ha sido enviado a evaluación por parte de evaluadores externos.";
                    n1 = conexNotificacion.InsertarNotificacion(notificacion1, urlA, visibleA, Idu, 1, fechactualizada, detallenotificacionA, general);
                    SendEmail(notificacion1, Idu, asuntoA);



                    //crear notificacion y enviar correo para evaluadores con documentos adjuntos
                    foreach (var item5 in ViewBag.Evaluadores)
                   {
                    notificacion2 = "Como parte del proceso de " + procesoa + ". El libro " + titulol + " le ha sido asignado para realizar la respectiva evaluación de este proceso. Para ello se le ha facilitado el documento correspondiente al borrador del libro para efectuar la evaluación solicitada.";
                    urlE = "Proceso/VerProcesoEvaluador?Idlibro=" + IDlibro + "&Idproceso=" + Idproceso + "&Ida=" + item5.IdasignacionEvaluacion + ""; ;
                    n2 = conexNotificacion.InsertarNotificacion(notificacion2, urlE, visibleE, item5.Idevaluador, 7, fechactualizada, detallenotificacionE, general);
                    SendEmailDocumentos3(notificacion2, item5.Idevaluador, asuntoE, Idproceso, Idu);

                   }




                if (r != 0)
                {


                    TempData["OK"] = "La documentación del proceso ha sido enviada para evaluarse";
                    //crear notificación para autor

                }

                else
                {
                    TempData["ERROR"] = "Algo salió mal";
                    //return View();
                }
                }
                else
                {
                    TempData["ERROR2"] = "Documento pendiente por parte del autor. No se ha recibido el borrador del libro a evaluar.";
                }
            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";
            }
            return RedirectToAction("VerProcesoAdmin", new RouteValueDictionary(new { controller = "Proceso", action = "VerProcesoAdmin", IDlibro = IDlibro, Idproceso = Idproceso}));

        }







       
        //Función para iniciar procesos 
        public ActionResult IniciarProceso2(int IDlibro, int Idp1, int numproceso, int Idpanterior, string estadop)
        {
            int Idp = 0;
            bool est = false;
            bool general = false;
            string fechactualizada = System.DateTime.Now.ToString("dd/MM/yyyy" + " - " + "H:mm tt");
            int progresoL = 0;
            string procesoL = "";
            int Idtipoproceso = 0;
            int identificadorF = 0;
            int identificadorI = 0;
            string estadoF = ""; //Evaluándose
            string estadoI = "";
            int Iduser = 0;
            string titulol="";
            string procesoa = "";
            string visible1 = "Autor";
            string url = "Proceso/VerProcesoAutor?Idlibro=" + IDlibro + "&Idproceso=" + Idp1 + "";
            string detalleF = "Proceso finalizado";
            string asuntoF = ""; 
            string detalleI = "Nuevo proceso iniciado";
            string asuntoI = "";
            string notificacion1 = "";
            int n1, el, n2,r=0;
            string url2 = "";
            string notificacion2 = "";
          
            



            if (numproceso == 1)
            {
                Idpanterior = Idp1;
            }



            try
            {


                foreach (var item3 in conexProceso.VerProcesoxIdp(IDlibro, Idp1))
                {
                    titulol = item3.Titulo;
                    Iduser = Convert.ToInt32(item3.Idusuario);
                    procesoa = item3.Detalletipospro;
                    //nombresa = item3.PrimerNombre + " " + item3.ApellidoPrimero+" "+item3.ApellidoSegundo;
                    //identificador = Convert.ToInt32(item3.Identificador);

                }

                asuntoF = detalleF + " - [Libro: " + titulol + "]";
                asuntoI = detalleI + " - [Libro: " + titulol + "]";
                numproceso = numproceso + 1;

              


                foreach (var item2 in conexTipoPro.ListarTiposProcesosxnum(numproceso))
                {
                    procesoL = item2.Detalletipospro;
                    Idtipoproceso = item2.IDtiposprocesos;
                    progresoL = Convert.ToInt32(item2.Progreso);
                   
                    //identificador = Convert.ToInt32(item2.Identificador);
                }


                foreach (var item in conexEstado.ListarEstadoxIdentificador(Idtipoproceso, 1))
                {

                    estadoI = item.DetalleEstados;
                    identificadorI = item.IdentificadorEstados;

                }
                foreach (var item in conexEstado.ListarEstadoxIdentificador(Idtipoproceso, 8))
                {

                    estadoF = item.DetalleEstados;
                    identificadorF = item.IdentificadorEstados;

                }




                r = conexProceso.EditarProceso1(Idp1, fechactualizada, estadoF, Idpanterior, 0, identificadorF);

                if (r == 1)
                {
                    
                    //crear notificación para autor: Proceso finalizado 
                    notificacion1 = "El proceso de " + procesoa + " para el libro " + titulol + " ha finalizado correctamente.";
                
                    //enviar correo
                    n1 = conexNotificacion.InsertarNotificacion(notificacion1, url, visible1, Iduser, 1, fechactualizada, detalleF, general);
                    SendEmail(notificacion1, Iduser, asuntoF);


                    //iniciar siguiente proceso y editar libro
                    Idp = conexProceso.CrearProceso2(IDlibro, fechactualizada, fechactualizada, estadoI, Idtipoproceso, Idp1, identificadorI);
                    el = conexLibro.EditarLibroP(IDlibro, procesoL, progresoL, fechactualizada, estadoI, Idp, est);

                    if(Idp!=0 && el == 1)
                    {
                        //crear notificación para autor: nuevo proceso iniciado
                        //enviar correo
                        url2 = "Proceso/VerProcesoAutor?Idlibro=" + IDlibro + "&Idproceso=" + Idp + "";
                        notificacion2 = "Se ha iniciado el proceso de " + procesoL + " para el libro " + titulol + ".";


                        n2 = conexNotificacion.InsertarNotificacion(notificacion2, url2, visible1, Iduser, 1, fechactualizada, detalleI, general);
                        SendEmail(notificacion2, Iduser, asuntoI);


                        TempData["OK"] = "El proceso ha iniciado correctamente";
                    }
                    else
                    {
                        TempData["ERROR2"] = "Algo salió mal";
                    }
                   

                }
                else
                {
                    TempData["ERROR2"] = "Algo salió mal";
                }
 

            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";
            }
            
            return RedirectToAction("VerProcesoAdmin", new RouteValueDictionary(new { controller = "Proceso", action = "VerProcesoAdmin", Idlibro = IDlibro, Idproceso = Idp }));
        }


       





        //Función para eliminar proceso//
        public JsonResult EliminarTipoProceso(int Idtipoproceso)
        {
            int r = 0;
            try
            {
                r = conexTipoPro.EliminarTiposprocesos(Idtipoproceso);

                if (r == 1)
                {
                    TempData["OKEP"] = "OK";


                }

            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";

            }

            return Json(r, JsonRequestBehavior.AllowGet);
        }






        //Función para eliminar observación de proceso//
        public JsonResult EliminarObservacion(int Idobservacion)
        {
            int r = 0;
            try
            {
                r = conexObser.EliminarObservacion(Idobservacion);

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

        




        

        //Función para agregar observación de proceso
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddObservacion(int Idlibro1, int Idproceso1,string titulo, string detalle, string descrip, bool resp, HttpPostedFileBase Documento)
        {
            string emisor = Session["Nombres"].ToString();
            int ID = Convert.ToInt32(Session["Id"]);
            string fecha = System.DateTime.Now.ToString("dd/MM/yyyy" + " - " + "H:mm tt");
            int r,ed,p,el= 0;
            int n1 = 0;
            string titulol = "";
            bool general = false;
            int Idu = 0;
            string detallen = "Nueva observación";
            int Idpanterior = 0;
            string procesoL = "";
            int progresoL = 0;
            string Detalle = "Proceso reversado";
            string urlnotificacion = "Proceso/VerProcesoAutor?Idlibro=" + Idlibro1 + "&Idproceso=" + Idproceso1 + "";
            string visible = "Autor";
            string estadoDoc = "Enviado";
            int idemisor = Convert.ToInt32(Session["Id"]);
            string urlnotificacion1 = "Proceso/VerProcesoAdmin?Idlibro=" + Idlibro1 + "&Idproceso=" + Idproceso1 + "";
            string visible1 = "Administrador";
            int Idtipoproceso = 0;
            string estado = "Proceso reversado";
   


            foreach (var item2 in conexProceso.VerProcesoxIdp(Idlibro1, Idproceso1))
            {
                Idpanterior = Convert.ToInt32(item2.Idprocanterior);
                procesoL = item2.Detalletipospro;
                progresoL = Convert.ToInt32(item2.ProgresoL);
                Idu = Convert.ToInt32(item2.Idusuario);
                titulol = item2.Titulo;
                Idtipoproceso = Convert.ToInt32(item2.Idtipoproceso);

            }
            try
            {
               
                r = conexObser.AgregarObservacion(Idproceso1, descrip, detalle, emisor, titulo,fecha,true,"Enviado",ID,false,0,false);
                if (r ==1)
                {
                    TempData["OK"] = "Observación registrada";



                    if (resp == true)
                    {
                        

                        int visibleautor = 1;
                        int visibleadmin = 0;
                        int visiblevaluador = 0;

                     
                        string notificacion = "Su libro " + titulol + ", ha sido reversado en el proceso de " + procesoL + ", realice las correcciones pertinentes y vuelva a enviar el libro a revisión.";
                        string asunto = Detalle + " - [Libro: " + titulol + "]";
                     
                        //notificar
                        n1 = conexNotificacion.InsertarNotificacion(notificacion, urlnotificacion, visible, Idu, 1, fecha, Detalle,general);

                       


                        if (Documento != null)
                        {
                            bool obligatorio = true;
                            string fecha1 = System.DateTime.Now.ToString("dd-MM-yyyy" + " " + "H-mm");
                            string Extension = GetExtension(Documento.FileName);
                            string NombreDoc = Idlibro1 + "_" + fecha1 +"_Libro_con_observaciones." + Extension;
                            string ruta = Server.MapPath("~/Files/Documents/");

                            if (Extension == "txt" || Extension == "doc" || Extension == "docx" || Extension == "pdf" || Extension == "rtf")
                            {

                                Documento.SaveAs(ruta + NombreDoc);
                                int d = conexDocs.InsertarDocumento(Idproceso1, NombreDoc, Detalle, visibleautor, emisor, fecha, estadoDoc, visibleadmin, visiblevaluador, idemisor,0,obligatorio);
                                //enviar correo con archivo adjunto

                                SendEmailDocumentos(notificacion, Idu, asunto, Idproceso1, idemisor);

                            }
                            else
                            {
                                TempData["ERROR2"] = "Formato de archivo no aceptado";
                            }

                        }
                        else
                        {
                            //enviar correo sin archivo adjunto
                            SendEmail(notificacion, Idu, asunto);
                          
                        }


                        ///editar estado de documentos enviados por admin 
                     
                        ed = conexDocs.EditarEstadoDocumentosEnviados(idemisor, Idproceso1, "Enviado", "Revisado");



                        ///editar estado de documentos enviados por autor 
                     
                        ed = conexDocs.EditarEstadoDocumentosEnviados(Idu, Idproceso1, "Enviado", "Revisado");


                        foreach(var item in conexEstado.ListarEstadoTiposProcesos(Idtipoproceso))
                        {
                            if (item.IdentificadorEstados == 3)
                            {
                                estado = item.DetalleEstados;
                               
                                break;
                            }
                        }

                        p = conexProceso.EditarProceso1(Idproceso1, fecha,estado, Idpanterior, 0,3);

                        el = conexLibro.EditarLibroP(Idlibro1, procesoL, progresoL, fecha,estado, Idproceso1, true);


                    }
                    else
                    {
                        //crear notificación
                        if (Session["Type"].ToString() == "Autor")
                        {

                            string notificacion = emisor + " ha agregado una nueva observación sobre el libro " + titulol + ", para el proceso de " + procesoL + ".";
                            string asunto = detallen + " - [Libro: " + titulol + "]";
                        


                            foreach (var item in conexUser.BuscarUsuarioxTipoUsuario(visible1))
                            {
                                n1 = conexNotificacion.InsertarNotificacion(notificacion, urlnotificacion1, visible1, item.IDusuario, 5, fecha, detallen,general);
                                break;
                            }
                            //enviar correo a editorial
                            SendEmailNotificacionEditorial(notificacion, asunto);





                            return RedirectToAction("VerProcesoAutor", new RouteValueDictionary(new { controller = "Proceso", action = "VerProcesoAutor", IDlibro = Idlibro1, Idproceso = Idproceso1 }));


                        }

                        if (Session["Type"].ToString() == "Administrador")
                        {

                            string notificacion = "Se ha agregado una nueva observación sobre el libro " + titulol + ", para el proceso de " + procesoL + ".";
                            string asunto = detallen + " - [Libro: " + titulol + "]";
                        
                            //notificar
                            n1 = conexNotificacion.InsertarNotificacion(notificacion, urlnotificacion, visible, Idu, 5, fecha, detallen,general);

                            //enviar correo
                            SendEmail(notificacion, Idu, asunto);



                            return RedirectToAction("VerProcesoAdmin", new RouteValueDictionary(new { controller = "Proceso", action = "VerProcesoAdmin", IDlibro = Idlibro1, Idproceso = Idproceso1 }));
                        }



                    }



                }

            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";
            }

           

            return RedirectToAction("VerProcesoAdmin", new RouteValueDictionary(new { controller = "Proceso", action = "VerProcesoAdmin", IDlibro = Idlibro1, Idproceso = Idproceso1 }));
        }





       





        //Función para eliminar comentario//
        [HttpPost]
        public JsonResult EliminarComentario(int Idcomentario)
        {
            int r = 0;
            try
            {
                r = conexComent.EliminarComentario(Idcomentario);

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






        //Función para eliminar documento//
        [HttpPost]
        public JsonResult EliminarDocumento(int IDdocu)
        {
            int r = 0;
            try
            {
                r = conexDocs.EliminarDocumento(IDdocu);
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



        //Función para eliminar estados de proceso//
        [HttpPost]
        public JsonResult EliminarEstadosProceso(int idestado)
        {
            int r = 0;
            try
            {
                r = conexEstado.EliminarEstadoTiposProcesos(idestado);
                if (r == 2)
                {
                    TempData["OKEstado"] = "OK";


                }

            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";

            }

            return Json(r, JsonRequestBehavior.AllowGet);
        }






        //Función para eliminar mensajes de estados de proceso//
        [HttpPost]
        public JsonResult EliminarMensajeProceso(int idmensaje)
        {
            int r = 0;
            try
            {
                r = conexMensaje.EliminarMensajeEstadoTiposProcesos(idmensaje);
                if (r == 2)
                {
                    TempData["OKMensaje"] = "OK";


                }

            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";

            }

            return Json(r, JsonRequestBehavior.AllowGet);
        }






        //Función para agregar comentario//
        [HttpPost]
        public JsonResult AgregarComentario(int idproceso,int Idlibro1, string comentario)
        {

            string emisor = Session["Nombres"].ToString();
            string fecha = System.DateTime.Now.ToString("dd/MM/yyyy" + " - " + "H:mm tt");
            int r = 0;
            bool general = false;
            int n1 = 0;
            string titulol = "";
            string proceso = "";
            int Idu = 0;
            int Idusuario = Convert.ToInt32(Session["Id"]);
            string detallen = "Nuevo comentario";
            string visible1 = "Administrador";
            string urlnotificacion1 = "Proceso/VerProcesoAdmin?Idlibro=" + Idlibro1 + "&Idproceso=" + idproceso + "";
            string urlnotificacion2 = "Proceso/VerProcesoAutor?Idlibro=" + Idlibro1 + "&Idproceso=" + idproceso + "";
            string visible2 = "Autor";



            foreach (var item in conexProceso.VerProcesoxIdp(Idlibro1,idproceso))
            {
                proceso = item.Detalletipospro;
                titulol = item.Titulo;
                Idu = Convert.ToInt32(item.Idusuario);
            }
            try
            {
                //aqui agrregar comentario
                 r = conexComent.InsertarComentarios(idproceso, Idusuario, comentario, fecha);


                TempData["OK"] = "Comentario registrado";
                string asunto = detallen + " - [Libro: " + titulol + "]";


                if (r != 0)
                {
                    //crear notificación
            

                    if (Session["Type"].ToString() == "Autor")
                    {

                        string notificacion = emisor + " ha agregado un nuevo comentario sobre el libro " + titulol + ", para el proceso de " + proceso + ".";
                      
                        foreach (var item in conexUser.BuscarUsuarioxTipoUsuario(visible1))
                        {
                            n1 = conexNotificacion.InsertarNotificacion(notificacion, urlnotificacion1, visible1, item.IDusuario, 4, fecha, detallen,general);
                            break;
                        }
                        //enviar correo

                        SendEmailNotificacionEditorial(notificacion, asunto);



                    }
                    if (Session["Type"].ToString() == "Administrador")
                    {

                        string notificacion ="Se ha agregado un nuevo comentario sobre el libro " + titulol + ", para el proceso de " + proceso + ".";
                        n1 = conexNotificacion.InsertarNotificacion(notificacion, urlnotificacion2, visible2, Idu, 4, fecha, detallen,general);
                        SendEmail(notificacion, Idu, asunto);
                        //enviar correo

                    }

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














