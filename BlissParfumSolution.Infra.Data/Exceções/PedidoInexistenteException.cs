using System;

namespace BlissParfumSolution.Infra.Data.Exceções
{
    [Serializable]
    public class PedidoInexistenteException : Exception
    {
        public PedidoInexistenteException() : base("O pedido com este id não existe.")
        {
        }
    }
}