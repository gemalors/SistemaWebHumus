using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Negocio.Entidades;

namespace Negocio.Metodos
{
    public class M_Cuestionarios
    {
        
        DBHumusEntities DB = new DBHumusEntities();



        //Función para registrar cuestionario
        public int RegistrarCuestionario(string nombre, string descripcion, string fechacreado,string asignado, string tipo,int numc)
        {


            int r = 3;

            try
            {

                r = Convert.ToInt32(DB.RegitrarCuestionario(nombre, descripcion, fechacreado,asignado,tipo,numc).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }






        //Función apara insertar asiganción de cuestionario de evaluación de pares académicos
        public int InsertarAsignacion(int numc, int Idp,string fecha)
        {


            int r = 1;

            try
            {

                r = Convert.ToInt32(DB.InsertarAsignacionCuestionario(numc,Idp,fecha).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }











        //Función para finalizar cuestionario
        public int FinalizarCuestionario(int Iduser, int Idc, string fecha, int Ida)
        {


            int r = 3;

            try
            {

                r = Convert.ToInt32(DB.FinalizarCuestionario(Iduser, Idc, fecha, Ida).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }









        //Función para editar fecha de edición de asignación de cuestionario
        public int EditarFechaAsigancion(int Iduser, int Idc, string fecha, int Ida)
        {


            int r = 3;

            try
            {

                r = Convert.ToInt32(DB.EditarFechaEdicionAsignacion(Iduser, Idc, fecha, Ida).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }









        //Función para ver detalle de asignación de cuestionario
        public List<E_AsignacionCuestionario> VerDetalleAsignacion(int Idasignacion)
        {
            List<E_AsignacionCuestionario> Listdetalle = new List<E_AsignacionCuestionario>();

            foreach (var item in DB.VerDetalleAsignacion(Idasignacion))
            {
                Listdetalle.Add(new E_AsignacionCuestionario()
                {
                    
            


                    IDAsignacionCuestionario = item.IDAsignacionCuestionario,
                    Fechafinalizado = item.Fechafinalizado,
                    PrimerNombre = item.PrimerNombre,
                    SegundoNombre = item.SegundoNombre,
                    ApellidoPrimero = item.ApellidoPrimero,
                    ApellidoSegundo=item.ApellidoSegundo,
                    Idcuestionario=item.Idcuestionario,
                    Email = item.Email,
                    Nombre =item.Nombre,
                    FechaAsignado=item.FechaAsignado,
                    Idtipousuario=item.Idtipousuario,
                    Detalleusuario = item.Detalleusuario,
                    Idpersona=item.Idpersona,
                    TipoCuestionario=item.TipoCuestionario,
                    UsuarioAsignado=item.UsuarioAsignado,
                    Finalizado=item.Finalizado,
                    FechaAsignacion = item.FechaAsignacion







                });
            }

            return Listdetalle;
        }





        //Función para listar cuestionarios
        public List<E_Cuestionarios> ListarCuestionarios()
        {
            List<E_Cuestionarios> ListCuestionarios = new List<E_Cuestionarios>();

            foreach (var item in DB.ListarCuestionarios())
            {
                ListCuestionarios.Add(new E_Cuestionarios()
                {
                    IDcuestionario = item.IDcuestionario,
                    Nombre = item.Nombre,
                    Fechacreado=item.Fechacreado,
                    Estado=item.Estado,
                    Descripcion = item.Descripcion,
                    NumeroCuestionario=item.NumeroCuestionario,
                    FechaAsignado = item.FechaAsignado,
                    Idpreg=item.Idpreg,
                    UsuarioAsignado=item.UsuarioAsignado,
                    TipoCuestionario=item.TipoCuestionario,
                    Finalizado=item.Finalizado,


                });
            }

            return ListCuestionarios;
        }




      


        //Función para ver cuestionario por Idcuestionario y tipo de usuario
        public List<E_Cuestionarios> VerCuestionario(int Idcuestionario, string tipouser)
        {
            List<E_Cuestionarios> ListCuestionarios = new List<E_Cuestionarios>();

            foreach (var item in DB.VerDetalleCuestionario(Idcuestionario,tipouser))
            {
                ListCuestionarios.Add(new E_Cuestionarios()
                {
                    IDcuestionario = item.IDcuestionario,
                    Nombre = item.Nombre,
                    Fechacreado = item.Fechacreado,
                    Estado = item.Estado,
                    Descripcion = item.Descripcion,
                    UsuarioAsignado = item.UsuarioAsignado,
                    FechaAsignado = item.FechaAsignado,
                    Idpreg = item.Idpreg,
                    NumeroCuestionario = item.NumeroCuestionario,
                    TipoCuestionario = item.TipoCuestionario,




                });
            }

            return ListCuestionarios;
        }





        //Función para ver cuestionario por Idcuestionario 
        public List<E_Cuestionarios> VerCuestionarioRegistrado(int Idcuestionario)
        {
            List<E_Cuestionarios> ListCuestionarios = new List<E_Cuestionarios>();

            foreach (var item in DB.VerCuestionarioRegistrado(Idcuestionario))
            {
                ListCuestionarios.Add(new E_Cuestionarios()
                {
                    IDcuestionario = item.IDcuestionario,
                    Nombre = item.Nombre,
                    Fechacreado = item.Fechacreado,
                    Estado = item.Estado,
                    Descripcion = item.Descripcion,
                    UsuarioAsignado = item.UsuarioAsignado,
                    FechaAsignado = item.FechaAsignado,
                    Idpreg = item.Idpreg,
                    NumeroCuestionario = item.NumeroCuestionario,
                    TipoCuestionario = item.TipoCuestionario,
                    Finalizado=item.Finalizado



                });
            }

            return ListCuestionarios;
        }






        //Función para ver respuesta de pregunta tipo 4 pregunta abierta con varias respuestas de cuestionario
        public List<E_Respuestas> VerRespuestaPregTipo4(int Iduser, int Idc, int Ida)
        {
            List<E_Respuestas> ListRespuesta = new List<E_Respuestas>();

            foreach (var item in DB.VerRespuestaPregTipo4(Iduser,Idc,Ida))
            {
                ListRespuesta.Add(new E_Respuestas()
                {

                   
                    IDrespuesta=item.IDrespuesta, 
                    Idpregunta=item.Idpregunta,
                    DescripcionRespuestaAbierta=item.DescripcionRespuestaAbierta,
                    titulorespabierta=item.Titulorespabierta,
                    Idobservacionpreg=item.Idobservacionpreg,
                    Respuestas=item.Respuestas

                });;
            }

            return ListRespuesta;
        }





        //Función para ver respuesta de pregunta abierta de cuestionario
        public List<E_Respuestas> VerRespuestaPregAbierta(int Iduser, int Idc, int Ida)
        {
            List<E_Respuestas> ListRespuesta = new List<E_Respuestas>();

            foreach (var item in DB.VerRespuestaPregAbierta(Iduser, Idc, Ida))
            {
                ListRespuesta.Add(new E_Respuestas()
                {


                    IDrespuesta = item.IDrespuesta,
                    Idpregunta = item.Idpregunta,
                    DescripcionRespuestaAbierta = item.DescripcionRespuestaAbierta


                }); ;
            }

            return ListRespuesta;
        }









        //Función para ver respuesta de pregunta de opciones de cuestionario (tipo de opciones y respuestas)
        public List<E_Respuestas> VerRespuestaPregOpcionesTipo1(int Iduser, int Idc,int Ida)
        {
            List<E_Respuestas> ListRespuesta = new List<E_Respuestas>();

            foreach (var item in DB.VerRespuestaPregOpcionesTipo1(Iduser, Idc,Ida))
            {
                ListRespuesta.Add(new E_Respuestas()
                {


                   Idpregunta=item.Idpregunta, 
                   IDrespuesta=item.IDrespuesta, 
                   Idopcionpreg=item.Idopcionpreg, 
                   IdrespuestaLogica=item.IdrespuestaLogica, 
                   DescripcionOpcionRespuesta=item.DescripcionOpcionRespuesta, 
                   descripOpcpreg=item.descripOpcpreg



                });
            }

            return ListRespuesta;
        }






        //Función para ver respuesta de pregunta de opciones de cuestionario (tipo de solo respuestas)
        public List<E_Respuestas> VerRespuestaPregOpcionesTipo2(int Iduser, int Idc,int Ida)
        {
            List<E_Respuestas> ListRespuesta = new List<E_Respuestas>();

            foreach (var item in DB.VerRespuestaPregOpcionesTipo2(Iduser, Idc, Ida))
            {
                ListRespuesta.Add(new E_Respuestas()
                {


                    Idpregunta = item.Idpreg,
                    IDrespuesta = item.IDrespuesta,
                    Idopcionpreg = item.Idopcionpreg,
                    IdrespuestaLogica = item.IdrespuestaLogica,
                    DescripcionOpcionRespuesta = item.DescripcionOpcionRespuesta
                   


                });
            }

            return ListRespuesta;
        }






    




        //Función para editar cuestionario
        public int EditarCuestionario(int Idcuestionario, string nombre, string descripcion, string fechacreado, string asignado, string tipo)
        {
            int r = 3;
            try
            {
                r = Convert.ToInt32(DB.EditarCuestionario(Idcuestionario, nombre, descripcion, fechacreado,asignado,tipo).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }






      




        //Función para editar Idpregunta de cuestionario
        public int EditarCuestionarioIdpreg(int IdP,int Idc)
        {
            int r = 3;
            try
            {
                r = Convert.ToInt32(DB.EditarIdpregCuestionario(IdP,Idc).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }





        //Función para eliminar cuestionario
        public int EliminarCuestionario(int Id)
        {
            int r = 1;
            try
            {
                DB.EliminarCuestionarios(Id);

            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }




        //Función para registrar y editar respuestas a cuestionario asignado para pares académicos
        public int InsertarEditarRespuestasPreguntasEA(int idc,int ida,int idresp, int idob,int idpregunta, int user, int idresplogica, string respabierta, string titulorespabierta, int idopcionpreg,int identificadorTipoPregunta, string tipoOpciones, bool estado,int respuestas)
        {
          

            int r = 3;

            try
            {

                r = Convert.ToInt32(DB.RespuestasPreguntasCuestionario(idc,ida,idresp,idob,idpregunta,user,idresplogica, respabierta, titulorespabierta, idopcionpreg,identificadorTipoPregunta, tipoOpciones, estado,respuestas).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }






        //Función para eliminar asignación de cuestionario
        public int EliminarAsignacionCuestionarioUsuario(int Ida)
        {


            int r = 1;

            try
            {

                r = Convert.ToInt32(DB.EliminarAsignacionCuestionarioUsuario(Ida).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }








        //Función para eliminar respuesta de cuestionario
        public int EliminarRespuestaObservacion(int Id)
        {


            int r = 1;

            try
            {

                r = Convert.ToInt32(DB.EliminarRespuesta(Id).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }







    }
}
