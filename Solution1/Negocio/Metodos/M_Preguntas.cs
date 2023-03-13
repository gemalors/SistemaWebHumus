using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Negocio.Entidades;

namespace Negocio.Metodos
{
    public class M_Preguntas
    {
        DBHumusEntities DB = new DBHumusEntities();





        //Función para insertar pregunta
        public int InsertarPregunta(int Idc, int Idtipopreg, string descripcionpreg, bool obligatorio, int ordenpreg, string leyendasup, string tiposopciones, int idpregA)
        {



            int r = 0;

            try
            {

                r = Convert.ToInt32(DB.InsertarPregunta(Idc,Idtipopreg, descripcionpreg, obligatorio,ordenpreg,leyendasup,tiposopciones,idpregA).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 0;
            }
            return r;
        }




        //Función para insertar pregunta de tipo abierta
        public int InsertarPreguntaAbierta(int idtipodato, int Idtipreg, bool especificarRango, string valormax, string valormin)
        {
            


            int r = 0;

            try
            {

                r = Convert.ToInt32(DB.InsertarPreguntaAbierta(idtipodato,Idtipreg,especificarRango,valormax,valormin).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 0;
            }
            return r;
        }








        //Función para insertar pregunta de tipo 4 pregunta de observaciones
        public int InsertarPreguntaObservaciones(int Idpreg, string detalleobser, int ordenobser, string leyenda,bool respuesta )
        {



            int r = 3;

            try
            {

                r = Convert.ToInt32(DB.InsertarPreguntaObservaciones(Idpreg,detalleobser,ordenobser,leyenda,respuesta).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }







        //Función para editar pregunta de tipo 4 pregunta de observaciones
        public int EditarPreguntaObservaciones(int idpregobser, string detalleobser, string leyenda,bool respuesta)
        {



            int r = 3;

            try
            {

                r = Convert.ToInt32(DB.EditarPreguntaObservaciones(idpregobser, detalleobser, leyenda,respuesta).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }





        //Función para eliminar pregunta de tipo 4 pregunta de observaciones
        public int EliminarPreguntaObservaciones(int idpregobser)
        {



            int r = 0;

            try
            {

                r = Convert.ToInt32(DB.EliminarPreguntaObservaciones(idpregobser).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }






        //Función para insertar pregunta de tipo opciones - selección
        public int InsertarPreguntaSeleccion(int Idpreg, string descripcionpregS, int identificadorpregS)
        {

           

            int r = 3;

            try
            {

                r = Convert.ToInt32(DB.InsertarPreguntaSeleccion(Idpreg, descripcionpregS,identificadorpregS).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }





        //Función para insertar opciones de respuesta para pregunta de selección
        public int InsertarOpcionesRespuestaPreguntaSeleccion(string descripcionopresp, int ordenopresp, int idpreg)
        {


            int r = 3;

            try
            {

                r = Convert.ToInt32(DB.InsertarOpcionesRespuestaPreguntaSeleccion(descripcionopresp,ordenopresp,idpreg).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }






        //Función para editar pregunta
        public int EditarPregunta(int Idpreg, int Idc, int Idtipopreg, string descripcionpreg, bool obligatorio, string leyendasup,string tiposopciones,int orden)
        {



            int r = 3;

            try
            {

                r = Convert.ToInt32(DB.EditarPregunta(Idpreg, Idc, Idtipopreg, descripcionpreg, obligatorio, leyendasup,tiposopciones, orden).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }






        //Función para editar pregunta de tipo abierta
        public int EditarPreguntaAbierta(int idtipodato, int IdtipregA, bool especificarRango, string valormax, string valormin)
        {



            int r = 3;

            try
            {

                r = Convert.ToInt32(DB.EditarPreguntaAbierta(idtipodato, IdtipregA, especificarRango, valormax, valormin).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }





        //Función para editar pregunta de opciones - selección
        public int EditarOpcionPreguntaSeleccion(int Idopcion, string descripcionop)
        {



            int r = 3;

            try
            {

                r = Convert.ToInt32(DB.EditarOpcionPreguntaSeleccion(Idopcion, descripcionop).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }







        //Función para editar opciones de respuesta de pregunta
        public int EditarOpcionRespuestaPreguntaSeleccion(int Idopresp, string descripcionopresp)
        {


            int r = 3;

            try
            {

                r = Convert.ToInt32(DB.EditarOpcionRespuestaPreguntaSeleccion(Idopresp, descripcionopresp).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }







        //Función para ver tipos de preguntas
        public List<E_TipoPregunta> ListarTipodePregunta()
        {
            List<E_TipoPregunta> ListTipoPregunta = new List<E_TipoPregunta>();

            foreach (var item in DB.ListarTipodePregunta())
            {
                ListTipoPregunta.Add(new E_TipoPregunta()
                {

                IDtipopregunta=item.IDtipopregunta,
                IdentificadorTipoPregunta = item.IdentificadorTipoPregunta,
                tipopregunta = item.tipopregunta, 
               


                });
            }

            return ListTipoPregunta;
        }





        //Función para ver tipo de dato de pregunta abierta
        public List<E_TipodeDato> ListarTipodeDatoPreguntaAbierta()
        {
            List<E_TipodeDato> ListTipodeDatoPregunta = new List<E_TipodeDato>();

            foreach (var item in DB.ListarTipodeDatoPreguntaAbierta())
            {
                ListTipodeDatoPregunta.Add(new E_TipodeDato()
                {

                   IDtipodato=item.IDtipodato,
                    TipoHtml=item.TipoHtml,
                    IdentificadorTipoDato = item.IdentificadorTipoDato,
                    DescripcionTipoDato = item.DescripcionTipoDato,
                    


                });
            }

            return ListTipodeDatoPregunta;
        }





        //Función para ver pregunta insertada
        public List<E_Preguntas> VerPreguntaInsertada(int Idpreg)
        {
            List<E_Preguntas> ListPregunta = new List<E_Preguntas>();

            foreach (var item in DB.VerPreguntaInsertada(Idpreg))
            {
                ListPregunta.Add(new E_Preguntas()
                {
                    IDpregunta=item.IDpregunta,
                    Idtipopregunta=item.Idtipopregunta,
                    Descripcion = item.Descripcion,
                    Obligatorio=item.Obligatorio,
                    Orden=item.Orden,
                    LeyendaSuperior=item.LeyendaSuperior,
                    Idcuestionario=item.Idcuestionario,
                    IdentificadorTipoPregunta = item.IdentificadorTipoPregunta,
                    TiposOpciones=item.TiposOpciones,
                    tipopregunta =item.tipopregunta,
                    IdpregAnterior=item.IdpregAnterior




                });
            }

            return ListPregunta;
        }






        //Función para ver opciones de pregunta de selección x Idpregunta
        public List<E_OpcionPreguntaSeleccion> VerOpcionesdePreguntaSeleccion(int Idpreg)
        {
            List<E_OpcionPreguntaSeleccion> ListOpcionesdePregunta = new List<E_OpcionPreguntaSeleccion>();

            foreach (var item in DB.VerOpcionesdePreguntaSeleccion(Idpreg))
            {
                ListOpcionesdePregunta.Add(new E_OpcionPreguntaSeleccion()
                {
                    IDopcionPreguntaSeleccion=item.IDopcionPreguntaSeleccion,
                    Idpregunta=item.Idpregunta,
                    descripOpcpreg = item.descripOpcpreg,
                    IdentificadorOpcionPregunta = item.IdentificadorOpcionPregunta


                });
            }

            return ListOpcionesdePregunta;
        }





        //Función para ver opciones de respuesta de pregunta de selección x Idpregunta
        public List<E_OpcionesRespuesta> VerOpcionesRespuestaSeleccion(int Idpreg)
        {
            List<E_OpcionesRespuesta> ListOpcionesRespuesta = new List<E_OpcionesRespuesta>();

            foreach (var item in DB.VerOpcionesRespuestaSeleccion(Idpreg))
            {
                ListOpcionesRespuesta.Add(new E_OpcionesRespuesta()
                {

                    IDrespuestaopcion = item.IDrespuestaopcion,
                    OrdenOpcionRespuesta = item.OrdenOpcionRespuesta,
                    DescripcionOpcionRespuesta = item.DescripcionOpcionRespuesta,
                    Idpreg=item.Idpreg
    

                });
            }

            return ListOpcionesRespuesta;
        }





        //Función para ver pregunta abierta x Idcuestionario
        public List<E_PreguntaAbierta> VerPreguntaAbierta(int Idcuestionario)
        {
            List<E_PreguntaAbierta> ListPregunta = new List<E_PreguntaAbierta>();

            foreach (var item in DB.ConsultarPreguntaAbierta(Idcuestionario))
            {
                ListPregunta.Add(new E_PreguntaAbierta()
                {
                    IDpreguntaAbierta = item.IDpreguntaAbierta,
                    Idpregunta =item.IDpregunta,
                    Idcuestionario=item.Idcuestionario,
                    Idtipodato=item.IDtipodato,
                    IdentificadorTipoDato = item.IdentificadorTipoDato,
                    TipoHtml=item.TipoHtml,
                    DescripcionTipoDato = item.DescripcionTipoDato,
                    EspecificarRango =item.EspecificarRango,
                    ValorMax=item.ValorMax,
                    ValorMin=item.ValorMin,

                    


                });
            }

            return ListPregunta;
        }





        //Función para ver pregunta tipo 4 observaciones x Idcuestionario
        public List<E_PreguntaObservaciones> VerPreguntaObservaciones(int Idcuestionario)
        {
            List<E_PreguntaObservaciones> ListPregunta = new List<E_PreguntaObservaciones>();

            foreach (var item in DB.ConsultarPreguntaObservaciones(Idcuestionario))
            {
                ListPregunta.Add(new E_PreguntaObservaciones()
                {
                    Idcuestionario=item.Idcuestionario,
                    IDpreguntaObservaciones=item.IDpreguntaObservaciones, 
                    Idpregunta=item.Idpreg, 
                    Leyendainferior=item.Leyendainferior, 
                    Detallepregobservacion=item.Detallepregobservacion, 
                    Ordenobservacion=item.Ordenobservacion,
                    Respuestas=item.Respuestas



                });
            }

            return ListPregunta;
        }




        //Función para ver pregunta tipo 4 observaciones x Idpregunta
        public List<E_PreguntaObservaciones> VerPreguntaObservacionesxIdpreg(int Idpreg)
        {
            List<E_PreguntaObservaciones> ListPregunta = new List<E_PreguntaObservaciones>();

            foreach (var item in DB.VerPreguntaObservacionesxIdpreg(Idpreg))
            {
                ListPregunta.Add(new E_PreguntaObservaciones()
                {
                    
                    IDpreguntaObservaciones = item.IDpreguntaObservaciones,
                    Idpregunta = item.Idpregunta,
                    Leyendainferior = item.Leyendainferior,
                    Detallepregobservacion = item.Detallepregobservacion,
                    Ordenobservacion = item.Ordenobservacion,
                     Respuestas = item.Respuestas


                });
            }

            return ListPregunta;
        }







        //Función para ver preguntas de selección  x Idcuestionario
        public List<E_OpcionPreguntaSeleccion> VerPreguntaSeleccion(int Idcuestionario)
        {
            List<E_OpcionPreguntaSeleccion> ListPregunta = new List<E_OpcionPreguntaSeleccion>();

            foreach (var item in DB.ConsultarPreguntaSeleccion(Idcuestionario))
            {
                ListPregunta.Add(new E_OpcionPreguntaSeleccion()
                {
                    Idpregunta=item.Idpregunta,
                    Idcuestionario = item.Idcuestionario,
                    descripOpcpreg = item.descripOpcpreg,
                    IDopcionPreguntaSeleccion = item.IDopcionPreguntaSeleccion,
                    IdentificadorOpcionPregunta = item.IdentificadorOpcionPregunta


                });
            }

            return ListPregunta;
        }






        //Función para ver opciones de respuesta de pregunta de selección  x Idcuestionario
        public List<E_OpcionesRespuesta> VerOpcionRespuestaSeleccion(int Idcuestionario)
        {
            List<E_OpcionesRespuesta> ListPregunta = new List<E_OpcionesRespuesta>();

            foreach (var item in DB.ConsultarOpcionesRespuestaPreguntaSeleccion(Idcuestionario))
            {
                ListPregunta.Add(new E_OpcionesRespuesta()
                {
                    Idpreg=item.Idpreg,
                    Idcuestionario = item.Idcuestionario,
                    DescripcionOpcionRespuesta = item.DescripcionOpcionRespuesta,
                    OrdenOpcionRespuesta = item.OrdenOpcionRespuesta,
                    IdentificadorTipoPregunta = item.IdentificadorTipoPregunta,
                    IDrespuestaopcion = item.IDrespuestaopcion
                  


                });
            }

            return ListPregunta;
        }





        //Función para ver preguntas registradas x Idcuestionario
        public List<E_Preguntas> ConsultarPreguntas(int Idcuestionario)
        {
            List<E_Preguntas> ListPregunta = new List<E_Preguntas>();

            foreach (var item in DB.ConsultarPreguntas(Idcuestionario))
            {
                ListPregunta.Add(new E_Preguntas()
                {
                    IDpregunta = item.IDpregunta,
                    Descripcion=item.Descripcion,
                    Obligatorio =item.Obligatorio,
                    Orden=item.Orden,
                    LeyendaSuperior=item.LeyendaSuperior,
                    Idcuestionario = item.Idcuestionario,
                    Idtipopregunta=item.IDtipopregunta,
                    IdentificadorTipoPregunta = item.IdentificadorTipoPregunta,
                    TiposOpciones=item.TiposOpciones,
                    IdpregAnterior=item.IdpregAnterior


                });
            }

            return ListPregunta;
        }







        //Función para ver pregunta abierta registrada x Idcuestionario
        public List<E_PreguntaAbierta> VerPreguntaAbiertaInsertada(int Idpreg)
        {
            List<E_PreguntaAbierta> ListPregunta = new List<E_PreguntaAbierta>();

            foreach (var item in DB.VerPreguntaAbierta(Idpreg))
            {
                ListPregunta.Add(new E_PreguntaAbierta()
                {

                   IDpreguntaAbierta=item.IDpreguntaAbierta,
                    EspecificarRango=item.EspecificarRango, 
                    ValorMax=item.ValorMax,
                    ValorMin=item.ValorMin,
                    Idtipodato=item.IDtipodato,
                    Idpregunta=item.Idpregunta,
                    DescripcionTipoDato=item.DescripcionTipoDato,
                    TipoHtml =item.TipoHtml, 
                   


                });
            }

            return ListPregunta;
        }







        //Función para eliminar pregunta abierta
        public int EliminarPreguntaAbierta(int IdpregA)
        {



            int r = 3;

            try
            {

                r = Convert.ToInt32(DB.EliminarPreguntaAbierta(IdpregA).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }





        //Función para eliminar pregunta 
        public int EliminarPregunta(int Idpreg)
        {



            int r = 3;

            try
            {

                r = Convert.ToInt32(DB.EliminarPregunta(Idpreg).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }







      

        //Función para eliminar opción de pregunta de selección
        public int EliminarOpcionPreguntaSeleccion(int Idopcion)
        {



            int r = 3;

            try
            {

                r = Convert.ToInt32(DB.EliminarOpcionPreguntaSeleccion(Idopcion).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }






        //Función para eliminar opción de respuesta de pregunta de selección
        public int EliminarOpcionRespuestaPreguntaSeleccion(int Idopresp)
        {
           

            int r = 3;

            try
            {

                r = Convert.ToInt32(DB.EliminarOpcionRespuestaPreguntaSeleccion(Idopresp).FirstOrDefault());
            }
            catch (Exception)
            {
                r = 3;
            }
            return r;
        }






    }

}
