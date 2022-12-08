using System.Collections.Generic;

namespace BlissParfumSolution.Domain.pedido
{
    public interface IPedidoRepository
    {
        void CadastraPedido(Pedido pedido);
        void ExcluirPedido(long idPedido);
        string AcompanharStatus(long idPedido);
        void AtualizarStatus(long idPedido, string status);
        Pedido BuscarPedidoPorId(long idPedido);
        List<Pedido> BuscarPedidos();
    }
}
