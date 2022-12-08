using System;

namespace BlissParfumSolution.Infra.Data.Repository
{
    [Serializable]
    public class PedidosNaoCadastradosExcpetion : Exception
    {
        public PedidosNaoCadastradosExcpetion() : base("Não há pedidos cadastados em nossa base de dados.")
        {
        }
    }
}