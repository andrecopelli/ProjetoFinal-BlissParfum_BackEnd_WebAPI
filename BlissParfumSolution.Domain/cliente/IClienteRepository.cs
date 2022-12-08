using System.Collections.Generic;

namespace BlissParfumSolution.Domain.cliente
{
    public interface IClienteRepository
    {
        void CadastrarCliente(Cliente cliente);
        void EditarCliente(Cliente cliente);
        void ExcluirCliente(string cpf);
        Cliente BuscarClientePorCpf(string cpf);
        List<Cliente> BuscarClientes();
        Cliente BuscarClientePorId(long id);
    }
}
