using System.Collections.Generic;

namespace BlissParfumSolution.Domain.produto
{
    public interface IProdutoRepository
    {
        void CadastraProduto(Produto produto);
        void EditarProduto(Produto produto);
        void EntradaEstoque(long idProduto, int quantidade);
        void SaidaEstoque(long idProduto, int quantidade);
        void AtivarProduto(Produto produto);
        void DesativarProduto(Produto produto);
        Produto BuscarProdutoPorId(long idProduto);
        List<Produto> BuscarProdutos();
        List<Produto> BuscarProdutosAtivos();
    }
}
