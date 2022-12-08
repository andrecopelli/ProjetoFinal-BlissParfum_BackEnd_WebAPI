using System;

namespace BlissParfumSolution.Infra.Data.Repository
{
    [Serializable]
    internal class ProdutosNaoCadastradosException : Exception
    {
        public ProdutosNaoCadastradosException() : base("Não há produtos cadastrados.")
        {
        }
    }
}