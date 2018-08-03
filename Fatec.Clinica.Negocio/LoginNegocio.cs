using Fatec.Clinica.Dado;
using Fatec.Clinica.Dominio.Dto;
using Fatec.Clinica.Dominio.Excecoes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fatec.Clinica.Negocio
{
    /// <summary>
    /// 
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
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PacienteDto LoginPaciente(string user, string senha)
        {
            var obj = _loginRepositorio.LoginPacienteEmail(user, senha);

            if (obj == null)
            {
                obj = _loginRepositorio.LoginPacienteCpf(user, senha);
                if (obj == null)
                {
                    throw new NaoEncontradoException("Usuario ou Senha Invalidos !");
                }
            }

            return obj;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
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

            return obj;
        }
    }
}
