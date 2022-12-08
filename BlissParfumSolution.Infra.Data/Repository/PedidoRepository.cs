using BlissParfumSolution.Domain.cliente;
using BlissParfumSolution.Domain.pedido;
using BlissParfumSolution.Infra.Data.DAO;
using BlissParfumSolution.Infra.Data.Exceções;
using System.Collections.Generic;

namespace BlissParfumSolution.Infra.Data.Repository
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly ClienteDAO _clienteDAO;
        private readonly ProdutoDAO _produtoDAO;
        private readonly PedidoDAO _pedidoDAO;
        private readonly ProdutoRepository _produtoRepository;

        public PedidoRepository()
        {
            _clienteDAO = new ClienteDAO();
            _produtoDAO = new ProdutoDAO();
            _pedidoDAO = new PedidoDAO();
            _produtoRepository = new ProdutoRepository();
        }
        public string AcompanharStatus(long idPedido)
        {
            var pedidoBuscado = _pedidoDAO.BuscaPedidoPorId(idPedido);
            if (pedidoBuscado == null)
            {
                throw new PedidoInexistenteException();
            }
            return pedidoBuscado.Status;
        }

        public void AtualizarStatus(long idPedido, string status)
        {
            var pedidoBuscado = _pedidoDAO.BuscaPedidoPorId(idPedido);
            if (pedidoBuscado == null)
            {
                throw new PedidoInexistenteException();
            }
            pedidoBuscado.Status = status;
            _pedidoDAO.AtualizarPedido(pedidoBuscado);
        }

        public Pedido BuscarPedidoPorId(long idPedido)
        {
            var pedidoBuscado = _pedidoDAO.BuscaPedidoPorId(idPedido);
            if (pedidoBuscado == null)
            {
                throw new PedidoInexistenteException();
            }
            return pedidoBuscado;
        }

        public List<Pedido> BuscarPedidos()
        {
            var listaPedidos = _pedidoDAO.BuscarPedidos();
            if (listaPedidos.Count == 0)
            {
                throw new PedidosNaoCadastradosExcpetion();
            }
            return listaPedidos;
        }

        public void CadastraPedido(Pedido pedido)
        {
            pedido.Produto = _produtoDAO.BuscaProdutoPorId(pedido.Produto.IdProduto);  //Passa o objeto inteiro do produto para o pedido
            if (pedido.Cliente == null) //Faz a distinção entre um pedido com e sem cliente
            {
                if (pedido.PedidoValido()) //Valida as entradas do pedido
                {
                    _produtoRepository.SaidaEstoque(pedido.Produto.IdProduto, pedido.Quantidade); //Da a saída da quantidade de produtos no estoque
                    pedido.RetornaValorTotal(); //Método da classe pedido que retorna o valor total do pedido e atribuí
                    _pedidoDAO.CadastrarPedidoSemCpf(pedido); //Método que recebe os pedidos sem CPF e insere no BD via camada DAO
                }
            }
            else
            {
                var clienteExistente = _clienteDAO.BuscaClientePorCpf(pedido.Cliente.Cpf); //Se chegar aqui o CPF do cliente foi digitado
                if (clienteExistente == null) //Se o cliente não existir dispara a exceção
                {
                    throw new ClienteInexistenteException();
                }
                else  //Se o cliente existir
                {
                    if (pedido.Cliente.CpfValido())  //Valida o CPF
                    {
                        if (pedido.PedidoValido()) //Valida o Pedido
                        {
                            if (pedido.Produto.EstaAtivo) //Verifica se o produto está ativo, se estiver desativado dispara exceção
                            {
                                pedido.Cliente = clienteExistente; //Atribui o cliente ao pedido
                                pedido.RetornaValorTotal(); //Atribui o valor total
                                pedido.Cliente.AtribuiPontos(pedido.ValorTotal); //Calcula os pontos de fidelidade do cliente através do valor total
                                _clienteDAO.AtualizarPontos(pedido.Cliente.Cpf, pedido.Cliente.PontosFidelidade); //Atualiza os pontos do cliente no BD
                                _produtoRepository.SaidaEstoque(pedido.Produto.IdProduto, pedido.Quantidade); //Da a saída da quantidade de produtos no estoque
                                _pedidoDAO.CadastrarPedido(pedido); //Cadastra o pedido no BD
                            }
                            else
                            {
                                throw new ProdutoDesativadoException();
                            }
                        }
                        else
                        {
                            throw new PedidoInvalidoException();
                        }
                    }
                    else
                    {
                        throw new CpfInvalidoException();
                    }
                }
                

            }
            
        }

        public void ExcluirPedido(long idPedido)
        {
            var pedidoBuscado = _pedidoDAO.BuscaPedidoPorId(idPedido);
            if (pedidoBuscado == null)
            {
                throw new PedidoInexistenteException();
            }
            _pedidoDAO.ExcluirPedido(pedidoBuscado);
        }
    }
}
