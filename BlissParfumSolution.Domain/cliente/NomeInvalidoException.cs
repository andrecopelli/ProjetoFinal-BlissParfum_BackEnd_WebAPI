using System;

namespace BlissParfumSolution.Domain.cliente
{
    public class NomeInvalidoException : Exception
    {
        public NomeInvalidoException() : base("O nome digitado é inválido!")
        { }
    }
}
