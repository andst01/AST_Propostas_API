using Propostas.Application.Interfaces;
using Propostas.Application.DTO;
using Propostas.Domain.Entidade;
using Propostas.Domain.Interfaces;
using Propostas.Application.Request;
using Propostas.Application.Interfaces.Map;

namespace Propostas.Application
{
    public class PropostaApp : AppBase<Proposta, 
                                       PropostaRequest, 
                                       PropostaDTO>, IPropostaApp
    {
        private readonly IPropostaRepositorio _repositorio;
        private readonly IMapBase<PropostaDTO, Proposta> _mapEntityToDto;

       
        public PropostaApp(IPropostaRepositorio repositorio,
                           IMapBase<Proposta, PropostaRequest> mapRequestToEntity,
                           IMapBase<PropostaDTO, Proposta> mapEntityToDto) 
            : base(repositorio, mapRequestToEntity, mapEntityToDto)
        {
            _repositorio = repositorio;
            _mapEntityToDto = mapEntityToDto;
        }

        public async Task<List<PropostaDTO>> ObterDadosPropostaClienteAsync()
        {
            var result = await _repositorio.ObterPropostaClienteAsync();
            var retorno = result.Select(x =>_mapEntityToDto.Map(x)).ToList();
            return retorno;
        }

        public async Task<List<PropostaDTO>> ObterPropostaAprovadaSemApoliceAsync()
        {
            var result = await _repositorio.ObterPropostaAprovadaSemApoliceAsync();
            var retorno = result.Select(x => _mapEntityToDto.Map(x)).ToList();
            return retorno;
        }

        public async Task<PropostaDTO> ObterPropostaClientePorIdAsync(int id)
        {
            var proposta = await _repositorio.ObterPropostaClientePorIdAsync(id);
            return _mapEntityToDto.Map(proposta);
        }

        public async Task<List<PropostaDTO>> ObterTodosComFiltroAsync(DateTime? dataCriacao, string? numeroProposta, int status)
        {
            var result = await _repositorio.ObterTodosComFiltroAsync(dataCriacao, numeroProposta, status);
            var retorno = result.Select(x => _mapEntityToDto.Map(x)).ToList();
            return retorno;
        }
    }
}
