using System;

namespace BlissParfumSolution.Infra.Data.Repository
{
    [Serializable]
    internal class ClienteJaCadastradoException : Exception
    {
        public ClienteJaCadastradoException() : base ("Esse cliente já foi cadastrado.")
        {
        }

       
    }
}