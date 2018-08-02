using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fatec.Clinica.Api.Model
{
    public class MedicoInput
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public int Crm { get; set; }
        public int IdEspecialidade { get; set; }
    }
}
