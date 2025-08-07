using via_entrega.Interfaces.Common;

namespace via_entrega.interfaces.Common
{
	public interface IPessoaF : ICRUD<IPessoaF>
	{	
		public Task<List<IPessoaF?>> CreateAsync();	
		public Task<IPessoaF?> GetByIdAsync(int id);
		public Task<IPessoaF?> UpdateAsync(IPessoaF entity);		
		public Task<bool> DeleteAsync(int id);
	}
}
