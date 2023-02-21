using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Negocio.Entidades;
namespace Negocio.Metodos
{
   public class M_Tiposproceso
    {


        DBHumusEntities DB = new DBHumusEntities();




        //Función para agregar tipos de procesos
        public int AgregarTiposprocesos(string detalletipospro, int progreso, string duracionproceso, int Numero, int identificador, string descripcion)
        {
            int r = 3;


            try
            {

                r = Convert.ToInt32(DB.AgregarTiposProcesos(detalletipospro, progreso, duracionproceso,Numero,identificador,descripcion).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }







        //Función para editar tipos de procesos
        public int EditarTiposprocesos(int Idtipoproceso, string detalletipospro, int progreso, string duracionproceso,string descripcion , int numero)
        {
            int r = 3;


            try
            {

                r = Convert.ToInt32(DB.EditarTiposProcesos(Idtipoproceso, detalletipospro, progreso, duracionproceso,descripcion,numero).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }








        //Función para eliminar tipos de procesos
        public int EliminarTiposprocesos(int Idtipoproceso)
        {
            int r = 3;


            try
            {

                r = Convert.ToInt32(DB.EliminarTiposProcesos(Idtipoproceso).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }







        //Función para listar tipos procesos
        public List<E_Tiposproceso> ListarTiposProcesos()
        {
            List<E_Tiposproceso> listatiposproces = new List<E_Tiposproceso>();

            foreach (var item in DB.ListarTiposProcesos())
            {
                listatiposproces.Add(new E_Tiposproceso()
                {
                    IDtiposprocesos = item.IDtiposprocesos,
                    Numeroprocesos=item.Numeroprocesos,
                    Detalletipospro = item.Detalletipospro,
                    Progreso = item.Progreso,
                    Duracionproceso = item.Duracionproceso,
                    Identificador = item.Identificador,
                    Descripciontipoproceso = item.Descripciontipoproceso
           


                });
            }

            return listatiposproces;
        }






 






        //Función para listar tipos procesos x número de proceso
        public List<E_Tiposproceso> ListarTiposProcesosxnum(int num)
        {
            List<E_Tiposproceso> listatiposproces = new List<E_Tiposproceso>();

            foreach (var item in DB.ListarTiposProcesosxnum(num))
            {
                listatiposproces.Add(new E_Tiposproceso()
                {
                    IDtiposprocesos = item.IDtiposprocesos,
                    Numeroprocesos = item.Numeroprocesos,
                    Detalletipospro = item.Detalletipospro,
                    Progreso = item.Progreso,
                    Duracionproceso = item.Duracionproceso,
                    Identificador =item.Identificador,
                    Descripciontipoproceso = item.Descripciontipoproceso,
                


                });
            }

            return listatiposproces;
        }





    }
}
