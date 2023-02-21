using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Negocio.Metodos;
using Rotativa;

namespace WebApplication1.Controllers
{
    public class ConsultaController : Controller
    {
        M_Usuarios conexUser = new M_Usuarios();
        M_DatosContactoEditorial conexDatosEditorial = new M_DatosContactoEditorial();
        M_InformacionEditorial conexInfoWeb = new M_InformacionEditorial();
        M_Formatos conexFormatos = new M_Formatos();
        M_SlidersEditorial conexSliders = new M_SlidersEditorial();
        M_Noticias conexNoticias = new M_Noticias();
        M_Notificaciones conexNotificacion = new M_Notificaciones();
        M_Carreras conexcarrera = new M_Carreras();
        M_Consultas conexconsulta = new M_Consultas();
        M_Periodos conexPeriodo = new M_Periodos();
        M_Categorias conexcate = new M_Categorias();
       



        //vista para gestionar datos de consultas como: categorías. carreras, períodos académicos
        public ActionResult DatosConsultas()
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

            

            ViewBag.Categorias = conexcate.Vercategorias();
            ViewBag.Carreras = conexcarrera.VerCarreras();
            ViewBag.Periodos = conexPeriodo.ListarPeriodos();

            ViewBag.Usuario = conexUser.BuscarUsuario(Session["Username"].ToString());
            return View();
        }
       




        //Vista para visualizar informe con graficos estadisticos con opción de imprimir documento mediante navegador web
        public ActionResult DetalleInforme(int idc,int ide)
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


            ViewBag.idconsulta = idc;
            ViewBag.Identificador = ide;
            ViewBag.DetalleConsulta = conexconsulta.VerDetalleConsulta(idc);
            ViewBag.Librospublicadosxcarrera = conexconsulta.ConsultarNumLibrosPublicadosxCarrera();


            foreach (var item in ViewBag.Librospublicadosxcarrera)
            {
                ViewBag.TotalLibros = item.TotalLibros;
                ViewBag.TotalCarreras = item.TotalCarreras;
                ViewBag.LibsProceso = item.TotalLibrosProceso;
                ViewBag.LibsPublicado = item.TotalLibrosPublicados;


            }

            if (ide == 1)
            {
                ViewBag.Librospublicadosxcategoria = conexconsulta.ConsultarNumLibrosPublicadosxCategoria();
                foreach (var item in ViewBag.Librospublicadosxcategoria)
                {
                    ViewBag.TotalCate = item.TotalCategorias;
                }
            }
            

            if (ide == 2)
            {
                ViewBag.Libperiodos = conexconsulta.VerNumLibrosPublicadosxPeriodo(idc);
                foreach (var item in ViewBag.Libperiodos)
                {
                    ViewBag.detperiodo = item.detalleperiodo;
                    ViewBag.Periodo = item.detalleperiodo;
                    ViewBag.Totalp = item.TotalLibrosPublicadosxp;
                }

                foreach (var item in conexconsulta.ConsultarComparativaPeriodanterior())
                {
                    ViewBag.pANT = Convert.ToInt32(item.periodoanterior);
                    ViewBag.pACT = Convert.ToInt32(item.periodoactual);
                    ViewBag.nombreant = item.periodoANT;
                    ViewBag.nombreact = item.periodoACT;
                }


               

            }

            if (ide == 3)
            {
                ViewBag.Libyears = conexconsulta.VerNumLibrosPublicadosxYears(idc);
                foreach (var item in ViewBag.Libyears)
                {
                    ViewBag.detalleyear = item.detalleyear;
                    ViewBag.Totaly = item.TotalLibrosPublicadosxa;
                }

                foreach (var item in conexconsulta.ConsultarComparativaYearanterior())
                {
                    ViewBag.LibANT = item.yanterior;
                    ViewBag.LibACT = item.yactual;
                    ViewBag.yanterior = item.yANT;
                    ViewBag.yactual = item.yACT;


                }

            }

            if (ide == 4)
            {
                ViewBag.VistasLibros = conexconsulta.ConsultarNumVistasLibrosxCarrera();
                foreach (var item in ViewBag.VistasLibros)
                {
                    ViewBag.Vistas = item.Totalvistaslibros;
                }

                ViewBag.DetalleVistasLibros = conexconsulta.VerLibrosVistas();

            }

            if (ide == 5)
            {
                ViewBag.VistasxYears = conexconsulta.VerNumVistasLibrosxYears(idc);
                foreach (var item in ViewBag.VistasxYears)
                {
                    ViewBag.Vyear = item.detalleyear;
                    ViewBag.Totalvyear = item.Totalvistaslibrosxa;
                    ViewBag.Vistast = item.Totalvistaslibros;

                }

                foreach (var item in conexconsulta.ConsultarComparativaNumVistasLibrosxYearAnterior())
                {
                    ViewBag.TvistasANT = item.TotalvistaslibrosANT;
                    ViewBag.TvistasACT = item.TotalvistaslibrosACT;
                    ViewBag.YACT = item.yearACT;
                    ViewBag.YANT = item.yearANT;
                }

            }



      

            return View();
        }






        //Vista de estadísticas
        public ActionResult Estadisticas()
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

            int idp = 0, idy = 0, idy1 = 0;
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



        
          
         
            foreach (var item in conexconsulta.ConsultarNumLibrosPublicadosxCarrera())
            {
                ViewBag.Tcarreras = item.TotalCarreras;
                ViewBag.TotalLibs = Convert.ToInt32(item.TotalLibros);
                ViewBag.LibsPublicados = Convert.ToInt32(item.TotalLibrosPublicados);
                ViewBag.LibsProceso = item.TotalLibrosProceso;
                break;
            }



           

            foreach(var item1 in conexconsulta.ConsultarNumVistasLibrosxCarrera())
            {
                ViewBag.Vistas=item1.Totalvistaslibros;
                break;
            }
            

            ViewBag.Consultas = conexconsulta.ListarConsultas();


            foreach(var item3 in ViewBag.Consultas)
            {
                if (item3.Identificador == 2 && item3.Parametros==true)
                {
                    idp = item3.Idparametro;
                }
                if(item3.Identificador == 3 && item3.Parametros == true)
                {
                    idy = item3.Idparametro;
                }
                if (item3.Identificador == 5 && item3.Parametros == true)
                {
                    idy1 = item3.Idparametro;
                }
            }


            foreach(var item in conexconsulta.ConsultarNumLibrosPublicadosxPeriodo(idp))
            {
                ViewBag.Totalp = item.TotalLibrosPublicadosxp;
                ViewBag.Periodo = item.detalleperiodo;
                break;
            }


            foreach (var item in conexconsulta.ConsultarNumLibrosPublicadosxYear(idy))
            {
                ViewBag.Totaly = item.TotalLibrosPublicadosxa;
                ViewBag.year = item.detalleyear;
                break;
            }


            foreach (var item in conexconsulta.ConsultarNumVistasLibrosxYear(idy1))
            {
                ViewBag.year1 = item.detalleyear;
                ViewBag.Vistasxa = item.Totalvistaslibrosxa;
                break;
            }
            


            ViewBag.Periodos = conexPeriodo.ListarPeriodos();
            ViewBag.years = conexPeriodo.ListarYears();
        



            ViewBag.Usuario = conexUser.BuscarUsuario(Session["Username"].ToString());

            return View();
        }








        //Función para editar consulta
        [HttpPost]
        public ActionResult Editarconsulta(int idconsulta,int idparametro)
        {
            int r = 0;
         


            try
            {

                r = conexconsulta.EditarConsulta(idconsulta,idparametro);

                if (r != 3)
                {
                    TempData["OK"] = "consulta realizada";
                }

            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";
                //return View();
            }


            return RedirectToAction("Estadisticas");
        }








        //Función para agregar nueva categoria
        [HttpPost]
        public JsonResult AgregarCategoria(string detalle)
        {
            int r = 0;
            try
            {
                r = conexcate.Ingresarcategorias(detalle);
                if (r == 1)
                {
                    TempData["OK"] = "Categoría registrada";
                }
                else if (r == 2)
                {
                    TempData["ERROR2"] = "Esta categoría ya existe";
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








        //Función para agregar carrera
        [HttpPost]
        public ActionResult AgregarCarrera(string carrera)
        {
            int r = 0;



            try
            {
                r = conexcarrera.Ingresarcarreras(carrera);
                if (r == 1)
                {
                    TempData["OK"] = "Carrera registrada correctamente";
                }
                if (r == 2)
                {
                    TempData["ERROR2"] = "Carrera ya registrada";
                }

            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";
                //return View();
            }


            return RedirectToAction("DatosConsultas");
        }






        //Función para agregar período académico
        [HttpPost]
        public ActionResult AgregarPeriodo(string detallep, int ordenp,DateTime fechainiciop, DateTime fechafinp, int ident)
        {
            int r = 0;



            try
            {
                if(ordenp<=0 || ident <= 0)
                {
                    TempData["ERROR2"] = "No se aceptan valores negativos, vuelva a intentarlo e ingrese los valores correctos.";
                    return RedirectToAction("DatosConsultas");
                }
                r = conexPeriodo.RegistrarPeriodo(detallep,ordenp,fechainiciop,fechafinp,ident);
                if (r == 1)
                {
                    TempData["OK"] = "Período académico registrado correctamente";
                }else if (r == 2)
                {
                    TempData["ERROR2"] = "Número de identificador ya existe, intente con un número diferente.";
                }
                else
                {
                    TempData["ERROR"] = "Algo salió mal";
                }
                

            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";
                //return View();
            }


            return RedirectToAction("DatosConsultas");
        }








        //Función para editar categoria
        [HttpPost]
        public ActionResult EditarCategoria(int Idcate, string detallecate)
        {
            int r = 0;
           


            try
            {

                r = conexcate.Editarcategorias(Idcate, detallecate);

                if (r == 1)
                {
                    TempData["OK"] = "Categoria editada correctamente";
                }

            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";
                //return View();
            }


            return RedirectToAction("DatosConsultas");
        }







        //Función para editar carrera
        [HttpPost]
        public ActionResult EditarCarrera(int Idcarrera1, string detallecarrera1)
        {
            int r = 0;



            try
            {

                r = conexcarrera.Editarcarreras(Idcarrera1, detallecarrera1);

                if (r == 2)
                {
                    TempData["OK"] = "Carrera editada correctamente";
                }

            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";
                //return View();
            }


            return RedirectToAction("DatosConsultas");
        }







        //Función para editar período académico
        [HttpPost]
        public ActionResult EditarPeriodo(int Idp, string detallep1, DateTime fechainiciop1, DateTime fechafinp1)
        {
            int r = 0;



            try
            {
               
                r = conexPeriodo.EditarPeriodo(Idp,detallep1,fechainiciop1,fechafinp1);

                if (r == 2)
                {
                    TempData["OK"] = "Período editado correctamente";
                }
                else
                {
                    TempData["ERROR"] = "Algo salió mal";
                }

            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";
                //return View();
            }


            return RedirectToAction("DatosConsultas");
        }






        //Función para eliminar categoria//
        [HttpPost]
        public JsonResult EliminarCategoria(int Idcate)
        {
            int r = 0;
            try
            {
                r = conexcate.Eliminarcategorias(Idcate);
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







        //Función para eliminar carrera//
        [HttpPost]
        public JsonResult EliminarCarrera(int Idcarrera)
        {
            int r = 0;
            try
            {
                r = conexcarrera.Eliminarcarreras(Idcarrera);
                if (r == 2)
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





        //Función para eliminar período acdémico//
        [HttpPost]
        public JsonResult EliminarPeriodo(int Idperiodo)
        {
            int r = 0;
            try
            {
                r = conexPeriodo.EliminarPeriodo(Idperiodo);
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








        //Listo -  gráfico 1
        //Función para obtener total de libros publicados por cada carrera - identificador 1
        [HttpGet]
        public JsonResult ConsultarNumLibrosPublicadosxCarrera()
        {


            var lista = conexconsulta.ConsultarNumLibrosPublicadosxCarrera();


            return Json(lista, JsonRequestBehavior.AllowGet);
        }





        //Función para obtener total de libros publicados por cada categoría 
        [HttpGet]
        public JsonResult ConsultarNumLibrosPublicadosxCategoria()
        {


            var lista = conexconsulta.ConsultarNumLibrosPublicadosxCategoria();


            return Json(lista, JsonRequestBehavior.AllowGet);
        }






        //Listo - gráfico 2
        //Función para obtener libros publicados por períodos académicos
        [HttpGet]
        public JsonResult ConsultarNumLibrosPublicadosxPeriodo()
        {
            int idpe = 0;
            foreach (var item in conexconsulta.ListarConsultas())
            {
                if (item.Identificador == 2 && item.Parametros == true)
                {
                    idpe = Convert.ToInt32(item.Idparametro);
                }
            }

            var lista = conexconsulta.ConsultarNumLibrosPublicadosxPeriodo(idpe);


            return Json(lista, JsonRequestBehavior.AllowGet);
        }






        //Listo -  gráfico 3
        //Función para obtener libros publicados por año
        [HttpGet]
        public JsonResult ConsultarNumLibrosPublicadosxYear()
        {
            int idy = 0;
            foreach (var item in conexconsulta.ListarConsultas())
            {
                if (item.Identificador == 3 && item.Parametros == true)
                {
                    idy = Convert.ToInt32(item.Idparametro);
                }
            }

            var lista = conexconsulta.ConsultarNumLibrosPublicadosxYear(idy);


            return Json(lista, JsonRequestBehavior.AllowGet);
        }





        //Listo - gráfico 4
        //Función para obtener num de vistas de libros en página web
        [HttpGet]
        public JsonResult ConsultarNumVistasLibrosxCarrera()
        {


            var lista = conexconsulta.ConsultarNumVistasLibrosxCarrera();


            return Json(lista, JsonRequestBehavior.AllowGet);
        }







        //Listo - gráfico 5
        //Función para consultar num de vistas de libros en página web por año
        [HttpGet]
        public JsonResult ConsultarNumVistasLibrosxYear()
        {
            int idy = 0;
            foreach (var item in conexconsulta.ListarConsultas())
            {
                if (item.Identificador == 5 && item.Parametros == true)
                {
                    idy = Convert.ToInt32(item.Idparametro);
                }
            }

            var lista = conexconsulta.ConsultarNumVistasLibrosxYear(idy);


            return Json(lista, JsonRequestBehavior.AllowGet);
        }


       






        //Función para listas num de libros publicados por años
        [HttpGet]
        public JsonResult VerLibrospublicadosxYears()
        {
           

            var lista = conexconsulta.VerLibrospublicadosxYears();


            return Json(lista, JsonRequestBehavior.AllowGet);
        }


        


        //para el apartado de consulta por año
        //Función para obtener comparación de total de libros año anterior y actual por cada carrera 
        [HttpGet]
        public JsonResult ConsultarComparativaYearanterior()
        {


            var lista = conexconsulta.ConsultarComparativaYearanterior();


            return Json(lista, JsonRequestBehavior.AllowGet);
        }





        //para el apartado de consulta por año vistas libros
        //Función para obtener comparación de total de vistas de libros año anterior y actual por cada carrera 
        [HttpGet]
        public JsonResult ConsultarComparativaNumVistasLibrosxYearAnterior()
        {


            var lista = conexconsulta.ConsultarComparativaNumVistasLibrosxYearAnterior();


            return Json(lista, JsonRequestBehavior.AllowGet);
        }




       
     



        //para el apartado de consulta por periodo
        //Función para obtener comparación de total de libros periodo anterior y actual por cada carrera -identificador 2
        [HttpGet]
        public JsonResult ConsultarComparativaPeriodanterior()
        {


            var lista = conexconsulta.ConsultarComparativaPeriodanterior();


            return Json(lista, JsonRequestBehavior.AllowGet);
        }








       
        //Función para obtener num de vistas de libros en página web por años
        [HttpGet]
        public JsonResult VistasLibrosxYears()
        {


            var lista = conexconsulta.VistasLibrosxYears();


            return Json(lista, JsonRequestBehavior.AllowGet);
        }







    }
}
