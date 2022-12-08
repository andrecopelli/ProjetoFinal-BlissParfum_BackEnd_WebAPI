using BlissParfumSolution.Domain.produto;
using BlissParfumSolution.Infra.Data.DAO;
using BlissParfumSolution.Infra.Data.Exceções;
using System.Collections.Generic;

namespace BlissParfumSolution.Infra.Data.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private ProdutoDAO _produtoDAO;

        public ProdutoRepository()
        {
            _produtoDAO = new ProdutoDAO();
        }
        public void AtivarProduto(Produto produto)
        {
            produto.AtivaProduto();
            _produtoDAO.AtualizarProduto(produto);
        }

        public Produto BuscarProdutoPorId(long idProduto)
        {
            var produtoBuscado = _produtoDAO.BuscaProdutoPorId(idProduto);
            if (produtoBuscado == null)
            {
                throw new ProdutoInexistenteException();
            }
            return produtoBuscado;
        }

        public List<Produto> BuscarProdutos()
        {
            var listaProdutos = _produtoDAO.BuscarProdutos();
            if (listaProdutos.Count == 0)
            {
                throw new ProdutosNaoCadastradosException();
            }
            return listaProdutos;
        }

        public List<Produto> BuscarProdutosAtivos()
        {
            var listaProdutos = _produtoDAO.BuscarProdutosAtivos();
            if (listaProdutos.Count == 0)
            {
                throw new ProdutosNaoCadastradosException();
            }
            return listaProdutos;
        }

        public void CadastraProduto(Produto produto)
        {
            if (produto.ProdutoValido())
            {
                if (produto.DataValidadeValida())
                {
                    if (produto.VolumeValido())
                    {
                        if (produto.PrecoValido())
                        {
                            _produtoDAO.CadastrarProduto(produto);
                        }
                        else
                        {
                            throw new PrecoInvalidoException();
                        }
                    }
                    else
                    {
                        throw new VolumeInvalidoException();
                    }
                }
                else
                {
                    throw new DataDeValidadeInvalidaException();
                }
            }
            else
            {
                throw new ProdutoInvalidoException();
            }
        }

        public void DesativarProduto(Produto produto)
        {
            produto.DestativaProduto();
            _produtoDAO.AtualizarProduto(produto);
        }

        public void EditarProduto(Produto produto)
        {
            var produtoBuscado = _produtoDAO.BuscaProdutoPorId(produto.IdProduto);
            if (produtoBuscado == null)
            {
                throw new ProdutoInexistenteException();
            }
            _produtoDAO.AtualizarProduto(produto);

        }

        public void EntradaEstoque(long idProduto, int quantidade)
        {
            var produtoBuscado = _produtoDAO.BuscaProdutoPorId(idProduto);
            if (produtoBuscado == null)
            {
                throw new ProdutoInexistenteException();
            }
            produtoBuscado.Estoque += quantidade;
            _produtoDAO.AtualizarProduto(produtoBuscado);
        }

        public void SaidaEstoque(long idProduto, int quantidade)
        {
            var produtoBuscado = _produtoDAO.BuscaProdutoPorId(idProduto);
            if (produtoBuscado == null)
            {
                throw new ProdutoInexistenteException();
            }
            else
            {
                if (produtoBuscado.Estoque < quantidade)
                {
                    throw new EstoqueInsuficienteException();
                }
                else
                {
                    produtoBuscado.Estoque -= quantidade;
                    _produtoDAO.AtualizarProduto(produtoBuscado);
                }
            }
        }
    }
}
