using Fatec.Clinica.Dominio;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using Fatec.Clinica.Dado.Configuracao;
using Fatec.Clinica.Dominio.Dto;

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
                var lista = connection.Query<MedicoDto>($"SELECT M.Id, M.Nome, M.Cpf, M.Crm, M.IdEspecialidade, E.Nome As Especialidade " +
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
                var obj = connection.QueryFirstOrDefault<MedicoDto>($"SELECT M.Id,  M.Nome, M.Cpf, M.Crm, M.IdEspecialidade, E.Nome As Especialidade " +
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
                var obj = connection.Query<MedicoDto>($"SELECT M.Id,  M.Nome, M.Cpf, M.Crm, M.IdEspecialidade, E.Nome As Especialidade " +
                                                                    $"FROM [Medico] M " +
                                                                    $"JOIN [Especialidade] E ON M.IdEspecialidade = E.Id " +
                                                                    $"WHERE E.Id = {id}");
                return obj;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="crm"></param>
        /// <returns></returns>
        public Medico SelecionarPorCrm(int crm)
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
                                              $"(IdEspecialidade, Nome, Cpf, Crm) " +
                                                    $"VALUES ({entity.IdEspecialidade}," +
                                                            $"'{entity.Nome}'," +
                                                            $"'{entity.Cpf}'," +
                                                            $"{entity.Crm})" +
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
                                   $"SET  IdEspecialidade = {entity.IdEspecialidade}," +
                                   $"Nome = '{entity.Nome}'," +
                                   $"CPF = '{entity.Cpf}'," +
                                   $"CRM = {entity.Crm} " +
                                   $"WHERE Id = {entity.Id}");
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
