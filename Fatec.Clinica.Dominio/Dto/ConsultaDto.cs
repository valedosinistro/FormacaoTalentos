using System;
using System.Collections.Generic;
using System.Text;

namespace Fatec.Clinica.Dominio.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public class ConsultaDto
    {
        public int Id { get; set; }
        public int IdPaciente { get; set; }
        public int IdMedico { get; set; }
        public char Status { get; set; }
        public DateTime DataConsulta { get; set; }
        public TimeSpan Horario { get; set; }
    }
}
