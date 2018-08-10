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
    [Route("api/Consulta")]
    public class ConsultaController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        private ConsultaNegocio _ConsultaNegocio;


        /// <summary>
        /// 
        /// </summary>
        public ConsultaController()
        {
            _ConsultaNegocio = new ConsultaNegocio();

        }

        /// <summary>
        /// Método que obtem uma lista de consulta
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(ConsultaDto), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult Get()
        {
            return Ok(_ConsultaNegocio.Selecionar());
        }

        /// <summary>
        /// Método que seleciona um Consulta
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(PacienteDto), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetId(int id)
        {
            return Ok(_ConsultaNegocio.SelecionarPorId(id));
        }


        /// <summary>
        /// Metodo que retorna consultas pelo Id do Paciente
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("paciente/{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(ConsultaDto), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetIdPaciente(int id)
        {
            return Ok(_ConsultaNegocio.SelecionarPorPaciente(id));
        }

        /// <summary>
        /// Metodo que retorna consultas pelo Id do Medico
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("medico/{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(ConsultaDto), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetIdMedico(int id)
        {
            return Ok(_ConsultaNegocio.SelecionarPorMedico(id));
        }

        /// <summary>
        /// Metodo que retorna consultas pelo Status
        /// </summary>
        /// <param name="Status"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("status/{Status}")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(ConsultaDto), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetStatus(char Status)
        {
            return Ok(_ConsultaNegocio.SelecionarPorStatus(Status));
        }

        /// <summary>
        /// Metodo que retorna consultas pela Data
        /// </summary>
        /// <param name="DataConsulta"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("data/{DataConsulta}")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(ConsultaDto), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetStatus(DateTime DataConsulta)
        {
            return Ok(_ConsultaNegocio.SelecionarPorData(DataConsulta));
        }

        /// <summary>
        /// Método que insere um consulta
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, typeof(Consulta), nameof(HttpStatusCode.Created))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public IActionResult Post([FromBody]ConsultaInput input)
        {

            var objConsulta = new Consulta()
            {
                IdPaciente = input.IdPaciente,
                IdMedico = input.IdMedico,
                DataConsulta = input.DataConsulta,
                Horario = input.Horario,
                Status = input.Status,
            };

            var idConsulta = _ConsultaNegocio.Inserir(objConsulta);
            objConsulta.Id = idConsulta;
            return CreatedAtRoute(nameof(GetId), new { id = idConsulta }, objConsulta);
        }

        /// <summary>
        /// Método que altera uma consulta
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.Accepted, typeof(Consulta), nameof(HttpStatusCode.Accepted))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public IActionResult Put([FromRoute]int id, [FromBody]ConsultaInput input)
        {
            var objConsulta = new Consulta()
            {
                Status = input.Status
            };

            var obj = _ConsultaNegocio.Alterar(id, objConsulta);
            return Accepted(obj);
        }

        /// <summary>
        /// Método que deleta um consulta
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[HttpDelete]
        //[Route("{id}")]
        //[SwaggerResponse((int)HttpStatusCode.OK)]
        //[SwaggerResponse((int)HttpStatusCode.NotFound)]
        //public IActionResult Delete([FromRoute]int id)
        //{
        //    _ConsultaNegocio.Deletar(id);
        //    return Ok();
        //}
    }
}