using System.Collections.Generic;
using Fatec.Clinica.Dado;
using Fatec.Clinica.Dominio;
using Fatec.Clinica.Dominio.Dto;
using Fatec.Clinica.Dominio.Excecoes;

namespace Fatec.Clinica.Negocio
{
    /// <summary>
    /// 
    /// </summary>
    public class MedicoNegocio
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly MedicoRepositorio _medicoRepositorio;

        /// <summary>
        /// 
        /// </summary>
        public MedicoNegocio()
        {
            _medicoRepositorio = new MedicoRepositorio();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<MedicoDto> Selecionar()
        {
            return _medicoRepositorio.Selecionar();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MedicoDto SelecionarPorId(int id)
        {
            var obj = _medicoRepositorio.SelecionarPorId(id);

            if (obj == null)
                throw new NaoEncontradoException();

            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<MedicoDto> SelecionarPorEspecialidade(int id)
        {
           return _medicoRepositorio.SelecionarPorEspecialidade(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Inserir(Medico entity)
        {
            var crmExistente = _medicoRepositorio.SelecionarPorCrm(entity.Crm);

            if(crmExistente != null)
                throw new ConflitoException($"Já existe cadastrado o CRM {crmExistente.Crm}!");

            var cpfExistente = _medicoRepositorio.SelecionarPorCpf(entity.Cpf);

            if (cpfExistente != null)
                throw new ConflitoException($"Já existe cadastrado o CPF {cpfExistente.Cpf}!");


            return _medicoRepositorio.Inserir(entity);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public MedicoDto Alterar(int id, Medico entity)
        {
            var crmExistente = _medicoRepositorio.SelecionarPorCrm(entity.Crm);

            if (crmExistente != null)
            {
                if (crmExistente.Id != id)
                    throw new ConflitoException($"Já existe cadastrado o CRM {crmExistente.Crm}, para outro médico!");
            }

            var cpfExistente = _medicoRepositorio.SelecionarPorCpf(entity.Cpf);

            if (cpfExistente != null)
            {
                if (cpfExistente.Id != id)
                    throw new ConflitoException($"Já existe cadastrado o CPF {cpfExistente.Cpf}, para outro médico!");
            }

            entity.Id = id;
            _medicoRepositorio.Alterar(entity);

            return _medicoRepositorio.SelecionarPorId(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void Deletar(int id)
        {
            var obj = SelecionarPorId(id);

            _medicoRepositorio.Deletar(obj.Id);
        }
    }
}
