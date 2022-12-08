using System;

namespace BlissParfumSolution.Infra.Data.Exceções
{
    [Serializable]
    internal class ProdutoInexistenteException : Exception
    {
        public ProdutoInexistenteException() :base("Esse produto não está cadastrado.")
        {
        }
    }
}