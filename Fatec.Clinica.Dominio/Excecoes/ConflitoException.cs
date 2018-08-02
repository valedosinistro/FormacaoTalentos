using System;

namespace Fatec.Clinica.Dominio.Excecoes
{
    [Serializable]
    public class ConflitoException : Exception
    {
        public ConflitoException()
        {
        }

        public ConflitoException(int id)
        {
        }

        public ConflitoException(string message) : base(message)
        {
        }
    }
}
