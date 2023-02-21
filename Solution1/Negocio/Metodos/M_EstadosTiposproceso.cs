using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Negocio.Entidades;


namespace Negocio.Metodos
{
    public class M_EstadosTiposproceso
    {


        DBHumusEntities DB = new DBHumusEntities();


        //Función para agregar estados para tipos de procesos
        public int AgregarEstadoTiposProcesos(int identificador,string detalle, int idtipoproceso, bool requerimientosAutor, bool revision, bool reversado, bool requerimientosAdmin, bool resultados, bool iniciarProceso, bool asignarEvaluadores, bool evaluacionPares, bool publicacionLibro )

        {
            int r = 3;


            try
            {

                r = Convert.ToInt32(DB.AgregarEstadoTiposProcesos(identificador,detalle,idtipoproceso,requerimientosAutor,revision,reversado,requerimientosAdmin,resultados,iniciarProceso,asignarEvaluadores,evaluacionPares,publicacionLibro).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }







        //Función para editar estados para tipos de procesos
        public int EditarEstadoTiposProcesos(int idestado, string detalle, bool requerimientosAutor, bool revision, bool reversado, bool requerimientosAdmin, bool resultados, bool iniciarProceso, bool asignarEvaluadores, bool evaluacionPares, bool publicacionLibro)
        {
            int r = 3;


            try
            {

                r = Convert.ToInt32(DB.EditarEstadoTiposProcesos(idestado,detalle, requerimientosAutor, revision, reversado, requerimientosAdmin, resultados, iniciarProceso, asignarEvaluadores, evaluacionPares, publicacionLibro).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }








        //Función para eliminar estados para tipos de procesos
        public int EliminarEstadoTiposProcesos(int idestado)
        {
            int r = 3;


            try
            {

                r = Convert.ToInt32(DB.EliminarEstadoTiposProcesos(idestado).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }







        //Función para listar estados para tipos procesos
        public List<E_EstadosTiposproceso> ListarEstadoTiposProcesos(int idtipoproceso)
        {
            List<E_EstadosTiposproceso> lista = new List<E_EstadosTiposproceso>();

            foreach (var item in DB.ListarEstadoTiposProcesos(idtipoproceso))
            {
                lista.Add(new E_EstadosTiposproceso()
                {


                    IDEstadosTiposproceso=item.IDEstadosTiposproceso,
                    IdentificadorEstados=item.IdentificadorEstados,
                    DetalleEstados=item.DetalleEstados,
                    Idtipoproceso=item.Idtipoproceso,
                    VRequerimientosAutor=item.VRequerimientosAutor,
                    VEnviarProcesoRevision=item.VEnviarProcesoRevision,
                    VEnviarRevisionReversado=item.VEnviarRevisionReversado,
                    VRequerimientosAdmin=item.VRequerimientosAdmin,
                    VEnviarResultados=item.VEnviarResultados,
                    VIniciarSiguienteProceso=item.VIniciarSiguienteProceso,
                    VAsignarEvaluadores=item.VAsignarEvaluadores,
                    VEnviarEvaluacionPares=item.VEnviarEvaluacionPares,
                    VPublicacionLibro=item.VPublicacionLibro

                });
            }

            return lista;
        }






        //Función para listar estados para tipos procesos x dentificador
        public List<E_EstadosTiposproceso> ListarEstadoxIdentificador(int idtipoproceso,int identificador)
        {
            List<E_EstadosTiposproceso> lista = new List<E_EstadosTiposproceso>();

            foreach (var item in DB.ListarEstadoxIdentificador(idtipoproceso,identificador))
            {
                lista.Add(new E_EstadosTiposproceso()
                {


                    IDEstadosTiposproceso = item.IDEstadosTiposproceso,
                    IdentificadorEstados = item.IdentificadorEstados,
                    DetalleEstados = item.DetalleEstados,
                    Idtipoproceso = item.Idtipoproceso

                });
            }

            return lista;
        }



        //Función para listar estados para tipos procesos x detalle de respuesta de resultado de proceso
        public List<E_EstadosTiposproceso> ListarEstadoxDetalleRespuesta(int idtipoproceso, string respconsulta)
        {
            List<E_EstadosTiposproceso> lista = new List<E_EstadosTiposproceso>();

            foreach (var item in DB.ListarEstadoxDetalleRespuesta(idtipoproceso, respconsulta))
            {
                lista.Add(new E_EstadosTiposproceso()
                {


                    IDEstadosTiposproceso = item.IDEstadosTiposproceso,
                    IdentificadorEstados = item.IdentificadorEstados,
                    DetalleEstados = item.DetalleEstados,
                    Idtipoproceso = item.Idtipoproceso

                });
            }

            return lista;
        }




    }
}
