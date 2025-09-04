namespace via_entrega.interfaces.Common
{
	public interface IEmpresaPj 
	{
		Task<List<IEmpresaPj?>> CreateAsync();
		Task<IEmpresaPj?> GetByIdAsync(int id);
		Task<IEmpresaPj?> UpdateAsync(IEmpresaPj entity);
		Task<bool> DeleteAsync(int id);
	}
	
}
		