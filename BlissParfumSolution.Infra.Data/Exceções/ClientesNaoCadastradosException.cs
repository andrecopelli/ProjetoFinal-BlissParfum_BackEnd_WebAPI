using System;

namespace BlissParfumSolution.Infra.Data.Exceções
{
    public class ClientesNaoCadastradosException : Exception
    {
        public ClientesNaoCadastradosException() : base("Não há clientes cadastrados.")
        {
        }
    }
}
