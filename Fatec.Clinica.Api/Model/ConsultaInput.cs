using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fatec.Clinica.Api.Model
{
    public class ConsultaInput
    {
        public int IdPaciente { get; set; }
        public int IdMedico { get; set; }
        public DateTime DataConsulta { get; set; }
        public DateTime Horario { get; set; }
        public char Status { get; set; }
    }
}
