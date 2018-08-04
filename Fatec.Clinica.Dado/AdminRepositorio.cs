using Dapper;
using Fatec.Clinica.Dado.Configuracao;
using Fatec.Clinica.Dominio;
using Fatec.Clinica.Dominio.Dto;
using System.Data.SqlClient;

namespace Fatec.Clinica.Dado
{
    public class AdminRepositorio
    {
        /// <summary>
        /// Administrador pode ativar conta de usuário do tipo médico
        /// </summary>
        /// <param name="id"></param>

        public void AtivarMedicoAdmin(int id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                connection.Execute($"UPDATE [Medico]" +
                                   $"SET Ativo_Adm = 1" +
                                   $"WHERE Id = {id}");
            }

        }

        /// <summary>
        /// Administrador pode desativar conta de usuário do tipo médico
        /// </summary>
        /// <param name="id"></param>

        public void DesativarrMedicoAdmin(int id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                connection.Execute($"UPDATE [Medico]" +
                                   $"SET Ativo_Adm = 0" +
                                   $"WHERE Id = {id}");
            }

        }



        /// <summary>
        /// Administrador pode ativar conta de usuário do tipo paciente
        /// </summary>
        /// <param name="id"></param>
        /// 
        public void AtivarPacienteAdmin(int id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                connection.Execute($"UPDATE [Paciente]" +
                                   $"SET Ativo_Adm = 1" +
                                   $"WHERE Id = {id}");
            }

        }

        /// <summary>
        /// Administrador pode ativar conta de usuário do tipo paciente
        /// </summary>
        /// <param name="id"></param>
        /// 
        public void DesativarPacienteAdmin(int id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                connection.Execute($"UPDATE [Paciente]" +
                                   $"SET Ativo_Adm = 0" +
                                   $"WHERE Id = {id}");
            }

        }

        /// <summary>
        /// Selecionar campo Ativo_Adm do médico especifico
        /// </summary>
        /// <param name="id"></param>
        /// 
        public Medico SelecionarCampoAtivoMedico(int id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {

                var obj = connection.QueryFirstOrDefault<Medico>($"SELECT Ativo_Adm " +
                                                                 $"FROM [Medico] " +
                                                                 $"WHERE Id = '{id}'");
                return obj;
            }

        }

        /// <summary>
        /// Selecionar campo Ativo_Adm do Paciente especifico
        /// </summary>
        /// <param name="id"></param>
        /// 
        public Paciente SelecionarCampoAtivoPaciente(int id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {

                var obj = connection.QueryFirstOrDefault<Paciente>($"SELECT Ativo_Adm " +
                                                                 $"FROM [Paciente] " +
                                                                 $"WHERE Id = '{id}'");
                return obj;
            }

        }
    }
}
