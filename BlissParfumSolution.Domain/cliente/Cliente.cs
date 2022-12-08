using System;

namespace BlissParfumSolution.Domain.cliente
{
    public class Cliente
    {
        public long IdCliente { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime DataDeNascimento { get; set; }
        public decimal PontosFidelidade { get; set; }

        public Cliente()
        {

        }
        public Cliente(string nome, string cpf, DateTime dataNascimento) 
        { 
            Nome = nome;
            Cpf = cpf;
            DataDeNascimento = dataNascimento;
        }

        public bool NomeValido() 
        {
            if(string.IsNullOrEmpty(Nome))
            {
                throw new NomeInvalidoException();
            }
            
            return true;
        }

        public bool DataValida()
        {
            if (DataDeNascimento > DateTime.Now)
            {
                throw new DataInvalidaException();
            }

            return true;
        }

        public bool CpfValido()
        {
            if (string.IsNullOrEmpty(Cpf) || Cpf.Length != 11)
            {
                throw new CpfInvalidoException();
            }
            return true;
        }

        public void AtribuiPontos(decimal valorTotal) 
        {
            decimal pontosAdicionar = valorTotal * 2;
            PontosFidelidade += pontosAdicionar;
        }
    }
}
