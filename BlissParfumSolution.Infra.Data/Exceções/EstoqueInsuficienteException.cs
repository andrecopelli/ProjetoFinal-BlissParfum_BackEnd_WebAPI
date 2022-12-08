using System;

namespace BlissParfumSolution.Infra.Data.Exceções
{
    [Serializable]
    internal class EstoqueInsuficienteException : Exception
    {
        public EstoqueInsuficienteException() : base ("A quantidade digitada deve ser maior do que a quantidade disponível no estoque.")
        {
        }
    }
}