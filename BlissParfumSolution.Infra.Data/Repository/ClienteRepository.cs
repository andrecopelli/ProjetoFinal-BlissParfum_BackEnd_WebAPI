using BlissParfumSolution.Domain.cliente;
using BlissParfumSolution.Infra.Data.DAO;
using BlissParfumSolution.Infra.Data.Exceções;
using System.Collections.Generic;

namespace BlissParfumSolution.Infra.Data.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private ClienteDAO _clienteDAO;
        private PedidoDAO _pedidoDAO;

        public ClienteRepository()
        {
            _clienteDAO = new ClienteDAO();
            _pedidoDAO= new PedidoDAO();
        }

        public Cliente BuscarClientePorCpf(string cpf)
        {
            Cliente clienteBuscado = new Cliente();
            clienteBuscado.Cpf = cpf;
            if (clienteBuscado.CpfValido())
            {
                clienteBuscado = _clienteDAO.BuscaClientePorCpf(cpf);
                if (clienteBuscado == null)
                {
                    throw new ClienteInexistenteException();
                }
                else
                {
                    return clienteBuscado;
                }
            }
            else
            {
                throw new CpfInvalidoException();
            }
        }

        public Cliente BuscarClientePorId(long id)
        {
            Cliente clienteBuscado = new Cliente();
            clienteBuscado.IdCliente = id;
            clienteBuscado = _clienteDAO.BuscaClientePorId(id);
            return clienteBuscado;
        }

        public List<Cliente> BuscarClientes()
        {
            var listaDeClientes = _clienteDAO.BuscarClientes();
            if (listaDeClientes.Count == 0)
            {
                throw new ClientesNaoCadastradosException();
            }
            return listaDeClientes;
        }

        public void CadastrarCliente(Cliente cliente)
        {
            if (cliente.CpfValido())
            {
                Cliente clienteBuscado = _clienteDAO.BuscaClientePorCpf(cliente.Cpf);
                if (clienteBuscado == null)
                {
                    if (cliente.NomeValido())
                    {
                        if (cliente.DataValida())
                        {
                            _clienteDAO.CadastrarCliente(cliente);
                        }
                        else
                        {
                            throw new DataInvalidaException();
                        }
                    }
                    else
                    {
                        throw new NomeInvalidoException();
                    }
                }
                else
                {
                    throw new ClienteJaCadastradoException();
                }
            }
            else
            {
                throw new CpfInvalidoException();
            }
        }

        public void EditarCliente(Cliente cliente)
        {
            if (cliente.CpfValido())
            {
                _clienteDAO.EditarCliente(cliente);
            }
            else
            {
                throw new CpfInvalidoException();
            }
        }

        public void ExcluirCliente(string cpf)
        {
            Cliente clienteBuscado = new Cliente();
            clienteBuscado.Cpf = cpf;
            if (clienteBuscado.CpfValido())
            {
                clienteBuscado = _clienteDAO.BuscaClientePorCpf(cpf);
                if (clienteBuscado == null)
                {
                    throw new ClienteInexistenteException();
                }
                else
                {
                    _pedidoDAO.DeletaPedidosPorCpf(cpf);
                    _clienteDAO.ExcluirCliente(clienteBuscado.Cpf);
                }
            }
            else
            {
                throw new CpfInvalidoException();
            }
        }
    }
}
