using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Negocio.Entidades;


namespace Negocio.Metodos
{
    public class M_RespuestaRequerimiento
    {


        DBHumusEntities DB = new DBHumusEntities();



        //Función para agregar respuesta de requerimiento de proceso
        public int AgregarRespuestaRequerimiento(int idrequerimiento, string detalle, int progreso)
        {
            int r = 3;
            try
            {
                r = Convert.ToInt32(DB.AgregarRespuestaRequerimiento(idrequerimiento,detalle,progreso).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }






        //Función para editar respuesta de requerimiento de proceso
        public int EditarRespuestaRequerimiento(int idrespuesta, string detalle, int progreso)
        {
            int r = 3;
            try
            {
                r = Convert.ToInt32(DB.EditarRespuestaRequerimiento(idrespuesta, detalle, progreso).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }






        //Función para eliminar respuesta de requerimiento de proceso
        public int EliminarRespuestaRequerimiento(int idrespuesta)
        {
            int r = 1;
            try
            {
                DB.EliminarRespuestaRequerimiento(idrespuesta);

            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }





        //Función para listar opciones de respuesta de requerimiento de proceso x Idrequerimiento
        public List<E_RespuestaRequerimiento> ListarRespuestasRequerimiento(int Idrequerimiento)
        {

            List<E_RespuestaRequerimiento> lista = new List<E_RespuestaRequerimiento>();

            foreach (var item in DB.ListarRespuestaRequerimiento(Idrequerimiento))
            {

                lista.Add(new E_RespuestaRequerimiento()
                {
                    IDrespuestaRequerimiento = item.IDrespuestaRequerimiento,
                    DetalleResp=item.DetalleResp,
                    ProgresoResp=item.ProgresoResp,
                     Idrequerimiento =item.Idrequerimiento,
                    Detallerequerimiento=item.Detallerequerimiento,
                    Idtipoproceso=item.Idtipoproceso,
                    Detalletipospro=item.Detalletipospro,
                    respuesta=item.respuesta,

                  
               

                });
            }

            return lista;
        }






   






        //Función para listar opciones de respuesta de requerimiento de proceso x Idrespuesta y idrequerimiento
        public List<E_RespuestaRequerimiento> ListarRespuestasRequerimientoxIdRequerimiento(int idresp, int idrequerimiento)
        {

            List<E_RespuestaRequerimiento> lista = new List<E_RespuestaRequerimiento>();

            foreach (var item in DB.ListarRespuestasRequerimientoxIdRequerimiento(idresp, idrequerimiento))
            {

                lista.Add(new E_RespuestaRequerimiento()
                {
                    IDrespuestaRequerimiento = item.IdrespSeleccion,
                    Idrequerimiento = item.Idrequerimiento,
                    //IdrespSeleccion = item.IdrespSeleccion,

                    DetalleResp = item.Detalle,
                    ProgresoResp = item.Progreso,
                    Detallerequerimiento = item.Detallerequerimiento,
                    Obligatorio = item.Obligatorio,
                    ResultadoProceso = item.ResultadoProceso


                });
            }

            return lista;
        }






        //Función para listar opciones de respuesta de requerimiento de proceso x tipo de proceso
        public List<E_RespuestaRequerimiento> ListarRespuestasRequerimientoxTipoProceso(int idtipoproceso, string visible)
        {

            List<E_RespuestaRequerimiento> lista = new List<E_RespuestaRequerimiento>();

            foreach (var item in DB.ListarRespuestasRequerimientoxTipoProceso(idtipoproceso, visible))
            {

                lista.Add(new E_RespuestaRequerimiento()
                {
                    IDrespuestaRequerimiento = item.IdrespSeleccion,
                    Idrequerimiento = item.Idrequerimiento,
                    IdrespSeleccion = item.IdrespSeleccion,

                    DetalleResp = item.Detalle,
                    ProgresoResp = item.Progreso,
                    Detallerequerimiento = item.Detallerequerimiento,
                    Obligatorio = item.Obligatorio,
                    ResultadoProceso=item.ResultadoProceso

                });
            }

            return lista;
        }






    }
}
