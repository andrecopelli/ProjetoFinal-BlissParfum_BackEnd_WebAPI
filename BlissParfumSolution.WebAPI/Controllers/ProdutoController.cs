using BlissParfumSolution.Domain.produto;
using BlissParfumSolution.Infra.Data.Repository;
using BlissParfumSolution.WebAPI.Exceções;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BlissParfumSolution.WebAPI.Controllers
{
    [ApiController]
    [Route("produtos")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoController()
        {
            _produtoRepository = new ProdutoRepository();
        }

        [HttpPost]
        public IActionResult PostProduto(Produto novoProduto)
        {
            try
            {
                _produtoRepository.CadastraProduto(novoProduto);
                return Ok(new Resposta(200, "Produto cadastrado com sucesso."));
            }
            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }
        }

        [HttpGet]
        public IActionResult GetProdutos()
        {
            try
            {
                return Ok(_produtoRepository.BuscarProdutos());

            }
            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }
        }

        [HttpGet("ativos")]
        public IActionResult GetProdutosAtivos()
        {
            try
            {
                return Ok(_produtoRepository.BuscarProdutosAtivos());

            }
            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }
        }

        [HttpGet("idproduto")]
        public IActionResult GetProdutosPorId([FromQuery] long idProduto)
        {
            try
            {
                return Ok(_produtoRepository.BuscarProdutoPorId(idProduto));
            }
            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }
        }

        [HttpPut]
        public IActionResult PutProduto([FromBody] Produto produtoEditado)
        {
            try
            {
                var produtoBuscado = _produtoRepository.BuscarProdutoPorId(produtoEditado.IdProduto);
                _produtoRepository.EditarProduto(produtoEditado);
                return Ok(new Resposta(200, "Produto atualizado com sucesso."));
            }
            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }
        }

        [HttpPatch("/idproduto/ativar")]
        public IActionResult PatchProdutoAtivar([FromQuery] long idProduto)
        {
            try
            {
                var produtoAtivado = _produtoRepository.BuscarProdutoPorId(idProduto);
                _produtoRepository.AtivarProduto(produtoAtivado);
                return Ok(new Resposta(200, "Produto ativado com sucesso."));
            }
            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }
        }

        [HttpPatch("/idproduto/desativar")]
        public IActionResult PatchProdutoDesativar([FromQuery] long idProduto)
        {
            try
            {
                var produtoDesativado = _produtoRepository.BuscarProdutoPorId(idProduto);
                _produtoRepository.DesativarProduto(produtoDesativado);
                return Ok(new Resposta(200, "Produto desativado com sucesso."));
            }
            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }
        }

        [HttpPatch("idproduto/entrada-estoque")]
        public IActionResult PatchProdutoEntradaEstoque([FromQuery] long idProduto, [FromQuery] int quantidade)
        {
            try
            {
                var produtoBuscado = _produtoRepository.BuscarProdutoPorId(idProduto);
                _produtoRepository.EntradaEstoque(produtoBuscado.IdProduto, quantidade);
                return Ok(new Resposta(200, "Estoque atualizado com sucesso."));
            }
            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }
        }

        [HttpPatch("idproduto/saida-estoque")]
        public IActionResult PatchSaidaEntradaEstoque([FromQuery] long idProduto, [FromQuery] int quantidade)
        {
            try
            {
                var produtoBuscado = _produtoRepository.BuscarProdutoPorId(idProduto);
                _produtoRepository.SaidaEstoque(produtoBuscado.IdProduto, quantidade);
                return Ok(new Resposta(200, "Estoque atualizado com sucesso."));
            }
            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }
        }

    }
}

