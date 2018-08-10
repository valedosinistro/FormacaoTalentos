using Dapper;
using Fatec.Clinica.Dado.Configuracao;
using Fatec.Clinica.Dominio;
using Fatec.Clinica.Dominio.Dto;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Fatec.Clinica.Dado
{
    /// <summary>
    /// 
    /// </summary>
    public class PacienteRepositorio
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PacienteDto> Selecionar()
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var lista = connection.Query<PacienteDto>($"SELECT P.Id, P.Email, P.Nome, P.Cpf, P.Sexo, P.Telefone, P.Data_Nasc, P.Ativo, P.Ativo_Adm FROM [Paciente] P");
                return lista;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PacienteDto SelecionarPorId(int id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.QueryFirstOrDefault<PacienteDto>($"SELECT P.Id, P.Email, P.Nome, P.Cpf, P.Sexo, P.Telefone, P.Data_Nasc, P.Ativo, P.Ativo_Adm FROM [Paciente] P WHERE P.Id = {id}");
                return obj;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        public PacienteDto SelecionarPorCpf(string cpf)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.QueryFirstOrDefault<PacienteDto>($"SELECT * " +
                                                                 $"FROM [Paciente] " +
                                                                 $"WHERE Cpf = '{cpf}'");
                return obj;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public PacienteDto SelecionarPorEmail(string email)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.QueryFirstOrDefault<PacienteDto>($"SELECT * " +
                                                                 $"FROM [Paciente] " +
                                                                 $"WHERE Email = '{email}'");
                return obj;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public PacienteDto SelecionarPorEmailPorId(string email, int id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.QueryFirstOrDefault<PacienteDto>($"SELECT * " +
                                                                 $"FROM [Paciente] " +
                                                                 $"WHERE Email = '{email}' AND Id != {id}");
                return obj;

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Inserir(Paciente entity)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                return connection.QuerySingle<int>($"DECLARE @ID int;" +
                                              $"INSERT INTO [Paciente] " +
                                              $"(Email,Senha, Nome, Cpf, Telefone, Sexo, Data_Nasc, Ativo, Ativo_Adm) " +
                                                    $"VALUES (" +
                                                            $"'{entity.Email}'," +
                                                            $"'{entity.Senha}'," +
                                                            $"'{entity.Nome}'," +
                                                            $"'{entity.Cpf}'," +
                                                            $"'{entity.Telefone}'," +
                                                            $"'{entity.Sexo}'," +
                                                            $"'{entity.Data_Nasc}'," +
                                                            $"'{entity.Ativo}'," +
                                                            $"'{entity.Ativo_Adm}')" +
                                              $"SET @ID = SCOPE_IDENTITY();" +
                                              $"SELECT @ID");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public void Alterar(Paciente entity)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                connection.Execute($"UPDATE [Paciente] " +
                                   $"SET  Telefone = '{entity.Telefone}'," +
                                   $"Email = '{entity.Email}'," +
                                   $"Senha = '{entity.Senha}' " +
                                   $"WHERE Id = {entity.Id}");
            }
        }


        /// <summary>
        /// Desativar Paciente
        /// </summary>
        /// <param name="id"></param>
        /// 
        public void DesativarPaciente(int id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                connection.Execute($"UPDATE [Paciente]" +
                                   $"SET Ativo = 0" +
                                   $"WHERE Id = {id}");
            }

        }

        /// <summary>
        /// Ativar Paciente
        /// </summary>
        /// <param name="id"></param>
        /// 
        public void AtivarPaciente(int id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                connection.Execute($"UPDATE [Paciente]" +
                                   $"SET Ativo = 1" +
                                   $"WHERE Id = {id}");
            }

        }

        /// <summary>
        /// Selecionar campo ativo do Paciente especifico
        /// </summary>
        /// <param name="id"></param>
        /// 
        public Paciente SelecionarCampoAtivo(int id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {

                var obj = connection.QueryFirstOrDefault<Paciente>($"SELECT Ativo " +
                                                                 $"FROM [Paciente] " +
                                                                 $"WHERE Id = '{id}'");
                return obj;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void Deletar(int id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                connection.Execute($"DELETE " +
                                   $"FROM [Paciente] " +
                                   $"WHERE Id = {id}");
            }
        }
    }
}
