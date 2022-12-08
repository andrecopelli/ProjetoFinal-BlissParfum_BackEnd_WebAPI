using System;

namespace BlissParfumSolution.Domain.pedido
{
    [Serializable]
    public class QuantidadeMaiorQueEstoqueException : Exception
    {
        public QuantidadeMaiorQueEstoqueException() : base("A quantidade deve ser menor ou igual a quantidade em estoque!")
        {
        }
    }
}
