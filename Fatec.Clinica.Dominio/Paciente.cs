using System;
using System.Collections.Generic;
using System.Text;

namespace Fatec.Clinica.Dominio
{
    public class Paciente:Pessoa
    {
        public string Telefone { get; set; }
        public DateTime Data_Nasc { get; set; }
    }
}
