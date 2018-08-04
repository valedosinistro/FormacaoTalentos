using System.Net;
using Fatec.Clinica.Negocio;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;


namespace Fatec.Clinica.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Admin")]
    public class AdminController : Controller
    {
        private AdminNegocio _adminNegocio;

        public AdminController()
        {
            _adminNegocio = new AdminNegocio();

        }

        /// <summary>
        /// Método que verifica e atualiza status de Ativo/Desativo ADM do Médico
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Medico/MudarAtivoAdm/{id}")]
        [SwaggerResponse((int)HttpStatusCode.Accepted)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public IActionResult MudarAtivoMedicoAdmin([FromRoute]int id)
        {
            _adminNegocio.MudarAtivoMedicoAdmin(id);
            return Accepted();
        }

        /// <summary>
        /// Método que verifica e atualiza status de Ativo/Desativo ADM do Paciente
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Paciente/MudarAtivoAdm/{id}")]
        [SwaggerResponse((int)HttpStatusCode.Accepted)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public IActionResult MudarAtivoPacienteAdmin([FromRoute]int id)
        {
            _adminNegocio.MudarAtivoPacienteAdmin(id);
            return Accepted();
        }


    }
}