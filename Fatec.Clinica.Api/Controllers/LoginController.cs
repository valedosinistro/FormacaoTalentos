using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Fatec.Clinica.Api.Model;
using Fatec.Clinica.Dominio;
using Fatec.Clinica.Dominio.Dto;
using Fatec.Clinica.Negocio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Fatec.Clinica.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    [Route("api/Login")]
    public class LoginController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        private LoginNegocio _LoginNegocio;


        /// <summary>
        /// 
        /// </summary>
        public LoginController()
        {
            _LoginNegocio = new LoginNegocio();

        }

        /// <summary>
        /// Método de Login Paciente
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Paciente")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(PacienteDto), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public IActionResult LoginPaciente([FromBody]LoginInput input)
        {

            return Ok(_LoginNegocio.LoginPaciente(input.user,input.senha));
        }

        /// <summary>
        /// Método de Login Medico
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Medico")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(MedicoDto), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public IActionResult LoginMedico([FromBody]LoginInput input)
        {

            return Ok(_LoginNegocio.LoginMedico(input.user, input.senha));
        }
    }
}