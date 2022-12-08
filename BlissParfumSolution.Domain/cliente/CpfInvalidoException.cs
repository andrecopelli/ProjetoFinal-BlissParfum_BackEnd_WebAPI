using System;

namespace BlissParfumSolution.Domain.cliente
{
    public class CpfInvalidoException : Exception
    {
        public CpfInvalidoException() : base("O CPF digitado é inválido!")
        { }
    }
}
