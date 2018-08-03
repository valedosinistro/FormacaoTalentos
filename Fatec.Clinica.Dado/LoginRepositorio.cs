using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using Fatec.Clinica.Dado.Configuracao;
using Fatec.Clinica.Dominio;
using Fatec.Clinica.Dominio.Dto;
using System.Data.SqlClient;

namespace Fatec.Clinica.Dado
{
    /// <summary>
    /// 
    /// </summary>
    public class LoginRepositorio
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PacienteDto LoginPacienteEmail(string email, string senha)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.QueryFirstOrDefault<PacienteDto>($"SELECT P.Id, P.Email, P.Nome, P.Cpf, P.Sexo, P.Telefone, P.Data_Nasc, P.Ativo, P.Ativo_Adm FROM [Paciente] P WHERE P.Email = '{email}' AND P.Senha = '{senha}'");
                return obj;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PacienteDto LoginPacienteCpf(string cpf, string senha)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.QueryFirstOrDefault<PacienteDto>($"SELECT P.Id, P.Email, P.Nome, P.Cpf, P.Sexo, P.Telefone, P.Data_Nasc, P.Ativo, P.Ativo_Adm FROM [Paciente] P WHERE P.Cpf = '{cpf}' AND P.Senha = '{senha}'");
                return obj;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MedicoDto LoginMedicoEmail(string email, string senha)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.QueryFirstOrDefault<MedicoDto>($"SELECT M.Id,M.Email, M.Sexo, M.Nome, M.Cpf, M.Crm, M.IdEspecialidade, M.Telefone_r, M.Telefone_c, M.Endereco_C, M.Cidade, M.Estado, M.Ativo, M.Ativo_Adm, E.Nome As Especialidade " +
                                                                 $"FROM [Medico] M " +
                                                                 $"JOIN [Especialidade] E ON M.IdEspecialidade = E.Id " +
                                                                 $"WHERE M.Email = '{email}' AND M.Senha = '{senha}'");
                return obj;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MedicoDto LoginMedicoCpf(string cpf, string senha)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.QueryFirstOrDefault<MedicoDto>($"SELECT M.Id,M.Email, M.Sexo, M.Nome, M.Cpf, M.Crm, M.IdEspecialidade, M.Telefone_r, M.Telefone_c, M.Endereco_C, M.Cidade, M.Estado, M.Ativo, M.Ativo_Adm, E.Nome As Especialidade " +
                                                                 $"FROM [Medico] M " +
                                                                 $"JOIN [Especialidade] E ON M.IdEspecialidade = E.Id " +
                                                                 $"WHERE M.Cpf = '{cpf}' AND M.Senha = '{senha}'");
                return obj;
            }
        }
    }
}
