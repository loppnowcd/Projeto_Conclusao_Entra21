namespace via_entrega.Interfaces.Common
{
	public interface ICRUD<T>
	{
		public Task<List<T?>> GetAllAsync();
		public Task<T?> GetByIdAsync(Guid id);
		public Task<Guid?> CreateAsync(T entity);
		public Task<T?> UpdateAsync(T entity);
		public Task<bool> DeleteAsync(Guid id);	
	}
}
