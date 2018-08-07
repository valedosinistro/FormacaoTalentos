using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fatec.Clinica.Api.Model
{
    public class MedicoAlteraInput
    {
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Telefone_r { get; set; }
        public string Telefone_c { get; set; }
        public string Endereco_c { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
    }
}
