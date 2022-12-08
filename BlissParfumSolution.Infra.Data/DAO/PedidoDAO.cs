using BlissParfumSolution.Domain.cliente;
using BlissParfumSolution.Domain.pedido;
using BlissParfumSolution.Domain.produto;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BlissParfumSolution.Infra.Data.DAO
{
    public class PedidoDAO
    {
        private const string _connectionString = @"server=.\SQLexpress;initial catalog=BLISS_PARFUM_DB;integrated security=true;";

        public Pedido BuscaPedidoPorId(Int64 idPedido)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @"SELECT PD.ID_PEDIDO, PD.CPF_CLIENTE, PD.PRODUTO_ID, PD.QUANTIDADE, PD.VALOR_TOTAL,
                                   PD.STATUS, PD.DATA_PEDIDO, C.NOME AS 'CLIENTE', PR.NOME AS 'PRODUTO', 
                                   PR.ESTA_ATIVO, PR.VALIDADE, PR.PRECO FROM PEDIDOS PD
                                   INNER JOIN PRODUTOS PR ON PR.ID_PRODUTO = PD.PRODUTO_ID
                                   LEFT JOIN CLIENTES C ON C.CPF = PD.CPF_CLIENTE 
                                   WHERE ID_PEDIDO = @ID_DIGITADO";
                    comando.Parameters.AddWithValue("@ID_DIGITADO", idPedido);
                    comando.CommandText = sql;
                    var leitor = comando.ExecuteReader();
                    while (leitor.Read())
                    {
                        Pedido pedidoBuscado = new Pedido();
                        pedidoBuscado.IdPedido = Convert.ToInt64(leitor["ID_PEDIDO"].ToString());
                        pedidoBuscado.Quantidade = Convert.ToInt32(leitor["QUANTIDADE"].ToString());
                        pedidoBuscado.DataPedido = Convert.ToDateTime(leitor["DATA_PEDIDO"].ToString());
                        pedidoBuscado.ValorTotal = Convert.ToDecimal(leitor["VALOR_TOTAL"].ToString());
                        pedidoBuscado.Status = leitor["STATUS"].ToString();
                        pedidoBuscado.Cliente = new Cliente();
                        pedidoBuscado.Produto = new Produto();
                        pedidoBuscado.Cliente.Cpf = leitor["CPF_CLIENTE"].ToString();
                        pedidoBuscado.Cliente.Nome = leitor["CLIENTE"].ToString();
                        pedidoBuscado.Produto.IdProduto = Convert.ToInt64(leitor["PRODUTO_ID"].ToString());
                        pedidoBuscado.Produto.Nome = leitor["PRODUTO"].ToString();
                        pedidoBuscado.Produto.EstaAtivo = Convert.ToBoolean(leitor["ESTA_ATIVO"]);
                        pedidoBuscado.Produto.Validade = Convert.ToDateTime(leitor["VALIDADE"].ToString());
                        pedidoBuscado.Produto.Preco = Convert.ToDecimal(leitor["PRECO"].ToString());
                        return pedidoBuscado;
                    }
                }
            }
            return null;
        }

        public List<Pedido> BuscarPedidos()
        {
            var listaPedidos = new List<Pedido>();
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @"SELECT PD.ID_PEDIDO, PD.CPF_CLIENTE, PD.PRODUTO_ID, PD.QUANTIDADE, PD.VALOR_TOTAL,
                                   PD.STATUS, PD.DATA_PEDIDO, C.NOME AS 'CLIENTE', PR.NOME AS 'PRODUTO', 
                                   PR.ESTA_ATIVO, PR.VALIDADE, PR.PRECO FROM PEDIDOS PD
                                   INNER JOIN PRODUTOS PR ON PR.ID_PRODUTO = PD.PRODUTO_ID
                                   LEFT JOIN CLIENTES C ON C.CPF = PD.CPF_CLIENTE";
                    comando.CommandText = sql;
                    var leitor = comando.ExecuteReader();
                    while (leitor.Read())
                    {   
                        Pedido pedidoBuscado = new Pedido();
                        pedidoBuscado.IdPedido = Convert.ToInt64(leitor["ID_PEDIDO"].ToString());
                        pedidoBuscado.Quantidade = Convert.ToInt32(leitor["QUANTIDADE"].ToString());
                        pedidoBuscado.DataPedido = Convert.ToDateTime(leitor["DATA_PEDIDO"].ToString());
                        pedidoBuscado.ValorTotal = Convert.ToDecimal(leitor["VALOR_TOTAL"].ToString());
                        pedidoBuscado.Status = leitor["STATUS"].ToString();
                        pedidoBuscado.Cliente = new Cliente();
                        pedidoBuscado.Produto = new Produto();
                        pedidoBuscado.Cliente.Cpf = leitor["CPF_CLIENTE"].ToString();
                        pedidoBuscado.Cliente.Nome = leitor["CLIENTE"].ToString();
                        pedidoBuscado.Produto.IdProduto = Convert.ToInt64(leitor["PRODUTO_ID"].ToString());
                        pedidoBuscado.Produto.Nome = leitor["PRODUTO"].ToString();
                        pedidoBuscado.Produto.EstaAtivo = Convert.ToBoolean(leitor["ESTA_ATIVO"]);
                        pedidoBuscado.Produto.Validade = Convert.ToDateTime(leitor["VALIDADE"].ToString());
                        pedidoBuscado.Produto.Preco = Convert.ToDecimal(leitor["PRECO"].ToString());
                        listaPedidos.Add(pedidoBuscado);
                    }
                }
            }
            return listaPedidos;
        }

        public void CadastrarPedido(Pedido pedido)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @"INSERT PEDIDOS (CPF_CLIENTE, PRODUTO_ID, QUANTIDADE, DATA_PEDIDO, VALOR_TOTAL, STATUS) VALUES
                                   (@CPF_CLIENTE, @PRODUTO_ID, @QUANTIDADE, @DATA_PEDIDO, @VALOR_TOTAL, @STATUS)";
                    comando.Parameters.AddWithValue("@CPF_CLIENTE", pedido.Cliente == null ? DBNull.Value : pedido.Cliente.Cpf);
                    comando.Parameters.AddWithValue("@PRODUTO_ID", pedido.Produto.IdProduto);
                    comando.Parameters.AddWithValue("@QUANTIDADE", pedido.Quantidade);
                    comando.Parameters.AddWithValue("@DATA_PEDIDO", pedido.RetornaDataPedido());
                    comando.Parameters.AddWithValue("@VALOR_TOTAL", pedido.RetornaValorTotal());
                    comando.Parameters.AddWithValue("@STATUS", pedido.RetornaStatus());
                    comando.CommandText = sql;
                    comando.ExecuteNonQuery();
                }
            }
        }

        public void CadastrarPedidoSemCpf(Pedido pedido)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @"INSERT PEDIDOS (PRODUTO_ID, QUANTIDADE, DATA_PEDIDO, VALOR_TOTAL, STATUS) VALUES
                                   (@PRODUTO_ID, @QUANTIDADE, @DATA_PEDIDO, @VALOR_TOTAL, @STATUS)";
                    //comando.Parameters.AddWithValue("@CPF_CLIENTE", pedido.Cliente == null ? DBNull.Value : pedido.Cliente.Cpf);
                    comando.Parameters.AddWithValue("@PRODUTO_ID", pedido.Produto.IdProduto);
                    comando.Parameters.AddWithValue("@QUANTIDADE", pedido.Quantidade);
                    comando.Parameters.AddWithValue("@DATA_PEDIDO", pedido.RetornaDataPedido());
                    comando.Parameters.AddWithValue("@VALOR_TOTAL", pedido.RetornaValorTotal());
                    comando.Parameters.AddWithValue("@STATUS", pedido.RetornaStatus());
                    comando.CommandText = sql;
                    comando.ExecuteNonQuery();
                }
            }
        }
        

        public void AtualizarPedido(Pedido pedido)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @"UPDATE PEDIDOS SET CPF_CLIENTE = @CPF_CLIENTE, PRODUTO_ID = @PRODUTO_ID, QUANTIDADE = @QUANTIDADE,
                                   DATA_PEDIDO = @DATA_PEDIDO, VALOR_TOTAL = @VALOR_TOTAL, STATUS = @STATUS
                                   WHERE ID_PEDIDO = @ID_PEDIDO";
                    comando.Parameters.AddWithValue("@ID_PEDIDO", pedido.IdPedido);
                    comando.Parameters.AddWithValue("@CPF_CLIENTE", pedido.Cliente == null ? DBNull.Value : pedido.Cliente.IdCliente);
                    comando.Parameters.AddWithValue("@PRODUTO_ID", pedido.Produto.IdProduto);
                    comando.Parameters.AddWithValue("@QUANTIDADE", pedido.Quantidade);
                    comando.Parameters.AddWithValue("@DATA_PEDIDO", pedido.RetornaDataPedido());
                    comando.Parameters.AddWithValue("@VALOR_TOTAL", pedido.RetornaValorTotal());
                    comando.Parameters.AddWithValue("@STATUS", pedido.RetornaStatus());
                    comando.CommandText = sql;
                    comando.ExecuteNonQuery();
                }
            }
        }

        public void ExcluirPedido(Pedido pedido)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @"DELETE FROM PEDIDOS WHERE ID_PEDIDO = @ID_PEDIDO";
                    comando.Parameters.AddWithValue("@ID_PEDIDO", pedido.IdPedido);
                    comando.CommandText = sql;
                    comando.ExecuteNonQuery();
                }
            }
        }

        public void DeletaPedidosPorCpf(string cpf)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @"DELETE FROM PEDIDOS WHERE CPF_CLIENTE = @CPF_DIGITADO";
                    comando.Parameters.AddWithValue("@CPF_DIGITADO", cpf);
                    comando.CommandText = sql;
                    comando.ExecuteNonQuery();
                }
            }
        }
    }
}
