namespace Fatec.Clinica.Dominio
{
    public class Medico : Pessoa
    {
        public string Crm { get; set; }
        public int IdEspecialidade { get; set; }
        public string Telefone_r { get; set; }
        public string Telefone_c { get; set; }
        public string Endereco_c { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
    }
}
