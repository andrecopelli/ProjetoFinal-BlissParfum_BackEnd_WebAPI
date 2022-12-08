using System;

namespace BlissParfumSolution.Domain.produto
{
    public class Produto
    {
        public Int64 IdProduto { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Volume { get; set; }
        public DateTime Validade { get; set; }
        public decimal Preco { get; set; }
        public int Estoque { get; set; }
        public bool EstaAtivo { get; set; }

        public Produto()
        {
            Estoque = 0;
            EstaAtivo = true;
        }
        public Produto(string nome, string descricao, int volume, DateTime validade, decimal preco)
        {
            Nome = nome;
            Descricao = descricao;
            Volume = volume;
            Validade = validade;
            Estoque = 0;
            EstaAtivo = true;
            Preco = preco;
        }

        public bool ProdutoValido() 
        {
            if (string.IsNullOrEmpty(Nome) || string.IsNullOrEmpty(Descricao))
            {
                throw new ProdutoInvalidoException();
            }
            return true;
        }

        public bool DataValidadeValida() 
        { 
            if (Validade < DateTime.Now)
            {
                throw new DataDeValidadeInvalidaException();
            }
            return true;
        }

        public bool VolumeValido() 
        {
            if (Volume <= 0)
            {
                throw new VolumeInvalidoException();
            }
            return true;
        }

        public bool PrecoValido()
        {
            if (Preco <= 0)
            {
                throw new PrecoInvalidoException();
            }
            return true;
        }
        
        public void AtivaProduto()
        {
            EstaAtivo = true;
        }

        public void DestativaProduto()
        {
            EstaAtivo = false;
        }

        public bool RetornaEstaAtivo()
        {
            return EstaAtivo;
        }

        public int RetornaEstoque()
        {
            return Estoque;
        }
    }
}
