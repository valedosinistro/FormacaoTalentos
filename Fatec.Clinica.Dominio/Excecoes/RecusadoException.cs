using System;


namespace Fatec.Clinica.Dominio.Excecoes
{
    [Serializable]
    public class RecusadoException : Exception
    {
        public RecusadoException()
        {
        }

        public RecusadoException(int id)
        {
        }

        public RecusadoException(string message) : base(message)
        {
        }

    }
}
