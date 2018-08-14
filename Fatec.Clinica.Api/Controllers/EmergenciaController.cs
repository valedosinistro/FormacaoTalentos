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
    [Produces("application/json")]
    [Route("api/Emergencia")]
    public class EmergenciaController : Controller
    {
        private EmergenciaNegocio _EmergenciaNegocio;


        /// <summary>
        /// 
        /// </summary>
        public EmergenciaController()
        {
            _EmergenciaNegocio = new EmergenciaNegocio();

        }

        /// <summary>
        /// Método que exibi emergencia para o médico
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("",Name = "GetEmergencia")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Emergencia), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult ExibirEmergenciaParaOMedico()
        {
            return Ok(_EmergenciaNegocio.ExibirEmergenciaParaOMedico());
        }

        /// <summary>
        /// Método que cria uma Emergencia
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, typeof(Emergencia), nameof(HttpStatusCode.Created))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public IActionResult Post([FromBody]EmergenciaCriarInput input)
        {
            var obj = new Emergencia()
            {
               IdPaciente = input.IdPaciente,
               DataHora = DateTime.Now
            };

            var idEmergencia = _EmergenciaNegocio.CriarEmergencia(obj);

            return CreatedAtRoute("GetEmergencia", obj);
        }

        /// <summary>
        ///  Método que altera uma status para Atendendo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idMedico"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}/atendendo/medico/{idMedico}")]
        [SwaggerResponse((int)HttpStatusCode.Accepted, typeof(Emergencia), nameof(HttpStatusCode.Accepted))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public IActionResult PutAtendendo([FromRoute]int idMedico,int id)
        {

            _EmergenciaNegocio.AlterarStatusAtendendo(idMedico,id);
            return Accepted();
        }

        /// <summary>
        ///  Método que altera uma status para Realizado
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idMedico"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}/realizado")]
        [SwaggerResponse((int)HttpStatusCode.Accepted, typeof(Emergencia), nameof(HttpStatusCode.Accepted))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public IActionResult PutRealizado([FromRoute]int id)
        {

            _EmergenciaNegocio.AlterarStatusRealizada(id);
            return Accepted();
        }
    }
}