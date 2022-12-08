using System;

namespace BlissParfumSolution.Infra.Data.Exceções
{
    [Serializable]
    internal class ClienteInexistenteException : Exception
    {
        public ClienteInexistenteException() : base("Este cliente não está cadastrado.")
        {
        }
    }
}