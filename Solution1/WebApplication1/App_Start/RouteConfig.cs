using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplication1
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");





            //  routes.MapRoute(
            //    name: "PublicarCuestionario",
            //    url: "{controller}/{action}/{id}/{id1}",
            //    defaults: new { controller = "Cuestionario", action = "PublicarCuestionario", Idevaluador = UrlParameter.Optional, Id= UrlParameter.Optional}
            //); solo para vistas 





            routes.MapRoute(
              name: "EvaluacionParAcademico",
              url: "{controller}/{action}/{id}/{id1}",
              defaults: new { controller = "Reporte", action = "EvaluacionParAcademico", Idcuestionario = UrlParameter.Optional, Id = UrlParameter.Optional }
          );


            routes.MapRoute(
             name: "Respuestaprueba",
             url: "{controller}/{action}/{id}/{id1}",
             defaults: new { controller = "Reporte", action = "Respuestaprueba", Idcuestionario = UrlParameter.Optional, Id = UrlParameter.Optional }
         );

            routes.MapRoute(
             name: "Respuesta",
             url: "{controller}/{action}/{id}",
             defaults: new { controller = "Reporte", action = "Respuesta", Idasignacion = UrlParameter.Optional }
         );




            //   routes.MapRoute(
            //    name: "OpcionesProceso",
            //    url: "{controller}/{action}/{id}/{id1}",
            //    defaults: new { controller = "Proceso", action = "OpcionesProceso", Idproceso = UrlParameter.Optional, Idreq = UrlParameter.Optional }
            //);


         


          

            routes.MapRoute(
             name: "VerProcesoAdmin",
             url: "{controller}/{action}/{id}/{id1}",
             defaults: new { controller = "Proceso", action = "VerProcesoAdmin", Idlibro = UrlParameter.Optional, Idproceso = UrlParameter.Optional }
           );

            routes.MapRoute(
             name: "VerProcesoAutor",
             url: "{controller}/{action}/{id}/{id1}",
             defaults: new { controller = "Proceso", action = "VerProcesoAutor", Idlibro = UrlParameter.Optional, Idproceso = UrlParameter.Optional }
           );




            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Usuario", action = "Login", id = UrlParameter.Optional }
            );




            //routes.MapRoute(
            //    name: "AgregarLibro",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Libro", action = "AgregarLibro", IDlibro = UrlParameter.Optional }
            //);


            routes.MapRoute(
              name: "PublicacionLibro",
              url: "{controller}/{action}/{id}/{id1}",
              defaults: new { controller = "Libro", action = "PublicacionLibro", Idlibro = UrlParameter.Optional, Idproceso = UrlParameter.Optional }
          );


            routes.MapRoute(
                name: "RecoveryPass",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Usuario", action = "RecoveryPass", token = UrlParameter.Optional }
            );



           // routes.MapRoute(
           //    name: "DetalleLibro",
           //    url: "{controller}/{action}/{id}",
           //    defaults: new { controller = "Libro", action = "DetalleLibro", IDlibro = UrlParameter.Optional }
           //);



          //  routes.MapRoute(
          //    name: "Imprime",
          //    url: "{controller}/{action}/{id}",
          //    defaults: new { controller = "Libro", action = "Imprime", IDLibro = UrlParameter.Optional }
          //);


           // routes.MapRoute(
           //    name: "InformeLibro",
           //    url: "{controller}/{action}/{id}",
           //    defaults: new { controller = "Libro", action = "InformeLibro", IDLibro = UrlParameter.Optional }
           //);




            routes.MapRoute(
      name: "VerProcesoEvaluador",
      url: "{controller}/{action}/{id}/{id1}",
      defaults: new { controller = "Proceso", action = "VerProcesoEvaluador", IDlibro = UrlParameter.Optional, Idproceso = UrlParameter.Optional }
      );





            routes.MapRoute(
                   name: "Evaluadores",
                   url: "{controller}/{action}/{id}/{id1}",
                   defaults: new { controller = "Proceso", action = "Evaluadores", IDlibro = UrlParameter.Optional, Idproceso = UrlParameter.Optional }
                   );





        }
    }
}
