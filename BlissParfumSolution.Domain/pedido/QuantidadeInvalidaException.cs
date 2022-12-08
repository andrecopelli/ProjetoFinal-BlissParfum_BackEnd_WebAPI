using System;

namespace BlissParfumSolution.Domain.pedido
{
    [Serializable]
    public class QuantidadeInvalidaException : Exception
    {
        public QuantidadeInvalidaException() : base("A quantidade deve ser maior que zero!")
        {
        }
    }
}