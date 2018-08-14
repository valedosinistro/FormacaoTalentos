using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fatec.Clinica.Api.Model
{
    public class PacienteInput
    {
        public string Email { get; set; }
        public string Senha { get; set; }
       

        public string Nome { get; set; }
        public string Cpf { get; set; }
        public char Sexo { get; set; }
        public string Telefone { get; set; }
        public DateTime Data_Nasc { get; set; }
    }
}
