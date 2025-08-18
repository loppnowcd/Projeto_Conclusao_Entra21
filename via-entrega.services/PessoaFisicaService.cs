using via_entrega.entities.Registrations;
using via_entrega.interfaces.Common;
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

		public async Task<Guid?> CreateAsync(PessoaFisica pessoa)
		{
			if (string.IsNullOrEmpty(pessoa.Nome))
				throw new ArgumentException("O nome é obrigatório.");

			if (pessoa.Cpf.Length != 11)
				throw new ArgumentException("CPF inválido.");

			// Regra: CPF único
			var existente = await _pessoaFisicaRepository.BuscarPorCpfAsync(pessoa.Cpf);
			if (existente != null)
				throw new InvalidOperationException("CPF já cadastrado.");

			return await _pessoaFisicaRepository.CreateAsync(pessoa);
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
