using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Negocio.Entidades;


namespace Negocio.Metodos
{
   public class M_Notificaciones
    {

        DBHumusEntities DB = new DBHumusEntities();





        //Función para agregar notificación
        public int InsertarNotificacion(string notificacion, string urlnotificacion, string visible,int iduser, int idtipo,string fecha,string detallenotificacion,bool general)
        {
              

            int r = 1;

            try
            {

                r = Convert.ToInt32(DB.CrearNotificacion(notificacion,urlnotificacion,visible,iduser,idtipo,fecha,detallenotificacion,general).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }







        //Función para editar notificación
        public int EditarNotificacion(int Idnoti,string notificacion, string urlnotificacion, string visible, int iduser, int idtipo, string fecha, string detallenotificacion,bool general)
        {


            int r = 1;

            try
            {

                r = Convert.ToInt32(DB.EditarNotificacion(Idnoti,notificacion, urlnotificacion, visible, iduser, idtipo, fecha, detallenotificacion,general).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }







        //Función para cambiar estado de notificación x Idusuario
        public int CambiarEstadoNotificacion(int Iduser)
        {


            int r = 1;

            try
            {

                r = Convert.ToInt32(DB.CambiarEstadoNotificacion(Iduser).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }





        //Función para publicar notificación
        public int PublicarNotificacion(int idnoti,string visible)
        {


            int r = 1;

            try
            {

                r = Convert.ToInt32(DB.PublicarNotificacion(idnoti,visible).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }






        //Función para ver 5 notificaciones por Idusuario
        public List<E_Notificaciones> VerNotificacionesxIduser(int Iduser)
        {
            List<E_Notificaciones> ListNotifi = new List<E_Notificaciones>();

            foreach (var item in DB.VerNotificacionesxIduser(Iduser))
            {
                ListNotifi.Add(new E_Notificaciones()
                {

                     IDnotificacion=item.IDnotificacion,
                     Notificacion=item.Notificacion, 
                     Estado=item.Estado,
                     Fechanotifi=item.Fechanotifi, 
                     UrlNotificacion=item.UrlNotificacion, 
                     Visible=item.Visible, 
                     Idusuario=item.Idusuario,
                     Idtiponotificacion=item.Idtiponotificacion, 
                     DetalleNotificacion=item.DetalleNotificacion,
                     DetalleTipoNotificacion=item.DetalleTipoNotificacion,
                     Icono=item.Icono,
                    TotalNotificacionesrecibidas = item.TotalNotificacionesrecibidas,
                    TotalNotificacionesnuevas = item.TotalNotificacionesnuevas,
                    General=item.General



                });
            }

            return ListNotifi;
        }







        //Función para ver todas las notificaciones por Idusuario
        public List<E_Notificaciones> VerTdodoNotificacionesxIduser(int Iduser)
        {
            List<E_Notificaciones> ListNotifi = new List<E_Notificaciones>();

            foreach (var item in DB.VerTodoNotificacionesxIduser(Iduser))
            {
                ListNotifi.Add(new E_Notificaciones()
                {

                    IDnotificacion = item.IDnotificacion,
                    Notificacion = item.Notificacion,
                    Estado = item.Estado,
                    Fechanotifi = item.Fechanotifi,
                    UrlNotificacion = item.UrlNotificacion,
                    Visible = item.Visible,
                    Idusuario = item.Idusuario,
                    Idtiponotificacion = item.Idtiponotificacion,
                    DetalleNotificacion = item.DetalleNotificacion,
                    DetalleTipoNotificacion = item.DetalleTipoNotificacion,
                    Icono = item.Icono,
                    TotalNotificacionesrecibidas = item.TotalNotificacionesrecibidas,
                    TotalNotificacionesnuevas = item.TotalNotificacionesnuevas,
                    General=item.General


                });
            }

            return ListNotifi;
        }







        //Función para ver tipo de notificaciones
        public List<E_TipoNotificacion> VerTiposNotificaciones()
        {
            List<E_TipoNotificacion> ListNotifi = new List<E_TipoNotificacion>();

            foreach (var item in DB.VerTiposNotificaciones())
            {
                ListNotifi.Add(new E_TipoNotificacion()
                {


                    IDtipoNotificacion = item.IDtipoNotificacion,
                    DetalleTipoNotificacion = item.DetalleTipoNotificacion,
                    Icono = item.Icono



                });
            }

            return ListNotifi;
        }








        //Función para ver notificaciones registradas
        public List<E_Notificaciones> VerNotificacionesRegistradas()
        {
            List<E_Notificaciones> ListNotifi = new List<E_Notificaciones>();

            foreach (var item in DB.VerNotificacionesRegistradas())
            {
                ListNotifi.Add(new E_Notificaciones()
                {

                    IDnotificacion = item.IDnotificacion,
                    Notificacion = item.Notificacion,
                    Estado = item.Estado,
                    Fechanotifi = item.Fechanotifi,
                    UrlNotificacion = item.UrlNotificacion,
                    Visible = item.Visible,
                    Idusuario = item.Idusuario,
                    Idtiponotificacion = item.Idtiponotificacion,
                    DetalleNotificacion = item.DetalleNotificacion,
                    DetalleTipoNotificacion = item.DetalleTipoNotificacion,
                    Icono = item.Icono,
                    General = item.General,
                    Publicado=item.Publicado


                });
            }

            return ListNotifi;
        }






        //Función para eliminar notificación
        public int EliminarNotificacion(int Idnotifi)
        {


            int r = 1;

            try
            {

                r = Convert.ToInt32(DB.EliminarNotificacion(Idnotifi).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }






        //Función para eliminar todas las notificaciones
        public int EliminarTodoNotificaciones(int Iduser)
        {


            int r = 1;

            try
            {

                r = Convert.ToInt32(DB.EliminarTodoNotificaciones(Iduser).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }






    }
}
