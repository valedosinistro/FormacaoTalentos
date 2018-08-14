using System;
using System.Collections.Generic;
using System.Text;

namespace Fatec.Clinica.Dominio.Dto
{
    public class MedicoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string Crm { get; set; }
        public int IdEspecialidade { get; set; }
        public string Telefone_r { get; set; }
        public string Telefone_c { get; set; }
        public char Sexo { get; set; }
        public string Endereco_c { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public Boolean Ativo { get; set; }
        public Boolean Ativo_Adm { get; set; }
        public string Especialidade { get; set; }
    }
}
