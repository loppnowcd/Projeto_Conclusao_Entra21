using via_entrega.entities.Registrations;
using via_entrega.interfaces.Repositories;
using via_entrega.interfaces.Services;

namespace via_entrega.services
{
    public class DadosEnderecoService : IDadosEnderecoService<DadosEndereco>
    {
        private readonly IDadosEnderecoRepository _dadosEnderecoRepository;

        public DadosEnderecoService(IDadosEnderecoRepository dadosEnderecoRepository)
        {
            _dadosEnderecoRepository = dadosEnderecoRepository;
        }

        public async Task<IEnumerable<DadosEndereco>> BuscarPorCidadeAsync(string cidade)
        {
            return await _dadosEnderecoRepository.BuscarPorCidadeAsync(cidade);
        }

        public async Task<IEnumerable<DadosEndereco>> BuscarPorCepAsync(string cep)
        {
            return await _dadosEnderecoRepository.BuscarPorCepAsync(cep);
        }

        public async Task<Guid?> CreateAsync(DadosEndereco entity)
        {
            return await _dadosEnderecoRepository.CreateAsync(entity);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _dadosEnderecoRepository.DeleteAsync(id);
        }

        public async Task<List<DadosEndereco?>> GetAllAsync()
        {
            return await _dadosEnderecoRepository.GetAllAsync();
        }

        public async Task<DadosEndereco?> GetByIdAsync(Guid id)
        {
            return await _dadosEnderecoRepository.GetByIdAsync(id);
        }

        public async Task<DadosEndereco?> UpdateAsync(DadosEndereco entity)
        {
            return await _dadosEnderecoRepository.UpdateAsync(entity);
        }
    }
}
