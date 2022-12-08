using BlissParfumSolution.Domain.cliente;
using BlissParfumSolution.Infra.Data.Repository;
using BlissParfumSolution.WebAPI.Exceções;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BlissParfumSolution.WebAPI.Controllers
{
    [ApiController]
    [Route("clientes")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteController()
        {
            _clienteRepository = new ClienteRepository();
        }

        [HttpPost]
        public IActionResult PostCliente(Cliente novoCliente)
        {
            try
            {
                _clienteRepository.CadastrarCliente(novoCliente);
                return Ok(new Resposta(200, "Cliente cadastrado com sucesso."));
            }
            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }
        }

        [HttpGet]
        public IActionResult GetClientes()
        {
            try
            {
                return Ok(_clienteRepository.BuscarClientes());

            }
            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }
        }

        [HttpGet("cpf")]
        public IActionResult GetClientePorCpf([FromQuery] string cpf)
        {
            try
            {
                return Ok(_clienteRepository.BuscarClientePorCpf(cpf));
            }
            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }
        }

        [HttpGet("id")]
        public IActionResult GetClientePorId([FromQuery] long id)
        {
            try
            {
                return Ok(_clienteRepository.BuscarClientePorId(id));
            }
            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }
        }

        [HttpPut]
        public IActionResult PutCliente([FromBody] Cliente clienteEditado)
        {
            try
            {
                _clienteRepository.EditarCliente(clienteEditado);
                return Ok(new Resposta(200, "Cliente atualizado com sucesso."));
            }
            catch (Exception e)
            {
                return StatusCode(500, new Resposta(500, e.Message));
            }

        }

        [HttpDelete("cpf")]
        public IActionResult DeleteCliente([FromQuery] string cpf)
        {
            try
            {
                var clienteBuscado = _clienteRepository.BuscarClientePorCpf(cpf);
                _clienteRepository.ExcluirCliente(clienteBuscado.Cpf);
                return Ok(new Resposta(200, "Cliente deletado com suceso."));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
