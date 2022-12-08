using System;

namespace BlissParfumSolution.Domain.pedido
{
    public class PedidoInvalidoException : Exception
    {
        public PedidoInvalidoException() : base("O campo produto precisa ser preenchido!")
        { }
    }
}

