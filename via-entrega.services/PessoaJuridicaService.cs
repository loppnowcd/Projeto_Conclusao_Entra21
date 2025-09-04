using via_entrega.entities.Registrations;
using via_entrega.interfaces.Repositories;
using via_entrega.interfaces.Services;
using via_entrega.repositoriess.Registrations;

namespace via_entrega.services
{
    public class PessoaJuridicaService : IPessoaJuridicaService
    {
        private readonly IPessoaJuridicaRepository _pessoaJuridicaRepository;

        public PessoaJuridicaService(IPessoaJuridicaRepository pessoaJuridicaRepository)
        {
            _pessoaJuridicaRepository = pessoaJuridicaRepository;
        }

        public async Task<PessoaJuridica> BuscarPorCnpjAsync(string cnpj)
        {
            return await _pessoaJuridicaRepository.BuscarPorCnpjAsync(cnpj);
        }

        public async Task<IEnumerable<PessoaJuridica>> BuscarPorRazaoSocialAsync(string razaoSocial)
        {
            return await _pessoaJuridicaRepository.BuscarPorRazaoSocialAsync(razaoSocial);
        }

        public async Task<Guid?> CreateAsync(PessoaJuridica pessoaJuridica)
        {
			if (string.IsNullOrWhiteSpace(pessoaJuridica.NomeFatasia))
				throw new ArgumentException("A razão social é obrigatória.");

			if (string.IsNullOrWhiteSpace(pessoaJuridica.Cnpj) || pessoaJuridica.Cnpj.Length != 14)
				throw new ArgumentException("CNPJ inválido.");

			return await _pessoaJuridicaRepository.CreateAsync(pessoaJuridica);
		}

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _pessoaJuridicaRepository.DeleteAsync(id);
        }

        public async Task<List<PessoaJuridica?>> GetAllAsync()
        {
            return await _pessoaJuridicaRepository.GetAllAsync();
        }

        public async Task<PessoaJuridica?> GetByIdAsync(Guid id)
        {
            return await _pessoaJuridicaRepository.GetByIdAsync(id);
        }

        public async Task<PessoaJuridica?> UpdateAsync(PessoaJuridica entity)
        {
            return await _pessoaJuridicaRepository.UpdateAsync(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _pessoaJuridicaRepository.SaveChangesAsync();
        }
    }
}
