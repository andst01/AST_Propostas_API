using System.Linq.Expressions;

namespace Propostas.Domain.Interfaces
{
    public interface IRepositorioBase<T> where T : class
    {
        Task<T> AdicionarAsync(T entity);

        Task<T> AtualizarAsync(T entity, object id);

        Task ExcluirAsync(int id);

        Task<List<T>> ObterTodosAsync();

        Task<T> ObterPorIdAssyn(int id);

        Task<int> SaveChangesAsync();

        Task<IEnumerable<T>> ObterPorFiltroAsync(Expression<Func<T, bool>> filter = null,
                                      Func<IQueryable<T>, IQueryable<T>> include = null,
                                      bool asNoTracking = true);
    }
}
