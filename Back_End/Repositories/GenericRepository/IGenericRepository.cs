using Back_End.Models.BaseEntity;

namespace Back_End.Repositories.GenericRepository
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {         
        Task<List<TEntity>> GetAll();
        IQueryable<TEntity> GetAllAsQueryable();
        Task<TEntity> GetById(Guid id);
        void Create(TEntity entity);
        Task CreateAsync(TEntity entity);
        void CreateRange(IEnumerable<TEntity> entities);
        Task CreateRangeAsync(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);

        void Delete(TEntity entity);
        void DeleteRange(IEnumerable<TEntity> entities);

        TEntity FindById(object id);
        Task<TEntity> FindByIdAsync(object id);
        bool Save();
        Task<bool> SaveAsync();
    }
}
