using System;
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
                throw new NaoEncontradoException("Médico não encontrado !");

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
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<MedicoDto> SelecionarMedicosAtivos()
        {
            return _medicoRepositorio.SelecionarMedicosAtivos();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Inserir(Medico entity)
        {
            //Verifica campos nulos
            if (!VerificaCamposObrigatorios(entity))
                throw new ConflitoException("Por favor preencha todos os campos obrigatórios !");
            
            //Verifica se os campos Email e Senha estão preenchidos
            if (String.IsNullOrEmpty(entity.Email) || String.IsNullOrEmpty(entity.Senha))
                throw new ConflitoException("Email ou senha não estão preenchidos !");

            var emailExistente = _medicoRepositorio.SelecionarPorEmail(entity.Email);

            //Verifica se já existe um usuario com o Email já cadastrado
            if (emailExistente != null)
                throw new ConflitoException($"Já existe usuário cadastrado com Email {emailExistente.Email}!");

            var crmExistente = _medicoRepositorio.SelecionarPorCrm(entity.Crm);

            //Verifica CRM existente
            if (crmExistente != null)
                throw new ConflitoException($"Já existe cadastrado o CRM {crmExistente.Crm}!");

            var cpfExistente = _medicoRepositorio.SelecionarPorCpf(entity.Cpf);

            //Verifica CPF existente
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
            var emailExistente = _medicoRepositorio.SelecionarPorEmailPorId(entity.Email,id);
            //Verifica se já existe um usuario com o Email já cadastrado
            if (emailExistente != null)
                throw new ConflitoException($"Já existe usuário cadastrado com Email {emailExistente.Email}!");

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void MudarAtivoMedico(int id)
        {
            var obj = _medicoRepositorio.SelecionarCampoAtivo(id);

            if (obj.Ativo == true)
            {
                _medicoRepositorio.DesativarMedico(id);
            }
            else
            {
                _medicoRepositorio.AtivarMedico(id);
            }

           
        }

        // Verifica se os campos obrigatórios estão preenchidos
        private bool VerificaCamposObrigatorios(Medico entity)
        {
            if (String.IsNullOrEmpty(entity.Email) || String.IsNullOrEmpty(entity.Senha) || String.IsNullOrEmpty(entity.Sexo.ToString()))
            {
                return false;
            }

            if (String.IsNullOrEmpty(entity.Nome) || String.IsNullOrEmpty(entity.Cpf) || String.IsNullOrEmpty(entity.Crm))
            {
                return false;
            }

            if(String.IsNullOrEmpty(entity.Telefone_r) || String.IsNullOrEmpty(entity.Telefone_c) || String.IsNullOrEmpty(entity.Endereco_c))
            {
                return false;
            }

            if (String.IsNullOrEmpty(entity.Cidade) || String.IsNullOrEmpty(entity.Estado) || String.IsNullOrEmpty(entity.IdEspecialidade.ToString()))
            {
                return false;
            }

            return true;

        }
    }
}
