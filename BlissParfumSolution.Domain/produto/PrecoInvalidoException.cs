using System;

namespace BlissParfumSolution.Domain.produto
{
    [Serializable]
    public class PrecoInvalidoException : Exception
    {
        public PrecoInvalidoException() : base("O preço digitado é inválido.")
        {
        }
    }
}