using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Negocio.Entidades;

namespace Negocio.Metodos
{
    public class M_Noticias
    {
        DBHumusEntities DB = new DBHumusEntities();




        //Función para agregar noticia
        public int AgregarNoticia(string detalleNoticia, string descripcionNoticia,string imagennoticia,string titulonoticia)
        {
            int r = 1;
            try
            {
                r = Convert.ToInt32(DB.AgregarNoticia(detalleNoticia,descripcionNoticia,imagennoticia,titulonoticia).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }






        //Función para editar noticia
        public int EditarNoticia(int Idnoticia, string detalleNoticia, string descripcionNoticia, string imagennoticia, string titulonoticia)
        {
            int r = 1;
            try
            {
                r = Convert.ToInt32(DB.EditarNoticia(Idnoticia,detalleNoticia,descripcionNoticia,imagennoticia,titulonoticia).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }






        //Función para eliminar noticia
        public int EliminarNoticia(int Idnoticia)
        {
            int r = 1;
            try
            {
                DB.EliminarNoticia(Idnoticia);

            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }






        //Función para publicar noticia
        public int PublicarNoticia(int Idnoticia,string fecha,DateTime fechap, string urlacceso)
        {
            int r = 1;
            try
            {
                DB.PublicarNoticia(Idnoticia,fecha,fechap,urlacceso);

            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }





        //Función para ver lista de noticias de editorial en página web
        public List<E_Noticias> VerNoticiasEditorial()
        {
            List<E_Noticias> ListNoticias = new List<E_Noticias>();

            foreach (var item in DB.VerNoticias())
            {
                ListNoticias.Add(new E_Noticias()
                {

                    IDNoticia = item.IDNoticia,
                    DetalleNoticia = item.DetalleNoticia,
                    DescripcionNoticia = item.DescripcionNoticia,
                    EliminaNoticia = item.EliminaNoticia,
                    ImagenNoticia = item.ImagenNoticia,
                    Titulonoticia = item.Titulonoticia,
                    Fechapublica = item.Fechapublica,
                    Estado = item.Estado,
                    Urlacceso=item.Urlacceso,
                    Fechapublicacion=item.Fechapublicacion


                }) ;
            }

            return ListNoticias;
        }









        //Función para ver detalle de noticia de editorial en página web x Id
        public List<E_Noticias> VerDetalleNoticiasEditorial(int Idnoticia)
        {
            List<E_Noticias> ListNoticias = new List<E_Noticias>();

            foreach (var item in DB.VerDetalleNoticia(Idnoticia))
            {
                ListNoticias.Add(new E_Noticias()
                {

                    IDNoticia = item.IDNoticia,
                    DetalleNoticia = item.DetalleNoticia,
                    DescripcionNoticia = item.DescripcionNoticia,
                    EliminaNoticia = item.EliminaNoticia,
                    ImagenNoticia = item.ImagenNoticia,
                     Titulonoticia = item.Titulonoticia,
                    Fechapublica = item.Fechapublica,
                    Estado = item.Estado,
                    Urlacceso=item.Urlacceso


                });
            }

            return ListNoticias;
        }






        //Función para agregar imagen a noticia
        public int AgregarImagenNoticia(string detalleimg, int idnoticia)
        {
            int r = 1;
            try
            {
                r = Convert.ToInt32(DB.AgregarImagen(detalleimg,idnoticia).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }






        //Función para editar imagen a noticia
        public int EditarImagenNoticia(int Idimg, string detalleimg)
        {
            int r = 1;
            try
            {
                r = Convert.ToInt32(DB.EditarImagen(Idimg,detalleimg).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }






        //Función para eliminar imagen a noticia
        public int EliminarImagenNoticia(int Idimg)
        {
            int r = 1;
            try
            {
                DB.EliminarImagen(Idimg);

            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }







       
        //Función para ver lista de imágenes de noticia
        public List<E_Imagenes> VerImagenesdeNoticia(int Idnoticia)
        {
            List<E_Imagenes> ListImagenes = new List<E_Imagenes>();

            foreach (var item in DB.ListarImagenesxIdnoticia(Idnoticia))
            {
                ListImagenes.Add(new E_Imagenes()
                {
                    IDimagen=item.IDimagen, 
                    Imagendetalle=item.Imagendetalle, 
                    Idnoticia=item.Idnoticia


                });
            }

            return ListImagenes;
        }






    }
}
