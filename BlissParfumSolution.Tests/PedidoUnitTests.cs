using BlissParfumSolution.Domain.cliente;
using BlissParfumSolution.Domain.pedido;
using BlissParfumSolution.Domain.produto;
using NUnit.Framework;
using System;

namespace BlissParfumSolution.Tests
{
    public class PedidoUnitTests
    {
        
        [Test]
        public void Quando_ProdutoForInstanciado_CorretamenteComCLiente_Entao_DeveRetornarTrue()
        {
            Cliente cliente = new Cliente("Joao", "00000000000", DateTime.Now.AddYears(-20));
            Produto produto = new Produto("Perfume", "Cheiroso", 100, DateTime.Now.AddYears(5), 150.50M);

            Pedido pedido = new Pedido();
            pedido.Cliente = cliente;
            pedido.Produto = produto;
            pedido.Quantidade = 1;

            bool pedidoValido = pedido.PedidoValido();

            Assert.IsTrue(pedidoValido);
        }

        [Test]
        public void Quando_ProdutoForInstanciado_CorretamenteSemCLiente_Entao_DeveRetornarTrue()
        {
            Produto produto = new Produto("Perfume", "Cheiroso", 100, DateTime.Now.AddYears(5), 150.50M);

            Pedido pedido = new Pedido();
            pedido.Produto = produto;
            pedido.Quantidade = 1;

            bool pedidoValido = pedido.PedidoValido();

            Assert.IsTrue(pedidoValido);
        }

        [Test]
        public void Quando_ProdutoForInstanciado_SemProduto_Entao_DeveRetornarExcessao()
        {
            Cliente cliente = new Cliente("Joao", "00000000000", DateTime.Now.AddYears(-20));

            Pedido pedido = new Pedido();
            pedido.Cliente = cliente;
            pedido.Produto = null;
            pedido.Quantidade = 1;

            PedidoInvalidoException ex = Assert.Throws<PedidoInvalidoException>(() => pedido.PedidoValido());

            Assert.That(ex.Message, Is.EqualTo("O campo produto precisa ser preenchido!"));
        }

        [Test]
        public void Quando_ProdutoForInstanciado_ComQuantidade0_Entao_DeveRetornarExcessao()
        {
            Cliente cliente = new Cliente("Joao", "00000000000", DateTime.Now.AddYears(-20));
            Produto produto = new Produto("Perfume", "Cheiroso", 100, DateTime.Now.AddYears(5), 150.50M);
            
            Pedido pedido = new Pedido();
            pedido.Cliente = cliente;
            pedido.Produto = produto;
            pedido.Quantidade = 0;

            QuantidadeInvalidaException ex = Assert.Throws<QuantidadeInvalidaException>(() => pedido.PedidoValido());

            Assert.That(ex.Message, Is.EqualTo("A quantidade deve ser maior que zero!"));
        }

        [Test]
        public void Quando_ProdutoForInstanciado_ComCompraMaiorQueEstoque_Entao_DeveRetornarExcessao()
        {
            Cliente cliente = new Cliente("Joao", "00000000000", DateTime.Now.AddYears(-20));
            Produto produto = new Produto("Perfume", "Cheiroso", 100, DateTime.Now.AddYears(5), 150.50M);
            produto.Estoque = 1;

            Pedido pedido = new Pedido();
            pedido.Cliente = cliente;
            pedido.Produto = produto;
            pedido.Quantidade = 2;

            QuantidadeMaiorQueEstoqueException ex = Assert.Throws<QuantidadeMaiorQueEstoqueException>(() => pedido.QuantidadeValida());

            Assert.That(ex.Message, Is.EqualTo("A quantidade deve ser menor ou igual a quantidade em estoque!"));
        }
    }
}
