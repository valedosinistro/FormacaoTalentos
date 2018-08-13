using Fatec.Clinica.Dado;
using Fatec.Clinica.Dominio;
using Fatec.Clinica.Dominio.Dto;
using Fatec.Clinica.Dominio.Excecoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fatec.Clinica.Negocio
{
    /// <summary>
    /// 
    /// </summary>
    public class ConsultaNegocio
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ConsultaRepositorio _ConsultaRepositorio;

        /// <summary>
        /// 
        /// </summary>
        public ConsultaNegocio()
        {
            _ConsultaRepositorio = new ConsultaRepositorio();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ConsultaDto> Selecionar()
        {
            return _ConsultaRepositorio.Selecionar();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ConsultaDto SelecionarPorId(int id)
        {
            var obj = _ConsultaRepositorio.SelecionarPorId(id);

            if (obj == null)
                throw new NaoEncontradoException("Consulta não encontrada !");

            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdPaciente"></param>
        /// <returns></returns>
        public IEnumerable<ConsultaDto> SelecionarPorPaciente(int IdPaciente)
        {
            var lista = _ConsultaRepositorio.SelecionarPorPaciente(IdPaciente);

            if (lista == null)
                throw new NaoEncontradoException("Consulta não encontrada !");

            return lista;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdMedico"></param>
        /// <returns></returns>
        public IEnumerable<ConsultaDto> SelecionarPorMedico(int IdMedico)
        {
            var lista = _ConsultaRepositorio.SelecionarPorMedico(IdMedico);

            if (lista == null)
                throw new NaoEncontradoException("Consulta não encontrada !");

            return lista;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Status"></param>
        /// <returns></returns>
        public ConsultaDto SelecionarPorStatus(char Status)
        {
            var lista = _ConsultaRepositorio.SelecionarPorStatus(Status);

            if (lista == null)
                throw new NaoEncontradoException("Consulta não encontrada !");

            return lista;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DataConsulta"></param>
        /// <returns></returns>
        public ConsultaDto SelecionarPorStatus(DateTime DataConsulta)
        {
            var lista = _ConsultaRepositorio.SelecionarPorData(DataConsulta);

            if (lista == null)
                throw new NaoEncontradoException("Consulta não encontrada !");

            return lista;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DataConsulta"></param>
        /// <returns></returns>
        public ConsultaDto SelecionarPorData(DateTime DataConsulta)
        {
            var lista = _ConsultaRepositorio.SelecionarPorData(DataConsulta);

            if (lista == null)
                throw new NaoEncontradoException("Consulta não encontrada !");

            return lista;
        }

        ///<summary>
        /// Método que retorna lista de horas possiveis para Agendamento de um determinado médico/data
        ///</summary>
        ///<param name = "DataConsulta" ></ param >
        ///<param name = "idMedico" ></ param >
        /// <returns></returns>
        public List<string> ListaDeHorasDisponiveis(DateTime DataConsulta, int IdMedico)
        {

            //Gera uma lista com as Horas Agendadas do dia do médico
            var listaHorasAgendada = _ConsultaRepositorio.ListaDeHorasAgendada(DataConsulta, IdMedico).ToList();

            //Cria lista com 24hrs 
            var horasTotais = Enumerable.Range(00, 24).Select(i => (DateTime.MinValue.AddHours(i).AddMinutes(0).AddSeconds(0).ToLongTimeString())).ToList();

            //Remove horas agendadas da lista 24hrs
            foreach(ConsultaDto HorasAgendada in listaHorasAgendada)
            {
                horasTotais.Remove(HorasAgendada.Horario.ToString());
            }

            //Retorna horas disponives para consulta
            return horasTotais;

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Inserir(Consulta entity)
        {
            //Verifica campos nulos
            if (!VerificaCamposObrigatorios(entity))
                throw new ConflitoException("Por favor preencha todos os campos obrigatórios !");

            var consultaExistente = _ConsultaRepositorio.SelecionarConsultaPorDataEHoraEMedico(entity.DataConsulta, entity.Horario, entity.IdMedico);

            //Verifica Consultas ao mesmo tempo existente
            if (consultaExistente != null)
                throw new ConflitoException($"Já existe uma consulta nesta Data e Horario com este médico!");

            var MedicoDesativado = _ConsultaRepositorio.VerificaSeOMedicoEstaAtivo(entity.IdMedico);

            //Verifica Consultas ao mesmo tempo existente
            if (MedicoDesativado != null)
                throw new ConflitoException($"Este médico esta desativado");



            return _ConsultaRepositorio.Inserir(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ConsultaDto Alterar(int id, Consulta entity)
        {
            //Verifica campos nulos
            if (!VerificaCamposObrigatoriosAlt(entity))
                throw new ConflitoException("Por favor preencha todos os campos obrigatórios !");

            entity.Id = id;
            _ConsultaRepositorio.Alterar(entity);

            return _ConsultaRepositorio.SelecionarPorId(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void Deletar(int id)
        {
            var obj = SelecionarPorId(id);

            _ConsultaRepositorio.Deletar(obj.Id);
        }


        // Verifica se os campos obrigatórios estão preenchidos
        private bool VerificaCamposObrigatorios(Consulta entity)
        {
            if (String.IsNullOrEmpty(entity.DataConsulta.ToString()) || String.IsNullOrEmpty(entity.Horario.ToString()))
            {
                return false;
            }

            if (String.IsNullOrEmpty(entity.IdMedico.ToString()) || String.IsNullOrEmpty(entity.IdPaciente.ToString()))
            {
                return false;
            }

            return true;

        }


        // Verifica se os campos obrigatórios estão preenchidos
        private bool VerificaCamposObrigatoriosAlt(Consulta entity)
        {
            if (String.IsNullOrEmpty(entity.Status.ToString()))
            {
                return false;
            }
            return true;

        }
    }
}
