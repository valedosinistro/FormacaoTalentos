using System.Net;
using Fatec.Clinica.Api.Model;
using Fatec.Clinica.Dominio;
using Fatec.Clinica.Negocio;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Fatec.Clinica.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Especialidade")]
    public class EspecialidadeController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        private EspecialidadeNegocio _especialidadeNegocio;

        /// <summary>
        /// 
        /// </summary>
        public EspecialidadeController()
        {
            _especialidadeNegocio = new EspecialidadeNegocio();
        }

        /// <summary>
        /// Método que obtem uma lista de métodos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Especialidade), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult Get()
        {
            return Ok(_especialidadeNegocio.Selecionar());
        }

        /// <summary>
        /// Método que seleciona um médico..
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}", Name="GetId")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Especialidade), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetId(int id)
        {
            return Ok(_especialidadeNegocio.SelecionarPorId(id));
        }

        /// <summary>
        /// Método que insere um médico..
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, typeof(Especialidade), nameof(HttpStatusCode.Created))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public IActionResult Post([FromBody]EspecialidadeInput input)
        {
            var obj = new Especialidade()
            {
                Nome = input.Nome
            };

            var idEspecialidade = _especialidadeNegocio.Inserir(obj);
            obj.Id = idEspecialidade;
            return CreatedAtRoute("GetId", new { id = idEspecialidade }, obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.Accepted, typeof(Medico), nameof(HttpStatusCode.Accepted))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public IActionResult Put([FromRoute]int id, [FromBody]MedicoInput input)
        {
            var obj = new Especialidade()
            {
                Nome = input.Nome
            };

            var objReturn = _especialidadeNegocio.Alterar(id, obj);
            return Accepted(objReturn);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult Delete([FromRoute]int id)
        {
            _especialidadeNegocio.Deletar(id);
            return Ok();
        }
    }
}