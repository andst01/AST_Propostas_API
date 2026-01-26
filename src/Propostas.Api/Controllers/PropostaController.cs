using Microsoft.AspNetCore.Mvc;
using Propostas.Application.Interfaces;
using Propostas.Application.DTO;

namespace Propostas.Api.Controllers
{
    public class PropostaController : ControllerBase
    {
        private readonly IPropostaApp _application;
        private readonly ILogger<PropostaController> _logger;

        public PropostaController(IPropostaApp application, 
                                 ILogger<PropostaController> logger)
        {
            _application = application;
            _logger = logger;
        }

        [HttpGet]
        [Route("Obter/{id}")]
        [ProducesResponseType(typeof(PropostaDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterPorId(int id)
        {
            _logger.LogInformation("Obtendo proposta com ID: {Id} ", id);
            return Ok(await _application.ObterPorIdAssyn(id));
        }

        [HttpGet]
        [Route("ObterTodos")]
        [ProducesResponseType(typeof(PropostaDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterTodos()
        {
            _logger.LogInformation("Obtendo todas as propostas");
            return Ok(await _application.ObterTodosAsync());
        }

        [HttpGet]
        [Route("ObterDadosPropostaCliente")]
        [ProducesResponseType(typeof(PropostaDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterDadosPropostaClienteAsync()
        {
            _logger.LogInformation("Obtendo todas as propostas com clientes ");
            return Ok(await _application.ObterDadosPropostaClienteAsync());
        }

        [HttpGet]
        [Route("ObterPropostaAprovadaSemApolice")]
        [ProducesResponseType(typeof(PropostaDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterPropostaAprovadaSemApoliceAsync()
        {
            _logger.LogInformation("Obtendo todas as propostas sem apolice");
            return Ok(await _application.ObterPropostaAprovadaSemApoliceAsync());
        }

        [HttpPost]
        [Route("Novo")]
        [ProducesResponseType(typeof(PropostaDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Novo([FromBody] PropostaDTO request)
        {
            _logger.LogInformation("Adicionando nova proposta");
            return Ok(await _application.AdicionarAsync(request));
        }

        [HttpPut]
        [Route("Atualizar")]
        [ProducesResponseType(typeof(PropostaDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Atualizar([FromBody] PropostaDTO request)
        {
            _logger.LogInformation("Atualizando proposta com ID: {Id} ", request.Id);
            return Ok(await _application.AtualizarAsync(request, request.Id));
        }

        [HttpDelete]
        [Route("Excluir/{id}")]
        [ProducesResponseType(typeof(PropostaDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Excluir(int id)
        {
            _logger.LogInformation("Atualizando proposta com ID: {Id} ", id);
            return Ok(await _application.ExcluirAsync(id));
        }
    }
}
