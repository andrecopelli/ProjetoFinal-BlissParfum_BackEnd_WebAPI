using BlissParfumSolution.Domain.pedido;
using BlissParfumSolution.Infra.Data.Repository;
using BlissParfumSolution.WebAPI.Exceções;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BlissParfumSolution.WebAPI.Controllers
{
    [ApiController]
    [Route("pedidos")]
    public class PedidoController : Controller
    {
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoController()
        {
            _pedidoRepository = new PedidoRepository();
        }

        [HttpPost]
        public IActionResult PostPedido([FromBody] Pedido pedido)
        {
            try
            {
                _pedidoRepository.CadastraPedido(pedido);
                return Ok(new Resposta(200, $"Seu pedido foi cadastrado com sucesso."));
            }
            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }
        }

        [HttpGet]
        public IActionResult GetPedidos()
        {
            try
            {
                return Ok(_pedidoRepository.BuscarPedidos());
            }
            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }
        }

        [HttpGet("idpedido")]
        public IActionResult GetPedidoPorId([FromQuery] long idPedido)
        {
            try
            {
                return Ok(_pedidoRepository.BuscarPedidoPorId(idPedido));
            }
            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }
        }

        [HttpDelete]
        public IActionResult DeletePedido([FromQuery] long idPedido)
        {
            try
            {
                _pedidoRepository.ExcluirPedido(idPedido);
                return Ok(new Resposta(200, "Pedido deletado com suceso."));
            }
            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }
        }

        [HttpGet("idpedido/status")]
        public IActionResult GetStatusPorId([FromQuery] long idPedido)
        {
            try
            {
                return Ok(_pedidoRepository.AcompanharStatus(idPedido));
            }
            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }
        }

        [HttpPatch]
        [HttpGet("idpedido/status/atualizar")]
        public IActionResult PatchStatus([FromQuery] long idPedido, [FromQuery] string status)
        {
            try
            {
                _pedidoRepository.AtualizarStatus(idPedido, status);
                return Ok(new Resposta(200, $"O status do seu pedido foi alterado com sucesso."));
            }
            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }
        }
    }
}
