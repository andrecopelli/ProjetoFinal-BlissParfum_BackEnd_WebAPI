using BlissParfumSolution.Domain.cliente;
using BlissParfumSolution.Domain.produto;
using System;

namespace BlissParfumSolution.Domain.pedido
{
    public class Pedido
    {
        public Int64 IdPedido { get; set; }
        public Cliente? Cliente { get; set; }
        public  Produto Produto { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataPedido { get; set; }
        public decimal ValorTotal { get ; set; }
        public string Status { get; set; }

        public Pedido()
        {
            DataPedido = DateTime.Now;
            Status = "Em Andamento";
        }

        public Pedido(Cliente? cliente, Produto produto, int quantidade)
        {
            Cliente = cliente;
            Produto = produto;
            Quantidade = quantidade;
            Status = "Em Andamento.";
            DataPedido = DateTime.Now;
        }

        public bool PedidoValido()
        {
            if (Produto is null)
            {
                throw new PedidoInvalidoException();
            }
            if (Quantidade <= 0)
            {
                throw new QuantidadeInvalidaException();
            }
            return true;
        }

        public bool QuantidadeValida()
        {
            if(Quantidade > Produto.Estoque)
            {
                throw new QuantidadeMaiorQueEstoqueException();
            }
            return true;
        }

        public decimal RetornaValorTotal()
        {
            return ValorTotal = Produto.Preco * Quantidade;
        }

        public string RetornaStatus()
        {
            return Status;
        }

        public DateTime RetornaDataPedido()
        {
            return DataPedido;
        }
    }
}
