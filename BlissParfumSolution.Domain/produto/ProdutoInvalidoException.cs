using System;

namespace BlissParfumSolution.Domain.produto
{
    public class ProdutoInvalidoException : Exception
    {
        public ProdutoInvalidoException() : base("O nome do produto e a descrição precisam ser preenchidos!")
        { }
    }
}
