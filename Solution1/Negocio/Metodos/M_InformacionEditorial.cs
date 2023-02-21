using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Negocio.Entidades;


namespace Negocio.Metodos
{
    public class M_InformacionEditorial
    {
        DBHumusEntities DB = new DBHumusEntities();




        
        //Función para agregar información de la Editorial en página web
        public int AgregarInformacionEditorial(string tituloinfo, string enunciadoinfo, string imageninfo, string urlinfo, int pagina)
        {
           
            int r = 1;

            try
            {

                r = Convert.ToInt32(DB.AgregarInformacion(tituloinfo,enunciadoinfo, imageninfo, urlinfo,pagina).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }







        //Función para editar información de la Editorial en página web
        public int EditarInformacionEditorial(int Idinfo,string tituloinfo, string enunciadoinfo, string imageninfo, string urlinfo,int pagina)
        {

            int r = 1;

            try
            {

                r = Convert.ToInt32(DB.EditarInformacion(Idinfo,tituloinfo, enunciadoinfo, imageninfo, urlinfo,pagina).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }







        //Función para eliminar información de la Editorial en página web
        public int EliminarInformacionEditorial(int Idinfo)
        {
            int r = 1;

            try
            {

                r = Convert.ToInt32(DB.EliminarInformacionEditorial(Idinfo).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }





        
        //Función para ver lista de información de la Editorial en página web
        public List<E_InformacionEditorial> VerInformacionEDitorial()
        {
            List<E_InformacionEditorial> ListInfo = new List<E_InformacionEditorial>();

            foreach (var item in DB.VerInformacionEditorial())
            {
                ListInfo.Add(new E_InformacionEditorial()
                {

                    IDinformacion = item.IDinformacion,
                    TituloInformacion = item.TituloInformacion,
                    EnunciadoInformacion = item.EnunciadoInformacion,
                    ImagenInfo = item.ImagenInfo,
                    UrlInfo = item.UrlInfo,
                    Pagina = item.Pagina




                }) ;
            }

            return ListInfo;
        }






     
        //Función para ver detalle de información de la Editorial x Id en página web
        public List<E_InformacionEditorial> VerDetalleInformacionEDitorial(int Idinfo)
        {
            List<E_InformacionEditorial> ListInfo = new List<E_InformacionEditorial>();

            foreach (var item in DB.VerDetalleInformacionEditorial(Idinfo))
            {
                ListInfo.Add(new E_InformacionEditorial()
                {

                    IDinformacion = item.IDinformacion,
                    TituloInformacion = item.TituloInformacion,
                    EnunciadoInformacion = item.EnunciadoInformacion,
                    ImagenInfo = item.ImagenInfo,
                    UrlInfo = item.UrlInfo,
                    Pagina=item.Pagina
                  

                });
            }

            return ListInfo;
        }
   
    
    

    
    }
}
