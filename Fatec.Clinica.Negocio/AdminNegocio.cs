using Fatec.Clinica.Dado;
using Fatec.Clinica.Dominio.Dto;
using Fatec.Clinica.Dominio.Excecoes;

namespace Fatec.Clinica.Negocio
{
    /// <summary>
    /// Classe referente as regras do Administrador
    /// </summary>
    public class AdminNegocio
    {
        private readonly AdminRepositorio _adminRepositorio;
 

        public AdminNegocio()
        {
            _adminRepositorio = new AdminRepositorio();
            
        }

        /// <summary>
        /// Método que verifica e atualiza status de Ativo/Desativo ADM do Médico
        /// </summary>
        /// <param name="id"></param>
        public void MudarAtivoMedicoAdmin(int id)
        {
            var obj = _adminRepositorio.SelecionarCampoAtivoMedico(id);

            if (obj == null)
                throw new NaoEncontradoException($"Médico com Id: {id} não encontrado !");

            if (obj.Ativo_Adm == true)
            {
                _adminRepositorio.DesativarrMedicoAdmin(id);
            }
            else
            {
                _adminRepositorio.AtivarMedicoAdmin(id);
            }

        }

        /// <summary>
        /// Método que verifica e atualiza status de Ativo/Desativo ADM do Paciente
        /// </summary>
        /// <param name="id"></param>
        public void MudarAtivoPacienteAdmin(int id)
        {
            var obj = _adminRepositorio.SelecionarCampoAtivoPaciente(id);

            if (obj == null)
                throw new NaoEncontradoException($"Paciente com Id: {id} não encontrado !");

            if (obj.Ativo_Adm == true)
            {
                _adminRepositorio.DesativarPacienteAdmin(id);
            }
            else
            {
                _adminRepositorio.AtivarPacienteAdmin(id);
            }

        }
    }
}
