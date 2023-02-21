using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Negocio.Metodos;
using Rotativa;



namespace WebApplication1.Controllers
{


    public class ReporteController : Controller
    {
        M_Libros conexLibro = new M_Libros();
        M_Usuarios conexUser = new M_Usuarios();
        M_Procesos conexProceso = new M_Procesos();
        M_Evaluadores conexEvaluador = new M_Evaluadores();
        M_Autores conexAutores = new M_Autores();
        M_DatosContactoEditorial conexDatosEditorial = new M_DatosContactoEditorial();
        M_Notificaciones conexNotificacion = new M_Notificaciones();
        M_Carreras conexcarrera = new M_Carreras();
        M_Categorias conexCate = new M_Categorias();
        M_Periodos conexPeriodo = new M_Periodos();
  





        //Función de ejemplo 
        //Función para generar documento pdf
        public ActionResult CuestionarioRespuestasPdf()
        {
            //FileStream fs = new FileStream("D:/Copia-Sistema/SistemaHumus/Solution1/WebApplication1/Files/Reportes/reporte.pdf", FileMode.Create);
            MemoryStream ms = new MemoryStream();
            Document document = new Document(iTextSharp.text.PageSize.A4, 85.0394f, 85.0394f, 70.8661f, 85.0394f);
            PdfWriter pw = PdfWriter.GetInstance(document, ms);



            //string pathImage = Server.MapPath("/ImagesWeb/nlog2.png");
            string pathImage = Server.MapPath("/ImagesWeb/LG.png");
            string pathImageMW = Server.MapPath("/ImagesWeb/LG.png");
            pw.PageEvent = new HeaderFooter(pathImage, pathImageMW);

            //document.SetPageSize(PageSize.A4);


            //Para vista horizontal de la hoja
            //document.SetPageSize(PageSize.LEGAL.Rotate());



            //marca de agua texto
            //pw.PageEvent = new PdfWriterEvents();


            document.Open();

            BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.EMBEDDED);
            Font fontText = new Font(bf, 10, 0, BaseColor.BLACK);


            PdfPTable table = new PdfPTable(5);
            //table.WidthPercentage = 100f; //para ocupar todoel espacio del documento sin dejar margenes
            document.Add(new Phrase("\n"));



            foreach (var item in conexUser.VerUsuarios())
            {
                //table.AddCell(new Paragraph());
                PdfPCell _cell = new PdfPCell();
                _cell = new PdfPCell(new Paragraph(item.IDusuario.ToString(), fontText));

                _cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(_cell);

                //table.AddCell(new Paragraph(item.IDusuario.ToString()));
                table.AddCell(new Paragraph(item.PrimerNombre + " " + item.ApellidoPrimero+" "+item.ApellidoSegundo, fontText));
                table.AddCell(new Paragraph(item.Email, fontText));
                table.AddCell(new Paragraph(item.Tipo_usuario, fontText));

                _cell = new PdfPCell(new Paragraph(item.Filial, fontText));
                _cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(_cell);
                //table.AddCell(new Paragraph(item.Filial));

            }

            //document.Add(new Paragraph("HOLA PDF \n ESTO ES UNA PRUEBA!!!"));




            document.Add(table);

            document.Close();

            byte[] bytesStream = ms.ToArray();

            ms = new MemoryStream();
            ms.Write(bytesStream, 0, bytesStream.Length);
            ms.Position = 0;
            return new FileStreamResult(ms, "application/pdf");

        }






        //Función para ver informe de libro publicado desde la vista de libros archivados
        public ActionResult InformeDetalleLibro(int IDlibro)
        {

            if (Session["Type"] == null)
            {
                TempData["ERRORD"] = "No tiene acceso a esta sección del sistema. Inicia sesión";
                return RedirectToAction("Login", "Usuario");
            }
            int Id =Convert.ToInt32(Session["Id"]);


             if (Session["Type"].ToString() == "Autor")
            {
                foreach(var item in conexLibro.VerDetalleLibro(IDlibro))
                {
                    if (Id != item.Idusuario)
                    {
                        TempData["ERRORD"] = "Algo salió mal";
                        return RedirectToAction("Login", "Usuario");
                    }
                }
                
            }
           



           

            try
            {




                string titulo = "INFORME DE LIBRO PUBLICADO";



                MemoryStream ms = new MemoryStream();
                Document document = new Document(iTextSharp.text.PageSize.A4, 85.0394f, 85.0394f, 170.079f, 85.0394f);
                PdfWriter pw = PdfWriter.GetInstance(document, ms);

                string pathImage = Server.MapPath("/ImagesWeb/LG.png");
                string logoespam = Server.MapPath("/ImagesWeb/espamlogoe1.png");
                string Image2escudo = Server.MapPath("/ImagesWeb/escudo2.png");
                pw.PageEvent = new HeaderFooter1(Image2escudo, logoespam, pathImage, titulo);


                document.Open();



                //BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.EMBEDDED);
                BaseFont bf2 = BaseFont.CreateFont(BaseFont.TIMES_BOLD, BaseFont.CP1250, BaseFont.EMBEDDED);
                //Font fontText = new Font(bf, 10, 0, BaseColor.BLACK);
                Font titulos = new Font(bf2, 12, 0, BaseColor.BLACK);

                //Font fontparrafo = new Font(bf, 12, 0, BaseColor.DARK_GRAY);
                var fontparrafo = FontFactory.GetFont("Times New Roman", 12, 0, BaseColor.DARK_GRAY);
                var fontresp = FontFactory.GetFont("Times New Roman", 11, 0, BaseColor.DARK_GRAY);
                var fontpreg = FontFactory.GetFont(FontFactory.TIMES_BOLD, 12, 0, BaseColor.BLACK);




                ////BaseFont bp = BaseFont.CreateFont(BaseFont.TIMES_BOLD, BaseFont.CP1250, BaseFont.EMBEDDED);

                ////Font fontpreg = new Font(bp, 12, 0, BaseColor.BLACK);
                //Font fontresp = new Font(bf, 11, 0, BaseColor.DARK_GRAY);





                document.Open();

                document.Add(new Paragraph("Información general del libro", titulos) { Alignment = Element.ALIGN_JUSTIFIED });
                document.Add(new Paragraph("\n"));

                //Font fontText = new Font(bf, 10, 0, BaseColor.BLACK);


                var table = new PdfPTable(2);
                float[] widths = new float[] { 30f, 70f }; //ancho para cadad columna


                table.SetWidths(widths);
                table.WidthPercentage = 100f;
                //table.WidthPercentage = 100f; //para ocupar todoel espacio del documento sin dejar margenes

                //var tbprueba = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f };



                foreach (var item in conexLibro.VerDetalleLibro(IDlibro))
                {

                    table.AddCell(new PdfPCell(new Phrase("Título", fontresp)) { BorderColor = BaseColor.GRAY, BorderWidthBottom = 0, BackgroundColor = BaseColor.LIGHT_GRAY, VerticalAlignment = Element.ALIGN_LEFT, PaddingBottom = 5, PaddingTop = 5 });
                    table.AddCell(new PdfPCell(new Phrase(item.Titulo, fontparrafo)) { BorderColor = BaseColor.GRAY, BorderWidthBottom = 0, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });

                    table.AddCell(new PdfPCell(new Phrase("Código ISBN", fontresp)) { BorderColor = BaseColor.GRAY, BorderWidthBottom = 0, BackgroundColor = BaseColor.LIGHT_GRAY, VerticalAlignment = Element.ALIGN_LEFT, PaddingBottom = 5, PaddingTop = 5 });
                    table.AddCell(new PdfPCell(new Phrase(item.CodigoISBN, fontparrafo)) { BorderColor = BaseColor.GRAY, BorderWidthBottom = 0, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });

                    table.AddCell(new PdfPCell(new Phrase("Fecha de publicación", fontresp)) { BorderColor = BaseColor.GRAY, BorderWidthBottom = 0, BackgroundColor = BaseColor.LIGHT_GRAY, VerticalAlignment = Element.ALIGN_LEFT, PaddingBottom = 5, PaddingTop = 5 });
                    table.AddCell(new PdfPCell(new Phrase(item.Fechapublica, fontparrafo)) { BorderColor = BaseColor.GRAY, BorderWidthBottom = 0, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });

                    table.AddCell(new PdfPCell(new Phrase("Carrera", fontresp)) { BorderColor = BaseColor.GRAY, BorderWidthBottom = 0, BackgroundColor = BaseColor.LIGHT_GRAY, VerticalAlignment = Element.ALIGN_LEFT, PaddingBottom = 5, PaddingTop = 5 });
                    table.AddCell(new PdfPCell(new Phrase(item.Carrera, fontparrafo)) { BorderColor = BaseColor.GRAY, BorderWidthBottom = 0, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });


                    table.AddCell(new PdfPCell(new Phrase("Categoría", fontresp)) { BorderColor = BaseColor.GRAY, BorderWidthBottom = 0, BackgroundColor = BaseColor.LIGHT_GRAY, VerticalAlignment = Element.ALIGN_LEFT, PaddingBottom = 5, PaddingTop = 5 });
                    table.AddCell(new PdfPCell(new Phrase(item.Detallecategoria, fontparrafo)) { BorderColor = BaseColor.GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });



                }



                document.Add(table);

                document.Add(new Phrase("\n"));
                document.Add(new Paragraph("\n"));
                document.Add(new Paragraph("Autores del libro", titulos) { Alignment = Element.ALIGN_JUSTIFIED });
                document.Add(new Paragraph("\n"));
                PdfPTable tableA = new PdfPTable(2);
                float[] widths2 = new float[] { 50f, 50f }; //ancho para cadad columna

                tableA.SetWidths(widths2);
                tableA.WidthPercentage = 100f;
                //table.WidthPercentage = 100f; //para ocupar todoel espacio del documento sin dejar margenes



                tableA.AddCell(new PdfPCell(new Phrase("Nombres y apellidos", fontresp)) { BorderColor = BaseColor.GRAY, BorderWidthBottom = 0, BackgroundColor = BaseColor.LIGHT_GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });

                tableA.AddCell(new PdfPCell(new Phrase("Correo electrónico", fontresp)) { BorderColor = BaseColor.GRAY, BorderWidthBottom = 0, BackgroundColor = BaseColor.LIGHT_GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });




                foreach (var item in conexAutores.VerAutores(IDlibro))
                {

                    tableA.AddCell(new PdfPCell(new Phrase(item.PrimerNombre+" "+item.SegundoNombre + " " + item.ApellidoPrimero+" "+item.ApellidoSegundo, fontparrafo)) { BorderColor = BaseColor.GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });

                    tableA.AddCell(new PdfPCell(new Phrase(item.Email, fontparrafo)) { BorderColor = BaseColor.GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });



                }


                document.Add(tableA);


                if (Session["Type"].ToString() != "Autor")
                {
                document.Add(new Paragraph("\n"));
                document.Add(new Phrase("\n"));

             

                document.Add(new Paragraph("Evaluadores del libro", titulos) { Alignment = Element.ALIGN_JUSTIFIED });
                document.Add(new Paragraph("\n"));
                PdfPTable tableE = new PdfPTable(2);
                float[] widths3 = new float[] { 50f, 50f }; //ancho para cadad columna

                tableE.SetWidths(widths3);
                tableE.WidthPercentage = 100f;
                //table.WidthPercentage = 100f; //para ocupar todoel espacio del documento sin dejar margenes
                PdfPCell _cellE = new PdfPCell();

                tableE.AddCell(new PdfPCell(new Phrase("Nombres y apellidos", fontresp)) { BorderColor = BaseColor.GRAY, BorderWidthBottom = 0, BackgroundColor = BaseColor.LIGHT_GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });

                tableE.AddCell(new PdfPCell(new Phrase("Correo electrónico", fontresp)) { BorderColor = BaseColor.GRAY, BorderWidthBottom = 0, BackgroundColor = BaseColor.LIGHT_GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });



                foreach (var item2 in conexEvaluador.VerEvaluadores(IDlibro))
                {


                    tableE.AddCell(new PdfPCell(new Phrase(item2.PrimerNombre+" "+item2.SegundoNombre + " " + item2.ApellidoPrimero+" "+item2.ApellidoSegundo, fontparrafo)) { BorderColor = BaseColor.GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });

                    tableE.AddCell(new PdfPCell(new Phrase(item2.Email, fontparrafo)) { BorderColor = BaseColor.GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });



                }



                document.Add(tableE);
            }




                document.Add(new Paragraph("\n"));
                document.Add(new Phrase("\n"));

                document.Add(new Paragraph("Procesos del libro", titulos) { Alignment = Element.ALIGN_JUSTIFIED });
                document.Add(new Paragraph("\n"));
                PdfPTable tableL = new PdfPTable(2);
                float[] widths4 = new float[] { 50f, 50f }; //ancho para cadad columna

                tableL.SetWidths(widths4);
                tableL.WidthPercentage = 100f;
                //table.WidthPercentage = 100f; //para ocupar todoel espacio del documento sin dejar margenes



                tableL.AddCell(new PdfPCell(new Phrase("Procesos", fontresp)) { BorderColor = BaseColor.GRAY, BorderWidthBottom = 0, BackgroundColor = BaseColor.LIGHT_GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });

                tableL.AddCell(new PdfPCell(new Phrase("Fecha de finalización", fontresp)) { BorderColor = BaseColor.GRAY, BorderWidthBottom = 0, BackgroundColor = BaseColor.LIGHT_GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });

                foreach (var item3 in conexProceso.VerProceso(IDlibro))
                {


                    tableL.AddCell(new PdfPCell(new Phrase(item3.Detalletipospro, fontparrafo)) { BorderColor = BaseColor.GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });

                    tableL.AddCell(new PdfPCell(new Phrase(item3.ActualizadoFecha, fontparrafo)) { BorderColor = BaseColor.GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });



                }



                document.Add(tableL);




                document.Close();



                byte[] bytesStream = ms.ToArray();

                ms = new MemoryStream();
                ms.Write(bytesStream, 0, bytesStream.Length);
                ms.Position = 0;


                return new FileStreamResult(ms, "application/pdf");
            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";

                if (Session["Type"].ToString() == "Autor"){
                    return RedirectToAction("ListaLibrosArchivados", "Libro");
                }
                if (Session["Type"].ToString() == "Administrador")
                {
                    return RedirectToAction("LibrosArchivados", "Libro");
                }
                
                 
            }



            if (Session["Type"].ToString() == "Autor")
            {
                return RedirectToAction("ListaLibrosArchivados", "Libro");
            }
            if (Session["Type"].ToString() == "Administrador")
            {
                return RedirectToAction("LibrosArchivados", "Libro");
            }

            return RedirectToAction("LibrosArchivados", "Libro");
        }






        //Función para ver informe de todos los libros publicados
        public ActionResult InformeLibrosPublicados()
        {

            if (Session["Type"] == null)
            {
                TempData["ERRORD"] = "No tiene acceso a esta sección del sistema. Inicia sesión";
                return RedirectToAction("Login", "Usuario");
            }
            //int Id = Convert.ToInt32(Session["Id"]);


            if (Session["Type"].ToString() != "Administrador")
            {


                TempData["ERRORD"] = "No tiene acceso a esta sección del sistema. Inicia sesión";
                return RedirectToAction("Login", "Usuario");

            }






            try
            {



                string titulo = "INFORME DE LIBROS PUBLICADOS";



                MemoryStream ms = new MemoryStream();
                Document document = new Document(iTextSharp.text.PageSize.A4, 85.0394f, 70.8661f, 170.079f, 85.0394f);
                PdfWriter pw = PdfWriter.GetInstance(document, ms);

                string pathImage = Server.MapPath("/ImagesWeb/LG.png");
                string logoespam = Server.MapPath("/ImagesWeb/espamlogoe1.png");
                string Image2escudo = Server.MapPath("/ImagesWeb/escudo2.png");
                pw.PageEvent = new HeaderFooter1(Image2escudo, logoespam, pathImage, titulo);


                document.Open();



             
                BaseFont bf2 = BaseFont.CreateFont(BaseFont.TIMES_BOLD, BaseFont.CP1250, BaseFont.EMBEDDED);
              
                Font titulos = new Font(bf2, 12, 0, BaseColor.BLACK);

         
                var fontparrafo = FontFactory.GetFont("Times New Roman", 10, 0, BaseColor.DARK_GRAY);
                var fontresp = FontFactory.GetFont("Times New Roman", 11, 0, BaseColor.DARK_GRAY);
                var fontpreg = FontFactory.GetFont(FontFactory.TIMES_BOLD, 12, 0, BaseColor.BLACK);





                document.Open();



                var table = new PdfPTable(4);
                float[] widths = new float[] { 25f, 20f,20f,35f }; //ancho para cadad columna


                table.SetWidths(widths);
                table.WidthPercentage = 100f;
              

                table.AddCell(new PdfPCell(new Phrase("Título", fontresp)) { BorderColor = BaseColor.GRAY, BorderWidthBottom = 0, BackgroundColor = BaseColor.LIGHT_GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });
                table.AddCell(new PdfPCell(new Phrase("Publicado", fontresp)) { BorderColor = BaseColor.GRAY, BorderWidthBottom = 0, BackgroundColor = BaseColor.LIGHT_GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });
                table.AddCell(new PdfPCell(new Phrase("Carrera", fontresp)) { BorderColor = BaseColor.GRAY, BorderWidthBottom = 0, BackgroundColor = BaseColor.LIGHT_GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });
                table.AddCell(new PdfPCell(new Phrase("Autor(es)", fontresp)) { BorderColor = BaseColor.GRAY, BorderWidthBottom = 0, BackgroundColor = BaseColor.LIGHT_GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });


                foreach (var item in conexLibro.VerTodosLosLibros())
                {

                    table.AddCell(new PdfPCell(new Phrase(item.Titulo, fontparrafo)) { BorderColor = BaseColor.GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });
                    table.AddCell(new PdfPCell(new Phrase(item.Fechapublica, fontparrafo)) { BorderColor = BaseColor.GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });
                    table.AddCell(new PdfPCell(new Phrase(item.Carrera, fontparrafo)) { BorderColor = BaseColor.GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });
                    table.AddCell(new PdfPCell(new Phrase(item.Autores, fontparrafo)) { BorderColor = BaseColor.GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });


                }



                document.Add(table);

            


                document.Close();



                byte[] bytesStream = ms.ToArray();

                ms = new MemoryStream();
                ms.Write(bytesStream, 0, bytesStream.Length);
                ms.Position = 0;


                return new FileStreamResult(ms, "application/pdf");
            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";

                
            }


            return RedirectToAction("Informes", "Reporte");
        }






        //Función para ver informe de todos los libros publicados por carrera
        public ActionResult InformeLibrosPublicadosxCarrera(int idc)
        {

            if (Session["Type"] == null)
            {
                TempData["ERRORD"] = "No tiene acceso a esta sección del sistema. Inicia sesión";
                return RedirectToAction("Login", "Usuario");
            }
            //int Id = Convert.ToInt32(Session["Id"]);


            if (Session["Type"].ToString() != "Administrador")
            {


                TempData["ERRORD"] = "No tiene acceso a esta sección del sistema. Inicia sesión";
                return RedirectToAction("Login", "Usuario");

            }






            try
            {



                string titulo = "INFORME DE LIBROS PUBLICADOS";



                MemoryStream ms = new MemoryStream();
                Document document = new Document(iTextSharp.text.PageSize.A4, 85.0394f, 70.8661f, 170.079f, 85.0394f);
                PdfWriter pw = PdfWriter.GetInstance(document, ms);

                string pathImage = Server.MapPath("/ImagesWeb/LG.png");
                string logoespam = Server.MapPath("/ImagesWeb/espamlogoe1.png");
                string Image2escudo = Server.MapPath("/ImagesWeb/escudo2.png");
                pw.PageEvent = new HeaderFooter1(Image2escudo, logoespam, pathImage, titulo);


                document.Open();




                BaseFont bf2 = BaseFont.CreateFont(BaseFont.TIMES_BOLD, BaseFont.CP1250, BaseFont.EMBEDDED);

                Font titulos = new Font(bf2, 12, 0, BaseColor.BLACK);


                var fontparrafo = FontFactory.GetFont("Times New Roman", 10, 0, BaseColor.DARK_GRAY);
                var fontresp = FontFactory.GetFont("Times New Roman", 11, 0, BaseColor.DARK_GRAY);
                var fontpreg = FontFactory.GetFont(FontFactory.TIMES_BOLD, 12, 0, BaseColor.BLACK);



                string carrera = "";
                foreach(var item in conexcarrera.VerCarreras())
                {
                    if (item.IDcarrera == idc)
                    {
                        carrera = item.Carrera;
                    }
                }

                document.Open();

                document.Add(new Paragraph("Libros publicados por la carrera de "+carrera+".", titulos) { Alignment = Element.ALIGN_JUSTIFIED });
                document.Add(new Paragraph("\n"));

                var table = new PdfPTable(4);
                float[] widths = new float[] { 25f, 20f, 20f, 35f }; //ancho para cadad columna


                table.SetWidths(widths);
                table.WidthPercentage = 100f;


                table.AddCell(new PdfPCell(new Phrase("Título", fontresp)) { BorderColor = BaseColor.GRAY, BorderWidthBottom = 0, BackgroundColor = BaseColor.LIGHT_GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });
                table.AddCell(new PdfPCell(new Phrase("Publicado", fontresp)) { BorderColor = BaseColor.GRAY, BorderWidthBottom = 0, BackgroundColor = BaseColor.LIGHT_GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });
                table.AddCell(new PdfPCell(new Phrase("Carrera", fontresp)) { BorderColor = BaseColor.GRAY, BorderWidthBottom = 0, BackgroundColor = BaseColor.LIGHT_GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });
                table.AddCell(new PdfPCell(new Phrase("Autor(es)", fontresp)) { BorderColor = BaseColor.GRAY, BorderWidthBottom = 0, BackgroundColor = BaseColor.LIGHT_GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });


                foreach (var item in conexLibro.MostrarLibrosxCarrera(Convert.ToInt32(idc)))
                {

                    table.AddCell(new PdfPCell(new Phrase(item.Titulo, fontparrafo)) { BorderColor = BaseColor.GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });
                    table.AddCell(new PdfPCell(new Phrase(item.Fechapublica, fontparrafo)) { BorderColor = BaseColor.GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });
                    table.AddCell(new PdfPCell(new Phrase(item.Carrera, fontparrafo)) { BorderColor = BaseColor.GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });
                    table.AddCell(new PdfPCell(new Phrase(item.Autores, fontparrafo)) { BorderColor = BaseColor.GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });


                }



                document.Add(table);




                document.Close();



                byte[] bytesStream = ms.ToArray();

                ms = new MemoryStream();
                ms.Write(bytesStream, 0, bytesStream.Length);
                ms.Position = 0;


                return new FileStreamResult(ms, "application/pdf");
            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";


            }


            return RedirectToAction("Informes", "Reporte");
        }







        //Función para ver informe de todos los libros publicados por categoría
        public ActionResult InformeLibrosPublicadosxCategoria(int idcat)
        {

            if (Session["Type"] == null)
            {
                TempData["ERRORD"] = "No tiene acceso a esta sección del sistema. Inicia sesión";
                return RedirectToAction("Login", "Usuario");
            }
            //int Id = Convert.ToInt32(Session["Id"]);


            if (Session["Type"].ToString() != "Administrador")
            {


                TempData["ERRORD"] = "No tiene acceso a esta sección del sistema. Inicia sesión";
                return RedirectToAction("Login", "Usuario");

            }






            try
            {



                string titulo = "INFORME DE LIBROS PUBLICADOS";



                MemoryStream ms = new MemoryStream();
                Document document = new Document(iTextSharp.text.PageSize.A4, 85.0394f, 70.8661f, 170.079f, 85.0394f);
                PdfWriter pw = PdfWriter.GetInstance(document, ms);

                string pathImage = Server.MapPath("/ImagesWeb/LG.png");
                string logoespam = Server.MapPath("/ImagesWeb/espamlogoe1.png");
                string Image2escudo = Server.MapPath("/ImagesWeb/escudo2.png");
                pw.PageEvent = new HeaderFooter1(Image2escudo, logoespam, pathImage, titulo);


                document.Open();




                BaseFont bf2 = BaseFont.CreateFont(BaseFont.TIMES_BOLD, BaseFont.CP1250, BaseFont.EMBEDDED);

                Font titulos = new Font(bf2, 12, 0, BaseColor.BLACK);


                var fontparrafo = FontFactory.GetFont("Times New Roman", 10, 0, BaseColor.DARK_GRAY);
                var fontresp = FontFactory.GetFont("Times New Roman", 11, 0, BaseColor.DARK_GRAY);
                var fontpreg = FontFactory.GetFont(FontFactory.TIMES_BOLD, 12, 0, BaseColor.BLACK);



                string categoria= "";
                foreach (var item in conexCate.Vercategorias())
                {
                    if (item.IDcategoria == idcat)
                    {
                        categoria = item.Detallecategoria;
                    }
                }

                document.Open();

                document.Add(new Paragraph("Libros publicados correspondientes a la categoría de " + categoria + ".", titulos) { Alignment = Element.ALIGN_JUSTIFIED });
                document.Add(new Paragraph("\n"));

                var table = new PdfPTable(4);
                float[] widths = new float[] { 25f, 20f, 20f, 35f }; //ancho para cadad columna


                table.SetWidths(widths);
                table.WidthPercentage = 100f;


                table.AddCell(new PdfPCell(new Phrase("Título", fontresp)) { BorderColor = BaseColor.GRAY, BorderWidthBottom = 0, BackgroundColor = BaseColor.LIGHT_GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });
                table.AddCell(new PdfPCell(new Phrase("Publicado", fontresp)) { BorderColor = BaseColor.GRAY, BorderWidthBottom = 0, BackgroundColor = BaseColor.LIGHT_GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });
                table.AddCell(new PdfPCell(new Phrase("Carrera", fontresp)) { BorderColor = BaseColor.GRAY, BorderWidthBottom = 0, BackgroundColor = BaseColor.LIGHT_GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });
                table.AddCell(new PdfPCell(new Phrase("Autor(es)", fontresp)) { BorderColor = BaseColor.GRAY, BorderWidthBottom = 0, BackgroundColor = BaseColor.LIGHT_GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });


                foreach (var item in conexLibro.MostrarLibrosxCategoria(Convert.ToInt32(idcat)))
                {

                    table.AddCell(new PdfPCell(new Phrase(item.Titulo, fontparrafo)) { BorderColor = BaseColor.GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });
                    table.AddCell(new PdfPCell(new Phrase(item.Fechapublica, fontparrafo)) { BorderColor = BaseColor.GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });
                    table.AddCell(new PdfPCell(new Phrase(item.Carrera, fontparrafo)) { BorderColor = BaseColor.GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });
                    table.AddCell(new PdfPCell(new Phrase(item.Autores, fontparrafo)) { BorderColor = BaseColor.GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });


                }



                document.Add(table);




                document.Close();



                byte[] bytesStream = ms.ToArray();

                ms = new MemoryStream();
                ms.Write(bytesStream, 0, bytesStream.Length);
                ms.Position = 0;


                return new FileStreamResult(ms, "application/pdf");
            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";


            }


            return RedirectToAction("Informes", "Reporte");
        }







        //Función para ver informe de todos los libros publicados por año
        public ActionResult InformeLibrosPublicadosxYear(int y)
        {

            if (Session["Type"] == null)
            {
                TempData["ERRORD"] = "No tiene acceso a esta sección del sistema. Inicia sesión";
                return RedirectToAction("Login", "Usuario");
            }
            //int Id = Convert.ToInt32(Session["Id"]);


            if (Session["Type"].ToString() != "Administrador")
            {


                TempData["ERRORD"] = "No tiene acceso a esta sección del sistema. Inicia sesión";
                return RedirectToAction("Login", "Usuario");

            }






            try
            {



                string titulo = "INFORME DE LIBROS PUBLICADOS";



                MemoryStream ms = new MemoryStream();
                Document document = new Document(iTextSharp.text.PageSize.A4, 85.0394f, 70.8661f, 170.079f, 85.0394f);
                PdfWriter pw = PdfWriter.GetInstance(document, ms);

                string pathImage = Server.MapPath("/ImagesWeb/LG.png");
                string logoespam = Server.MapPath("/ImagesWeb/espamlogoe1.png");
                string Image2escudo = Server.MapPath("/ImagesWeb/escudo2.png");
                pw.PageEvent = new HeaderFooter1(Image2escudo, logoespam, pathImage, titulo);


                document.Open();




                BaseFont bf2 = BaseFont.CreateFont(BaseFont.TIMES_BOLD, BaseFont.CP1250, BaseFont.EMBEDDED);

         
                var titulos = FontFactory.GetFont(FontFactory.TIMES_BOLD, 12, 0, BaseColor.BLACK);

                var fontparrafo = FontFactory.GetFont("Times New Roman", 10, 0, BaseColor.DARK_GRAY);
                var fontresp = FontFactory.GetFont("Times New Roman", 11, 0, BaseColor.DARK_GRAY);
                var fontpreg = FontFactory.GetFont(FontFactory.TIMES_BOLD, 12, 0, BaseColor.BLACK);



                int year = y;
              
                document.Open();

                document.Add(new Paragraph("Libros publicados correspondientes al año " + y + ".", titulos) { Alignment = Element.ALIGN_JUSTIFIED });
                document.Add(new Paragraph("\n"));

                var table = new PdfPTable(4);
                float[] widths = new float[] { 25f, 20f, 20f, 35f }; //ancho para cadad columna


                table.SetWidths(widths);
                table.WidthPercentage = 100f;


                table.AddCell(new PdfPCell(new Phrase("Título", fontresp)) { BorderColor = BaseColor.GRAY, BorderWidthBottom = 0, BackgroundColor = BaseColor.LIGHT_GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });
                table.AddCell(new PdfPCell(new Phrase("Publicado", fontresp)) { BorderColor = BaseColor.GRAY, BorderWidthBottom = 0, BackgroundColor = BaseColor.LIGHT_GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });
                table.AddCell(new PdfPCell(new Phrase("Carrera", fontresp)) { BorderColor = BaseColor.GRAY, BorderWidthBottom = 0, BackgroundColor = BaseColor.LIGHT_GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });
                table.AddCell(new PdfPCell(new Phrase("Autor(es)", fontresp)) { BorderColor = BaseColor.GRAY, BorderWidthBottom = 0, BackgroundColor = BaseColor.LIGHT_GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });


                foreach (var item in conexLibro.MostrarLibrosxYear(Convert.ToInt32(y)))
                {

                    table.AddCell(new PdfPCell(new Phrase(item.Titulo, fontparrafo)) { BorderColor = BaseColor.GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });
                    table.AddCell(new PdfPCell(new Phrase(item.Fechapublica, fontparrafo)) { BorderColor = BaseColor.GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });
                    table.AddCell(new PdfPCell(new Phrase(item.Carrera, fontparrafo)) { BorderColor = BaseColor.GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });
                    table.AddCell(new PdfPCell(new Phrase(item.Autores, fontparrafo)) { BorderColor = BaseColor.GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });


                }



                document.Add(table);




                document.Close();



                byte[] bytesStream = ms.ToArray();

                ms = new MemoryStream();
                ms.Write(bytesStream, 0, bytesStream.Length);
                ms.Position = 0;


                return new FileStreamResult(ms, "application/pdf");
            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";


            }

            return RedirectToAction("Informes", "Reporte");
        }







        //Función para ver informe de todos los libros publicados por período académico
        public ActionResult InformeLibrosPublicadosxPeriodo(int idpe)
        {

            if (Session["Type"] == null)
            {
                TempData["ERRORD"] = "No tiene acceso a esta sección del sistema. Inicia sesión";
                return RedirectToAction("Login", "Usuario");
            }
            //int Id = Convert.ToInt32(Session["Id"]);


            if (Session["Type"].ToString() != "Administrador")
            {


                TempData["ERRORD"] = "No tiene acceso a esta sección del sistema. Inicia sesión";
                return RedirectToAction("Login", "Usuario");

            }






            try
            {



                string titulo = "INFORME DE LIBROS PUBLICADOS";



                MemoryStream ms = new MemoryStream();
                Document document = new Document(iTextSharp.text.PageSize.A4, 85.0394f, 70.8661f, 170.079f, 85.0394f);
                PdfWriter pw = PdfWriter.GetInstance(document, ms);

                string pathImage = Server.MapPath("/ImagesWeb/LG.png");
                string logoespam = Server.MapPath("/ImagesWeb/espamlogoe1.png");
                string Image2escudo = Server.MapPath("/ImagesWeb/escudo2.png");
                pw.PageEvent = new HeaderFooter1(Image2escudo, logoespam, pathImage, titulo);


                document.Open();




                BaseFont bf2 = BaseFont.CreateFont(BaseFont.TIMES_BOLD, BaseFont.CP1250, BaseFont.EMBEDDED);


                var titulos = FontFactory.GetFont(FontFactory.TIMES_BOLD, 12, 0, BaseColor.BLACK);

                var fontparrafo = FontFactory.GetFont("Times New Roman", 10, 0, BaseColor.DARK_GRAY);
                var fontresp = FontFactory.GetFont("Times New Roman", 11, 0, BaseColor.DARK_GRAY);
                var fontpreg = FontFactory.GetFont(FontFactory.TIMES_BOLD, 12, 0, BaseColor.BLACK);



                string periodo = "";
                foreach(var item in conexPeriodo.ListarPeriodos())
                {
                    if (item.IDperiodo == idpe)
                    {
                        periodo = item.Detalleperiodo;
                    }
                }

                document.Open();

                document.Add(new Paragraph( periodo + ".", titulos) { Alignment = Element.ALIGN_JUSTIFIED });
                document.Add(new Paragraph("\n"));

                var table = new PdfPTable(4);
                float[] widths = new float[] { 25f, 20f, 20f, 35f }; //ancho para cadad columna


                table.SetWidths(widths);
                table.WidthPercentage = 100f;


                table.AddCell(new PdfPCell(new Phrase("Título", fontresp)) { BorderColor = BaseColor.GRAY, BorderWidthBottom = 0, BackgroundColor = BaseColor.LIGHT_GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });
                table.AddCell(new PdfPCell(new Phrase("Publicado", fontresp)) { BorderColor = BaseColor.GRAY, BorderWidthBottom = 0, BackgroundColor = BaseColor.LIGHT_GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });
                table.AddCell(new PdfPCell(new Phrase("Carrera", fontresp)) { BorderColor = BaseColor.GRAY, BorderWidthBottom = 0, BackgroundColor = BaseColor.LIGHT_GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });
                table.AddCell(new PdfPCell(new Phrase("Autor(es)", fontresp)) { BorderColor = BaseColor.GRAY, BorderWidthBottom = 0, BackgroundColor = BaseColor.LIGHT_GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });


                foreach (var item in conexLibro.MostrarLibrosxPeriodo(Convert.ToInt32(idpe)))
                {

                    table.AddCell(new PdfPCell(new Phrase(item.Titulo, fontparrafo)) { BorderColor = BaseColor.GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });
                    table.AddCell(new PdfPCell(new Phrase(item.Fechapublica, fontparrafo)) { BorderColor = BaseColor.GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });
                    table.AddCell(new PdfPCell(new Phrase(item.Carrera, fontparrafo)) { BorderColor = BaseColor.GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });
                    table.AddCell(new PdfPCell(new Phrase(item.Autores, fontparrafo)) { BorderColor = BaseColor.GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });


                }



                document.Add(table);




                document.Close();



                byte[] bytesStream = ms.ToArray();

                ms = new MemoryStream();
                ms.Write(bytesStream, 0, bytesStream.Length);
                ms.Position = 0;


                return new FileStreamResult(ms, "application/pdf");
            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";


            }


            return RedirectToAction("Informes", "Reporte");
        }








        //Función para ver informe de todos los libros publicados por carrera y año
        public ActionResult InformeLibrosPublicadosxCarreraYear(int idc,int yearp)
        {

            if (Session["Type"] == null)
            {
                TempData["ERRORD"] = "No tiene acceso a esta sección del sistema. Inicia sesión";
                return RedirectToAction("Login", "Usuario");
            }
            //int Id = Convert.ToInt32(Session["Id"]);


            if (Session["Type"].ToString() != "Administrador")
            {


                TempData["ERRORD"] = "No tiene acceso a esta sección del sistema. Inicia sesión";
                return RedirectToAction("Login", "Usuario");

            }






            try
            {



                string titulo = "INFORME DE LIBROS PUBLICADOS";



                MemoryStream ms = new MemoryStream();
                Document document = new Document(iTextSharp.text.PageSize.A4, 85.0394f, 70.8661f, 170.079f, 85.0394f);
                PdfWriter pw = PdfWriter.GetInstance(document, ms);

                string pathImage = Server.MapPath("/ImagesWeb/LG.png");
                string logoespam = Server.MapPath("/ImagesWeb/espamlogoe1.png");
                string Image2escudo = Server.MapPath("/ImagesWeb/escudo2.png");
                pw.PageEvent = new HeaderFooter1(Image2escudo, logoespam, pathImage, titulo);


                document.Open();




                BaseFont bf2 = BaseFont.CreateFont(BaseFont.TIMES_BOLD, BaseFont.CP1250, BaseFont.EMBEDDED);

             

                var titulos = FontFactory.GetFont(FontFactory.TIMES_BOLD, 12, 0, BaseColor.BLACK);
                var fontparrafo = FontFactory.GetFont("Times New Roman", 10, 0, BaseColor.DARK_GRAY);
                var fontresp = FontFactory.GetFont("Times New Roman", 11, 0, BaseColor.DARK_GRAY);
                var fontpreg = FontFactory.GetFont(FontFactory.TIMES_BOLD, 12, 0, BaseColor.BLACK);



                string carrera = "";
                foreach (var item in conexcarrera.VerCarreras())
                {
                    if (item.IDcarrera == idc)
                    {
                        carrera = item.Carrera;
                    }
                }

                document.Open();

                document.Add(new Paragraph("Libros publicados por la carrera de " + carrera + " en el año "+yearp+".", titulos) { Alignment = Element.ALIGN_JUSTIFIED });
                document.Add(new Paragraph("\n"));

                var table = new PdfPTable(4);
                float[] widths = new float[] { 25f, 20f, 20f, 35f }; //ancho para cadad columna


                table.SetWidths(widths);
                table.WidthPercentage = 100f;


                table.AddCell(new PdfPCell(new Phrase("Título", fontresp)) { BorderColor = BaseColor.GRAY, BorderWidthBottom = 0, BackgroundColor = BaseColor.LIGHT_GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });
                table.AddCell(new PdfPCell(new Phrase("Publicado", fontresp)) { BorderColor = BaseColor.GRAY, BorderWidthBottom = 0, BackgroundColor = BaseColor.LIGHT_GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });
                table.AddCell(new PdfPCell(new Phrase("Carrera", fontresp)) { BorderColor = BaseColor.GRAY, BorderWidthBottom = 0, BackgroundColor = BaseColor.LIGHT_GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });
                table.AddCell(new PdfPCell(new Phrase("Autor(es)", fontresp)) { BorderColor = BaseColor.GRAY, BorderWidthBottom = 0, BackgroundColor = BaseColor.LIGHT_GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });


                foreach (var item in conexLibro.MostrarLibrosxCarreraYear(idc,yearp))
                {

                    table.AddCell(new PdfPCell(new Phrase(item.Titulo, fontparrafo)) { BorderColor = BaseColor.GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });
                    table.AddCell(new PdfPCell(new Phrase(item.Fechapublica, fontparrafo)) { BorderColor = BaseColor.GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });
                    table.AddCell(new PdfPCell(new Phrase(item.Carrera, fontparrafo)) { BorderColor = BaseColor.GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });
                    table.AddCell(new PdfPCell(new Phrase(item.Autores, fontparrafo)) { BorderColor = BaseColor.GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });


                }



                document.Add(table);




                document.Close();



                byte[] bytesStream = ms.ToArray();

                ms = new MemoryStream();
                ms.Write(bytesStream, 0, bytesStream.Length);
                ms.Position = 0;


                return new FileStreamResult(ms, "application/pdf");
            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";


            }


            return RedirectToAction("Informes", "Reporte");
        }







        //Función para ver informe de todos los libros publicados por carrera y periodo
        public ActionResult InformeLibrosPublicadosxCarreraPeriodo(int idpe, int idc)
        {

            if (Session["Type"] == null)
            {
                TempData["ERRORD"] = "No tiene acceso a esta sección del sistema. Inicia sesión";
                return RedirectToAction("Login", "Usuario");
            }
            //int Id = Convert.ToInt32(Session["Id"]);


            if (Session["Type"].ToString() != "Administrador")
            {


                TempData["ERRORD"] = "No tiene acceso a esta sección del sistema. Inicia sesión";
                return RedirectToAction("Login", "Usuario");

            }






            try
            {



                string titulo = "INFORME DE LIBROS PUBLICADOS";



                MemoryStream ms = new MemoryStream();
                Document document = new Document(iTextSharp.text.PageSize.A4, 85.0394f, 70.8661f, 170.079f, 85.0394f);
                PdfWriter pw = PdfWriter.GetInstance(document, ms);

                string pathImage = Server.MapPath("/ImagesWeb/LG.png");
                string logoespam = Server.MapPath("/ImagesWeb/espamlogoe1.png");
                string Image2escudo = Server.MapPath("/ImagesWeb/escudo2.png");
                pw.PageEvent = new HeaderFooter1(Image2escudo, logoespam, pathImage, titulo);


                document.Open();




                BaseFont bf2 = BaseFont.CreateFont(BaseFont.TIMES_BOLD, BaseFont.CP1250, BaseFont.EMBEDDED);



                var titulos = FontFactory.GetFont(FontFactory.TIMES_BOLD, 12, 0, BaseColor.BLACK);
                var fontparrafo = FontFactory.GetFont("Times New Roman", 10, 0, BaseColor.DARK_GRAY);
                
                var fontresp = FontFactory.GetFont("Times New Roman", 11, 0, BaseColor.DARK_GRAY);
                var fontpreg = FontFactory.GetFont(FontFactory.TIMES_BOLD, 12, 0, BaseColor.BLACK);



                string carrera = "";
                string periodo = "";
                foreach (var item in conexcarrera.VerCarreras())
                {
                    if (item.IDcarrera == idc)
                    {
                        carrera = item.Carrera;
                    }
                }

                foreach (var item in conexPeriodo.ListarPeriodos())
                {
                    if (item.IDperiodo == idpe)
                    {
                        periodo = item.Detalleperiodo;
                    }
                }




                document.Open();

                document.Add(new Paragraph("Libros publicados por la carrera de " + carrera + ".", titulos) { Alignment = Element.ALIGN_JUSTIFIED });
                document.Add(new Paragraph(periodo + ".", fontparrafo) { Alignment = Element.ALIGN_JUSTIFIED });
                document.Add(new Paragraph("\n"));

                var table = new PdfPTable(4);
                float[] widths = new float[] { 25f, 20f, 20f, 35f }; //ancho para cadad columna


                table.SetWidths(widths);
                table.WidthPercentage = 100f;


                table.AddCell(new PdfPCell(new Phrase("Título", fontresp)) { BorderColor = BaseColor.GRAY, BorderWidthBottom = 0, BackgroundColor = BaseColor.LIGHT_GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });
                table.AddCell(new PdfPCell(new Phrase("Publicado", fontresp)) { BorderColor = BaseColor.GRAY, BorderWidthBottom = 0, BackgroundColor = BaseColor.LIGHT_GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });
                table.AddCell(new PdfPCell(new Phrase("Carrera", fontresp)) { BorderColor = BaseColor.GRAY, BorderWidthBottom = 0, BackgroundColor = BaseColor.LIGHT_GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });
                table.AddCell(new PdfPCell(new Phrase("Autor(es)", fontresp)) { BorderColor = BaseColor.GRAY, BorderWidthBottom = 0, BackgroundColor = BaseColor.LIGHT_GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });


                foreach (var item in conexLibro.MostrarLibrosxCarreraPeriodo(idpe, idc))
                {

                    table.AddCell(new PdfPCell(new Phrase(item.Titulo, fontparrafo)) { BorderColor = BaseColor.GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });
                    table.AddCell(new PdfPCell(new Phrase(item.Fechapublica, fontparrafo)) { BorderColor = BaseColor.GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });
                    table.AddCell(new PdfPCell(new Phrase(item.Carrera, fontparrafo)) { BorderColor = BaseColor.GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });
                    table.AddCell(new PdfPCell(new Phrase(item.Autores, fontparrafo)) { BorderColor = BaseColor.GRAY, HorizontalAlignment = Element.ALIGN_CENTER, PaddingBottom = 5, PaddingTop = 5 });


                }



                document.Add(table);




                document.Close();



                byte[] bytesStream = ms.ToArray();

                ms = new MemoryStream();
                ms.Write(bytesStream, 0, bytesStream.Length);
                ms.Position = 0;


                return new FileStreamResult(ms, "application/pdf");
            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";


            }



            return RedirectToAction("Informes", "Reporte");
        }








        public ActionResult Informes()
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


     

            ViewBag.Carreras = conexcarrera.VerCarreras();
            ViewBag.Categorias = conexCate.Vercategorias();
            ViewBag.Periodos = conexPeriodo.ListarPeriodos();

            ViewBag.DatosEditorial = conexDatosEditorial.VerDatosContactoEditorial();
            ViewBag.Usuario = conexUser.BuscarUsuario(Session["Username"].ToString());


            return View();
        }








        [HttpPost]
        public ActionResult ConsultaInformes(int idcarrera, int idperiodo, int year,int idcate,int tipoconsulta)
        {

            if (tipoconsulta == 0)
            {
                
                
                TempData["ERROR2"] = "No se ha seleccionado el tipo de consulta, seleccione el parámetro requerido y vuelva a intentarlo.";
                return RedirectToAction("Informes", new RouteValueDictionary(new { controller = "Reporte", action = "Informes", t = ViewBag.tbusca }));

                //return RedirectToAction("Informes", "Reporte");
            }


            if (tipoconsulta == 1)
            {
                
                return RedirectToAction("InformeLibrosPublicados", "Reporte");

            }
            if (tipoconsulta == 2)
            {
             
                if (idcarrera == 0)
                {
                    
                    TempData["ERROR2"] = "No se ha seleccionado carrera a consultar, seleccione el parámetro requerido y vuelva a intentarlo.";
                    return RedirectToAction("Informes", "Reporte");

                }
                return RedirectToAction("InformeLibrosPublicadosxCarrera", new RouteValueDictionary(new { controller = "Reporte", action = "InformeLibrosPublicadosxCarrera", Idc = idcarrera }));

            }
            if (tipoconsulta == 3)
            {
              
                if (idcate == 0)
                {
                    
                    TempData["ERROR2"] = "No se ha seleccionado categoría a consultar, seleccione el parámetro requerido y vuelva a intentarlo.";
                    return RedirectToAction("Informes", "Reporte");
                }
                return RedirectToAction("InformeLibrosPublicadosxCategoria", new RouteValueDictionary(new { controller = "Reporte", action = "InformeLibrosPublicadosxCategoria", Idcat = idcate  }));

            }
            if (tipoconsulta == 4)
            {
                
                if (idperiodo == 0)
                {
                    
                    TempData["ERROR2"] = "No se ha seleccionado período a consultar, seleccione el parámetro requerido y vuelva a intentarlo.";
                    return RedirectToAction("Informes", "Reporte");
                }
                return RedirectToAction("InformeLibrosPublicadosxPeriodo", new RouteValueDictionary(new { controller = "Reporte", action = "InformeLibrosPublicadosxPeriodo", idpe = idperiodo }));

            }
            if (tipoconsulta == 5)
            {
                
                
                if (year == 0 || year<0)
                {
                   TempData["ERROR2"] = "No se ha ingresado un valor permitido para año a consultar, seleccione el parámetro requerido y vuelva a intentarlo.";
                    return RedirectToAction("Informes", "Reporte");
                }
                return RedirectToAction("InformeLibrosPublicadosxYear", new RouteValueDictionary(new { controller = "Reporte", action = "InformeLibrosPublicadosxYear", y = year }));

            }
            if (tipoconsulta == 6)
            {
               
                if (idcarrera == 0)
                {
                    
                    TempData["ERROR2"] = "No se ha seleccionado carrera a consultar, seleccione el parámetro requerido y vuelva a intentarlo.";
                    return RedirectToAction("Informes", "Reporte");
                }
                if (year == 0 || year < 0)
                {
                    TempData["ERROR2"] = "No se ha ingresado un valor permitido para año a consultar, seleccione el parámetro requerido y vuelva a intentarlo.";
                    return RedirectToAction("Informes", "Reporte");
                }
              
                return RedirectToAction("InformeLibrosPublicadosxCarreraYear", new RouteValueDictionary(new { controller = "Reporte", action = "InformeLibrosPublicadosxCarreraYear", idc = idcarrera, yearp = year }));

            }
            if (tipoconsulta == 7)
            {
               
                if (idcarrera == 0)
                {
                    
                    TempData["ERROR2"] = "No se ha seleccionado carrera a consultar, seleccione el parámetro requerido y vuelva a intentarlo.";
                    return RedirectToAction("Informes", "Reporte");
                }
                if (idperiodo == 0)
                {
                 
                    TempData["ERROR2"] = "No se ha seleccionado período a consultar, seleccione el parámetro requerido y vuelva a intentarlo.";
                    return RedirectToAction("Informes", "Reporte");
                }
                return RedirectToAction("InformeLibrosPublicadosxCarreraPeriodo", new RouteValueDictionary(new { controller = "Reporte", action = "InformeLibrosPublicadosxCarreraPeriodo", idpe = idperiodo, idc = idcarrera }));

            }

            return RedirectToAction("Informes", "Reporte");



        }









    }



    //Clase para obtener el diseño del encabezado y pie de página de los documentos generados mediante itext-sharp
    class HeaderFooter1 : PdfPageEventHelper
    {
        string PathImage = null;
        string PathImage2 = null;
        string PathImage3 = null;
        string titulo1 = null;

        public HeaderFooter1(string logo1Path, string logo2Path, string logo3Path,string titulo)
        {
            PathImage = logo1Path;
            PathImage2 = logo2Path;
            PathImage3 = logo3Path;
            titulo1 = titulo;
        }
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            BaseFont bfH = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.EMBEDDED);

            Font fontTextH = new Font(bfH, 12, 0, BaseColor.BLACK);

        


            PdfPTable tbHeader = new PdfPTable(3);
            float[] widths = new float[] { 42.5197f, 340.157f, 42.5197f }; //ancho para cadad columna

            tbHeader.SetWidths(widths);
            tbHeader.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
            tbHeader.DefaultCell.Border = 0;

            

            

            tbHeader.AddCell(new Paragraph());

            PdfPCell _cell = new PdfPCell(new Paragraph("ESCUELA SUPERIOR POLITÉCNICA AGROPECUARIA DE MANABÍ MANUEL FELÍX LÓPEZ \n \n Editorial Humus \n \n", fontTextH));

            _cell.SetLeading(0f, 1.1732f); //interlineado
            _cell.HorizontalAlignment = Element.ALIGN_CENTER;
            _cell.Border = 0;

         
            tbHeader.AddCell(_cell);
        

            tbHeader.AddCell(new Paragraph());

            tbHeader.WriteSelectedRows(0, -1, writer.PageSize.GetLeft(document.LeftMargin) + 5, writer.PageSize.GetTop(document.TopMargin) +140, writer.DirectContent);
         

            PdfPTable tbHeader2 = new PdfPTable(3);
            float[] widths2 = new float[] { 42.5197f, 340.157f, 42.5197f }; 

            tbHeader2.SetWidths(widths2);
            tbHeader2.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
            tbHeader2.DefaultCell.Border = 0;
            //BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_BOLD, BaseFont.CP1250, BaseFont.EMBEDDED);
            //Font fontTitle = new Font(bf, 18, 0, BaseColor.BLACK);

            var fontTitle = FontFactory.GetFont(FontFactory.TIMES_BOLD, 18, 0, BaseColor.BLACK);
            



            tbHeader2.AddCell(new Paragraph());


            PdfPCell _cellt = new PdfPCell(new Paragraph(titulo1, fontTitle));

            _cellt.SetLeading(0f, 1.1732f); //interlineado
            _cellt.HorizontalAlignment = Element.ALIGN_CENTER;
            _cell.VerticalAlignment = Element.ALIGN_JUSTIFIED;
            _cellt.Border = 0;
          
            tbHeader2.AddCell(_cellt);


            tbHeader2.AddCell(new Paragraph());

            tbHeader2.WriteSelectedRows(0, -1, writer.PageSize.GetLeft(document.LeftMargin) - 2, writer.PageSize.GetTop(document.TopMargin) +70, writer.DirectContent);



            PdfPTable tbFooter = new PdfPTable(3);
            float[] widths3 = new float[] { 42.5197f, 340.157f, 42.5197f }; //ancho para cadad columna

            tbFooter.SetWidths(widths3);
            tbFooter.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
            tbFooter.DefaultCell.Border = 0;

            tbFooter.AddCell(new Paragraph());

            tbFooter.AddCell(new Paragraph());

            BaseFont bf2 = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.EMBEDDED);
            Font fontText2 = new Font(bf2, 10, 0, BaseColor.GRAY);

          
            //PdfPCell _cell2 =new PdfPCell(new Paragraph("Página " + writer.PageNumber, fontText2));
            //_cell2.SetLeading(0f, 1.1732f); //interlineado
            //_cell2.HorizontalAlignment = Element.ALIGN_RIGHT;
            //_cell2.Border = 0;

            //tbFooter.AddCell(_cell2);



            tbFooter.WriteSelectedRows(0, -1, writer.PageSize.GetLeft(document.LeftMargin)+35, writer.PageSize.GetBottom(document.BottomMargin) - 25, writer.DirectContent);

            //Begin Image

            Image logo1 = Image.GetInstance(PathImage);
            logo1.ScaleAbsoluteWidth(80);
            logo1.ScaleAbsoluteHeight(100);

            logo1.SetAbsolutePosition(writer.PageSize.GetLeft(document.LeftMargin) - 35, writer.PageSize.GetTop(document.TopMargin) +50);

            document.Add(logo1);

            Image logo2 = Image.GetInstance(PathImage2);
            logo2.ScaleAbsoluteWidth(80);
            logo2.ScaleAbsoluteHeight(100);

            logo2.SetAbsolutePosition(writer.PageSize.GetRight(document.RightMargin) - 35, writer.PageSize.GetTop(document.TopMargin) +55);

            document.Add(logo2);

            Image logo3 = Image.GetInstance(PathImage3);
            logo3.ScaleAbsoluteWidth(190);
            logo3.ScaleAbsoluteHeight(120);

            logo3.SetAbsolutePosition(writer.PageSize.GetLeft(document.LeftMargin) + 118, writer.PageSize.GetBottom(document.BottomMargin) - 100);
            document.Add(logo3);


            //End Image


        }
    }





}















