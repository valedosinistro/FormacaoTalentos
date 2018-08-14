using Dapper;
using Fatec.Clinica.Dado.Configuracao;
using Fatec.Clinica.Dominio;
using Fatec.Clinica.Dominio.Dto;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Fatec.Clinica.Dado
{
    public class EmergenciaRepositorio
    {
        
        public Emergencia ExibirEmergenciaParaOMedico()
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.QueryFirstOrDefault<Emergencia>($"SELECT * FROM Emergencia WHERE IdMedico IS NULL AND Status = 'P' ORDER BY DataHora DESC");
                return obj;
            }
        }

        public int CriarEmergencia(Emergencia entity)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                return connection.QuerySingle<int>($"DECLARE @ID int;" +
                                              $"INSERT INTO [Emergencia] " +
                                              $"(IdPaciente,DataHora,Status) " +
                                                    $"VALUES (" +
                                                            $"'{entity.IdPaciente}'," +
                                                            $"'{entity.DataHora}'," +           
                                                            $"'P')" +
                                              $"SET @ID = SCOPE_IDENTITY();" +
                                              $"SELECT @ID");
            }
        }




        public void AlterarStatusAtendendo(int IdMedico, int id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                connection.Execute($"UPDATE [Emergencia] " +
                                   $"SET IdMedico = {IdMedico}, Status = 'A' WHERE Id = {id}");
            }
        }

        public void AlterarStatusRealizada(int id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                connection.Execute($"UPDATE [Emergencia] " +
                                   $"SET  Status = 'R' WHERE Id={id}");
            }
        }

    }
}
