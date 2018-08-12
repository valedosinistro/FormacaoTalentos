using System;
using System.Collections.Generic;
using System.Text;

namespace Fatec.Clinica.Dominio.Dto
{
    public class PacienteDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public char Sexo { get; set; }
        public string Telefone { get; set; }
        public DateTime Data_Nasc { get; set; }
        public string DataNasc_Formatada
        {
            get
            {
                return Data_Nasc.ToShortDateString();
            }
        }
        public Boolean Ativo { get; set; }
        public Boolean Ativo_Adm { get; set; }

    }
}
