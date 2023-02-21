using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Negocio.Entidades;

namespace Negocio.Metodos
{
  public  class M_DatosContactoEditorial
    {

        DBHumusEntities DB = new DBHumusEntities();

    


        //Función para editar datos de contacto de Editorial en página web
        public int EditarDatosContacto(int Iddatos, string email, string horario, string direccion, string telefono)
        {
            
            int r = 1;

            try
            {

                r = Convert.ToInt32(DB.EditarDatosContacto(Iddatos,email,horario,direccion,telefono).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }

   





        //Función para ver datos de contacto de Editorial en página web
        public List<E_DatosContactoEditorial> VerDatosContactoEditorial()
        {
            List<E_DatosContactoEditorial> ListDatos = new List<E_DatosContactoEditorial>();

            foreach (var item in DB.VerDatosContacto())
            {
                ListDatos.Add(new E_DatosContactoEditorial()
                {

                 IDContactoEditorial=item.IDContactoEditorial, 
                 Email=item.Email, 
                 Horario=item.Horario,
                 Direccion=item.Direccion,
                 Telefono=item.Telefono



                });
            }

            return ListDatos;
        }
    
    
    
    
    
    }
}
