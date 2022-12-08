using BlissParfumSolution.Domain.produto;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BlissParfumSolution.Infra.Data.DAO
{
    public class ProdutoDAO
    {
        private const string _connectionString = @"server=.\SQLexpress;initial catalog=BLISS_PARFUM_DB;integrated security=true;";

        public Produto BuscaProdutoPorId(Int64 idProduto)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @"SELECT ID_PRODUTO, NOME, DESCRICAO, VOLUME, VALIDADE, ESTA_ATIVO, ESTOQUE, PRECO
                                   FROM PRODUTOS WHERE ID_PRODUTO = @ID_DIGITADO";
                    comando.Parameters.AddWithValue("@ID_DIGITADO", idProduto);
                    comando.CommandText = sql;
                    var leitor = comando.ExecuteReader();
                    while (leitor.Read())
                    {
                        Produto produtoBuscado = new Produto();
                        produtoBuscado.IdProduto = Convert.ToInt64(leitor["ID_PRODUTO"].ToString());
                        produtoBuscado.Nome = leitor["NOME"].ToString();
                        produtoBuscado.Descricao = leitor["DESCRICAO"].ToString();
                        produtoBuscado.Volume = Convert.ToInt32(leitor["VOLUME"].ToString());
                        produtoBuscado.Validade = Convert.ToDateTime(leitor["VALIDADE"].ToString());
                        produtoBuscado.EstaAtivo = Convert.ToBoolean(leitor["ESTA_ATIVO"]);
                        produtoBuscado.Estoque = Convert.ToInt32(leitor["ESTOQUE"].ToString());
                        produtoBuscado.Preco = Convert.ToDecimal(leitor["PRECO"].ToString());
                        return produtoBuscado;
                    }
                }
            }
            return null;
        }

        public List<Produto> BuscarProdutos()
        {
            var listaProdutos = new List<Produto>();
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @"SELECT ID_PRODUTO, NOME, DESCRICAO, VOLUME, VALIDADE, ESTA_ATIVO, PRECO, ESTOQUE
                                   FROM PRODUTOS";
                    comando.CommandText = sql;
                    var leitor = comando.ExecuteReader();
                    while (leitor.Read())
                    {
                        Produto produtoBuscado = new Produto();
                        produtoBuscado.IdProduto = Convert.ToInt64(leitor["ID_PRODUTO"].ToString());
                        produtoBuscado.Nome = leitor["NOME"].ToString();
                        produtoBuscado.Descricao = leitor["DESCRICAO"].ToString();
                        produtoBuscado.Volume = Convert.ToInt32(leitor["VOLUME"].ToString());
                        produtoBuscado.Validade = Convert.ToDateTime(leitor["VALIDADE"].ToString());
                        produtoBuscado.EstaAtivo = Convert.ToBoolean(leitor["ESTA_ATIVO"]);
                        produtoBuscado.Estoque = Convert.ToInt32(leitor["ESTOQUE"].ToString());
                        produtoBuscado.Preco = Convert.ToDecimal(leitor["PRECO"].ToString());
                        listaProdutos.Add(produtoBuscado);
                    }
                }
            }
            return listaProdutos;
        }

        public List<Produto> BuscarProdutosAtivos()
        {
            var listaProdutos = new List<Produto>();
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @"SELECT ID_PRODUTO, NOME, DESCRICAO, VOLUME, VALIDADE, ESTA_ATIVO, PRECO, ESTOQUE
                                   FROM PRODUTOS WHERE ESTOQUE > 0 AND ESTA_ATIVO = 1";
                    comando.CommandText = sql;
                    var leitor = comando.ExecuteReader();
                    while (leitor.Read())
                    {
                        Produto produtoBuscado = new Produto();
                        produtoBuscado.IdProduto = Convert.ToInt64(leitor["ID_PRODUTO"].ToString());
                        produtoBuscado.Nome = leitor["NOME"].ToString();
                        produtoBuscado.Descricao = leitor["DESCRICAO"].ToString();
                        produtoBuscado.Volume = Convert.ToInt32(leitor["VOLUME"].ToString());
                        produtoBuscado.Validade = Convert.ToDateTime(leitor["VALIDADE"].ToString());
                        produtoBuscado.EstaAtivo = Convert.ToBoolean(leitor["ESTA_ATIVO"]);
                        produtoBuscado.Estoque = Convert.ToInt32(leitor["ESTOQUE"].ToString());
                        produtoBuscado.Preco = Convert.ToDecimal(leitor["PRECO"].ToString());
                        listaProdutos.Add(produtoBuscado);
                    }
                }
            }
            return listaProdutos;
        }

        public void CadastrarProduto(Produto produto)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @"INSERT PRODUTOS (NOME, DESCRICAO, VOLUME, VALIDADE, ESTA_ATIVO, PRECO, ESTOQUE) VALUES
                                   (@NOME, @DESCRICAO, @VOLUME, @VALIDADE, @ESTA_ATIVO, @PRECO, @ESTOQUE)";
                    comando.Parameters.AddWithValue("@NOME", produto.Nome);
                    comando.Parameters.AddWithValue("@DESCRICAO", produto.Descricao);
                    comando.Parameters.AddWithValue("@VOLUME", produto.Volume);
                    comando.Parameters.AddWithValue("@VALIDADE", produto.Validade);
                    comando.Parameters.AddWithValue("@ESTA_ATIVO", produto.RetornaEstaAtivo());
                    comando.Parameters.AddWithValue("@PRECO", produto.Preco);
                    comando.Parameters.AddWithValue("@ESTOQUE", produto.RetornaEstoque());
                    comando.CommandText = sql;
                    comando.ExecuteNonQuery();
                }
            }
        }

        public void AtualizarProduto(Produto produto)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @"UPDATE PRODUTOS SET NOME = @NOME, DESCRICAO = @DESCRICAO, VOLUME = @VOLUME,
                                   VALIDADE = @VALIDADE, ESTA_ATIVO = @ESTA_ATIVO, PRECO = @PRECO, ESTOQUE = @ESTOQUE
                                   WHERE ID_PRODUTO = @ID_PRODUTO";
                    comando.Parameters.AddWithValue("@ID_PRODUTO", produto.IdProduto);
                    comando.Parameters.AddWithValue("@NOME", produto.Nome);
                    comando.Parameters.AddWithValue("@DESCRICAO", produto.Descricao);
                    comando.Parameters.AddWithValue("@VOLUME", produto.Volume);
                    comando.Parameters.AddWithValue("@VALIDADE", produto.Validade);
                    comando.Parameters.AddWithValue("@ESTA_ATIVO", produto.EstaAtivo);
                    comando.Parameters.AddWithValue("@PRECO", produto.Preco);
                    comando.Parameters.AddWithValue("@ESTOQUE", produto.RetornaEstoque());
                    comando.CommandText = sql;
                    comando.ExecuteNonQuery();
                }
            }
        }

        public void AtualizarEstoque(Produto produto)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @"UPDATE PRODUTOS SET NOME = @NOME, DESCRICAO = @DESCRICAO, VOLUME = @VOLUME,
                                   VALIDADE = @VALIDADE, ESTA_ATIVO = @ESTA_ATIVO, PRECO = @PRECO, ESTOQUE = @ESTOQUE
                                   WHERE ID_PRODUTO = @ID_PRODUTO";
                    comando.Parameters.AddWithValue("@ID_PRODUTO", produto.IdProduto);
                    comando.Parameters.AddWithValue("@NOME", produto.Nome);
                    comando.Parameters.AddWithValue("@DESCRICAO", produto.Descricao);
                    comando.Parameters.AddWithValue("@VOLUME", produto.Volume);
                    comando.Parameters.AddWithValue("@VALIDADE", produto.Validade);
                    comando.Parameters.AddWithValue("@ESTA_ATIVO", produto.EstaAtivo);
                    comando.Parameters.AddWithValue("@PRECO", produto.Preco);
                    comando.Parameters.AddWithValue("@ESTOQUE", produto.RetornaEstoque());
                    comando.CommandText = sql;
                    comando.ExecuteNonQuery();
                }
            }
        }
    }
}
