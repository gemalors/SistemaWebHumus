using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Negocio.Entidades;


namespace Negocio.Metodos
{
    public class M_Periodos
    {

        DBHumusEntities DB = new DBHumusEntities();




        //Función para agregar períodos académicos para generar consulta de estadísticas
        public int RegistrarPeriodo(string detalleperiodo, int ordenperiodo, DateTime fechainicio, DateTime fechafin)
        {


            int r = 1;

            try
            {

                r = Convert.ToInt32(DB.RegistroPeriodo(detalleperiodo,ordenperiodo,fechainicio,fechafin).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }







        //Función para editar períodos académicos para generar consulta de estadísticas
        public int EditarPeriodo(int Idp,string detalleperiodo, DateTime fechainicio, DateTime fechafin)
        {


            int r = 0;

            try
            {

                r = Convert.ToInt32(DB.EditarPeriodo(Idp,detalleperiodo, fechainicio, fechafin).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }






        //Función para listar períodos académicos
        public List<E_Periodos> ListarPeriodos()
        {
            List<E_Periodos> list = new List<E_Periodos>();

            foreach (var item in DB.ListarPeriodos())
            {
                list.Add(new E_Periodos()
                {
                    IDperiodo=item.IDperiodo,
                    Detalleperiodo=item.Detalleperiodo,
                    Ordenperiodo=item.Ordenperiodo,
                    Fechainicio=item.Fechainicio,
                    Fechafin=item.Fechafin,
                   


                });
            }

            return list;
        }








        //Función para listar años de períodos académicos
        public List<E_Years> ListarYears()
        {
            List<E_Years> list = new List<E_Years>();

            foreach (var item in DB.ListarYearPeriodo())
            {
                list.Add(new E_Years()
                {
                   
                    IDyear=item.IDyear,
                    Fechayear=item.Fechayear,
                    year=item.year


                });
            }

            return list;
        }










        //Función para eliminar período académico
        public int EliminarPeriodo(int idperiodo)
        {
            int r = 1;
            try
            {
                DB.EliminarPeriodo(idperiodo);

            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }




    }
}
