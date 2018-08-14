using System;
using System.Collections.Generic;
using System.Text;
using Fatec.Clinica.Dado;
using Fatec.Clinica.Dominio;
using Fatec.Clinica.Dominio.Dto;
using Fatec.Clinica.Dominio.Excecoes;

namespace Fatec.Clinica.Negocio
{
    /// <summary>
    /// 
    /// </summary>
    public class PacienteNegocio
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly PacienteRepositorio _pacienteRepositorio;

        /// <summary>
        /// 
        /// </summary>
        public PacienteNegocio()
        {
            _pacienteRepositorio = new PacienteRepositorio();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PacienteDto> Selecionar()
        {
            return _pacienteRepositorio.Selecionar();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PacienteDto SelecionarPorId(int id)
        {
            var obj = _pacienteRepositorio.SelecionarPorId(id);

            if (obj == null)
                throw new NaoEncontradoException("Paciente não encontrado !");

            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Inserir(Paciente entity)
        {
            //Verifica campos nulos
            if (!VerificaCamposObrigatorios(entity))
                throw new ConflitoException("Por favor preencha todos os campos obrigatórios !");
            
            //Verifica se os campos Email e Senha estão preenchidos
            if (String.IsNullOrEmpty(entity.Email) || String.IsNullOrEmpty(entity.Senha))
                throw new ConflitoException("Email ou senha não estão preenchidos !");

            var emailExistente = _pacienteRepositorio.SelecionarPorEmail(entity.Email);

            //Verifica se já existe um usuario com o Email já cadastrado
            if (emailExistente != null)
                throw new ConflitoException($"Já existe usuário cadastrado com Email {emailExistente.Email}!");

            var cpfExistente = _pacienteRepositorio.SelecionarPorCpf(entity.Cpf);

            //Verifica CPF existente
            if (cpfExistente != null)
                throw new ConflitoException($"Já existe cadastrado o CPF {cpfExistente.Cpf}!");


            return _pacienteRepositorio.Inserir(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public PacienteDto Alterar(int id, Paciente entity)
        {
            var emailExistente = _pacienteRepositorio.SelecionarPorEmailPorId(entity.Email,id);
            //Verifica se já existe um Paciente com o Email já cadastrado
            if (emailExistente != null)
                throw new ConflitoException($"Já existe usuário cadastrado com Email {emailExistente.Email}!");

            entity.Id = id;
            _pacienteRepositorio.Alterar(entity);

            return _pacienteRepositorio.SelecionarPorId(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void Deletar(int id)
        {
            var obj = SelecionarPorId(id);

            _pacienteRepositorio.Deletar(obj.Id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void MudarAtivoPaciente(int id)
        {
            var obj = _pacienteRepositorio.SelecionarCampoAtivo(id);

            if (obj.Ativo == true)
            {
                _pacienteRepositorio.DesativarPaciente(id);
            }
            else
            {
                _pacienteRepositorio.AtivarPaciente(id);
            }

           
        }

        // Verifica se os campos obrigatórios estão preenchidos
        private bool VerificaCamposObrigatorios(Paciente entity)
        {
            if (String.IsNullOrEmpty(entity.Email) || String.IsNullOrEmpty(entity.Senha))
            {
                return false;
            }

            if (String.IsNullOrEmpty(entity.Nome) || String.IsNullOrEmpty(entity.Cpf) || String.IsNullOrEmpty(entity.Data_Nasc.ToString()))
            {
                return false;
            }

            if(String.IsNullOrEmpty(entity.Telefone) || String.IsNullOrEmpty(entity.Sexo.ToString()))
            {
                return false;
            }

            return true;

        }
    }
}
