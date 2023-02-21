using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Negocio.Entidades;
namespace Negocio.Metodos

{
   public class M_RequerimientosProcesos
    {
        DBHumusEntities DB = new DBHumusEntities();

       



        //Función para agregar requerimientos de procesos
        public int AgregarRequerimientosProcesos(string detallerequerimiento, int Idtipopro, int idtipodato,string descripcionrequerimiento, string emisor, string resp,bool visibleadmin, bool visibleE,bool visibleautor,bool obligatorio,bool resultproceso) 
        {
            int r = 3;


            try
            {

                r = Convert.ToInt32(DB.AgregarRequerimientoProceso(detallerequerimiento, Idtipopro, idtipodato,descripcionrequerimiento,emisor,resp,visibleadmin,visibleE,visibleautor, obligatorio,resultproceso).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }






        //Función para editar requerimientos de procesos
        public int EditarRequerimientosProcesos(int Idrequerimiento, string detallerequerimiento, int Idtipopro, int idtipodato,string descripcionrequerimiento, string emisor, string resp, bool visibleadmin, bool visibleE, bool visibleautor,bool obligatorio,bool resultproceso)
        {
            int r = 3;


            try
            {

                r = Convert.ToInt32(DB.EditarRequerimientoProceso(Idrequerimiento, detallerequerimiento, Idtipopro, idtipodato,descripcionrequerimiento, emisor, resp,visibleadmin,visibleE,visibleautor, obligatorio,resultproceso).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }






        //Función para eliminar requerimientos de procesos
        public int EliminarRequerimientosProcesos(int Idrequerimiento)
        {
            int r = 3;


            try
            {

                r = Convert.ToInt32(DB.EliminarRequerimientoProceso(Idrequerimiento).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }






        //Función para listar requerimientos de procesos
        public List<E_RequerimientosProcesos> ListarRequerimientosProcesos(int Idproceso)
        {
            List<E_RequerimientosProcesos> listarequerimientos = new List<E_RequerimientosProcesos>();

            foreach (var item in DB.ListarRequerimientoProceso(Idproceso))
            {
                listarequerimientos.Add(new E_RequerimientosProcesos()
                {
                    IDrequerimiento = item.IDrequerimiento,
                    Detallerequerimiento = item.Detallerequerimiento,
              
                    IDtiposprocesos = item.IDtiposprocesos,
                    Detalletipospro = item.Detalletipospro,
                    Progreso = item.Progreso,
                    Tipodato=item.Tipodato,
                    IDtipodato=item.IDtipodato,
                    Emisor=item.Emisor,
                
                    respuesta=item.respuesta,
                    Numeroprocesos=item.Numeroprocesos,
                    Duracionproceso = item.Duracionproceso,
                    Descripcionrequerimiento=item.Descripcionrequerimiento,
                    Identificador = item.Identificador,
                    Descripciontipoproceso = item.Descripciontipoproceso,
                    ResultadoProceso=item.ResultadoProceso,

                    Visibleadmin =item.Visibleadmin,
                    Visibleautor=item.Visibleautor,
                    Visibleevaluador=item.Visibleevaluador,
                    Obligatorio=item.Obligatorio,
                   



                });
            }

            return listarequerimientos;
        }







        //Función para listar requerimiento x Idproceso y x Idlibro
        public List<E_RequerimientosProcesos> ListarRequerimientosProcesoxId(int Idtipoproceso,string visible)
        {
            List<E_RequerimientosProcesos> listarequerimientos = new List<E_RequerimientosProcesos>();

            foreach (var item in DB.ListarRequerimientoProcesoxId(Idtipoproceso,visible))
            {
                listarequerimientos.Add(new E_RequerimientosProcesos()
                {
                    IDrequerimiento = item.IDrequerimiento,
                    Detallerequerimiento = item.Detallerequerimiento,
                    Idtipoproceso = item.Idtipoproceso,
                   
                    Detalletipospro = item.Detalletipospro,
                    Progreso = item.Progreso,
                    Tipodato = item.Tipodato,
                    Emisor = item.Emisor,
                    Numeroprocesos = item.Numeroprocesos,
                    Duracionproceso = item.Duracionproceso,
                    Descripcionrequerimiento = item.Descripcionrequerimiento,
                    respuesta = item.respuesta,
                    Identificador = item.Identificador,
                    Descripciontipoproceso = item.Descripciontipoproceso,
                    ResultadoProceso=item.ResultadoProceso,
                    Visibleadmin =item.Visibleadmin,
                    Visibleautor=item.Visibleautor,
                    Visibleevaluador=item.Visibleevaluador,
                    Obligatorio=item.Obligatorio,
                    Obligatorios=item.Obligatorios,
                   



                });
            }

            return listarequerimientos;
        }






        //Función para listar requerimientos de procesos por el número de proceso
        public List<E_RequerimientosProcesos> ListarRequerimientosxProceso(int numproceso)
        {
            List<E_RequerimientosProcesos> listadocumentproces = new List<E_RequerimientosProcesos>();

            foreach (var item in DB.ListarRequerimientosxProceso(numproceso))
            {
                listadocumentproces.Add(new E_RequerimientosProcesos()
                {
                    IDrequerimiento = item.IDrequerimiento,
                    Detallerequerimiento = item.Detallerequerimiento,
                    Idtipoproceso = item.Idtipoproceso,
                    Numeroprocesos=item.Numeroprocesos,
                    IDtiposprocesos = item.IDtiposprocesos,
                    Detalletipospro = item.Detalletipospro,
                    Progreso = item.Progreso,
                    Duracionproceso = item.Duracionproceso,
                    //Iddescripcion = item.Iddescripcion,
                    //Descripciontipoproceso = item.Descripciontipoproceso,
                    //Numerodescrip = item.Numerodescrip
                    Obligatorio = item.Obligatorio,
                   


                });
            }

            return listadocumentproces;
        }
  
    
    
    
    }
}
