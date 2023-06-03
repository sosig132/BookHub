using Back_End.Data;
using Back_End.Models.BaseEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Back_End.Repositories.GenericRepository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly Context _context;

        protected readonly DbSet<TEntity> _table;
        
        public GenericRepository(Context context)
        {
            _context = context;
            _table = _context.Set<TEntity>();
        }
        public void Create(TEntity entity)
        {
            _table.Add(entity);
        }

        public async Task CreateAsync(TEntity entity)
        {
            await _table.AddAsync(entity);
        }

        public void CreateRange(IEnumerable<TEntity> entities)
        {
            _table.AddRange(entities);
        }

        public async Task CreateRangeAsync(IEnumerable<TEntity> entities)
        {
            await _table.AddRangeAsync();
        }

        public void Delete(TEntity entity)
        {
            _table.Remove(entity);
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            _table.RemoveRange(entities);
        }

        public TEntity FindById(object id)
        {
            return _table.Find(id);
        }

        public async Task<TEntity> FindByIdAsync(object id)
        {
            return await _table.FindAsync(id);
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await _table.AsNoTracking().ToListAsync();
        }

        public IQueryable<TEntity> GetAllAsQueryable()
        {
            return _table.AsNoTracking();
        }

        public Task<TEntity> GetById(Guid id)
        {
            return _table.FirstOrDefaultAsync(e => e.Id == id);
        }

        public bool Save()
        {
            return _context.SaveChanges()>0;
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync()>0;
        }

        public void Update(TEntity entity)
        {
            _table.Update(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            _table.UpdateRange(entities);
        }
    }
}
