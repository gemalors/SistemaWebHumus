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
    public class CuestionarioController : Controller
    {

        M_Libros conexLibro = new M_Libros();
        M_Usuarios conexUser = new M_Usuarios();
        M_Cuestionarios conexCuestionario = new M_Cuestionarios();
        M_Preguntas conexPreg = new M_Preguntas();
        M_Notificaciones conexNotificacion = new M_Notificaciones();
        M_ObservacionesProceso conexObser = new M_ObservacionesProceso();
        M_Procesos conexProceso = new M_Procesos();
        M_Documentos conexDocs = new M_Documentos();
        M_Evaluadores conexEvaluador = new M_Evaluadores();
        M_EstadosTiposproceso conexEstado = new M_EstadosTiposproceso();








        //Función para generar informe de evaluación de pares académicos en memorystream
        public ActionResult EvaluacionParAcademico(int Idcuestionario, int Idlibro, int Ida)
        {


            if (Session["Type"] == null)
            {
                TempData["ERRORD"] = "No tiene acceso a esta sección del sistema. Inicia sesión";
                return RedirectToAction("Login", "Usuario");
            }

            int Iduser = Convert.ToInt32(Session["Id"]);
            string titulol = "";
            string titulo = "";
            string descripcioncuestionario = "";
            int Idt = 0;
            string fecha = "";
            string NombresUser = "";
            int Idproceso = 0;
            string user = "";
            string tipouser = Session["Type"].ToString();


            try
            {
              


                foreach (var itemt in conexLibro.VerDetalleLibro(Idlibro))
                {
                    titulol = itemt.Titulo;
                    Idproceso = Convert.ToInt32(itemt.IdprocesoL);
                }


                ViewBag.DetalleAsigEvaluador = conexEvaluador.VerDetalleAsignacionEvaluador(Idproceso, Iduser);
                foreach (var item1 in ViewBag.DetalleAsigEvaluador)
                {

                    if (Ida != item1.IdasignacionEvaluacion)
                    {
                        TempData["ERRORD"] = "Algo salió mal";
                        return RedirectToAction("Login", "Usuario");
                    }
                    else
                    {
                        Idlibro = Convert.ToInt32(item1.Idlibro);
                    }

                }

                if (ViewBag.DetalleAsigEvaluador.Count == 0)
                {
                    TempData["ERRORD"] = "Algo salió mal";
                    return RedirectToAction("Login", "Usuario");
                }



                //Detalle de asignación
                foreach (var itema2 in conexCuestionario.VerDetalleAsignacion(Ida))
                {
                    fecha = System.DateTime.Now.ToString("dd/MM/yyyy");

                    NombresUser = itema2.PrimerNombre + " " + itema2.ApellidoPrimero+" "+itema2.ApellidoSegundo;

                    user = itema2.Detalleusuario;
                    if (Iduser == itema2.Idpersona)
                    {
                        Iduser = Convert.ToInt32(itema2.Idpersona);
                    }
                    else
                    {
                        TempData["ERRORD"] = "Algo salió mal";
                        return RedirectToAction("Login", "Usuario");

                    }
                    if (Idcuestionario != itema2.Idcuestionario)
                    {
                        TempData["ERRORD"] = "Algo salió mal";
                        return RedirectToAction("Login", "Usuario");
                    }
                   
                   
                    
                }

                foreach (var item in conexCuestionario.VerCuestionario(Idcuestionario, tipouser))
                {
                    titulo = item.Nombre;
                    descripcioncuestionario = item.Descripcion;
                    Idt = Convert.ToInt32(item.Idtipousuario);
                }


                MemoryStream ms = new MemoryStream();
                Document document = new Document(iTextSharp.text.PageSize.A4, 85.0394f, 85.0394f, 170.079f, 85.0394f);
                PdfWriter pw = PdfWriter.GetInstance(document, ms);

                string pathImage = Server.MapPath("/ImagesWeb/LG.png");
                string logoespam = Server.MapPath("/ImagesWeb/espamlogoe1.png");
                string Image2escudo = Server.MapPath("/ImagesWeb/escudo2.png");
                pw.PageEvent = new HeaderFooter1(Image2escudo, logoespam, pathImage, titulo);

                //PdfWriter.getInstance(document, new FileStream("Chap0707.pdf", FileMode.Create));

                // step 3: we parse the document
               





                document.Open();

                //BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.EMBEDDED);
                ////Font fontText = new Font(bf, 10, 0, BaseColor.BLACK);



                //BaseFont bp = BaseFont.CreateFont(BaseFont.TIMES_BOLD, BaseFont.CP1250, BaseFont.EMBEDDED);

             
                //Font fontresp1 = new Font(bf, 12, 0, BaseColor.DARK_GRAY);

                var fontparrafo = FontFactory.GetFont("Times New Roman", 12, 0, BaseColor.BLACK);
                var fontresp = FontFactory.GetFont("Times New Roman", 12, 0, BaseColor.DARK_GRAY);
                var fontpreg = FontFactory.GetFont(FontFactory.TIMES_BOLD, 12, 0, BaseColor.BLACK);
              
                //var fontpreg = FontFactory.GetFont("Times New Roman", 12, 0, BaseColor.BLACK);

                string descrip = descripcioncuestionario.Replace("<p>", "");
                descrip = descrip.Replace("</p>", "\n");
                descrip = descrip.Replace("<br>", "\n");



                document.Add(new Paragraph(descrip, fontparrafo) { Alignment = Element.ALIGN_JUSTIFIED });
                document.Add(new Paragraph("\n"));



                var tb1 = new PdfPTable(new float[] { 40f, 70f }) { WidthPercentage = 100f };
                var colt1 = new PdfPCell(new Phrase("TÍTULO DEL LIBRO:", fontpreg)) { BorderWidthRight = 0, BorderWidthBottom = 0, BorderWidthTop = 0, BorderWidthLeft = 0 };
                var colt2 = new PdfPCell(new Phrase(titulol, fontparrafo)) { BorderWidthLeft = 0, BorderWidthBottom = 0, BorderWidthRight = 0, BorderWidthTop = 0, HorizontalAlignment = Element.ALIGN_LEFT, PaddingLeft = -25 };

                tb1.AddCell(colt1);
                tb1.AddCell(colt2);


                document.Add(tb1);





                //preguntas registradas
                foreach (var item1 in conexPreg.ConsultarPreguntas(Idcuestionario))
                {
                    document.Add(new Paragraph("\n"));
                    document.Add(new Paragraph(item1.LeyendaSuperior, fontparrafo) { Alignment = Element.ALIGN_JUSTIFIED });


                    document.Add(new Paragraph(item1.Orden + ". " + item1.Descripcion, fontparrafo) { Alignment = Element.ALIGN_JUSTIFIED });



                    if (item1.Idtipopregunta == 1)
                    {
                        foreach (var item2 in conexCuestionario.VerRespuestaPregAbierta(Iduser, Idcuestionario, Ida))
                        {
                            if (item1.IDpregunta == item2.Idpregunta)
                            {

                                document.Add(new Paragraph(item2.DescripcionRespuestaAbierta, fontresp) { Alignment = Element.ALIGN_JUSTIFIED });
                            }
                        }
                    }




                    if (item1.Idtipopregunta == 2)
                    {
                        if (item1.TiposOpciones == "Opciones y respuestas")
                        {
                            foreach (var item3 in conexPreg.VerPreguntaSeleccion(Idcuestionario))
                            {
                                if (item1.IDpregunta == item3.Idpregunta)
                                {
                                    document.Add(new Paragraph("- " + item3.descripOpcpreg, fontparrafo) { Alignment = Element.ALIGN_LEFT });
                                    foreach (var item4 in conexCuestionario.VerRespuestaPregOpcionesTipo1(Iduser, Idcuestionario, Ida))
                                    {
                                        if (item3.IDopcionPreguntaSeleccion == item4.Idopcionpreg)
                                        {

                                            document.Add(new Paragraph("   " + item4.DescripcionOpcionRespuesta, fontresp) { Alignment = Element.ALIGN_JUSTIFIED });
                                        }
                                    }
                                }
                            }
                        }

                        if (item1.TiposOpciones == "Solo respuestas")
                        {
                            foreach (var item5 in conexCuestionario.VerRespuestaPregOpcionesTipo2(Iduser, Idcuestionario, Ida))
                            {
                                if (item1.IDpregunta == item5.Idpregunta)
                                {
                                    document.Add(new Paragraph("   " + item5.DescripcionOpcionRespuesta, fontresp) { Alignment = Element.ALIGN_JUSTIFIED });
                                }
                            }
                        }

                    }




                    if (item1.Idtipopregunta == 3)
                    {
                        if (item1.TiposOpciones == "Opciones y respuestas")
                        {
                            foreach (var item3 in conexPreg.VerPreguntaSeleccion(Idcuestionario))
                            {
                                if (item1.IDpregunta == item3.Idpregunta)
                                {
                                    document.Add(new Paragraph("- " + item3.descripOpcpreg, fontparrafo) { Alignment = Element.ALIGN_LEFT });

                                    foreach (var item4 in conexCuestionario.VerRespuestaPregOpcionesTipo1(Iduser, Idcuestionario, Ida))
                                    {
                                        if (item1.IDpregunta == item4.Idpregunta)
                                        {

                                            document.Add(new Paragraph("  " + item4.DescripcionOpcionRespuesta, fontresp) { Alignment = Element.ALIGN_JUSTIFIED });
                                        }
                                    }
                                }
                            }
                        }

                        if (item1.TiposOpciones == "Solo respuestas")
                        {
                            foreach (var item5 in conexCuestionario.VerRespuestaPregOpcionesTipo2(Iduser, Idcuestionario, Ida))
                            {
                                if (item1.IDpregunta == item5.Idpregunta)
                                {
                                    document.Add(new Paragraph("- " + item5.DescripcionOpcionRespuesta, fontresp) { Alignment = Element.ALIGN_JUSTIFIED });
                                }
                            }
                        }

                    }




                    if (item1.Idtipopregunta == 4)
                    {
                        document.Add(new Paragraph("\n"));

                        var tbp4 = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f };
                        var colp4 = new PdfPCell(new Phrase("OBSERVACIONES", fontpreg)) { BorderWidthBottom = 0, BorderColor = BaseColor.BLACK, Padding = 5f, HorizontalAlignment = Element.ALIGN_CENTER };

                        tbp4.AddCell(colp4);

                        document.Add(tbp4);


                        var fontresp1 = FontFactory.GetFont("Times New Roman", 11, 0, BaseColor.DARK_GRAY);
                      


                        foreach (var item6 in conexPreg.VerPreguntaObservaciones(Idcuestionario))
                        {
                            if (item1.IDpregunta == item6.Idpregunta)
                            {
                                //document.Add(new Paragraph("- " + item6.Detallepregobservacion, fontparrafo) { Alignment = Element.ALIGN_LEFT });
                                if (item6.Respuestas == 0)
                                {
                                    var tbp = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f };
                                    var colp = new PdfPCell(new Phrase(item6.Detallepregobservacion, fontpreg)) { BorderColor = BaseColor.BLACK, BorderWidthBottom = 0, Padding = 5f, HorizontalAlignment = Element.ALIGN_LEFT };

                                    tbp.AddCell(colp);

                                    document.Add(tbp);
                                }

                                foreach (var item7 in conexCuestionario.VerRespuestaPregTipo4(Iduser, Idcuestionario, Ida))
                                {
                                    if (item6.IDpreguntaObservaciones == item7.Idobservacionpreg)
                                    {
                                        if (item6.Respuestas == 0)
                                        {


                                            var tbpr4 = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f };
                                            string nueva1 = item7.DescripcionRespuestaAbierta.Replace("<p>", "");
                                            nueva1 = nueva1.Replace("</p>", "\n");
                                            nueva1 = nueva1.Replace("<br>", "\n");

                                            var p = tbpr4.AddCell(new PdfPCell(new Phrase(nueva1, fontresp1)) { BorderColor = BaseColor.BLACK, BorderWidthBottom = 0, BorderWidthTop = 0, Padding = 8f, HorizontalAlignment = Element.ALIGN_JUSTIFIED });
                                            p.SetLeading(0, 1.5f);

                                            tbpr4.AddCell(new PdfPCell(new Phrase("\n", fontresp1)) { BorderColor = BaseColor.BLACK, BorderWidthTop = 0, BorderWidthBottom = 0, HorizontalAlignment = Element.ALIGN_CENTER });

                                            document.Add(tbpr4);

                                        }
                                        else
                                        {

                                            var tbpr4 = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f };

                                            var p = tbpr4.AddCell(new PdfPCell(new Phrase(item7.titulorespabierta, fontpreg)) { BorderColor = BaseColor.BLACK, PaddingBottom = 3f, PaddingTop = 1f, PaddingLeft = 5f, BorderWidthBottom = 0, HorizontalAlignment = Element.ALIGN_JUSTIFIED });
                                            p.SetLeading(0, 1.5f);


                                            //replace
                                            string nueva = item7.DescripcionRespuestaAbierta.Replace("<p>", "");
                                            nueva = nueva.Replace("</p>", "\n");
                                            nueva = nueva.Replace("<br>", "\n");


                                            var p1 = tbpr4.AddCell(new PdfPCell(new Phrase(nueva, fontresp1)) { BorderColor = BaseColor.BLACK, BorderWidthBottom = 0, BorderWidthTop = 0, Padding = 8f, HorizontalAlignment = Element.ALIGN_JUSTIFIED });
                                            p1.SetLeading(0, 1.5f);

                                            tbpr4.AddCell(new PdfPCell(new Phrase("\n", fontresp1)) { BorderColor = BaseColor.BLACK, BorderWidthTop = 0, BorderWidthBottom = 0, HorizontalAlignment = Element.ALIGN_CENTER });

                                            document.Add(tbpr4);



                                        }

                                    }
                                }
                            }






                        }




                    }







                }
                //Datos de usuario que llena este documento v1

                //document.Add(new Paragraph("\n\n\n\n"));



                //document.Add(new Paragraph("_______________________________", fontpreg) { Alignment = Element.ALIGN_CENTER });

                //    document.Add(new Paragraph("Firma de evaluador", fontresp) { Alignment = Element.ALIGN_CENTER });
                //    document.Add(new Paragraph("Evaluado por " + NombresUser, fontresp) { Alignment = Element.ALIGN_CENTER });
                //    document.Add(new Paragraph("Fecha de evaluación el " +fecha, fontresp) { Alignment = Element.ALIGN_CENTER });


                //document.Add(new Paragraph("\n\n"));
                //Datos de usuario que llena este documento v2 con tabla






                var tbdatos = new PdfPTable(new float[] { 80f, 20f }) { WidthPercentage = 100f };
                var col1 = new PdfPCell(new Phrase("Fecha:", fontpreg)) { BorderWidthRight = 0, BorderWidthBottom = 0, BorderColor = BaseColor.BLACK, Padding = 5f };
                var col2 = new PdfPCell(new Phrase(fecha + " ", fontresp)) { BorderWidthLeft = 0, BorderWidthBottom = 0, BorderColor = BaseColor.BLACK, HorizontalAlignment = Element.ALIGN_RIGHT };

                tbdatos.AddCell(col1);
                tbdatos.AddCell(col2);

                document.Add(tbdatos);



                var tbprueba = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f };

                tbprueba.AddCell(new PdfPCell(new Phrase(" Firma " + user + "/a:", fontpreg)) { BorderColor = BaseColor.BLACK, BorderWidthBottom = 0, VerticalAlignment = Element.ALIGN_LEFT });
                tbprueba.AddCell(new PdfPCell(new Phrase("\n\n\n\n\n\n\n\n\n\n\n", fontresp)) { BorderColor = BaseColor.BLACK, BorderWidthBottom = 0, BorderWidthTop = 0, VerticalAlignment = Element.ALIGN_MIDDLE, Rowspan = 4 });
                tbprueba.AddCell(new PdfPCell(new Phrase("__________________________________", fontparrafo)) { BorderColor = BaseColor.BLACK, BorderWidthBottom = 0, BorderWidthTop = 0, HorizontalAlignment = Element.ALIGN_CENTER });
                tbprueba.AddCell(new PdfPCell(new Phrase(NombresUser, fontpreg)) { BorderColor = BaseColor.BLACK, BorderWidthBottom = 0, BorderWidthTop = 0, HorizontalAlignment = Element.ALIGN_CENTER });
                tbprueba.AddCell(new PdfPCell(new Phrase(user, fontpreg)) { BorderColor = BaseColor.BLACK, BorderWidthTop = 0, BorderWidthBottom = 0, HorizontalAlignment = Element.ALIGN_CENTER });
                tbprueba.AddCell(new PdfPCell(new Phrase("\n", fontresp)) { BorderColor = BaseColor.BLACK, BorderWidthTop = 0, HorizontalAlignment = Element.ALIGN_CENTER });

                document.Add(tbprueba);



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
                return RedirectToAction("VerProcesoEvaluador", new RouteValueDictionary(new { controller = "Proceso", action = "VerProcesoEvaluador", Idlibro = Idlibro, Idproceso = Idproceso, Ida = Ida }));


            }

        }






        //Finalizar evaluación de pares académicos
        //Función para crear documento de informe de evaluación de pares académicos que se envía a los autores
        public ActionResult InformeEvaluacionParAcademico(int IDlibro, int Idproceso, int Idcuestionario, int Ida)
        {
            if (Session["Type"] == null)
            {
                TempData["ERRORD"] = "No tiene acceso a esta sección del sistema. Inicia sesión";
                return RedirectToAction("Login", "Usuario");
            }

            int Iduser = Convert.ToInt32(Session["Id"]);
            string titulo = "";
            string descripcioncuestionario = "";
            int Idt = 0;
            bool general = false;
            int ed = 0;
            string emisor1 = Session["Nombres"].ToString(); ;
            string titulol = "";
            string tipouser = Session["Type"].ToString();
            string procesoa = "";
            int cont = 0;
            int Num = 0;
            string fecha = System.DateTime.Now.ToString("dd/MM/yyyy");
            string fecha1 = System.DateTime.Now.ToString("dd-MM-yyyy" + " " + "H-mm");
            int Idtipoproc = 0;
            string estado = "";
            int identificadorE = 0;
            int idpanterior = 0;
            bool est = false;
            int progreso = 0;
            string fechaproceso = System.DateTime.Now.ToString("dd/MM/yyyy" + " - " + "H:mm tt");
            int opresult = 0, r = 0,CE=0;


            try
            {

                foreach (var item in conexProceso.VerProcesoxIdp(IDlibro, Idproceso))
                {
                    titulol = item.Titulo;
                    procesoa = item.Detalletipospro;
                    Idtipoproc = Convert.ToInt32(item.Idtipoproceso);
                    idpanterior = Convert.ToInt32(item.Idprocanterior);
                    progreso = Convert.ToInt32(item.Progreso);
                    opresult = Convert.ToInt32(item.Opresultado);
                }

                foreach (var item in conexCuestionario.VerCuestionario(Idcuestionario, tipouser))
                {
                    titulo = item.Nombre;
                    descripcioncuestionario = item.Descripcion;
                    Idt = Convert.ToInt32(item.Idtipousuario);
                }

               


                foreach (var item in conexDocs.VerDocumentosenviados(Idproceso, Iduser))
                {
                    if (item.EstadoDocu == "Pendiente" && item.Detalledocu == "Publicable con correcciones" || item.Detalledocu == "Publicable" || item.Detalledocu == "No publicable")
                    {
                        cont++;
                    }
                }

                foreach (var item in conexEvaluador.VerDetalleAsignacionEvaluador(Idproceso, Iduser))
                {
                    Num = Convert.ToInt32(item.NumEvaluador);
                }




                if (cont == 1)
                {


                   

                    ed = conexDocs.EditarEstadoDocumentosEnviados(Iduser, Idproceso, "Enviado", "Revisado");



                    int c = conexCuestionario.FinalizarCuestionario(Iduser, Idcuestionario, fecha, Ida);



                    //FileStream fs = new FileStream("D:/Copia-Sistema/SistemaHumus/Solution1/WebApplication1/Files/Reportes/Informe-Evaluación-Par-Académico-"+IDlibro+".pdf", FileMode.Create);
               

                    string NombreDoc = IDlibro + "_" + fecha1 + "_" + "Informe-Evaluación-Par-Académico.pdf";

                    FileStream fs = new FileStream("D:/Copia-Sistema/SistemaHumus/Solution1/WebApplication1/Files/Documents/" + NombreDoc, FileMode.Create);



                    //MemoryStream ms = new MemoryStream();
                    Document document = new Document(iTextSharp.text.PageSize.A4, 85.0394f, 85.0394f, 170.079f, 85.0394f);
                    PdfWriter pw = PdfWriter.GetInstance(document, fs);

                    string pathImage = Server.MapPath("/ImagesWeb/LG.png");
                    string logoespam = Server.MapPath("/ImagesWeb/espamlogoe1.png");
                    string Image2escudo = Server.MapPath("/ImagesWeb/escudo2.png");
                    pw.PageEvent = new HeaderFooter1(Image2escudo, logoespam, pathImage, titulo);


                    document.Open();

                
            
                  

                    var fontparrafo = FontFactory.GetFont("Times New Roman", 12, 0, BaseColor.BLACK);
                    var fontresp = FontFactory.GetFont("Times New Roman", 12, 0, BaseColor.DARK_GRAY);
                    var fontpreg = FontFactory.GetFont(FontFactory.TIMES_BOLD, 12, 0, BaseColor.BLACK);


                    string descrip = descripcioncuestionario.Replace("<p>", "");
                    descrip = descrip.Replace("</p>", "\n");
                    descrip = descrip.Replace("<br>", "\n");



                   


                    document.Add(new Paragraph(descrip, fontparrafo) { Alignment = Element.ALIGN_JUSTIFIED });
                    document.Add(new Paragraph("\n"));



                    var tb1 = new PdfPTable(new float[] { 40f, 70f }) { WidthPercentage = 100f };
                    var colt1 = new PdfPCell(new Phrase("TÍTULO DEL LIBRO:", fontpreg)) { BorderWidthRight = 0, BorderWidthBottom = 0, BorderWidthTop = 0, BorderWidthLeft = 0 };
                    var colt2 = new PdfPCell(new Phrase(titulol, fontparrafo)) { BorderWidthLeft = 0, BorderWidthBottom = 0, BorderWidthRight = 0, BorderWidthTop = 0, HorizontalAlignment = Element.ALIGN_LEFT, PaddingLeft = -25 };

                    tb1.AddCell(colt1);
                    tb1.AddCell(colt2);


                    document.Add(tb1);



                    //preguntas
                    foreach (var item1 in conexPreg.ConsultarPreguntas(Idcuestionario))
                    {
                        document.Add(new Paragraph("\n"));
                        document.Add(new Paragraph(item1.LeyendaSuperior, fontparrafo) { Alignment = Element.ALIGN_JUSTIFIED });


                        document.Add(new Paragraph(item1.Orden + ". " + item1.Descripcion, fontparrafo) { Alignment = Element.ALIGN_JUSTIFIED });



                        if (item1.Idtipopregunta == 1)
                        {
                            foreach (var item2 in conexCuestionario.VerRespuestaPregAbierta(Iduser, Idcuestionario, Ida))
                            {
                                if (item1.IDpregunta == item2.Idpregunta)
                                {

                                    document.Add(new Paragraph(item2.DescripcionRespuestaAbierta, fontresp) { Alignment = Element.ALIGN_JUSTIFIED });
                                }
                            }
                        }



                        if (item1.Idtipopregunta == 2)
                        {
                            if (item1.TiposOpciones == "Opciones y respuestas")
                            {
                                foreach (var item3 in conexPreg.VerPreguntaSeleccion(Idcuestionario))
                                {
                                    if (item1.IDpregunta == item3.Idpregunta)
                                    {
                                        document.Add(new Paragraph("- " + item3.descripOpcpreg, fontparrafo) { Alignment = Element.ALIGN_LEFT });
                                        foreach (var item4 in conexCuestionario.VerRespuestaPregOpcionesTipo1(Iduser, Idcuestionario, Ida))
                                        {
                                            if (item3.IDopcionPreguntaSeleccion == item4.Idopcionpreg)
                                            {

                                                document.Add(new Paragraph("   " + item4.DescripcionOpcionRespuesta, fontresp) { Alignment = Element.ALIGN_JUSTIFIED });
                                            }
                                        }
                                    }
                                }
                            }

                            if (item1.TiposOpciones == "Solo respuestas")
                            {
                                foreach (var item5 in conexCuestionario.VerRespuestaPregOpcionesTipo2(Iduser, Idcuestionario, Ida))
                                {
                                    if (item1.IDpregunta == item5.Idpregunta)
                                    {
                                        document.Add(new Paragraph("   " + item5.DescripcionOpcionRespuesta, fontresp) { Alignment = Element.ALIGN_JUSTIFIED });
                                    }
                                }
                            }

                        }



                        if (item1.Idtipopregunta == 3)
                        {
                            if (item1.TiposOpciones == "Opciones y respuestas")
                            {
                                foreach (var item3 in conexPreg.VerPreguntaSeleccion(Idcuestionario))
                                {
                                    if (item1.IDpregunta == item3.Idpregunta)
                                    {
                                        document.Add(new Paragraph("- " + item3.descripOpcpreg, fontparrafo) { Alignment = Element.ALIGN_LEFT });

                                        foreach (var item4 in conexCuestionario.VerRespuestaPregOpcionesTipo1(Iduser, Idcuestionario, Ida))
                                        {
                                            if (item1.IDpregunta == item4.Idpregunta)
                                            {

                                                document.Add(new Paragraph("  " + item4.DescripcionOpcionRespuesta, fontresp) { Alignment = Element.ALIGN_JUSTIFIED });
                                            }
                                        }
                                    }
                                }
                            }

                            if (item1.TiposOpciones == "Solo respuestas")
                            {
                                foreach (var item5 in conexCuestionario.VerRespuestaPregOpcionesTipo2(Iduser, Idcuestionario, Ida))
                                {
                                    if (item1.IDpregunta == item5.Idpregunta)
                                    {
                                        document.Add(new Paragraph("- " + item5.DescripcionOpcionRespuesta, fontresp) { Alignment = Element.ALIGN_JUSTIFIED });
                                    }
                                }
                            }

                        }


                        if (item1.Idtipopregunta == 4)
                        {
                            document.Add(new Paragraph("\n"));

                            var tbp4 = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f };
                            var colp4 = new PdfPCell(new Phrase("OBSERVACIONES", fontpreg)) { BorderWidthBottom = 0, BorderColor = BaseColor.BLACK, Padding = 5f, HorizontalAlignment = Element.ALIGN_CENTER };

                            tbp4.AddCell(colp4);

                            document.Add(tbp4);

                            var fontresp1 = FontFactory.GetFont("Times New Roman", 11, 0, BaseColor.DARK_GRAY);



                            foreach (var item6 in conexPreg.VerPreguntaObservaciones(Idcuestionario))
                            {
                                if (item1.IDpregunta == item6.Idpregunta)
                                {
                                    //document.Add(new Paragraph("- " + item6.Detallepregobservacion, fontparrafo) { Alignment = Element.ALIGN_LEFT });
                                    if (item6.Respuestas == 0)
                                    {
                                        var tbp = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f };
                                        var colp = new PdfPCell(new Phrase(item6.Detallepregobservacion, fontpreg)) { BorderColor = BaseColor.BLACK, BorderWidthBottom = 0, Padding = 5f, HorizontalAlignment = Element.ALIGN_LEFT };

                                        tbp.AddCell(colp);

                                        document.Add(tbp);
                                    }

                                    foreach (var item7 in conexCuestionario.VerRespuestaPregTipo4(Iduser, Idcuestionario, Ida))
                                    {
                                        if (item6.IDpreguntaObservaciones == item7.Idobservacionpreg)
                                        {
                                            if (item6.Respuestas == 0)
                                            {


                                                var tbpr4 = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f };
                                                string nueva1 = item7.DescripcionRespuestaAbierta.Replace("<p>", "");
                                                nueva1 = nueva1.Replace("</p>", "\n");
                                                nueva1 = nueva1.Replace("<br>", "\n");

                                                var p = tbpr4.AddCell(new PdfPCell(new Phrase(nueva1, fontresp1)) { BorderColor = BaseColor.BLACK, BorderWidthBottom = 0, BorderWidthTop = 0, Padding = 8f, HorizontalAlignment = Element.ALIGN_JUSTIFIED });
                                                p.SetLeading(0, 1.5f);

                                                tbpr4.AddCell(new PdfPCell(new Phrase("\n", fontresp1)) { BorderColor = BaseColor.BLACK, BorderWidthTop = 0, BorderWidthBottom = 0, HorizontalAlignment = Element.ALIGN_CENTER });

                                                document.Add(tbpr4);

                                            }
                                            else
                                            {

                                                var tbpr4 = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f };

                                                var p = tbpr4.AddCell(new PdfPCell(new Phrase(item7.titulorespabierta, fontpreg)) { BorderColor = BaseColor.BLACK, PaddingBottom = 3f, PaddingTop = 1f, PaddingLeft = 5f, BorderWidthBottom = 0, HorizontalAlignment = Element.ALIGN_JUSTIFIED });
                                                p.SetLeading(0, 1.5f);


                                                //replace
                                                string nueva = item7.DescripcionRespuestaAbierta.Replace("<p>", "");
                                                nueva = nueva.Replace("</p>", "\n");
                                                nueva = nueva.Replace("<br>", "\n");


                                                var p1 = tbpr4.AddCell(new PdfPCell(new Phrase(nueva, fontresp1)) { BorderColor = BaseColor.BLACK, BorderWidthBottom = 0, BorderWidthTop = 0, Padding = 8f, HorizontalAlignment = Element.ALIGN_JUSTIFIED });
                                                p1.SetLeading(0, 1.5f);

                                                tbpr4.AddCell(new PdfPCell(new Phrase("\n", fontresp1)) { BorderColor = BaseColor.BLACK, BorderWidthTop = 0, BorderWidthBottom = 0, HorizontalAlignment = Element.ALIGN_CENTER });

                                                document.Add(tbpr4);



                                            }

                                        }
                                    }
                                }






                            }




                        }





                    }




                    var tbdatos4 = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f };
                    var t4 = new PdfPCell(new Phrase("", fontpreg)) { BorderWidthRight = 0, BorderWidthBottom = 0, BorderColor = BaseColor.BLACK, Padding = 5f };

                    tbdatos4.AddCell(t4);


                    document.Add(tbdatos4);



                    document.Close();



                 
                    //string estadoDoc = "Enviado en espera";
                    string estadoDoc = "Pendiente revisar";
                    int visibleautor = 1;
                    int visibleadmin = 0;
                    int visiblevaluador = 0;
                    string op = "Resultados de evaluación";
                    string estadoasignacion = "Evaluación finalizada";
                    string fecha2 = System.DateTime.Now.ToString("dd/MM/yyyy" + " - " + "H:mm tt");
                    string emisor = "Evaluador " + Num;


                


                    int d = conexDocs.InsertarDocumento(Idproceso, NombreDoc, op, visibleautor, emisor, fecha2, estadoDoc, visibleadmin, visiblevaluador, Iduser, 0,true);



                    //cambiar estado de documentos ya guardados para que sean visibles al admin cuando se finalice esta evaluación
              
                    ed = conexDocs.EditarEstadoDocumentosEnviados(Iduser, Idproceso, "Pendiente", "Pendiente revisar");

                    int eob = conexObser.EditarEstadoObservacionesEnviadas(Iduser, Idproceso, "Pendiente", "Enviado");

                    conexEvaluador.EditarEstadoAsignacionEvaluador(Idproceso, Iduser, estadoasignacion);



                    //crear notificación para admin
                    //enviar correo

                    string notificacion = "El evaluador " + emisor1 + " ha finalizado la evaluación correspondiente al proceso de " + procesoa + " del libro " + titulol + ".";
                    string detallenotificacion = "Evaluación finalizada";
                    string asunto = detallenotificacion + " - [Libro: " + titulol + "]";
                    string visiblenoti = "Administrador";
                    string url = "Proceso/VerProcesoAdmin?Idlibro=" + IDlibro + "&Idproceso=" + Idproceso + "";

                    foreach (var item in conexUser.BuscarUsuarioxTipoUsuario(visiblenoti))
                    {
                        int n = conexNotificacion.InsertarNotificacion(notificacion, url, visiblenoti, item.IDusuario, 7, fecha, detallenotificacion, general);
                        break;
                    }
                    //envio de correo a editorial
                    SendEmailDocumentos(notificacion, asunto, Idproceso, Iduser);


                    foreach (var item in conexEvaluador.VerEvaluadores(IDlibro))
                    {
                        if (item.Estado == "Evaluación finalizada")
                        {
                            CE++;
                        }
                    }

                    if (CE == 2)
                    {
                        foreach (var item in conexEstado.ListarEstadoxIdentificador(Idtipoproc, 9))
                        {
                            estado = item.DetalleEstados;
                            identificadorE = item.IdentificadorEstados;

                        }

                        r = conexProceso.EditarProceso1(Idproceso, fechaproceso, estado, idpanterior, opresult, identificadorE);
                        int el = conexLibro.EditarLibroP(IDlibro, procesoa, progreso, fechaproceso, estado, Idproceso, est);

                    }





                    TempData["OK"] = "Evaluación finalizada exitósamente";

                    return RedirectToAction("VerProcesoEvaluador", new RouteValueDictionary(new { controller = "Proceso", action = "VerProcesoEvaluador", Idlibro = IDlibro, Idproceso = Idproceso, Ida = Ida }));


                }

                else
                {
                    TempData["ERROR2"] = "No se ha agregado el informe de resultados de evaluación correspondiente. Por favor agregue el documento requerido y vuelva a intentar finalizar la evaluación.";
                    return RedirectToAction("VerProcesoEvaluador", new RouteValueDictionary(new { controller = "Proceso", action = "VerProcesoEvaluador", Idlibro = IDlibro, Idproceso = Idproceso, Ida = Ida }));

                }

            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";
                return RedirectToAction("VerProcesoEvaluador", new RouteValueDictionary(new { controller = "Proceso", action = "VerProcesoEvaluador", Idlibro = IDlibro, Idproceso = Idproceso, Ida = Ida }));

            }



        }









        //Función para envío de correo de notificación a Editorial sin archivos adjuntos 
        public void SendEmailNotificacionEditorial(string notificacion, string detallenotificacion)
        {



            string email = "edicionesHU@gmail.com";

            string emaildestino = email;
            try
            {
                
                string date = DateTime.Now.ToString("MM-dd-yyyy");
                string year = date.Substring(6, 4);

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








        //Vista de último cuestionario registrado
        public ActionResult VerCuestionario(int Idcuestionario)
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

            int Iduser = Convert.ToInt32(Session["Id"]);
            string tipouser = Session["Type"].ToString();

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


            ViewBag.PreguntaAbierta = conexPreg.VerPreguntaAbierta(Idcuestionario);
            ViewBag.OpcionesRespuestas = conexPreg.VerOpcionRespuestaSeleccion(Idcuestionario);
            ViewBag.Idcuestionario = Idcuestionario;
            ViewBag.PreguntaObservaciones = conexPreg.VerPreguntaObservaciones(Idcuestionario);
            ViewBag.PreguntaOpciones = conexPreg.VerPreguntaSeleccion(Idcuestionario);
            //ViewBag.Cuestionario = conexCuestionario.VerCuestionario(Idcuestionario,tipouser);
            ViewBag.Cuestionario = conexCuestionario.VerCuestionarioRegistrado(Idcuestionario);
            ViewBag.Preguntas = conexPreg.ConsultarPreguntas(Idcuestionario);
            ViewBag.Usuario = conexUser.BuscarUsuario(Session["Username"].ToString());
            return View();
        }







      




        //Vista de formulario de cuestionario a responder de evaluación de pares académicos
        public ActionResult EvaluacionLibro(int Idc, int Ida, int Idp)
        {

            if (Session["Type"] == null)
            {
                TempData["ERRORD"] = "No tiene acceso a esta sección del sistema. Inicia sesión";
                return RedirectToAction("Login", "Usuario");
            }

            int lib = 0;
            string tipouser = Session["Type"].ToString();
            int Iduser = Convert.ToInt32(Session["Id"]);
            ViewBag.DetalleAsigEvaluador = conexEvaluador.VerDetalleAsignacionEvaluador(Idp, Iduser);
            foreach (var item1 in ViewBag.DetalleAsigEvaluador)
            {
                ViewBag.Est = item1.Estado;
                if (Ida != item1.IdasignacionEvaluacion)
                {
                    TempData["ERRORD"] = "Algo salió mal";
                    return RedirectToAction("Login", "Usuario");
                }
                else
                {
                    lib = Convert.ToInt32(item1.Idlibro);
                }

            }

            if (ViewBag.DetalleAsigEvaluador.Count == 0)
            {
                TempData["ERRORD"] = "Algo salió mal";
                return RedirectToAction("Login", "Usuario");
            }

            ViewBag.DetalleAsignacion = conexCuestionario.VerDetalleAsignacion(Ida);


            foreach(var item in ViewBag.DetalleAsignacion)
            {
                if (item.Idpersona != Iduser)
                {
                    TempData["ERRORD"] = "Algo salió mal";
                    return RedirectToAction("Login", "Usuario");
                }
            }

         

            ViewBag.PreguntaAbierta = conexPreg.VerPreguntaAbierta(Idc);
            ViewBag.OpcionesRespuestas = conexPreg.VerOpcionRespuestaSeleccion(Idc);
            ViewBag.Idcuestionario = Idc;
          
            ViewBag.PreguntaOpciones = conexPreg.VerPreguntaSeleccion(Idc);
            ViewBag.Cuestionario = conexCuestionario.VerCuestionario(Idc, tipouser);
            //ViewBag.Cuestionario = conexCuestionario.VerCuestionarioRegistrado(Idc);
            ViewBag.Preguntas = conexPreg.ConsultarPreguntas(Idc);
            ViewBag.Idasignacion = Ida;
            ViewBag.Idlibro = lib;
            ViewBag.Idp = Idp;






            ViewBag.Usuario = conexUser.BuscarUsuario(Session["Username"].ToString());
            return View();
        }







        //Vista para gestionar cuestionarios registrados
        public ActionResult GestionarCuestionario()
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

           
           
            ViewBag.Cuestionarios = conexCuestionario.ListarCuestionarios();
            ViewBag.Usuario = conexUser.BuscarUsuario(Session["Username"].ToString());
            return View();
        }






       
        //Vista para gestionar preguntas registradas
        public ActionResult GestionarPreguntas(int Idcuestionario, int Idpreg)
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
            int idpregA = 0;
            foreach(var item in conexPreg.VerPreguntaInsertada(Idpreg))
            {
                if (item.Orden == 1)
                {
                    idpregA = item.IDpregunta;
                }
                else
                {
                    idpregA = Convert.ToInt32(item.IdpregAnterior);
                }
            }
            ViewBag.IdpregA = idpregA;
            ViewBag.Idpreg = Idpreg;
            ViewBag.Tipodato = conexPreg.ListarTipodeDatoPreguntaAbierta();
            ViewBag.PreguntaAbierta = conexPreg.VerPreguntaAbiertaInsertada(Idpreg);
            ViewBag.OpcionesRespuestas = conexPreg.VerOpcionesRespuestaSeleccion(Idpreg);
            ViewBag.PreguntaObservaciones = conexPreg.VerPreguntaObservacionesxIdpreg(Idpreg);
            ViewBag.PreguntaOpciones = conexPreg.VerOpcionesdePreguntaSeleccion(Idpreg);
            ViewBag.Pregunta = conexPreg.VerPreguntaInsertada(Idpreg);
            ViewBag.TiposPreguntas = conexPreg.ListarTipodePregunta();
            foreach(var item in conexCuestionario.VerCuestionarioRegistrado(Idcuestionario))
            {
                ViewBag.num = item.NumeroCuestionario;
                ViewBag.finalizado = item.Estado;
            }
            ViewBag.Idc = Idcuestionario;
            ViewBag.Cuestionarios = conexCuestionario.ListarCuestionarios();
            ViewBag.Usuario = conexUser.BuscarUsuario(Session["Username"].ToString());
            return View();
        }









        //Vista de proceso de libro evaluador
        public ActionResult ObservacionesEvaluacion(int Idproceso, int Ida)
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

            int Idcuestionario = 0;
            string tipouser = Session["Type"].ToString();
            int IDlibro = 0;
            int Id = Convert.ToInt32(Session["Id"]);

       
            ViewBag.DetalleAsigEvaluador = conexEvaluador.VerDetalleAsignacionEvaluador(Idproceso, Id);
            foreach (var item1 in ViewBag.DetalleAsigEvaluador)
            {
                ViewBag.Estado = item1.Estado;
                if (Ida != item1.IdasignacionEvaluacion)
                {
                    TempData["ERRORD"] = "Algo salió mal";
                    return RedirectToAction("Login", "Usuario");
                }
                else
                {
                    IDlibro = Convert.ToInt32(item1.Idlibro);
                }

            }

            if (ViewBag.DetalleAsigEvaluador.Count == 0)
            {
                TempData["ERRORD"] = "Algo salió mal";
                return RedirectToAction("Login", "Usuario");
            }





            ViewBag.DetalleAsignacion = conexCuestionario.VerDetalleAsignacion(Ida);
            foreach (var item2 in ViewBag.DetalleAsignacion)
            {
                Idcuestionario = item2.Idcuestionario;
                if (Id != item2.Idpersona)
                {
                    TempData["ERRORD"] = "Algo salió mal";
                    return RedirectToAction("Login", "Usuario");
                }
            }



            int ntotal = 0;
            int nnuevas = 0;

        
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
            ViewBag.Idlibro = IDlibro;
            ViewBag.Idproceso = Idproceso;
          
           

            ViewBag.Idcuestionario = Idcuestionario;



            ViewBag.PreguntasTipo4 = conexCuestionario.VerRespuestaPregTipo4(Id,Idcuestionario,Ida);

            

            ViewBag.Cuestionario = conexCuestionario.VerCuestionario(Idcuestionario, tipouser);
            ViewBag.Preguntas = conexPreg.ConsultarPreguntas(Idcuestionario);
            ViewBag.PreguntaObservaciones = conexPreg.VerPreguntaObservaciones(Idcuestionario);
            
            

            ViewBag.Usuario = conexUser.BuscarUsuario(Session["Username"].ToString());


            return View();

        }











        
        //Función para registrar pregunta
        [HttpPost]
        public ActionResult RegistroPregunta(int idpregA,int Idc, int Idtipopreg, bool obligatorio, int orden, string descripcion, string leyendasup,string especificar)
        {
            int IdP = 0;
            //int idpregA = 0;

           

            try
            {
                if (orden <= 0)
                {
                    TempData["ERROR2"] = "No se aceptan valores negativos, vuelva a intentarlo e ingrese el valor correcto.";
                    return RedirectToAction("GestionarPreguntas", new RouteValueDictionary(new { controller = "Cuestionario", action = "GestionarPreguntas", Idcuestionario = Idc, Idpreg = IdP }));

                }
                IdP = conexPreg.InsertarPregunta(Idc, Idtipopreg, descripcion, obligatorio, orden, leyendasup,especificar,idpregA);
                int r = conexCuestionario.EditarCuestionarioIdpreg(IdP,Idc);
                if (IdP > 0)
                {
                    TempData["OK"] = "Pregunta registrada correctamente";
                }
                else if (IdP == -1)
                {
                    TempData["ERROR2"] = "El número de orden de esta pregunta ya existe, intente con otro número.";
                }
                else
                {
                    TempData["ERROR"] = "Algo salió mal";
                }
               
               
                //return RedirectToAction("Index");
            }
            catch(Exception)
            {
                TempData["ERROR"] = "Algo salió mal";
                //return View();
            }

            return RedirectToAction("GestionarPreguntas", new RouteValueDictionary(new { controller = "Cuestionario", action = "GestionarPreguntas", Idcuestionario = Idc, Idpreg = IdP }));
          


        }






        //Función para editar pregunta
        [HttpPost]
        public ActionResult EditarPregunta(int Idc, int Idpreg,string leyendasup, string descripcion, bool obligatorio, int idtipopreg, string especificar)
        {
            int r = 0;
            try
            {
              
                r = conexPreg.EditarPregunta(Idpreg,Idc, idtipopreg, descripcion, obligatorio, leyendasup,especificar);

                if (r == 1)
                {
                    TempData["OK"] = "Pregunta editada correctamente";
                }
                else
                {
                    TempData["ERROR"] = "Algo salió mal";
                }

               

                //return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";
                //return View();
            }

            return RedirectToAction("GestionarPreguntas", new RouteValueDictionary(new { controller = "Cuestionario", action = "GestionarPreguntas", Idcuestionario = Idc, Idpreg = Idpreg }));



        }






        //Función para registrar pregunta de tipo abierta
        [HttpPost]
        public ActionResult RegistroPreguntaAbierta(int Idc, int idtipodato, int Idpreg1, bool especificar, string valormax, string valormin)
        {


            int IdPA = 0;
            try
            {
                IdPA = conexPreg.InsertarPreguntaAbierta(idtipodato, Idpreg1, especificar, valormax, valormin);
                if (IdPA ==1)
                {
                    TempData["OK"] = "Pregunta registrada correctamente";
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

            return RedirectToAction("GestionarPreguntas", new RouteValueDictionary(new { controller = "Cuestionario", action = "GestionarPreguntas", Idcuestionario = Idc, Idpreg = Idpreg1 }));
        }






        //Función para registrar pregunta de tipo4 abierta con varias respuestas
        [HttpPost]
        public ActionResult RegistroPreguntaTipo4(int Idc, int Idpreg, int ordenobser, string detalleobser, string leyenda, bool respuesta)
        {


            int r = 0;
            try
            {
                if (ordenobser <= 0)
                {
                    TempData["ERROR2"] = "No se aceptan valores negativos, vuelva a intentarlo.";
                    return RedirectToAction("GestionarPreguntas", new RouteValueDictionary(new { controller = "Cuestionario", action = "GestionarPreguntas", Idcuestionario = Idc, Idpreg = Idpreg }));
                }

            
                r = conexPreg.InsertarPreguntaObservaciones(Idpreg, detalleobser, ordenobser, leyenda,respuesta);
                if (r ==1)
                {
                    TempData["OK"] = "Datos de pregunta registrados correctamente";
                }
                else if (r == 2)
                {
                    TempData["ERROR2"] = "El número de orden ya existe, intente con un número diferente.";
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

            return RedirectToAction("GestionarPreguntas", new RouteValueDictionary(new { controller = "Cuestionario", action = "GestionarPreguntas", Idcuestionario = Idc, Idpreg = Idpreg }));
        }






        //Función para editar pregunta de tipo4 abierta con varias respuestas
        [HttpPost]
        public ActionResult EditPreguntaTipo4(int Idc,int Idpreg,int Idpregob, string detalleob, string leyendaob,bool respuesta)
        {


            int r = 0;
            try
            {
                r = conexPreg.EditarPreguntaObservaciones(Idpregob, detalleob, leyendaob,respuesta);
                if (r == 2)
                {
                    TempData["OK"] = "Datos de pregunta editados correctamente";
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

            return RedirectToAction("GestionarPreguntas", new RouteValueDictionary(new { controller = "Cuestionario", action = "GestionarPreguntas", Idcuestionario = Idc, Idpreg = Idpreg }));
        }








        //Función para editar pregunta de tipo abierta
        [HttpPost]
        public ActionResult EditarPreguntaAbierta(int Idc,int Idpregt1, int IdpregAbierta, int idtipodato, bool especificar, string valormax2, string valormin2)
        {


            int IdPA;
            try
            {
                IdPA = conexPreg.EditarPreguntaAbierta(idtipodato, IdpregAbierta, especificar, valormax2, valormin2);
                if (IdPA == 1)
                {
                    TempData["OK"] = "Pregunta abierta editada correctamente";
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

            return RedirectToAction("GestionarPreguntas", new RouteValueDictionary(new { controller = "Cuestionario", action = "GestionarPreguntas", Idcuestionario = Idc, Idpreg = Idpregt1 }));
        }








        //Función para registrar opciones de pregunta de selección
        [HttpPost]
        public ActionResult RegistroOpcionesPreguntaSeleccion(int Idc, int Idpreg, int identificadort2, string descripciont2)
        {

            int IdPS = 0;
            try
            {
                if (identificadort2 <= 0)
                {
                    TempData["ERROR2"] = "No se aceptan valores negativos, vuelva a intentarlo e ingrese el valor correcto.";
                    return RedirectToAction("GestionarPreguntas", new RouteValueDictionary(new { controller = "Cuestionario", action = "GestionarPreguntas", Idcuestionario = Idc, Idpreg = Idpreg }));
                }
                IdPS = conexPreg.InsertarPreguntaSeleccion(Idpreg, descripciont2,identificadort2);
                if (IdPS == 1)
                {
                    TempData["OK"] = "Opción de pregunta registrada correctamente";
                }else if (IdPS == 2)
                {
                    TempData["ERROR2"] = "El número de identificador ya existe, intente con un número diferente.";
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
           
           
            return RedirectToAction("GestionarPreguntas", new RouteValueDictionary(new { controller = "Cuestionario", action = "GestionarPreguntas", Idcuestionario = Idc, Idpreg = Idpreg }));
        }






        //Función para editar opciones de pregunta de selección
        [HttpPost]
        public ActionResult EditarOpcionesPreguntaSeleccion(int Idc, int Idpreg, int Idopcionpreg, string descripciont2)
        {

            int IdPS;
            try
            {
                

                IdPS = conexPreg.EditarOpcionPreguntaSeleccion(Idopcionpreg, descripciont2);
                if (IdPS == 1)
                {
                    TempData["OK"] = "Opción editada correctamente";
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


            return RedirectToAction("GestionarPreguntas", new RouteValueDictionary(new { controller = "Cuestionario", action = "GestionarPreguntas", Idcuestionario = Idc, Idpreg = Idpreg }));
        }







        //Función para registrar opciones de respuesta de pregunta de selección
        [HttpPost]
        public ActionResult RegistroOpcionesRespuestaPreguntaSeleccion(int Idc, int Idpreg, int orden, string descripcion)
        {
          
            int IdResp = 0;
            try
            {
                if (orden <= 0)
                {
                    TempData["ERROR2"] = "No se aceptan valores negativos, vuelva a intentarlo e ingrese el valor correcto.";
                    return RedirectToAction("GestionarPreguntas", new RouteValueDictionary(new { controller = "Cuestionario", action = "GestionarPreguntas", Idcuestionario = Idc, Idpreg = Idpreg }));
                }
            
                IdResp = conexPreg.InsertarOpcionesRespuestaPreguntaSeleccion(descripcion, orden, Idpreg);
                if (IdResp ==1)
                {
                    TempData["OK"] = "Opción de respuesta registrada correctamente";
                }
                else if (IdResp == 2)
                {
                    TempData["ERROR2"] = "El número de orden ya existe, intente con un número diferente.";
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
          
            return RedirectToAction("GestionarPreguntas", new RouteValueDictionary(new { controller = "Cuestionario", action = "GestionarPreguntas", Idcuestionario = Idc, Idpreg = Idpreg }));
        }







        //Función para editar opciones de respuesta de pregunta de selección
        [HttpPost]
        public ActionResult EditarOpcionesRespuestaPreguntaSeleccion(int Idc, int Idpreg, int Idrespopcion, string descripciont3)
        {

            int IdResp;
            try
            {
               

                IdResp = conexPreg.EditarOpcionRespuestaPreguntaSeleccion(Idrespopcion, descripciont3);
                if (IdResp == 1)
                {
                    TempData["OK"] = "Opción de respuesta editada correctamente";
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

            return RedirectToAction("GestionarPreguntas", new RouteValueDictionary(new { controller = "Cuestionario", action = "GestionarPreguntas", Idcuestionario = Idc, Idpreg = Idpreg }));
        }






        //Función para registrar cuestionarios
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult RegistroCuestionario(string nombre, string descripcion, DateTime fecha)
        {
            int numc = 1;
            int IdC ;
            string asignado = "Evaluador";
            string tipo = "Evaluación";
            string fechac = fecha.ToString("dd/MM/yyyy");
            try
            {

                if (numc <= 0)
                {
                    TempData["ERROR2"] = "No se aceptan valores negativos, vuelva a intentarlo e ingrese el valor correcto.";
                    return RedirectToAction("GestionarCuestionarios");
                }

                IdC = conexCuestionario.RegistrarCuestionario(nombre, descripcion, fechac,asignado,tipo,numc);
                if (IdC == 1)
                {
                    TempData["OK"] = "Cuestionario registrado correctamente";
                }
                else if (IdC == 2)
                {
                    TempData["ERROR2"] = "El número de cuestionario ya existe, intente con un número diferente para registrar este cuestionario.";
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
           
            return RedirectToAction("GestionarCuestionarios");
        }






        //Función para editar cuestionarios
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditCuestionario(int Idcuestionario, string nombre, string descripcion1, DateTime fecha)
        {
            string asignado = "Evaluador";
            string tipo = "Evaluación";
            string fechac = fecha.ToString("dd/MM/yyyy");
            int IdC;
            try
            {
                

                IdC = conexCuestionario.EditarCuestionario(Idcuestionario, nombre, descripcion1, fechac,asignado,tipo);
                if (IdC == 1)
                {
                    TempData["OK"] = "Cuestionario editado correctamente";
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

            return RedirectToAction("GestionarCuestionarios");
        }






        //Función para registrar y editar respuestas a cuestionario-evaluación de pares académicos
        [HttpPost]
        public ActionResult RegistrarRespuestasCuestionarioEA(int idc,int ida, int idresp,int idob, int idpregunta, string respabierta,string titulorespabierta, int identificadorTipoPregunta, int idopcionpreg, int idresplogica, string tipoOpciones, bool estado,int respuestas)
        {

            string fecha = System.DateTime.Now.ToString("dd/MM/yyyy");
            int user = Convert.ToInt32(Session["Id"]);
            int r = 0;
            try
            {

                r = conexCuestionario.InsertarEditarRespuestasPreguntasEA(idc,ida,idresp,idob,idpregunta,user,idresplogica,respabierta,titulorespabierta,idopcionpreg,identificadorTipoPregunta,tipoOpciones,estado,respuestas);
                if (r != 3)
                {
                    int a = conexCuestionario.EditarFechaAsigancion(user, idc, fecha, ida);
                }
                
               
            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";
            }

            return Json(r, JsonRequestBehavior.AllowGet);

            //return RedirectToAction("VerCuestionario");
        }









        //Función para registrar y editar respuestas de tipo observaciones a cuestionario-evaluación de pares académicos
        [HttpPost]
         [ValidateInput(false)]
        public ActionResult RegistrarRespuestaObservaciones(int idc, int ida, int idresp,int idob, int idpregunta, string respabierta, string titulorespabierta, int identificadorTipoPregunta, int idopcionpreg, int idresplogica, string tipoOpciones, bool estado,int respuestas)
        {

            string fecha = System.DateTime.Now.ToString("dd/MM/yyyy");
            int user = Convert.ToInt32(Session["Id"]);
            int r = 0;
            try
            {

                r = conexCuestionario.InsertarEditarRespuestasPreguntasEA(idc, ida, idresp,idob, idpregunta, user, idresplogica, respabierta, titulorespabierta, idopcionpreg, identificadorTipoPregunta, tipoOpciones, estado,respuestas);
                if (r != 3)
                {
                    int a = conexCuestionario.EditarFechaAsigancion(user, idc, fecha, ida);
                    if (r == 1)
                    {
                        TempData["OK"] = "Respuesta registrada"; 
                    }

                    if (r == 2)
                    {
                        TempData["OKE"] = "Respuesta editada";
                    }
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

            //return RedirectToAction("VerCuestionario");
        }








        //Función para eliminar cuestionarios
        [HttpPost]
        public JsonResult EliminarCuestionario(int Idc)
        {
            int r = 0;
            try
            {
                r = conexCuestionario.EliminarCuestionario(Idc);
                if (r == 1)
                {
                    TempData["OKEC"] = "OK";
                }
            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";

            }

            return Json(r, JsonRequestBehavior.AllowGet);
        }





        //Función para eliminar respuesta de cuestionario
        [HttpPost]
        public JsonResult EliminarRespuestaObservación(int Id)
        {
            int r = 0;
            try
            {
                r = conexCuestionario.EliminarRespuestaObservacion(Id);
                if (r == 1)
                {
                    TempData["OKEO"] = "OK";
                }
            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";

            }

            return Json(r, JsonRequestBehavior.AllowGet);
        }







        //Función para eliminar asignación de cuestionarios
        [HttpPost]
        public JsonResult EliminarAsignacionCuestionario(int Ida)
        {
            int r = 0;
            try
            {
                r = conexCuestionario.EliminarAsignacionCuestionarioUsuario(Ida);
                if (r == 1)
                {
                    TempData["OKAE"] = "OK";
                }
            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";

            }

            return Json(r, JsonRequestBehavior.AllowGet);
        }





        //Función para eliminar asignación de cuestionarios
        [HttpPost]
        public JsonResult EliminarPreguntaTipo4(int Idpregob)
        {
            int r = 0;
            try
            {
                r = conexPreg.EliminarPreguntaObservaciones(Idpregob);
                if (r == 2)
                {
                    TempData["OKEt4"] = "OK";
                }
            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";

            }

            return Json(r, JsonRequestBehavior.AllowGet);
        }





        //Función para eliminar pregunta abierta
        [HttpPost]
        public JsonResult EliminarPreguntaAbierta(int IdpregA)
        {
            int r = 0;
            try
            {
                r = conexPreg.EliminarPreguntaAbierta(IdpregA);
                if (r == 1)
                {
                    TempData["OKEt1"] = "OK";
                }
            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";

            }

            return Json(r, JsonRequestBehavior.AllowGet);
        }





        //Función para eliminar pregunta 
        [HttpPost]
        public JsonResult EliminarPregunta(int Idpreg)
        {
            int r = 0;
            try
            {
                r = conexPreg.EliminarPregunta(Idpreg);
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





        //Función para eliminar opción de pregunta 
        [HttpPost]
        public JsonResult EliminarOpcionPregunta(int Idopcionpreg)
        {
            int r = 0;
            try
            {
                r = conexPreg.EliminarOpcionPreguntaSeleccion(Idopcionpreg);
                if (r == 1)
                {
                    TempData["OKEt2"] = "OK";
                }
            }
            catch (Exception)
            {
                TempData["ERROR"] = "Algo salió mal";

            }

            return Json(r, JsonRequestBehavior.AllowGet);
        }





        //Función para eliminar opción de respuesta de pregunta 
        [HttpPost]
        public JsonResult EliminarOpcionRespuestaPregunta(int Idopcionresp)
        {
            int r = 0;
            try
            {
                r = conexPreg.EliminarOpcionRespuestaPreguntaSeleccion(Idopcionresp);
                if (r == 1)
                {
                    TempData["OKEt3"] = "OK";
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



            string emaildestino = "";
            string email = "edicionesHU@gmail.com";
            try
            {
                foreach (var item in conexUser.VerDetalleUsuario(Iduser))
                {

                    emaildestino = item.Email;

                }
                string date = DateTime.Now.ToString("MM-dd-yyyy");
                string year = date.Substring(6, 4);

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









        //Función para envío de correo de notificación con archivos adjuntos dirigido a la Editorial
        public void SendEmailDocumentos(string notificacion, string detallenotificacion, int Idproceso, int idemisor)
        {


            string path = "";

            string email = "edicionesHU@gmail.com";
            string emaildestino = email;
            try
            {

                string date = DateTime.Now.ToString("MM-dd-yyyy");
                string year = date.Substring(6, 4);

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("edicionesHU@gmail.com");



                foreach (var item1 in conexDocs.VerDocumentosenviados(Idproceso, idemisor))
                {
                    if (item1.EstadoDocu == "Pendiente revisar" && item1.Visibleadmin==true)
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










    }








    //Clase para diseño de encabezado y pie de página de documentos generados por itext-sharp
    class HeaderFooter : PdfPageEventHelper
    {
        string PathImage = null;
        string PathImageMW = null;
        public HeaderFooter(string logoPath, string marcaImagen)
        {
            PathImage = logoPath;
            PathImageMW = marcaImagen;
        }
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            // para imagen como marca de agua 

            PdfContentByte cb = writer.DirectContentUnder;
            Image image = Image.GetInstance(PathImageMW);

            float positionY = (writer.PageSize.Top / 2) - (image.Height / 2);
            float positionX = (writer.PageSize.Right / 2) - (image.Width / 2);


            image.SetAbsolutePosition(positionX, positionY);
            PdfGState state = new PdfGState();
            state.FillOpacity = 0.3f;
            cb.SetGState(state);
            cb.AddImage(image);



            //base.OnEndPage(writer, document);
            PdfPTable tbHeader = new PdfPTable(3);
            tbHeader.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
            tbHeader.DefaultCell.Border = 0;

            tbHeader.AddCell(new Paragraph());
            PdfPCell _cell = new PdfPCell(new Paragraph("Lista de usuarios registrados"));
            _cell.HorizontalAlignment = Element.ALIGN_CENTER;
            _cell.Border = 0;

            tbHeader.AddCell(_cell);

            tbHeader.AddCell(new Paragraph());

            tbHeader.WriteSelectedRows(0,-10,writer.PageSize.GetLeft(document.LeftMargin)-50,writer.PageSize.GetTop(document.TopMargin) + 40,writer.DirectContent);

            PdfPTable tbFooter = new PdfPTable(3);
            tbFooter.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
            tbFooter.DefaultCell.Border = 0;

            tbFooter.AddCell(new Paragraph());

            BaseFont bf2 = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.EMBEDDED);
            Font fontText2 = new Font(bf2, 10, 0, BaseColor.GRAY);

            _cell = new PdfPCell(new Paragraph("Editorial Humus -  ESPAM MFL",fontText2));
            _cell.HorizontalAlignment = Element.ALIGN_CENTER;
            _cell.Border = 0;
            tbFooter.AddCell(_cell);

            _cell = new PdfPCell(new Paragraph("Página "+writer.PageNumber,fontText2));
            _cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            
            _cell.Border = 0;
            tbFooter.AddCell(_cell);

            tbFooter.WriteSelectedRows(0, -1, document.LeftMargin, writer.PageSize.GetBottom(document.BottomMargin) - 5, writer.DirectContent);

            //Begin Image

            Image logo = Image.GetInstance(PathImage);
            logo.ScaleAbsoluteWidth(200);
            logo.ScaleAbsoluteHeight(150);

            logo.SetAbsolutePosition(writer.PageSize.GetLeft(document.LeftMargin)-80, writer.PageSize.GetTop(document.TopMargin)-50);

            document.Add(logo);

            //End Image


        }
    }




    //Clase para marca de agua en pdf generado por itext-sharp - (no se usa)
    class PdfWriterEvents : IPdfPageEvent
    {
        public void OnChapter(PdfWriter writer, Document document, float paragraphPosition, Paragraph title)
        {
           
        }

        public void OnChapterEnd(PdfWriter writer, Document document, float paragraphPosition)
        {
            //throw new NotImplementedException();
        }

        public void OnCloseDocument(PdfWriter writer, Document document)
        {
            
        }

        public void OnEndPage(PdfWriter writer, Document document)
        {
            string marcaAguaText = "iTextSharp 5.5.13";
            float positionX = writer.PageSize.Right / 2;
            float positionY = writer.PageSize.Top / 2;
            float fontSize = 80f;
            //float positionX = 100;
            //float positionY = 100;
            //float fontSize = 15f;
            float rotation = 45f;

            PdfContentByte cb = writer.DirectContentUnder;
            BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA,BaseFont.CP1250,BaseFont.EMBEDDED);
            cb.BeginText();
            cb.SetColorFill(BaseColor.LIGHT_GRAY);
            cb.SetFontAndSize(bf, fontSize);

            //int x, y;
            //for (x = 0;x<5;x++)
            //{
            //    for (y = 0; y < 7; y++)
            //    {
            //        cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, marcaAguaText, positionX, positionY, rotation);
            //        positionY = positionY + 100;

            //    }

            //    positionX = positionX + 100;
            //    positionY = 100; //reiniciamos el valor inicial


            //}


            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, marcaAguaText, positionX, positionY, rotation);
            cb.EndText();
        }

        public void OnGenericTag(PdfWriter writer, Document document, Rectangle rect, string text)
        {
           
        }

        public void OnOpenDocument(PdfWriter writer, Document document)
        {
            
        }

        public void OnParagraph(PdfWriter writer, Document document, float paragraphPosition)
        {
            
        }

        public void OnParagraphEnd(PdfWriter writer, Document document, float paragraphPosition)
        {
            
        }

        public void OnSection(PdfWriter writer, Document document, float paragraphPosition, int depth, Paragraph title)
        {
            
        }

        public void OnSectionEnd(PdfWriter writer, Document document, float paragraphPosition)
        {
            
        }

        public void OnStartPage(PdfWriter writer, Document document)
        {
           
        }
    }






}
