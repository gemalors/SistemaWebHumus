using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Negocio.Entidades;

namespace Negocio.Metodos
{
   public class M_Comentarios
    {
        DBHumusEntities DB = new DBHumusEntities();




        //Función para registrar comentario
        public int InsertarComentarios(int Idproceso,int Idusuario, string Comentario, string Fechacoment)
        {
            int r = 0;
       
            try
            {

                r = Convert.ToInt32(DB.InsertarComentario(Idproceso,Comentario,Fechacoment,Idusuario).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 0;
            }
            return r;
        }




        //Función para listar comentarios para cada proceos x Idproceso
        public List<E_Comentarios> VerComentarios(int Idprocess)
        {
            List<E_Comentarios> listacoment = new List<E_Comentarios>();

            foreach (var item in DB.VerComentarios(Idprocess))
            {
                listacoment.Add(new E_Comentarios()
                {
                    IDcomentario=item.IDcomentario,
                    Comentario=item.Comentario,
                    Fecha=item.Fecha,
                    Idproceso=item.Idproceso,
                    PrimerNombre = item.PrimerNombre,
                    SegundoNombre = item.SegundoNombre,
                    Foto = item.Foto,
                    ApellidoPrimero = item.ApellidoPrimero,
                    ApellidoSegundo=item.ApellidoSegundo,
                    Idusuario=item.IDusuario,
                    Idlibro=item.Idlibro


    });
            }

            return listacoment;
        }





        //Función para eliminar comentario
        public int EliminarComentario(int Idcomentario)
        {
            int r = 1;
            try
            {
                DB.EliminarComentario(Idcomentario);

            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }





    }
}
