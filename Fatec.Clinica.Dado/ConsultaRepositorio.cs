using Dapper;
using Fatec.Clinica.Dado.Configuracao;
using Fatec.Clinica.Dominio;
using Fatec.Clinica.Dominio.Dto;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Fatec.Clinica.Dado
{/// <summary>
/// 
/// </summary>
    public class ConsultaRepositorio
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ConsultaDto> Selecionar()
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var lista = connection.Query<ConsultaDto>($"SELECT C.Id, C.IdPaciente, C.IdMedico, C.DataConsulta, C.Horario, C.Status FROM [Consulta] C");
                return lista;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ConsultaDto SelecionarPorId(int id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.QueryFirstOrDefault<ConsultaDto>($"SELECT C.Id, C.IdPaciente, C.IdMedico, C.DataConsulta, C.Horario, C.Status FROM [Consulta] C WHERE C.Id = {id}");
                return obj;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdPaciente"></param>
        /// <returns></returns>
        public ConsultaDto SelecionarPorPaciente(int IdPaciente)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var lista = connection.QueryFirstOrDefault<ConsultaDto>($"SELECT * " +
                                                                 $"FROM [Consulta] " +
                                                                 $"WHERE IdPaciente = '{IdPaciente}'");
                return lista;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdMedico"></param>
        /// <returns></returns>
        public ConsultaDto SelecionarPorMedico(int IdMedico)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var lista = connection.QueryFirstOrDefault<ConsultaDto>($"SELECT * " +
                                                                 $"FROM [Consulta] " +
                                                                 $"WHERE IdMedico = '{IdMedico}'");
                return lista;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DataConsulta"></param>
        /// <returns></returns>
        public ConsultaDto SelecionarPorData(DateTime DataConsulta)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var lista = connection.QueryFirstOrDefault<ConsultaDto>($"SELECT * " +
                                                                 $"FROM [Consulta] " +
                                                                 $"WHERE DataConsulta = '{DataConsulta}'");
                return lista;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Status"></param>
        /// <returns></returns>
        public ConsultaDto SelecionarPorStatus(char Status)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var lista = connection.QueryFirstOrDefault<ConsultaDto>($"SELECT * " +
                                                                 $"FROM [Consulta] " +
                                                                 $"WHERE Status = '{Status}'");
                return lista;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Inserir(Consulta entity)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                return connection.QuerySingle<int>($"DECLARE @ID int;" +
                                              $"INSERT INTO [Consulta] " +
                                              $"(IdPaciente,IdMedico,DataConsulta,Horario,Status) " +
                                                    $"VALUES (" +
                                                            $"'{entity.IdPaciente}'," +
                                                            $"'{entity.IdMedico}'," +
                                                            $"'{entity.DataConsulta}'," +
                                                            $"'{entity.Horario}'," +
                                                            $"'A')" +
                                              $"SET @ID = SCOPE_IDENTITY();" +
                                              $"SELECT @ID");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public void Alterar(Consulta entity)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                connection.Execute($"UPDATE [Consulta] " +
                                   $"SET  Status = '{entity.Status}'," +
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
                                   $"FROM [Consulta] " +
                                   $"WHERE Id = {id}");
            }
        }
    }
}
