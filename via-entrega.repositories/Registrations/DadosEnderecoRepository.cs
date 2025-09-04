using via_entrega.entities.Registrations;
using via_entrega.interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace via_entrega.repositoriess.Registrations
{
    public class DadosEnderecoRepository : BaseRepository<DadosEndereco>, IDadosEnderecoRepository
    {
        private readonly ViaEntregaContext _context;

        public DadosEnderecoRepository(ViaEntregaContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DadosEndereco>> BuscarPorCidadeAsync(string cidade)
        {
            return await _context.Set<DadosEndereco>()
                .Where(e => e.Cidade.Contains(cidade))
                .ToListAsync();
        }

        public async Task<IEnumerable<DadosEndereco>> BuscarPorCepAsync(string cep)
        {
            return await _context.Set<DadosEndereco>()
                .Where(e => e.CEP == cep)
                .ToListAsync();
        }
    }
}
