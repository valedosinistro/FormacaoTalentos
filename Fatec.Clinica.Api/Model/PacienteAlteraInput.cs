using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fatec.Clinica.Api.Model
{
    public class PacienteAlteraInput
    {
        public string Email { get; set; }
        public string Senha { get; set; }

        public string Telefone { get; set; }
      
    }
}
