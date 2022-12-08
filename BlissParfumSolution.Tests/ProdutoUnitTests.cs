using BlissParfumSolution.Domain.produto;
using NUnit.Framework;
using System;

namespace BlissParfumSolution.Tests
{
    public class ProdutoUnitTests
    {
        
        [Test]
        public void QuandoProduto_ForInstanciadoCorretamente_EntaoDeveRetornarTrue()
        {
            Produto produto = new Produto();
            produto.Nome = "Perfume";
            produto.Descricao = "Cheiroso";
            produto.Volume = 100;
            produto.Validade = DateTime.Now.AddYears(5);
            produto.Preco = 150.50M;

            var produtoValido = produto.ProdutoValido();
            var dataValida = produto.DataValidadeValida();
            var volumeValido = produto.VolumeValido();
            var precoValido = produto.PrecoValido();

            Assert.IsTrue(produtoValido);
            Assert.IsTrue(dataValida);
            Assert.IsTrue(volumeValido);
            Assert.IsTrue(precoValido);
        }

        [Test]
        public void QuandoProduto_ForInstanciado_E_NomeVazio_EntaoDeveRetornarExcessao()
        {
            Produto produto = new Produto();
            produto.Nome = "";
            produto.Descricao = "Cheiroso";
            produto.Volume = 100;
            produto.Validade = DateTime.Now.AddYears(5);
            produto.Preco = 150.50M;

            ProdutoInvalidoException ex = Assert.Throws<ProdutoInvalidoException>(() => produto.ProdutoValido());

            Assert.That(ex.Message, Is.EqualTo("O nome do produto e a descrição precisam ser preenchidos!"));
        }

        [Test]
        public void QuandoProduto_ForInstanciado_E_DescricaoVazia_EntaoDeveRetornarExcessao()
        {
            Produto produto = new Produto();
            produto.Nome = "Perfume";
            produto.Descricao = "";
            produto.Volume = 100;
            produto.Validade = DateTime.Now.AddYears(5);
            produto.Preco = 150.50M;

            ProdutoInvalidoException ex = Assert.Throws<ProdutoInvalidoException>(() => produto.ProdutoValido());

            Assert.That(ex.Message, Is.EqualTo("O nome do produto e a descrição precisam ser preenchidos!"));
        }

        [Test]
        public void QuandoProduto_ForInstanciado_E_VolumeZero_EntaoDeveRetornarExcessao()
        {
            Produto produto = new Produto();
            produto.Nome = "Perfume";
            produto.Descricao = "Cheiroso";
            produto.Volume = 0;
            produto.Validade = DateTime.Now.AddYears(5);
            produto.Preco = 150.50M;

            VolumeInvalidoException ex = Assert.Throws<VolumeInvalidoException>(() => produto.VolumeValido());

            Assert.That(ex.Message, Is.EqualTo("O volume do perfume deve ser maior que zero!"));
        }

        [Test]
        public void QuandoProduto_ForInstanciado_E_DataMenorQueAtual_EntaoDeveRetornarExcessao()
        {
            Produto produto = new Produto();
            produto.Nome = "Perfume";
            produto.Descricao = "Cheiroso";
            produto.Volume = 100;
            produto.Validade = DateTime.Now.AddYears(-5);
            produto.Preco = 150.50M;

            DataDeValidadeInvalidaException ex = Assert.Throws<DataDeValidadeInvalidaException>(() => produto.DataValidadeValida());

            Assert.That(ex.Message, Is.EqualTo("A data de validade deve ser maior do que a data atual."));
        }

        [Test]
        public void QuandoProduto_ForInstanciado_E_PrecoZero_EntaoDeveRetornarExcessao()
        {
            Produto produto = new Produto();
            produto.Nome = "Perfume";
            produto.Descricao = "Cheiroso";
            produto.Volume = 100;
            produto.Validade = DateTime.Now.AddYears(-5);
            produto.Preco = 0;

            PrecoInvalidoException ex = Assert.Throws<PrecoInvalidoException>(() => produto.PrecoValido());

            Assert.That(ex.Message, Is.EqualTo("O preço digitado é inválido."));
        }

        [Test]
        public void QuandoProduto_ForInstanciado_Entao_EstoqueDeveSerZero()
        {
            Produto produto = new Produto();
            produto.Nome = "Perfume";
            produto.Descricao = "Cheiroso";
            produto.Volume = 100;
            produto.Validade = DateTime.Now.AddYears(-5);
            produto.Preco = 0;

            var estoqueEsperado = 0;

            Assert.That(produto.Estoque, Is.EqualTo(estoqueEsperado));
        }

        [Test]
        public void QuandoProduto_ForInstanciado_Entao_DeveIniciarAtivo()
        {
            Produto produto = new Produto();
            produto.Nome = "Perfume";
            produto.Descricao = "Cheiroso";
            produto.Volume = 100;
            produto.Validade = DateTime.Now.AddYears(-5);
            produto.Preco = 0;

            var statusEsperado = true;

            Assert.That(produto.EstaAtivo, Is.EqualTo(statusEsperado));
        }

        [Test]
        public void QuandoProduto_ForDesativado_Entao_DeveRetornarProdutoInativo()
        {
            Produto produto = new Produto();
            produto.Nome = "Perfume";
            produto.Descricao = "Cheiroso";
            produto.Volume = 100;
            produto.Validade = DateTime.Now.AddYears(-5);
            produto.Preco = 0;

            produto.DestativaProduto();

            Assert.That(produto.EstaAtivo, Is.EqualTo(false));
        }

        [Test]
        public void QuandoProduto_ForAtivado_Entao_DeveRetornarProdutoAtivo()
        {
            Produto produto = new Produto();
            produto.Nome = "Perfume";
            produto.Descricao = "Cheiroso";
            produto.Volume = 100;
            produto.Validade = DateTime.Now.AddYears(-5);
            produto.Preco = 0;
            produto.EstaAtivo = false;

            produto.AtivaProduto();

            Assert.That(produto.EstaAtivo, Is.EqualTo(true));
        }
    }
}
