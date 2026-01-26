using AutoMapper;
using Propostas.Application.Interfaces;
using Propostas.Application.DTO;
using Propostas.Domain.Entidade;
using Propostas.Domain.Interfaces;

namespace Propostas.Application
{
    public class PropostaApp : AppBase<Proposta, PropostaDTO>, IPropostaApp
    {
        private readonly IPropostaRepositorio _repositorio;
        public PropostaApp(IPropostaRepositorio repositorio, 
                           IMapper mapper) : base(repositorio, mapper)
        {
            _repositorio = repositorio;
        }

        public async Task<List<PropostaDTO>> ObterDadosPropostaClienteAsync()
        {
            var propostas = await _repositorio.ObterDadosPropostaClienteAsync();
            return _mapper.Map<List<PropostaDTO>>(propostas);
        }

        public async Task<List<PropostaDTO>> ObterPropostaAprovadaSemApoliceAsync()
        {
            var propostas = await _repositorio.ObterPropostaAprovadaSemApoliceAsync();
            return _mapper.Map<List<PropostaDTO>>(propostas);
        }
    }
}
