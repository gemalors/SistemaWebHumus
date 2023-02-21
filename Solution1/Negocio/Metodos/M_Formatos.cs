using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Negocio.Entidades;

namespace Negocio.Metodos
{
    public class M_Formatos
    {
        DBHumusEntities DB = new DBHumusEntities();






        //Función para ver tipos de formatos
        public List<E_Tipoformatos> VerTiposformatos()
        {
            List<E_Tipoformatos> listatipform = new List<E_Tipoformatos>();

            foreach (var item in DB.ConsultarTipoformatos())
            {
                listatipform.Add(new E_Tipoformatos()
                {
                    IDtipoformato=item.IDtipoformato,
                    Detalletipoform=item.Detalletipoform

                });
            }

            return listatipform;
        }







        //Función para agregar tipos de formatos
        public int CrearTipoformatos(string Detalletipoform)
        {
            int r = 3;
           

            try
            {

                r = Convert.ToInt32(DB.RegistroTipoformatos(Detalletipoform).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }







        //Función para editar tipos de formatos
        public int EditarTipoformatos(int Idtipoform, string Detalletipoform)
        {
            int r = 1;
            

            try
            {

                r = Convert.ToInt32(DB.EditarTipoformatos(Idtipoform, Detalletipoform).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }







        //Función para eliminar tipos de formatos
        public int EliminarTipoformatos(int Idtipoform)
        {
            int r = 1;
            try
            {
                DB.EliminarTipoformatos(Idtipoform);

            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }






        //Función para ver formatos
        public List<E_Formatos> ConsultarFormatos()
        {
            List<E_Formatos> listaform = new List<E_Formatos>();

            foreach (var item in DB.ConsultarFormato())
            {
                listaform.Add(new E_Formatos()
                {
                  IDformato=item.IDformato,
                  Archivo=item.Archivo,
                  Detallearchivo=item.Detallearchivo,
                  Idtipoformato=item.Idtipoformato,
                  Detalletipoform=item.Detalletipoform,
                  DescripcionFormato=item.DescripcionFormato
                  

                });
            }

            return listaform;
        }






        //Función para agregar formatos
        public int CrearFormatos(string Archivo, string Detallearchivo, int Idtipoformato,string descripcion)
        {
         
            int r = 1;


            try
            {

                r = Convert.ToInt32(DB.RegistroFormato(Archivo, Detallearchivo, Idtipoformato,descripcion).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }







        //Función para editar formatos
        public int EditarFormatos(int Idformato, string Archivo, string Detalleform, int Idtipoform,string descripcion)
        {
     

            int r = 1;

      
            try
            {

                r = Convert.ToInt32(DB.EditarFormato(Idformato,Archivo, Detalleform, Idtipoform, descripcion).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }







        //Función para eliminar formatos
        public int EliminarFormato(int Idformato)
        {
            int r = 1;


            try
            {


                r = Convert.ToInt32(DB.EliminarFormato(Idformato).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }






       


    }
}
