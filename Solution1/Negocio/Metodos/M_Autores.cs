using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Negocio.Entidades;
using Datos;

namespace Negocio.Metodos
{
   public class M_Autores
    {
        DBHumusEntities DB = new DBHumusEntities();





        //Función para agregar datos de autores de libro
        public int InsertarAutor(int IDlibro,string Nombre1,string Nombre2, string ApellidoP, string ApellidoS, string Cedula, string Email, string Telefono, string Direccion, string Filial)
        {
            int r = 0;

            try
            {

                r = Convert.ToInt32(DB.InsertarAutor(IDlibro,Nombre1,Nombre2,ApellidoP,ApellidoS,Cedula,Email,Telefono,Direccion, Filial).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 0;
            }
            return r;
        }








        //Función para listar datos de autores de libro
        public List<E_Tablainter1> VerAutores(int IDlibro)
        {
            List<E_Tablainter1> listAutores = new List<E_Tablainter1>();

            foreach (var item in DB.VerAutores(IDlibro))
            {
                listAutores.Add(new E_Tablainter1()
                {

                  
                    IDtablainter=item.IDtablainter,
                    Idautor=item.Idautor,
                    Idlibro=item.Idlibro,
                    PrimerNombre=item.PrimerNombre,
                    SegundoNombre=item.SegundoNombre,
                    ApellidoPrimero=item.ApellidoPrimero,
                    ApellidoSegundo=item.ApellidoSegundo,
                    Cedula=item.Cedula,
                    Email=item.Email,
                    Filial=item.Filial,
                    Telefono=item.Telefono,
                    Direccion=item.Direccion,
                    Titulo=item.Titulo,
                    inicialnombre=item.inicialnombre


                });;
            }

            return listAutores;
        }







        //Función para editar datos de autores de libro
        public int EditarAutor(int Idautor, string Nombre1, string Nombre2, string ApellidoP, string ApellidoS, string Cedula , string Email, string Telefono,  string Direccion, string Filial)
        {
            int r = 1;
            try
            {
                r = Convert.ToInt32(DB.EditarAutor(Idautor, Nombre1,Nombre2, ApellidoP,ApellidoS, Cedula, Email,Telefono,Direccion,Filial).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }







        //Función para eliminar autores de libro
        public int EliminarAutor(int Idautor)
        {
            int r = 1;
            try
            {
                DB.EliminarAutor(Idautor);

            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }






    }
}
