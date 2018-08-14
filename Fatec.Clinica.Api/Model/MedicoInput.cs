using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fatec.Clinica.Api.Model
{
    public class MedicoInput
    {
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public char Sexo { get; set; }
        public string Crm { get; set; }
        public int IdEspecialidade { get; set; }
        public string Telefone_r { get; set; }
        public string Telefone_c { get; set; }
        public string Endereco_c { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
    }
}
