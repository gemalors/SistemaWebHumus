using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Negocio.Entidades;
using SimpleCrypto;


namespace Negocio.Metodos
{
   public class M_Usuarios
    {
        DBHumusEntities DB = new DBHumusEntities();
       




        //Función para listar tipo de usuarios
        public List<E_Tipousuarios> VerTiposUsuarios()
        {
            List<E_Tipousuarios> listatipos = new List<E_Tipousuarios>();

            foreach(var item in DB.ConsultarTipoUsuario())
            {
                listatipos.Add(new E_Tipousuarios() {
                    IDtipousuario=item.IDtipousuario,
                    Detalleusuario=item.Detalleusuario
                });
            }

            return listatipos;
        }






        //Función para listar todos los usuarios registrados
        public List<E_Usuarios> VerUsuarios()
        {
            List<E_Usuarios> listausers = new List<E_Usuarios>();
            
            foreach (var item in DB.ListarUsuarios())
            {
               listausers.Add(new E_Usuarios()
                {
                    IDusuario=item.IDusuario,
                   PrimerNombre = item.PrimerNombre,
                   SegundoNombre = item.SegundoNombre,
                   Foto =item.Foto,
                   ApellidoPrimero = item.ApellidoPrimero,
                   ApellidoSegundo = item.ApellidoSegundo,
                   Email = item.Email,
                    Username = item.Username,
                   Password = item.Password,
                   Tipo_usuario =item.Tipo_usuario,
                   Telefono=item.Telefono,
                   Direccion=item.Direccion,
                   Filial=item.Filial,
                   Token_recovery = item.Token_recovery,
                   Estado=item.Estado,
                   Librosfinalizados=item.Librosfinalizados,
                   Librosproceso=item.Librosproceso




               });
            }

            return listausers;
        }





       


        //pendiente eliminar
        //Función para listar datos de usuario mediante el Idusuario
        public List<E_Usuarios> VerDetalleUsuario(int Id)
        {
            List<E_Usuarios> listausers = new List<E_Usuarios>();

            foreach (var item in DB.DetalleUsuario(Id))
            {
                listausers.Add(new E_Usuarios()
                {
                    IDusuario = item.IDusuario,
                    PrimerNombre = item.PrimerNombre,
                    SegundoNombre = item.SegundoNombre,
                    ApellidoPrimero = item.ApellidoPrimero,
                    ApellidoSegundo = item.ApellidoSegundo,
                    Email = item.Email,
                    Username = item.Username,
                   
                    Telefono = item.Telefono,
                    Direccion = item.Direccion,
                    Filial = item.Filial,
                   




                });
            }

            return listausers;
        }





        //Función para registrar usuarios
        public int Crearusuarios(string Nombre1,string Nombre2, string ApellidoP,string ApellidoS,string Email, string Username, string password,string Foto, int IdTipousuario, string Telefono, string Direccion, string Filial,int libproceso)
        {
            int r = 0;
            string Password;

            try
            {
                ICryptoService cryptoService = new PBKDF2();
                string Salt = cryptoService.GenerateSalt();
                string passEncriptada = cryptoService.Compute(password);
                Password = passEncriptada;
                
                r = Convert.ToInt32(DB.RegistroUsuario(Nombre1,Nombre2, ApellidoP,ApellidoS, Email, Username, Password, Salt, Foto, IdTipousuario, Telefono, Direccion, Filial,libproceso).FirstOrDefault());
            }
            catch(Exception)
            {
                r = 0;
            }
            return r;
        }







        //Función para modificar datos de dusuarios por parte del administrador//
        public int ModificarUsuarios(int Iduser,string Nombre1,string Nombre2, string ApellidoP,string ApellidoS, string Email, string Username, string password, string Telefono, string Direccion, string Foto,string Filial)
        {
            int r = 3;
            if (Foto == "")
            {
                Foto = null;
            }
            string Password;
            if (password != "")
            {
                try
                {
                    ICryptoService cryptoService = new PBKDF2();
                    string Salt = cryptoService.GenerateSalt();
                    string passEncriptada = cryptoService.Compute(password);
                    Password = passEncriptada;

                    r = Convert.ToInt32(DB.EditarUsuario(Iduser, Nombre1,Nombre2, ApellidoP,ApellidoS, Email, Username, Password, Salt, Telefono, Direccion).FirstOrDefault());
                }
                catch (Exception)
                {
                    r = 3;
                }
            }
            else
            {   
                r = Convert.ToInt32(DB.EditarPerfil2(Iduser, Nombre1,Nombre2, ApellidoP,ApellidoS, Email,Filial, Telefono, Direccion, Foto).FirstOrDefault());
            }
            return r;
        }





        //Función para editar token
        public int EditarToken(string email, string token_recovery)
        {
            int r = 3;
            
                try
                {
                    

                    r = Convert.ToInt32(DB.EditarToken(email,token_recovery).FirstOrDefault());
                }
                catch (Exception)
                {
                    r = 3;
                }
            
              
            return r;
        }








        //Función para login de usuarios - obtener datos de usuario logeado
        public List<E_Usuarios> LoginUsuarios(string Username, string password)
        {
          
        
            List<E_Usuarios> listauser = new List<E_Usuarios>();

            string Password = password;


            foreach (var item in DB.LoginUsers(Username, Password))
            {
                listauser.Add(new E_Usuarios()
                {
                   IDusuario=item.IDusuario,
                    PrimerNombre = item.PrimerNombre,
                    SegundoNombre = item.SegundoNombre,
                    ApellidoPrimero = item.ApellidoPrimero,
                    ApellidoSegundo = item.ApellidoSegundo,
                    Username = item.Username,
                    Salt = item.Salt,
                    Tipo_usuario = item.Tipo_usuario




                });


            }
                return listauser;
        }






        //Función para eliminar usuarios
        public int EliminarUsuarios(int Iduser)
        {
            int r = 3;
            

            try
            {
        

                r = Convert.ToInt32(DB.EliminarUsuario(Iduser).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }


        




        //Función para mostrar datos de usuario por usuario/cédula/username
        public List<E_Usuarios> BuscarUsuario(string Username)
        {
            List<E_Usuarios> listausers = new List<E_Usuarios>();

            foreach (var item in DB.ObtenerUsuario(Username))
            {
                listausers.Add(new E_Usuarios()
                {

                    PrimerNombre = item.PrimerNombre,
                    SegundoNombre = item.SegundoNombre,
                    IDusuario =item.IDusuario,
                    Foto = item.Foto,
                    ApellidoPrimero = item.ApellidoPrimero,
                    ApellidoSegundo = item.ApellidoSegundo,
                    Email = item.Email,
                    Filial=item.Filial,
                    Telefono=item.Telefono,
                    Direccion=item.Direccion,
                    Username = item.Username,
                    Password = item.Password,
                    Idtipousuario=item.Idtipousuario,
                    Tipo_usuario = item.Detalleusuario,
                    Salt = item.Salt,
                    Estado = item.Estado




                }) ; 
            }

            return listausers;
        }






        //Función para mostrar datos de usuarios por tipo usuario//
        public List<E_Usuarios> BuscarUsuarioxTipoUsuario(string tipousuer)
        {
            List<E_Usuarios> listausers = new List<E_Usuarios>();

            foreach (var item in DB.ObtenerUsuarioxTipoUsuario(tipousuer))
            {
                listausers.Add(new E_Usuarios()
                {

                    PrimerNombre = item.PrimerNombre,
                    SegundoNombre = item.SegundoNombre,
                    IDusuario = item.IDusuario,
                    Foto = item.Foto,
                    ApellidoPrimero = item.ApellidoPrimero,
                    ApellidoSegundo = item.ApellidoSegundo,
                    Email = item.Email,
                    Filial = item.Filial,
                    Telefono = item.Telefono,
                    Direccion = item.Direccion,
                    Username = item.Username,
                    Password = item.Password,
                    Idtipousuario = item.Idtipousuario,
                    Tipo_usuario = item.Detalleusuario,
                    Salt = item.Salt,
                    Estado = item.Estado




                });
            }

            return listausers;
        }








        //Función para verificar token//
        public List<E_Usuarios> VerificarToken(string token)
        {
            List<E_Usuarios> listausers = new List<E_Usuarios>();

            foreach (var item in DB.VerificarToken(token))
            {
                listausers.Add(new E_Usuarios()
                {

                    PrimerNombre = item.PrimerNombre,
                    SegundoNombre = item.SegundoNombre,
                    IDusuario = item.IDusuario,
                    ApellidoPrimero = item.ApellidoPrimero,
                    ApellidoSegundo = item.ApellidoSegundo,
                    Email = item.Email,
                    Username = item.Username,
                    Tipo_usuario = item.Detalleusuario
                });
            }

            return listausers;
        }







        //Función para editar perfil de usuario
        public int EditarPerfil(int Iduser, string Nombre1,string Nombre2, string ApellidoP,string ApellidoS, string Email, string Filial, string Telefono, string Direccion,string Pass,  string Foto)
        {
            int r = 3;
            string Password;
            if (Pass != "")
            {
                try
                {
                    ICryptoService cryptoService = new PBKDF2();
                   string Salt = cryptoService.GenerateSalt();
                    string passEncriptada = cryptoService.Compute(Pass);
                    Password = passEncriptada;
                    r = Convert.ToInt32(DB.EditarPerfil(Iduser, Nombre1,Nombre2, ApellidoP,ApellidoS, Email, Filial, Telefono, Direccion, Password, Salt, Foto).FirstOrDefault());
                }
                catch (Exception)
                {
                    r = 3;
                }
            }
            else
            {
                r = Convert.ToInt32(DB.EditarPerfil2(Iduser, Nombre1,Nombre2, ApellidoP,ApellidoS, Email, Filial,Telefono, Direccion, Foto).FirstOrDefault());
            }
           
            return r;
        }








        //Función para eliminar foto de usuario
        public int EliminarFoto(int Iduser)
        {
            int r = 3;
         
                try
                {
                   
                    r = Convert.ToInt32(DB.EliminarFoto(Iduser).FirstOrDefault());
                }
                catch (Exception)
                {
                    r = 3;
                }
          

            return r;
        }





        //Función para deshabilitar usuario
        public int DeshabilitarUsuario(int Iduser)
        {
            int r = 3;

            try
            {

                r = Convert.ToInt32(DB.DeshabilitarUsuario(Iduser).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }


            return r;
        }






        //Función para recuperar contraseña
        public int RecuperarPass(string token, string Pass)
        {
            int r = 3;
            string password;
           
                try
                {
                    ICryptoService cryptoService = new PBKDF2();
                    string Salt = cryptoService.GenerateSalt();
                    string passEncriptada = cryptoService.Compute(Pass);
                    password = passEncriptada;
                    r = Convert.ToInt32(DB.RecuperarPass(token,password, Salt).FirstOrDefault());
                }
                catch (Exception)
                {
                    r = 3;
                }
            

            return r;
        }




        //Función para actualizar número de libros finalizados de usuarios
        public int EditarNumLibrosFinalizados(int Iduser)
        {
            int r = 3;
         

            try
            {
              
                r = Convert.ToInt32(DB.EditarNumLibrosFinalizados(Iduser).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }


            return r;
        }





      




    }
}