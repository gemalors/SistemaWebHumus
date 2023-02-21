using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Negocio.Entidades;

namespace Negocio.Metodos
{
    public class M_Procesos
    {
        DBHumusEntities DB = new DBHumusEntities();





        //Función para crear proceso
        public int CrearProceso(int Idlibro, string Fechainicio, string Estado_Proceso, int Idtipoproceso,int identificador)
        {
            int r = 3;


            try
            {

                r = Convert.ToInt32(DB.IniciarProceso( Idlibro,Fechainicio,Estado_Proceso, Idtipoproceso,identificador).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }







        //Función para crear proceso siguiente
        public int CrearProceso2(int Idlibro, string fechainicio, string fechactualizada, string Estado_Proceso, int Idtipoproceso, int Idpanterior,int identificador)
        {
            int r = 3;


            try
            {

                r = Convert.ToInt32(DB.IniciarProceso2(Idlibro, fechainicio, fechactualizada, Estado_Proceso, Idtipoproceso, Idpanterior,identificador).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }







        //Función para editar proceso
        public int EditarProceso1(int Idproceso, string fechactualizada, string Estado_Proceso, int Idpanterior,int opresult,int identificador)
        {
            int r = 3;


            try
            {

                r = Convert.ToInt32(DB.EditarProceso(Idproceso, fechactualizada, Estado_Proceso, Idpanterior,opresult,identificador).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }







        
        //Función para crear ver detalles de proceso del libro x Idlibro
        public List<E_Procesos> VerProceso(int Idlibro)
        {
            List<E_Procesos> listaproceso = new List<E_Procesos>();

            foreach (var item in DB.VerProceso(Idlibro))
            {
                listaproceso.Add(new E_Procesos()
                {

                    IDproceso = item.IDproceso,
                    Titulo = item.Titulo,
                    InicioFecha = item.InicioFecha,
                    ActualizadoFecha = item.ActualizadoFecha,
                    Estado_Proceso = item.Estado,
                    Detalletipospro = item.Detalletipospro,
                   
                    Numeroprocesos=item.Numeroprocesos,
                    Idtipoproceso = item.Idtipoproceso,
              

                });
            }

            return listaproceso;
        }

   





        //Función para ver detalles de proceso de libro x Idproceso
        public List<E_Procesos> VerProcesoxIdp(int Idlibro, int Idproceso)
        {
            List<E_Procesos> listaproceso = new List<E_Procesos>();

            foreach (var item in DB.VerProcesoxId(Idlibro, Idproceso))
            {
                listaproceso.Add(new E_Procesos()
                {

                    IDproceso = item.IDproceso,
                    Titulo = item.Titulo,
                    InicioFecha = item.InicioFecha,
                    ActualizadoFecha = item.ActualizadoFecha,
                    Estado_Proceso = item.Estado_Proceso,
                    Detalletipospro = item.Detalletipospro,
                    PrimerNombre = item.PrimerNombre,
                    SegundoNombre = item.SegundoNombre,
                    Numeroprocesos = item.Numeroprocesos,
                    ApellidoPrimero = item.ApellidoPrimero,
                    ApellidoSegundo = item.ApellidoSegundo,
                    Idusuario = item.Idusuario,
                    Idlibro = item.Idlibro,
                    Estado = item.Estado,
                    Duracionproceso = item.Duracionproceso,
                    Opresultado=item.Opresultado,
                    Idtipoproceso = item.Idtipoproceso,
                    Idprocanterior = item.Idprocanterior,
                    Tipo_Proceso = item.Idtipoproceso,
                    Progreso = item.Progreso,
                    ProgresoL = item.ProgresoL,
                    Descripciontipoproceso=item.Descripciontipoproceso,
                    Identificador=item.Identificador,
                    IdentificaEstado=item.IdentificaEstado,
                    DetalleEstados=item.DetalleEstados,
                    Idestado=item.Idestado,

                    VRequerimientosAutor = item.VRequerimientosAutor,
                    VEnviarProcesoRevision = item.VEnviarProcesoRevision,
                    VEnviarRevisionReversado = item.VEnviarRevisionReversado,
                    VRequerimientosAdmin = item.VRequerimientosAdmin,
                    VEnviarResultados = item.VEnviarResultados,
                    VIniciarSiguienteProceso = item.VIniciarSiguienteProceso,
                    VAsignarEvaluadores = item.VAsignarEvaluadores,
                    VEnviarEvaluacionPares = item.VEnviarEvaluacionPares,
                    VPublicacionLibro = item.VPublicacionLibro
                    //IdentificadorEstados=item.IdentificadorEstados







                });
            }

            return listaproceso;
        }




    }
}
