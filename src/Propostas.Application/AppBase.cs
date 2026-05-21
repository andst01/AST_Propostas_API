using Propostas.Application.DTO;
using Propostas.Application.Interfaces;
using Propostas.Application.Interfaces.Map;
using Propostas.Domain.Interfaces;

namespace Propostas.Application
{
    public class AppBase<TEntity, TRequest, TDto> 
        : IAppBase<TEntity, TRequest, TDto>
        where TEntity : class
        where TRequest : class
        where TDto : BaseDTO
    {
        protected readonly IMapBase<TEntity, TRequest> _mapRequestToEntity;
        protected readonly IMapBase<TDto,  TEntity> _mapEntityToDto;
        protected readonly IRepositorioBase<TEntity> _repositorio;
       
        
        /// <summary>
        /// Initializes a new instance of the <see cref="AppBase{TEntity, TRequest, TDto}"/> class.
        /// </summary>
        /// <param name="repositorio">The repository.</param>
        /// <param name="mapRequestToEntity">The map request to entity.</param>
        /// <param name="mapEntityToDto">The map entity to DTO.</param>
        public AppBase(IRepositorioBase<TEntity> repositorio,
                       IMapBase<TEntity, TRequest> mapRequestToEntity,
                       IMapBase<TDto, TEntity> mapEntityToDto)
        {
            _repositorio = repositorio;
            _mapRequestToEntity = mapRequestToEntity;
            _mapEntityToDto = mapEntityToDto;
        }
        public async Task<TDto> AdicionarAsync(TRequest request)
        {
            // var entity = _mapper.Map<TEntity>(request);
            var entity = _mapRequestToEntity.Map(request);

            var resultado =  await _repositorio.AdicionarAsync(entity);

            await _repositorio.SaveChangesAsync();

            //var retorno = _mapper.Map<TDto>(resultado);
            var retorno = _mapEntityToDto.Map(resultado);

            retorno.Mensagem = new();
            retorno.Mensagem.Sucesso = true;
            retorno.Mensagem.Descricao = "Registro adicionado com sucesso.";

            return retorno;
        }

        
        public async Task<TDto> AtualizarAsync(TRequest request, object id)
        {
            //var entity = _mapper.Map<TEntity>(request);
            var entity = _mapRequestToEntity.Map(request);

            var resultado = await _repositorio.AtualizarAsync(entity, id);
            
            await _repositorio.SaveChangesAsync();

            //var retorno = _mapper.Map<TDto>(resultado);
            var retorno = _mapEntityToDto.Map(resultado);

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

            //return _mapper.Map<TDto>(retorno);
            return _mapEntityToDto.Map(retorno);
           
        }

        public async Task<List<TDto>> ObterTodosAsync()
        {
            var result = await _repositorio.ObterTodosAsync();
            var retorno = result.Select(x => _mapEntityToDto.Map(x)).ToList();

            // return _mapper.Map<List<TDto>>(retorno);
            return retorno;
        }
    }
}
