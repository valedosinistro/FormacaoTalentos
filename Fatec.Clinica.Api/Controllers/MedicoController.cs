using System;
using System.Net;
using Fatec.Clinica.Api.Model;
using Fatec.Clinica.Dominio;
using Fatec.Clinica.Dominio.Dto;
using Fatec.Clinica.Negocio;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Fatec.Clinica.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    [Route("api/Medico")]
    public class MedicoController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        private MedicoNegocio _medicoNegocio;
       

        /// <summary>
        /// 
        /// </summary>
        public MedicoController()
        {
            _medicoNegocio = new MedicoNegocio();
           
        }

        /// <summary>
        /// Método que obtem uma lista de médicos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(MedicoDto), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult Get()
        {
            return Ok(_medicoNegocio.Selecionar());
        }

        /// <summary>
        /// Método que seleciona um médico
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(MedicoDto), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetId(int id)
        {
            return Ok(_medicoNegocio.SelecionarPorId(id));
        }

        /// <summary>
        /// Método que obtem uma lista de médicos por especialidade
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Especialidade/{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(MedicoDto), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetEspecialidadeId(int id)
        {
            return Ok(_medicoNegocio.SelecionarPorEspecialidade(id));
        }

        /// <summary>
        /// Método que obtem uma lista de médicos ativos
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Ativos")]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(MedicoDto), nameof(HttpStatusCode.OK))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public IActionResult GetMedicosAtivos()
        {
            return Ok(_medicoNegocio.SelecionarMedicosAtivos());
        }

        /// <summary>
        /// Método que insere um médico
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, typeof(Medico), nameof(HttpStatusCode.Created))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public IActionResult Post([FromBody]MedicoInput input)
        {
 
            var objMedico = new Medico()
            {
                Cpf = input.Cpf,
                Crm =  input.Crm,
                IdEspecialidade = input.IdEspecialidade,
                Nome = input.Nome,
                Email = input.Email,
                Senha = input.Senha,
                Sexo = input.Sexo,
                Telefone_c = input.Telefone_c,
                Telefone_r = input.Telefone_r,
                Endereco_c = input.Endereco_c,
                Estado = input.Estado,
                Cidade = input.Cidade,
                Ativo = true,
                Ativo_Adm = true

            };

            var idMedico = _medicoNegocio.Inserir(objMedico);
            objMedico.Id = idMedico;
            return CreatedAtRoute(nameof(GetId), new { id = idMedico }, objMedico);
        }

        /// <summary>
        /// Método que altera um médico
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.Accepted, typeof(Medico), nameof(HttpStatusCode.Accepted))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public IActionResult Put([FromRoute]int id, [FromBody]MedicoAlteraInput input)
        {
            var objMedico = new Medico()
            {
                Telefone_c = input.Telefone_c,
                Telefone_r = input.Telefone_r,
                Endereco_c = input.Endereco_c,
                Cidade = input.Cidade,
                Estado = input.Estado,
                Email = input.Email,
                Senha = input.Senha
            };

            var obj = _medicoNegocio.Alterar(id, objMedico);
            return Accepted(obj);
        }

        /// <summary>
        /// Método que ativa/desativa um médico
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("MudarAtivo/{id}")]
        [SwaggerResponse((int)HttpStatusCode.Accepted)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public IActionResult MudarAtivo([FromRoute]int id)
        {
            _medicoNegocio.MudarAtivoMedico(id);
            return Accepted();
        }


        /// <summary>
        /// Método que deleta um médico
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[HttpDelete]
        //[Route("{id}")]
        //[SwaggerResponse((int)HttpStatusCode.OK)]
        //[SwaggerResponse((int)HttpStatusCode.NotFound)]
        //public IActionResult Delete([FromRoute]int id)
        //{
        //    _medicoNegocio.Deletar(id);
        //    return Ok();
        //}
    }
}