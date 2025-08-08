using via_entrega.entities.Registrations;
using via_entrega.interfaces.Repositories;
using via_entrega.interfaces.Services;

namespace via_entrega.services
{
    public class PessoaJuridicaService : IPessoaJuridicaService<PessoaJuridica>
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

        public async Task<Guid?> CreateAsync(PessoaJuridica entity)
        {
            return await _pessoaJuridicaRepository.CreateAsync(entity);
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
    }
}
