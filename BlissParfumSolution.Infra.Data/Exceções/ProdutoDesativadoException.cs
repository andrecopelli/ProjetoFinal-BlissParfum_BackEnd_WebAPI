using System;

namespace BlissParfumSolution.Infra.Data.Exceções
{
    [Serializable]
    public class ProdutoDesativadoException : Exception
    {
        public ProdutoDesativadoException() : base("Este produto não está ativo em nosso sitema. Ative-o para poder realizar operações.")
        {
        }
    }
}