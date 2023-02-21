using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Negocio.Entidades;

namespace Negocio.Metodos
{
    public class M_ObservacionesProceso
    {
        DBHumusEntities DB = new DBHumusEntities();




        //Función para agregar observación
        public int AgregarObservacion(int Idproceso, string observacion, string Detalle, string emisor, string titulo,string fecha, bool visible,string estado,int idemisor, bool respuesta,int Idreq,bool obligatoria)
        {
            

            int r = 3;

            try
            {

                r = Convert.ToInt32(DB.AgregarObservacion(Idproceso,observacion,Detalle,emisor,titulo,fecha,visible,estado,idemisor,respuesta,Idreq,obligatoria).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }






 

        //Función para editar estado de observaciones enviadas
        public int EditarEstadoObservacionesEnviadas(int idemisor, int idproceso, string estadoconsulta, string estadoedit)
        {


            int r = 3;

            try
            {

                r = Convert.ToInt32(DB.EditarEstadoObservacionesEnviadas(idemisor, idproceso, estadoconsulta,estadoedit).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }






        //Función para ver observaciones de procesos x Idproceso
        public List<E_ObservacionesProceso> VerObservaciones(int Idproceso)
        {
            List<E_ObservacionesProceso> listObservaciones = new List<E_ObservacionesProceso>();

            foreach (var item in DB.VerObservaciones(Idproceso))
            {
                listObservaciones.Add(new E_ObservacionesProceso()
                {

                    IDObservacion=item.IDObservacion,
                    Detalleobservacion=item.Detalleobservacion,
                    Descripcionobservacion=item.Descripcionobservacion,
                    Emisor=item.Emisor,
                    Titulo=item.Titulo,
                    Fechaobservacion=item.Fechaobservacion,
                    VisibleAutor = item.VisibleAutor,
                    EstadoObservacion = item.EstadoObservacion,
                    RespuestaProceso = item.RespuestaProceso,
                    Idemisor = item.Idemisor,
                    Obligatoria=item.Obligatoria
              



                });
            }

            return listObservaciones;
        }







       


        //Función para ver observaciones de procesos x Idproceso enviadas
        public List<E_ObservacionesProceso> VerObservacionesEnviadas(int Idproceso, int idemisor)
        {
            List<E_ObservacionesProceso> listObservaciones = new List<E_ObservacionesProceso>();

            foreach (var item in DB.VerObservacionesEnviadas(Idproceso, idemisor))
            {
                listObservaciones.Add(new E_ObservacionesProceso()
                {

                    IDObservacion = item.IDObservacion,
                    Detalleobservacion = item.Detalleobservacion,
                    Descripcionobservacion = item.Descripcionobservacion,
                    Emisor = item.Emisor,
                    Titulo = item.Titulo,
                    Fechaobservacion = item.Fechaobservacion,
                    VisibleAutor = item.VisibleAutor,
                    EstadoObservacion = item.EstadoObservacion,
                    RespuestaProceso = item.RespuestaProceso,
                    Idemisor = item.Idemisor,
                    Idrequerimiento = item.Idrequerimiento,
                    Obligatoria = item.Obligatoria



                });
            }

            return listObservaciones;
        }





        //Función para eliminar observación
        public int EliminarObservacion(int Idobservacion)
        {
            int r = 1;
            try
            {
                DB.EliminarObservacion(Idobservacion);

            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }






    }
}
