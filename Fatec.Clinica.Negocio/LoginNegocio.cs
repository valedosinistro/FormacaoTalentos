using Fatec.Clinica.Dado;
using Fatec.Clinica.Dominio.Dto;
using Fatec.Clinica.Dominio.Excecoes;


namespace Fatec.Clinica.Negocio
{
    /// <summary>
    /// Classe referente as regras do Sistema de Acesso
    /// </summary>
    public class LoginNegocio
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly LoginRepositorio _loginRepositorio;

        /// <summary>
        /// 
        /// </summary>
        public LoginNegocio()
        {
            _loginRepositorio = new LoginRepositorio();
        }

        /// <summary>
        /// Método que realiza login do paciente no sistema
        /// </summary>
        /// <param name="user"></param>
        /// <param name="senha"></param>
        /// <returns></returns>
        public PacienteDto LoginPaciente(string user, string senha)
        {
            var obj = _loginRepositorio.LoginPacienteEmail(user, senha);

            //Verifica se login e senha estão corretos
            if (obj == null)
            {
                obj = _loginRepositorio.LoginPacienteCpf(user, senha);
                if (obj == null)
                {
                    throw new NaoEncontradoException("Usuário ou Senha Invalidos !");
                }
            }

            //Verifica campo Ativo_Adm 
            if (!obj.Ativo_Adm)
            {
                throw new RecusadoException("Esse usuário foi desativado pelo Administrador do sistema !");
            }


    
            return obj;
        }


        /// <summary>
        /// Método que realiza login do médico no sistema
        /// </summary>
        /// <param name="user"></param>
        /// <param name="senha"></param>
        /// <returns></returns>
        public MedicoDto LoginMedico(string user, string senha)
        {
            var obj = _loginRepositorio.LoginMedicoEmail(user, senha);

            if (obj == null)
            {
                obj = _loginRepositorio.LoginMedicoCpf(user, senha);
                if (obj == null)
                {
                    throw new NaoEncontradoException("Usuario ou Senha Invalidos !");
                }
            }

            //Verifica campo Ativo_Adm 
            if (!obj.Ativo_Adm)
            {
                throw new RecusadoException("Esse usuário foi desativado pelo Administrador do sistema !");
            }

            return obj;
        }
    }
}
