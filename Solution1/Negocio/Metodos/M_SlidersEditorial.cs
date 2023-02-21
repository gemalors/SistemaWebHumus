using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Negocio.Entidades;

namespace Negocio.Metodos
{
  public class M_SlidersEditorial
    {
        DBHumusEntities DB = new DBHumusEntities();






        
        //Función para agregar slider para página web
        public int AgregarSlider(string titulo, string enunciado, string imagenslider, string url)
        {
          
            int r = 1;

            try
            {

                r = Convert.ToInt32(DB.AgregarSlider(titulo,enunciado,imagenslider,url).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }







        //Función para editar slider para página web
        public int EditarSlider(int Idslider, string titulo, string enunciado, string imagenslider, string url)
        {
            int r = 1;

            try
            {

                r = Convert.ToInt32(DB.EditarSlider(Idslider,titulo ,enunciado,imagenslider,url).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }







        //Función para eliminar slider para página web
        public int EliminarSlider(int Idslider)
        {
            int r = 1;

            try
            {

                r = Convert.ToInt32(DB.EliminarSlider(Idslider).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }






        //Función para ver sliders para página web
        public List<E_SlidersEditorial> VerSliders()
        {
            List<E_SlidersEditorial> ListSliders = new List<E_SlidersEditorial>();

            foreach (var item in DB.VerSliders())
            {
                ListSliders.Add(new E_SlidersEditorial()
                {

                    IDSlider=item.IDSlider, 
                    Titulo=item.Titulo,
                    Enunciado=item.Enunciado, 
                    ImagenSlider=item.ImagenSlider,
                    Urlslider=item.Urlslider
                   


                });
            }

            return ListSliders;
        }





    }
}
