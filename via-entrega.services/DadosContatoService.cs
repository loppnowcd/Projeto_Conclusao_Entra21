using via_entrega.entities.Registrations;
using via_entrega.interfaces.Repositories;
using via_entrega.interfaces.Services;

namespace via_entrega.services
{
    public class DadosContatoService : IDadosContatoService<DadosContato>
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
            return await _dadosContatoRepository.CreateAsync(entity);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _dadosContatoRepository.DeleteAsync(id);
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
            return await _dadosContatoRepository.UpdateAsync(entity);
        }
    }
}
