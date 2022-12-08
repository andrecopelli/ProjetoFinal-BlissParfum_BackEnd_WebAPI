using BlissParfumSolution.Domain.cliente;
using NUnit.Framework;
using System;

namespace BlissParfumSolution.Tests
{
    public class ClienteUnitTests
    {
        [Test]
        public void Quando_InstanciaClienteCorretamente_RetornaTrue()
        {
            Cliente cliente = new Cliente("João", "00000000000", DateTime.Now.AddYears(-20));

            var nomeValido = cliente.NomeValido();
            var cpfValido = cliente.CpfValido();
            var dataValida = cliente.DataValida();

            Assert.IsTrue(nomeValido);
            Assert.IsTrue(cpfValido);
            Assert.IsTrue(dataValida);
        }

        [Test]
        public void Quando_InstanciaCliente_E_NomeVazio_RetornaExcessao()
        {
            Cliente cliente = new Cliente("", "00000000000", DateTime.Now.AddYears(-20));

            NomeInvalidoException ex = Assert.Throws<NomeInvalidoException>(() => cliente.NomeValido());

            Assert.That(ex.Message, Is.EqualTo("O nome digitado é inválido!"));
        }

        [Test]
        public void Quando_InstanciaCliente_E_CpfVazio_RetornaExcessao()
        {
            Cliente cliente = new Cliente("João", "", DateTime.Now.AddYears(-20));

            CpfInvalidoException ex = Assert.Throws<CpfInvalidoException>(() => cliente.CpfValido());

            Assert.That(ex.Message, Is.EqualTo("O CPF digitado é inválido!"));
        }

        [Test]
        public void Quando_InstanciaCliente_E_CpfMenorQueEsperado_RetornaExcessao()
        {
            Cliente cliente = new Cliente("João", "000", DateTime.Now.AddYears(-20));

            CpfInvalidoException ex = Assert.Throws<CpfInvalidoException>(() => cliente.CpfValido());

            Assert.That(ex.Message, Is.EqualTo("O CPF digitado é inválido!"));
        }

        [Test]
        public void Quando_InstanciaCliente_E_CpfMaiorQueEsperado_RetornaExcessao()
        {
            Cliente cliente = new Cliente("João", "000000000000000", DateTime.Now.AddYears(-20));

            CpfInvalidoException ex = Assert.Throws<CpfInvalidoException>(() => cliente.CpfValido());

            Assert.That(ex.Message, Is.EqualTo("O CPF digitado é inválido!"));
        }

        [Test]
        public void Quando_InstanciaCliente_E_DataNascimentoMaiorQueAtual_RetornaExcessao()
        {
            Cliente cliente = new Cliente("João", "00000000000", DateTime.Now.AddYears(20));

            DataInvalidaException ex = Assert.Throws<DataInvalidaException>(() => cliente.DataValida());

            Assert.That(ex.Message, Is.EqualTo("A data de nascimento deve ser menor do que a data atual!"));
        }

        [Test]
        public void Quando_ClienteFazPedido_DeveReceberODobroDePontosDeFidelidade()
        {
            Cliente cliente = new Cliente("João", "00000000000", DateTime.Now.AddYears(-20));
            cliente.AtribuiPontos(1);
            var pontos = 2;
            Assert.That(cliente.PontosFidelidade, Is.EqualTo(pontos));
        }
    }
}