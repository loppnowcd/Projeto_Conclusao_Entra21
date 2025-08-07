
using Microsoft.EntityFrameworkCore;
using via_entrega.entities;
using via_entrega.Interfaces.Common;

namespace via_entrega.repositoriess
{
	public abstract class BaseRepository<T> : ICRUD<T> where T : class, entities.IEntityBase
	{
		private readonly ViaEntregaContext _viaEntregaContext;
		internal readonly DbSet<T> _dbSet;
		protected BaseRepository(ViaEntregaContext viaEntregaContext)
		{
			_dbSet = viaEntregaContext.Set<T>();
			_viaEntregaContext = viaEntregaContext;
		}

		public virtual async Task<List<T?>> GetAllAsync()
		{
			return await _dbSet.ToListAsync();
		}

		public virtual async Task<T?> GetByIdAsync(Guid id)
		{
			return await _dbSet.FindAsync(id);
		}

		public virtual async Task<Guid?> CreateAsync(T entity)
		{
			await _dbSet.AddAsync(entity);
			entity.CreatedAt = DateTime.UtcNow;
			await _viaEntregaContext.SaveChangesAsync();
			return entity.Id;
		}

		public virtual async Task<T?> UpdateAsync(T entity)
		{
			_dbSet.Update(entity);
			entity.UpdatedAt = DateTime.UtcNow;
			await _viaEntregaContext.SaveChangesAsync();
			return entity;
		}

		public virtual async Task<bool> DeleteAsync(Guid id)
		{
			T? entity = _dbSet.Find(id);

			if (entity is null) return false;

			entity.Active = false;
			entity.UpdatedAt = DateTime.UtcNow;
			await _viaEntregaContext.SaveChangesAsync();

			return true;
		}
	}
}
