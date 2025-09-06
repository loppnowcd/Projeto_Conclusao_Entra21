using via_entrega.entities.Registrations;
using via_entrega.interfaces.Repositories;
using via_entrega.interfaces.Services;

namespace via_entrega.services
{
	public class DadosContatoService : IDadosContatoService
	{
		private readonly IDadosContatoRepository _dadosContatoRepository;

		public DadosContatoService(IDadosContatoRepository dadosContatoRepository)
		{
			_dadosContatoRepository = dadosContatoRepository;
		}

		public async Task<IEnumerable<DadosContato>> BuscarPorEmailAsync(string email)
		{
			return await _dadosContatoRepository.BuscarPorEmailAsync(email);
		}

		public async Task<IEnumerable<DadosContato>> BuscarPorTelefoneAsync(string telefone)
		{
			return await _dadosContatoRepository.BuscarPorTelefoneAsync(telefone);
		}

		public async Task<Guid?> CreateAsync(DadosContato entity)
		{
			entity.Active = true;
			Guid? id = await _dadosContatoRepository.CreateAsync(entity);
			await SaveChangesAsync();
			return id;
		}

		public async Task<bool> DeleteAsync(Guid id)
		{
			await _dadosContatoRepository.DeleteAsync(id);
			await SaveChangesAsync();
			return true;
		}

		public async Task<List<DadosContato?>> GetAllAsync()
		{
			return await _dadosContatoRepository.GetAllAsync();
		}

		public async Task<DadosContato?> GetByIdAsync(Guid id)
		{
			return await _dadosContatoRepository.GetByIdAsync(id);
		}

		public async Task<DadosContato?> UpdateAsync(DadosContato entity)
		{
			DadosContato? dadosContato = await _dadosContatoRepository.UpdateAsync(entity);
			await SaveChangesAsync();
			return dadosContato;
		}

		public async Task SaveChangesAsync()
		{
			await _dadosContatoRepository.SaveChangesAsync();
		}
	}
}
