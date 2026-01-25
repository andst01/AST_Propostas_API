namespace Propostas.Application.Interfaces
{
    public interface IAppBase<TEntity, TViewModel> 
        where TEntity : class
        where TViewModel : class
    {
        Task<TViewModel> AdicionarAsync(TViewModel entity);

        Task<TViewModel> AtualizarAsync(TViewModel entity, object id);

        Task<int> ExcluirAsync(int id);

        Task<List<TViewModel>> ObterTodosAsync();

        Task<TViewModel> ObterPorIdAssyn(int id);

       
    }
}
