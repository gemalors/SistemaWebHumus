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
   public class M_Evaluadores
    {
        DBHumusEntities DB = new DBHumusEntities();




        //Función para asignar evaluador a un libro
        public int AsignarEvaluador(int IDlibro, int IDproceso, int IDevaluador, string estadoasignacion, int num)
        {
            int r = 0;

            
            try
            {


                r = Convert.ToInt32(DB.AsignarEvaluador(IDlibro, IDproceso, IDevaluador, estadoasignacion,num).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 0;
            }
            return r;
        }







        //Función para editar estado de asiganción de evaluador x Admin
        public int EditarEstadoAsignacionEvaluador(int IDproceso, int IDevaluador, string estadoasignacion)
        {
            int r = 3;


            try
            {


                r = Convert.ToInt32(DB.EditarEstadoAsignacionEvaluador(IDproceso, IDevaluador, estadoasignacion).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }






        //Función para editar estado de asiganción de evaluador2 x Evaluador
        public int EditarEstadoAsignacionEvaluador2(int IDproceso, string estadoasignacion, int Ida, int Ide)
        {
            int r = 3;


            try
            {


                r = Convert.ToInt32(DB.EditarEstadoAsignacionEvaluador2(IDproceso, estadoasignacion,Ida,Ide).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }






        //Función para editar número de libros finalizados de evaluador
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







        //Función para ver evaluadores de libro x Idlibro
        public List<E_Tablainter2> VerEvaluadores(int IDlibro)
        {
            List<E_Tablainter2> listEvaluadores = new List<E_Tablainter2>();

            foreach (var item in DB.VerEvaluadores(IDlibro))
            {
                listEvaluadores.Add(new E_Tablainter2()
                {

                    Idevaluador=item.IDusuario,
                    Idlibro = item.Idlibro,
                    PrimerNombre = item.PrimerNombre,
                    SegundoNombre = item.SegundoNombre,
                    ApellidoPrimero = item.ApellidoPrimero,
                    ApellidoSegundo = item.ApellidoSegundo,
                    Cedula = item.Cedula,
                    Email = item.Email,
                    IdasignacionEvaluacion=item.IdasignacionEvaluacion,
                    NumEvaluador=item.NumEvaluador,
                    Estado=item.estado_asignacion,


                });
            }

            return listEvaluadores;
        }






        //Función para ver detalle de asignación de evaluador
        public List<E_Tablainter2> VerDetalleAsignacionEvaluador(int idproceso, int idevaluador)
        {
            List<E_Tablainter2> list = new List<E_Tablainter2>();

            foreach (var item in DB.VerDetalleAsignacionEvaluador(idproceso,idevaluador))
            {
                list.Add(new E_Tablainter2()
                {
                    

                    IDtablainter2=item.IDtablainter2,
                    Idevaluador = item.Idevaluador,
                    Idproceso=item.Idproceso,
                    Estado=item.estado_asignacion,
                    NumEvaluador = item.NumEvaluador,
                    Idlibro=item.Idlibro,
                    IdasignacionEvaluacion=item.IdasignacionEvaluacion



                });
            }

            return list;
        }






        //Función para ver listado de evaluadores
        public List<E_Usuarios> ListaEvaluadores()
        {
            List<E_Usuarios> listEvaluador = new List<E_Usuarios>();

            foreach (var item in DB.ListarEvaluadores())
            {
                listEvaluador.Add(new E_Usuarios()
                {
                    IDusuario = item.IDusuario,
                    PrimerNombre = item.PrimerNombre,
                    SegundoNombre = item.SegundoNombre,
                    ApellidoPrimero = item.ApellidoPrimero,
                    ApellidoSegundo = item.ApellidoSegundo,
                    Username = item.Username,
                    Email = item.Email,
                    Cedula=item.Cedula,
                    Librosfinalizados=item.Librosevaluados,
                    Librosproceso=item.Librosevaluando, 
                    Foto=item.Foto,
                    Estado=item.Estado
                   



                });
            }

            return listEvaluador;
        }









    }





}

