using AutoMapper;
using Propostas.Application.DTO;
using Propostas.Application.Interfaces;
using Propostas.Domain.Interfaces;
using System.Linq.Expressions;

namespace Propostas.Application
{
    public class AppBase<TEntity, TRequest, TDto> 
        : IAppBase<TEntity, TRequest, TDto>
        where TEntity : class
        where TRequest : class
        where TDto : BaseDTO
    {

        protected readonly IRepositorioBase<TEntity> _repositorio;
        protected readonly IMapper _mapper;

        public AppBase(IRepositorioBase<TEntity> repositorio,
                       IMapper mapper)
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }
        public async Task<TDto> AdicionarAsync(TRequest request)
        {
           
            var entity = _mapper.Map<TEntity>(request);

            var resultado =  await _repositorio.AdicionarAsync(entity);

            await _repositorio.SaveChangesAsync();

            var retorno = _mapper.Map<TDto>(resultado);

            retorno.Mensagem = new();
            retorno.Mensagem.Sucesso = true;
            retorno.Mensagem.Descricao = "Registro adicionado com sucesso.";

            return retorno;
        }

        
        public async Task<TDto> AtualizarAsync(TRequest request, object id)
        {
            var entity = _mapper.Map<TEntity>(request);

            var resultado = await _repositorio.AtualizarAsync(entity, id);
            
            await _repositorio.SaveChangesAsync();

            var retorno = _mapper.Map<TDto>(resultado);

            retorno.Mensagem = new();
            retorno.Mensagem.Sucesso = true;
            retorno.Mensagem.Descricao = "Registro atualizado com sucesso.";
           
            return retorno;
        }

        public async Task<BaseDTO> ExcluirAsync(int id)
        {
            var retorno = new BaseDTO();
            retorno.Mensagem = new();

            await _repositorio.ExcluirAsync(id);
            
            var resultadp = await _repositorio.SaveChangesAsync();

            if(resultadp > 0)
            {
                retorno.Mensagem.Sucesso = true;
                retorno.Mensagem.Descricao = "Registro excluído com sucesso.";
            }
            else
            {
                retorno.Mensagem.Sucesso = false;
                retorno.Mensagem.Descricao = "Erro ao excluir o registro.";
            }

            return retorno;

        }

       
        public async Task<TDto> ObterPorIdAssyn(int id)
        {
            var retorno = await _repositorio.ObterPorIdAssyn(id);

            return _mapper.Map<TDto>(retorno);
           
        }

        public async Task<List<TDto>> ObterTodosAsync()
        {
            var retorno = await _repositorio.ObterTodosAsync();

            return _mapper.Map<List<TDto>>(retorno);
        }
    }
}
