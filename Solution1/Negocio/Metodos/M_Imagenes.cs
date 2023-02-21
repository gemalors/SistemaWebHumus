using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Negocio.Entidades;


namespace Negocio.Metodos
{
   public class M_Imagenes
    {
        DBHumusEntities DB = new DBHumusEntities();

        //Ver imágenes
        public List<E_Imagenes> VerImagenes()
        {

            List<E_Imagenes> lista = new List<E_Imagenes>();

            foreach (var item in DB.VerImagenes())
            {

                lista.Add(new E_Imagenes()
                {
                    IDImagen=item.IDImagen, 
                    DetalleImagen=item.DetalleImagen, 
                    DescripcionImagen=item.DescripcionImagen

                });
            }

            return lista;
        }

        //Agreagar imagen
        public int AgregarImagen(string detalleImagen, string descripcionImagen)
        {
            int r = 3;
            try
            {
                r = Convert.ToInt32(DB.AgregarImagen(detalleImagen,descripcionImagen).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }

        //Editar imagen
        public int EditarImagen(int Idimagen, string detalleImagen, string descripcionImagen)
        {
            int r = 3;
            try
            {
                r = Convert.ToInt32(DB.EditarImagen(Idimagen,detalleImagen,descripcionImagen).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }

        //Eliminar imagen
        public int EliminarImagen(int Idimagen)
        {
            int r = 3;
            try
            {
                DB.EliminarImagen(Idimagen);

            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }

    }
}
