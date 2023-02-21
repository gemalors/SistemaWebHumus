using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Negocio.Entidades;

namespace Negocio.Metodos
{
   public class M_InfoWeb
    {

        DBHumusEntities DB = new DBHumusEntities();

        //Editar informacion de página web
        public int EditarInfoWeb(int Id,string bienvenida, string mision, string vision, string email, string horario, string descripcionEditorial, string descripcionProceso, string imgEstructEditorial, string imgProcesoEditorial)
        {
           
            int r = 3;

            try
            {

                r = Convert.ToInt32(DB.EditarInfoWeb(Id, bienvenida, mision, vision, email, horario, descripcionEditorial,descripcionProceso,imgEstructEditorial,imgProcesoEditorial).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }


        public List<E_InfoWeb> ObtenerInfoWeb()
        {
            List<E_InfoWeb> listaweb = new List<E_InfoWeb>();

            foreach (var item in DB.VerInfoWeb())
            {
                listaweb.Add(new E_InfoWeb()
                {

                    
                    IDinfoweb=item.IDinfoweb,
                    Bienvenida=item.Bienvenida,
                    Mision=item.Mision,
                    Vision=item.Vision,
                    Emailweb=item.Emailweb,
                    HorarioAtencion=item.HorarioAtencion,
                    DescripcionEditorial=item.DescripcionEditorial,
                    ImgEstructEditorial=item.ImgEstructEditorial,
                    DescripcionProceso=item.DescripcionProceso,
                    ImgProcesoEditorial=item.ImgProcesoEditorial
                 

                 

                });
            }

            return listaweb;
        }

    }
}
