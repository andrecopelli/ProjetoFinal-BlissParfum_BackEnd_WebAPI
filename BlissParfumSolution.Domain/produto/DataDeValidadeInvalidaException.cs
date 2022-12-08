using System;

namespace BlissParfumSolution.Domain.produto
{
    [Serializable]
    public class DataDeValidadeInvalidaException : Exception
    {
        public DataDeValidadeInvalidaException() : base("A data de validade deve ser maior do que a data atual.")
        {
        }
    }
}