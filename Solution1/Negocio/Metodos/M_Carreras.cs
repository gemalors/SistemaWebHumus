using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Negocio.Entidades;


namespace Negocio.Metodos
{
    public class M_Carreras
    {

        DBHumusEntities DB = new DBHumusEntities();




        //Función para listar carreras
        public List<E_Carreras> VerCarreras()
        {

            List<E_Carreras> lista = new List<E_Carreras>();

            foreach (var item in DB.ConsultarCarreras())
            {

                lista.Add(new E_Carreras()
                {
                    IDcarrera = item.IDcarrera,
                    Carrera = item.Carrera

                });
            }

            return lista;
        }





        //Función para registrar carrera
        public int Ingresarcarreras(string carrera)
        {
            int r = 1;
            try
            {
                r = Convert.ToInt32(DB.RegistroCarrera(carrera).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }






        //Función para editar carreras
        public int Editarcarreras(int Idcarrera, string carrera)
        {
            int r = 2;
            try
            {
                r = Convert.ToInt32(DB.EditarCarreras(Idcarrera, carrera).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }






        //Función para eliminar carrera
        public int Eliminarcarreras(int Idcarrera)
        {
            int r = 2;
            try
            {
                DB.EliminarCarreras(Idcarrera);

            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }





    }
}
