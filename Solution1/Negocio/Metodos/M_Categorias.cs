using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Negocio.Entidades;

namespace Negocio.Metodos
{
   public class M_Categorias
    {
        DBHumusEntities DB = new DBHumusEntities();




        //Función para listar categorías
        public List<E_Categorias> Vercategorias()
        {

            List<E_Categorias> lista = new List<E_Categorias>();

            foreach(var item in DB.ConsultarCategoria()) {

                lista.Add(new E_Categorias()
                {
                   IDcategoria=item.IDcategoria, 
                   Detallecategoria=item.Detallecategoria, 
                    Eliminacategoria=item.Eliminacategoria

                });
            }

            return lista;
        }





        //Función para registrar categoría
        public int Ingresarcategorias(string Detallecate)
        {
            int r = 3;
            try
            {
                r = Convert.ToInt32(DB.RegistroCategoria(Detallecate).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }





       
        //Función para editar categoría
        public int Editarcategorias(int Idcate,string Detallecate)
        {
            int r = 3;
            try
            {
                r = Convert.ToInt32(DB.EditarCategoria(Idcate, Detallecate).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }






        //Función para eliminar categoría
        public int Eliminarcategorias(int Idcate)
        {
            int r = 1;
            try
            {
                DB.EliminarCategoria(Idcate);
               
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }
    
    
    
    
    
    
    }
}
