using System;
using System.Collections.Generic;
using System.Text;

namespace Fatec.Clinica.Dominio
{
    public class Consulta
    {
        public int Id { get; set; }
        public int IdPaciente { get; set; }
        public int IdMedico { get; set; }
        public DateTime DataConsulta { get; set; }
        public DateTime Horario { get; set; }
        public Char Status { get; set; }
    }
}
