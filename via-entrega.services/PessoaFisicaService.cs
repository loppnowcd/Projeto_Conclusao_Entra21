using via_entrega.entities.Registrations;
using via_entrega.interfaces.Repositories;
using via_entrega.interfaces.Services;

namespace via_entrega.services
{
	public class PessoaFisicaService : IPessoaFisicaService<PessoaFisica>
	{
		private readonly IPessoaFisicaRepository _pessoaFisicaRepository;
		public PessoaFisicaService(IPessoaFisicaRepository pessoaFisicaRepository)
		{
			_pessoaFisicaRepository = pessoaFisicaRepository;
		}

		public async Task<PessoaFisica> BuscarPorCpfAsync(string cpf)
		{
			return await _pessoaFisicaRepository.BuscarPorCpfAsync(cpf);
		}

		public async Task<IEnumerable<PessoaFisica>> BuscarPorNomeAsync(string nome)
		{
			return await _pessoaFisicaRepository.BuscarPorNomeAsync(nome);
		}

		public async Task<Guid?> CreateAsync(PessoaFisica entity)
		{
			return await _pessoaFisicaRepository.CreateAsync(entity);
		}

		public async Task<bool> DeleteAsync(Guid id)
		{
			return await _pessoaFisicaRepository.DeleteAsync(id);
		}

		public async Task<List<PessoaFisica?>> GetAllAsync()
		{
			return await _pessoaFisicaRepository.GetAllAsync();
		}

		public async Task<PessoaFisica?> GetByIdAsync(Guid id)
		{
			return await _pessoaFisicaRepository.GetByIdAsync(id);
		}

		public async Task<PessoaFisica?> UpdateAsync(PessoaFisica entity)
		{
			return await _pessoaFisicaRepository.UpdateAsync(entity);
		}
	}
}
