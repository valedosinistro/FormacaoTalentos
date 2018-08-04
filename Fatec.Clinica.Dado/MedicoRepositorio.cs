using Fatec.Clinica.Dominio;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using Fatec.Clinica.Dado.Configuracao;
using Fatec.Clinica.Dominio.Dto;
using System;

namespace Fatec.Clinica.Dado
{
    /// <summary>
    /// 
    /// </summary>
    public class MedicoRepositorio
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<MedicoDto> Selecionar()
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var lista = connection.Query<MedicoDto>($"SELECT M.Id,M.Email, M.Sexo, M.Nome, M.Cpf, M.Crm, M.IdEspecialidade, M.Telefone_r, M.Telefone_c, M.Endereco_C, M.Cidade, M.Estado, M.Ativo, M.Ativo_Adm, E.Nome As Especialidade " +
                                                        $"FROM [Medico] M " +
                                                        $"JOIN [Especialidade] E ON M.IdEspecialidade = E.Id");
                return lista;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MedicoDto SelecionarPorId(int id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.QueryFirstOrDefault<MedicoDto>($"SELECT M.Id,M.Email, M.Sexo, M.Nome, M.Cpf, M.Crm, M.IdEspecialidade, M.Telefone_r, M.Telefone_c, M.Endereco_C, M.Cidade, M.Estado, M.Ativo, M.Ativo_Adm, E.Nome As Especialidade " +
                                                                 $"FROM [Medico] M " +
                                                                 $"JOIN [Especialidade] E ON M.IdEspecialidade = E.Id " +
                                                                 $"WHERE M.Id = {id}");
                return obj;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<MedicoDto> SelecionarPorEspecialidade(int id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.Query<MedicoDto>($"SELECT M.Id,M.Email, M.Nome, M.Sexo, M.Cpf, M.Crm, M.IdEspecialidade, M.Telefone_r, M.Telefone_c, M.Endereco_C, M.Cidade, M.Estado, M.Ativo, M.Ativo_Adm, E.Nome As Especialidade " +
                                                                    $"FROM [Medico] M " +
                                                                    $"JOIN [Especialidade] E ON M.IdEspecialidade = E.Id " +
                                                                    $"WHERE E.Id = {id}");
                return obj;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<MedicoDto> SelecionarMedicosAtivos()
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.Query<MedicoDto>($"SELECT M.Id,M.Email, M.Nome, M.Sexo, M.Cpf, M.Crm, M.IdEspecialidade, M.Telefone_r, M.Telefone_c, M.Endereco_C, M.Cidade, M.Estado, M.Ativo, M.Ativo_Adm, E.Nome As Especialidade " +
                                                                    $"FROM [Medico] M " +
                                                                    $"JOIN [Especialidade] E ON M.IdEspecialidade = E.Id " +
                                                                    $"WHERE M.Ativo = {1}");
                return obj;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="crm"></param>
        /// <returns></returns>
        public Medico SelecionarPorCrm(string crm)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.QueryFirstOrDefault<Medico>($"SELECT * " +
                                                                  $"FROM [Medico] " +
                                                                  $"WHERE Crm = {crm}");
                return obj;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        public Medico SelecionarPorCpf(string cpf)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.QueryFirstOrDefault<Medico>($"SELECT * " +
                                                                 $"FROM [Medico] " +
                                                                 $"WHERE Cpf = '{cpf}'");
                return obj;
            }
        }

        public Medico SelecionarPorEmail(string email)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.QueryFirstOrDefault<Medico>($"SELECT * " +
                                                                 $"FROM [Medico] " +
                                                                 $"WHERE Email = '{email}'");
                return obj;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Inserir(Medico entity)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                return connection.QuerySingle<int>($"DECLARE @ID int;" +
                                              $"INSERT INTO [Medico] " +
                                              $"(IdEspecialidade,Email,Senha, Sexo, Nome, Cpf, Crm,Telefone_r,Telefone_c,Endereco_c,Cidade,Estado,Ativo,Ativo_Adm) " +
                                                    $"VALUES ("+
                                                            $"'{entity.IdEspecialidade}'," +
                                                            $"'{entity.Email}'," +
                                                            $"'{entity.Senha}'," +
                                                            $"'{entity.Sexo}'," +
                                                            $"'{entity.Nome}'," +
                                                            $"'{entity.Cpf}'," +
                                                            $"'{entity.Crm}'," +
                                                            $"'{entity.Telefone_r}'," +
                                                            $"'{entity.Telefone_c}'," +
                                                            $"'{entity.Endereco_c}'," +
                                                            $"'{entity.Cidade}'," +
                                                            $"'{entity.Estado}'," +
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
        public void Alterar(Medico entity)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                connection.Execute($"UPDATE [Medico] " +
                                   $"SET  Telefone_r = '{entity.Telefone_r}'," +
                                   $"Telefone_c = '{entity.Telefone_c}'," +
                                   $"Endereco_c = '{entity.Endereco_c}'," +
                                   $"Cidade = '{entity.Cidade}'," +
                                   $"Estado = '{entity.Estado}'," +
                                   $"Email = '{entity.Email}'," +
                                   $"Senha = '{entity.Senha}' " +
                                   $"WHERE Id = {entity.Id}");
            }
        }


        /// <summary>
        /// Desativar médico
        /// </summary>
        /// <param name="id"></param>
        /// 
        public void DesativarMedico(int id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                connection.Execute($"UPDATE [Medico]" +
                                   $"SET Ativo = 0" +
                                   $"WHERE Id = {id}");
            }
            
        }

        /// <summary>
        /// Ativar médico
        /// </summary>
        /// <param name="id"></param>
        /// 
        public void AtivarMedico(int id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                connection.Execute($"UPDATE [Medico]" +
                                   $"SET Ativo = 1" +
                                   $"WHERE Id = {id}");
            }

        }

        /// <summary>
        /// Selecionar campo ativo do médico especifico
        /// </summary>
        /// <param name="id"></param>
        /// 
        public Medico SelecionarCampoAtivo(int id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                
                var obj = connection.QueryFirstOrDefault<Medico>($"SELECT Ativo " +
                                                                 $"FROM [Medico] " +
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
                                   $"FROM [Medico] " +
                                   $"WHERE Id = {id}");
            }
        }

    }
}
