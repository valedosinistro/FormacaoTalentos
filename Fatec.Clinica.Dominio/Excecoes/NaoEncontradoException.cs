using System;

namespace Fatec.Clinica.Dominio.Excecoes
{
    [Serializable]
    public class NaoEncontradoException : Exception
    {
        public NaoEncontradoException()
        {
        }

        public NaoEncontradoException(int id)
        {
        }

        public NaoEncontradoException(string message) : base(message)
        {
        }
    }
}
