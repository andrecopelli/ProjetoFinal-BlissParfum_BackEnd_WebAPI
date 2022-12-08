using BlissParfumSolution.Domain.cliente;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BlissParfumSolution.Infra.Data.DAO
{
    public class ClienteDAO
    {
        private const string _connectionString = @"server=.\SQLexpress;initial catalog=BLISS_PARFUM_DB;integrated security=true;";

        public Cliente BuscaClientePorCpf(string cpfDigitado)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @"SELECT ID_CLIENTE, NOME, CPF, DATA_NASCIMENTO, PONTOS_FIDELIDADE FROM CLIENTES WHERE CPF = @CPF_DIGITADO";
                    comando.Parameters.AddWithValue("@CPF_DIGITADO", cpfDigitado);
                    comando.CommandText= sql;
                    var leitor = comando.ExecuteReader();
                    while (leitor.Read())
                    {
                        Cliente clienteBuscado = new Cliente();
                        clienteBuscado.IdCliente = Convert.ToInt64(leitor["ID_CLIENTE"].ToString());
                        clienteBuscado.Nome = leitor["NOME"].ToString();
                        clienteBuscado.Cpf = leitor["CPF"].ToString();
                        clienteBuscado.DataDeNascimento = Convert.ToDateTime(leitor["DATA_NASCIMENTO"].ToString());
                        clienteBuscado.PontosFidelidade = Convert.ToDecimal(leitor["PONTOS_FIDELIDADE"].ToString());
                        return clienteBuscado;
                    }
                }
            }
            return null;
        }

        public Cliente BuscaClientePorId(long id)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @"SELECT *FROM CLIENTES WHERE ID_CLIENTE = @CLIENTE_ID";
                    comando.Parameters.AddWithValue("@CLIENTE_ID", id);
                    comando.CommandText = sql;
                    var leitor = comando.ExecuteReader();
                    while (leitor.Read())
                    {
                        Cliente clienteBuscado = new Cliente();
                        clienteBuscado.IdCliente = Convert.ToInt64(leitor["ID_CLIENTE"].ToString());
                        clienteBuscado.Nome = leitor["NOME"].ToString();
                        clienteBuscado.Cpf = leitor["CPF"].ToString();
                        clienteBuscado.DataDeNascimento = Convert.ToDateTime(leitor["DATA_NASCIMENTO"].ToString());
                        clienteBuscado.PontosFidelidade = Convert.ToDecimal(leitor["PONTOS_FIDELIDADE"].ToString());
                        return clienteBuscado;
                    }
                }
            }
            return null;
        }

        public List<Cliente> BuscarClientes()
        {
            var listaClientes = new List<Cliente>();
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection= conexao;
                    string sql = @"SELECT ID_CLIENTE, NOME, CPF, DATA_NASCIMENTO, PONTOS_FIDELIDADE FROM CLIENTES";
                    comando.CommandText = sql;
                    var leitor = comando.ExecuteReader();
                    while (leitor.Read())
                    {
                        Cliente clienteBuscado = new Cliente();
                        clienteBuscado.IdCliente = Convert.ToInt64(leitor["ID_CLIENTE"].ToString());
                        clienteBuscado.Nome = leitor["NOME"].ToString();
                        clienteBuscado.Cpf = leitor["CPF"].ToString();
                        clienteBuscado.DataDeNascimento = Convert.ToDateTime(leitor["DATA_NASCIMENTO"].ToString());
                        clienteBuscado.PontosFidelidade = Convert.ToDecimal(leitor["PONTOS_FIDELIDADE"].ToString());
                        listaClientes.Add(clienteBuscado);
                    }
                }
            }
            return listaClientes;
        }

        public void CadastrarCliente(Cliente cliente)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @"INSERT CLIENTES (NOME, CPF, DATA_NASCIMENTO) VALUES (@NOME, @CPF, @DATA_NASCIMENTO)";
                    comando.Parameters.AddWithValue("@NOME", cliente.Nome);
                    comando.Parameters.AddWithValue("@CPF", cliente.Cpf);
                    comando.Parameters.AddWithValue("@DATA_NASCIMENTO", cliente.DataDeNascimento);
                    comando.CommandText = sql;
                    comando.ExecuteNonQuery();
                }
            }
        }

        public void EditarCliente(Cliente cliente)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @"UPDATE CLIENTES SET CPF = @CPF, NOME = @NOME, DATA_NASCIMENTO = @DATA_NASCIMENTO
                                 WHERE ID_CLIENTE = @ID";
                    comando.Parameters.AddWithValue("@ID", cliente.IdCliente);
                    comando.Parameters.AddWithValue("@CPF", cliente.Cpf);
                    comando.Parameters.AddWithValue("@NOME", cliente.Nome);
                    comando.Parameters.AddWithValue("@DATA_NASCIMENTO", cliente.DataDeNascimento);
                    comando.CommandText = sql;
                    comando.ExecuteNonQuery();
                }
            }
        }

        public void AtualizarPontos(string cpf, decimal pontos)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @"UPDATE CLIENTES SET PONTOS_FIDELIDADE = @PONTOS_FIDELIDADE
                                 WHERE CPF = @CPF";
                    comando.Parameters.AddWithValue("@CPF", cpf);
                    comando.Parameters.AddWithValue("@PONTOS_FIDELIDADE", pontos);
                    comando.CommandText = sql;
                    comando.ExecuteNonQuery();
                }
            }
        }

        public void ExcluirCliente(string cpfDigitado)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @"DELETE FROM CLIENTES WHERE CPF = @CPF_CLIENTE;";
                    comando.Parameters.AddWithValue("@CPF_CLIENTE", cpfDigitado);
                    comando.CommandText = sql;
                    comando.ExecuteNonQuery();
                }
            }
        }
    }
}
