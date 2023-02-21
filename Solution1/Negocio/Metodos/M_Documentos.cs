using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Negocio.Entidades;


namespace Negocio.Metodos
{
    public class M_Documentos
    {
        DBHumusEntities DB = new DBHumusEntities();




        //Función para ver documentos enviados de proceso por Idproceso
        public List<E_Documentos> VerDocumentosenviados(int Idproces,int idemisor)
        {
            List<E_Documentos> listadocs = new List<E_Documentos>();

            foreach (var item in DB.VerDocumentosEnviados(Idproces,idemisor))
            {
                listadocs.Add(new E_Documentos()
                {
                    IDdocumento=item.IDdocumento,
                    Documento=item.Documento,
                    Detalledocu=item.Detalledocu,
                    Idproceso=item.Idproceso,
                    Visibleautor=item.Visibleautor,
                   EstadoDocu=item.EstadoDoc,
                    InicioFecha= item.InicioFecha,
                    Estado=item.Estado,
                    Emisor=item.Emisor,
                    Fecha=item.Fecha,
                    ActualizadoFecha=item.ActualizadoFecha,
                    Idtipoproceso=item.Idtipoproceso,
                    Detalletipospro=item.Detalletipospro,
                    Progreso=item.Progreso,
                    Idlibro=item.Idlibro,
                    Titulo=item.Titulo,
                    Visibleadmin=item.Visibleadmin,
                    Visiblevaluador=item.Visiblevaluador,
                    Idemisor=item.Idemisor,
                    Idrequerimiento=item.Idrequerimiento,
                    DocObligatorio=item.DocObligatorio,


                });
            }

            return listadocs;
        }






        //Función para ver documentos recibidos de proceso por Idproceso
        public List<E_Documentos> VerDocumentosrecibidos(int Idproces, int idemisor)
        {
            List<E_Documentos> listadocs = new List<E_Documentos>();

            foreach (var item in DB.VerDocumentosRecibidos(Idproces, idemisor))
            {
                listadocs.Add(new E_Documentos()
                {
                    IDdocumento = item.IDdocumento,
                    Documento = item.Documento,
                    Detalledocu = item.Detalledocu,
                    Idproceso = item.Idproceso,
                    Visibleautor = item.Visibleautor,
                    EstadoDocu = item.EstadoDoc,
                    InicioFecha = item.InicioFecha,
                    Estado = item.Estado,
                    Emisor = item.Emisor,
                    Fecha = item.Fecha,
                    ActualizadoFecha = item.ActualizadoFecha,
                    Idtipoproceso = item.Idtipoproceso,
                    Detalletipospro = item.Detalletipospro,
                    Progreso = item.Progreso,
                    Idlibro = item.Idlibro,
                    Titulo = item.Titulo,
                    Visibleadmin = item.Visibleadmin,
                    Visiblevaluador = item.Visiblevaluador,
                    Idemisor = item.Idemisor,
                    Idrequerimiento = item.Idrequerimiento,
                    DocObligatorio=item.DocObligatorio,


                });
            }

            return listadocs;
        }






      
        //Función para ver documentos recibidos visibles solo para autor
        public List<E_Documentos> VerDocumentosAutor(int Idproces,int idemisor)
        {
            List<E_Documentos> listadocs = new List<E_Documentos>();

            foreach (var item in DB.VerDocumentosAutor(Idproces,idemisor))
            {
                listadocs.Add(new E_Documentos()
                {
                    IDdocumento = item.IDdocumento,
                    Documento = item.Documento,
                    Detalledocu = item.Detalledocu,
                    Idproceso = item.Idproceso,
                    Visibleautor = item.Visibleautor,
                   EstadoDocu=item.EstadoDoc,
                    InicioFecha = item.InicioFecha,
                    Estado = item.Estado,
                    ActualizadoFecha = item.ActualizadoFecha,
                    Idtipoproceso = item.Idtipoproceso,
                    Detalletipospro = item.Detalletipospro,
                    Progreso = item.Progreso,
                    Idlibro = item.Idlibro,
                    Titulo = item.Titulo,
                    Emisor=item.Emisor,
                    Fecha=item.Fecha,
                    Visibleadmin=item.Visibleadmin,
                    Visiblevaluador=item.Visiblevaluador,
                    Idemisor = item.Idemisor,
                    Idrequerimiento = item.Idrequerimiento,
                    DocObligatorio=item.DocObligatorio,



                });
            }

            return listadocs;
        }





        //Función para registrar documento
        public int InsertarDocumento(int Idproceso,string Documento, string Detalledocu, int visiblea, string emisor, string fecha, string estado, int visibleadmin, int visiblevaluador, int idemisor, int idreq,bool obligatorio)
        {



            int r = 0;


            try
            {

                r = Convert.ToInt32(DB.InsertarDocumento(Idproceso, Documento, Detalledocu,visiblea,emisor,fecha, estado,visibleadmin, visiblevaluador, idemisor, idreq,obligatorio).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 0;
            }
            return r;
        }





   





        //Función para editar estado de documentos enviados 
        public int EditarEstadoDocumentosEnviados(int idemisor, int Idproce, string estadoconsulta, string estadoedit)
        {

            int r = 3;


            try
            {

                r = Convert.ToInt32(DB.EditarEstadoDocumentosEnviados(idemisor, Idproce, estadoconsulta,estadoedit).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }



        //Función para editar estado de documentos enviados por evaluadores
        public int EditarEstadoDocumentosEnviadosxEvaluadores(int idadmin, int idautor,int Idproce, string estadoconsulta, string estadoedit)
        {

            int r = 3;


            try
            {

                r = Convert.ToInt32(DB.EditarEstadoDocumentosEnviadosxEvaluadores(idadmin,idautor, Idproce, estadoconsulta, estadoedit).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }






        //Función para eliminar documento
        public int EliminarDocumento(int IDdocu)
        {
            int r = 1;


            try
            {


                r = Convert.ToInt32(DB.EliminarDocumento(IDdocu).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }





       






    }
}
