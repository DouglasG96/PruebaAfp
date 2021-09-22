using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web;
using System.Web.UI;
using ArquitecturaWebApi.Models.Pacientes;
using ArquitecturaWebApi.Domain;

namespace ArquitecturaWebApi.Controllers
{
    public class PacientesController : ApiController
    {
        PacientesDomain pacientesDomain = new PacientesDomain();

        /// <summary>
        /// GET: api/Pacientes (retorna toda la lista)
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(IEnumerable<PacientesModel>))]
        public IEnumerable<PacientesModel> Get()
        {
            return pacientesDomain.ListPacientes().ToArray();
        }

        /// <summary>
        /// GET: api/Pacientes/5 (retorna los valores de un sólo registro según el valor id)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //public IHttpActionResult Get(int id)
        public HttpResponseMessage Get(int id)
        {
            HttpResponseMessage httpMsg = null;
            var usuario = pacientesDomain.ListPacientes().ToArray().FirstOrDefault((p) => p.idPaciente == id);
            if (usuario == null)
            {
                //NotFound() - en caso que se encuentre el registro;
                httpMsg = Request.CreateErrorResponse(HttpStatusCode.NotFound, "El Id (" + id.ToString() + ") no se encuentra registrado");
            }
            else {
                httpMsg = Request.CreateResponse(HttpStatusCode.OK, usuario);
            }
            return httpMsg;
        }

        // POST: api/Paciente
        /// <summary>
        /// Método para insertar los datos desde la Web API
        /// </summary>
        /// <param name="pacientesModel"></param>
        /// <returns></returns>
        public HttpResponseMessage Post([FromBody]PacientesModel pacientesModel)
        {
            HttpResponseMessage httpMsg = null;
            try
            {
                PacientesModel paciente = new PacientesModel();
                paciente.nombres = pacientesModel.nombres;
                paciente.apellidos = pacientesModel.apellidos;
                paciente.fechaNacimiento = pacientesModel.fechaNacimiento;
                paciente.estado = pacientesModel.estado;

                int result = pacientesDomain.InsertPacientes(paciente);//Invocamos el proceso insertar y capturamos el nuevo ID generado
                httpMsg = Request.CreateResponse(HttpStatusCode.OK, paciente);//Confirm Request

                if (httpMsg.IsSuccessStatusCode) {//Validamos si el registro fue satisfactorio
                    httpMsg = Get(result);//Listamos los datos del nuevo registro ingresado, como parámetro enviamos el ID nuevo generado
                }
                else
                {
                    httpMsg = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Ocurrio problemas al ingresar el registro");
                }
                return httpMsg;
            }
            catch (Exception ex)
            {
                httpMsg = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

            return httpMsg;
        }

        /// <summary>
        /// Método PUT con el fin de actualizar los datos
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pacientesModel"></param>
        /// <returns></returns>
        public HttpResponseMessage Put(int id, [FromBody]PacientesModel pacientesModel)
        {
            HttpResponseMessage httpMsg = null;
            try
            {
                PacientesModel paciente = new PacientesModel();
                paciente.nombres = pacientesModel.nombres;
                paciente.apellidos = pacientesModel.apellidos;
                paciente.fechaNacimiento = pacientesModel.fechaNacimiento;
                paciente.estado = pacientesModel.estado;
                paciente.idPaciente = id;

                pacientesDomain.UpdatePacientes(id, paciente);

                httpMsg = Request.CreateResponse(HttpStatusCode.OK, paciente);//Capturamos la respuesta de la petición del proceso realizado
                return httpMsg;
            }
            catch (Exception ex)
            {
                httpMsg = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return httpMsg;
        }

        // DELETE: api/Paciente/5
        public HttpResponseMessage Delete(int id)
        {
            HttpResponseMessage httpMsg = null;
            try
            {
                PacientesModel paciente = new PacientesModel();
                paciente.idPaciente = id;

                pacientesDomain.DeletePacientes(id);
                httpMsg = Request.CreateResponse(HttpStatusCode.OK, paciente);

                return httpMsg;
            }
            catch (Exception ex)
            {
                httpMsg = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

            return httpMsg;
        }
    }
}