using AutoMapper;
using Propostas.Application.Interfaces;
using Propostas.Application.DTO;
using Propostas.Domain.Entidade;
using Propostas.Domain.Interfaces;
using Propostas.Application.Request;

namespace Propostas.Application
{
    public class PropostaApp : AppBase<Proposta, 
                                       PropostaRequest, 
                                       PropostaDTO>, IPropostaApp
    {
        private readonly IPropostaRepositorio _repositorio;
        public PropostaApp(IPropostaRepositorio repositorio, 
                           IMapper mapper) : base(repositorio, mapper)
        {
            _repositorio = repositorio;
        }

        public async Task<List<PropostaDTO>> ObterDadosPropostaClienteAsync()
        {
            var propostas = await _repositorio.ObterPropostaClienteAsync();
            return _mapper.Map<List<PropostaDTO>>(propostas);
        }

        public async Task<List<PropostaDTO>> ObterPropostaAprovadaSemApoliceAsync()
        {
            var propostas = await _repositorio.ObterPropostaAprovadaSemApoliceAsync();
            return _mapper.Map<List<PropostaDTO>>(propostas);
        }

        public async Task<PropostaDTO> ObterPropostaClientePorIdAsync(int id)
        {
            var proposta = await _repositorio.ObterPropostaClientePorIdAsync(id);
            return _mapper.Map<PropostaDTO>(proposta);
        }
    }
}
