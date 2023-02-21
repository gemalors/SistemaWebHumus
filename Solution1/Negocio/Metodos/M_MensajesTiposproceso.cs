using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Negocio.Entidades;


namespace Negocio.Metodos
{
    public class M_MensajesTiposproceso
    {


        DBHumusEntities DB = new DBHumusEntities();


        //Función para agregar mensajes de estados para tipos de procesos
        public int AgregarMensajeEstadoTiposProcesos(int idestado, string descripcion, string visible)
        {
            int r = 3;


            try
            {

                r = Convert.ToInt32(DB.AgregarMensajeEstadoTiposProcesos(idestado, descripcion, visible).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }







        //Función para editar mensajes de estados para tipos de procesos
        public int EditarMensajeEstadoTiposProcesos(int idmensaje, string descripcion,string visible)
        {
            int r = 3;


            try
            {

                r = Convert.ToInt32(DB.EditarMensajeEstadoTiposProcesos(idmensaje, descripcion,visible).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }








        //Función para eliminar mensajes de estados para tipos de procesos
        public int EliminarMensajeEstadoTiposProcesos(int idmensaje)
        {
            int r = 3;


            try
            {

                r = Convert.ToInt32(DB.EliminarMensajeEstadoTiposProcesos(idmensaje).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }







        //Función para listar mensajes de estados para tipos procesos
        public List<E_MensajesTiposproceso> ListarMensajeEstadoTiposProcesos(int idestado)
        {
            List<E_MensajesTiposproceso> lista = new List<E_MensajesTiposproceso>();

            foreach (var item in DB.ListarMensajeEstadoTiposProcesos(idestado))
            {
                lista.Add(new E_MensajesTiposproceso()
                {


                IDMensajeTiposproceso=item.IDMensajeTiposproceso,
                    DescripcionMensaje=item.DescripcionMensaje,
                    VisibleMensaje=item.VisibleMensaje,
                    Idestados=item.Idestados,
                    IdentificadorEstados=item.IdentificadorEstados,
                    DetalleEstados=item.DetalleEstados,
                    Idtipoproceso=item.Idtipoproceso,
                    Detalletipospro=item.Detalletipospro

                });
            }

            return lista;
        }





        //Función para ver mensaje de estado para proceso
        public List<E_MensajesTiposproceso> VerMensajeEstadoProceso(int idestado,int idtipoproceso, string visible)
        {
            List<E_MensajesTiposproceso> lista = new List<E_MensajesTiposproceso>();

            foreach (var item in DB.VerMensajeEstadoProceso(idestado,idtipoproceso,visible))
            {
                lista.Add(new E_MensajesTiposproceso()
                {


                    IDMensajeTiposproceso = item.IDMensajeTiposproceso,
                    DescripcionMensaje = item.DescripcionMensaje,
                    VisibleMensaje = item.VisibleMensaje,
                    Idestados = item.Idestados,
                    IdentificadorEstados = item.IdentificadorEstados,
                    DetalleEstados = item.DetalleEstados,
                    Idtipoproceso = item.Idtipoproceso,
                    Detalletipospro = item.Detalletipospro

                });
            }

            return lista;
        }



    }
}
