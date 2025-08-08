using via_entrega.entities.Registrations;
using via_entrega.interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace via_entrega.repositoriess.Registrations
{
    public class DadosContatoRepository : BaseRepository<DadosContato>, IDadosContatoRepository
    {
        private readonly ViaEntregaContext _context;

        public DadosContatoRepository(ViaEntregaContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DadosContato>> BuscarPorEmailAsync(string email)
        {
            return await _context.Set<DadosContato>()
                .Where(d => d.Email.Contains(email))
                .ToListAsync();
        }

        public async Task<IEnumerable<DadosContato>> BuscarPorTelefoneAsync(string telefone)
        {
            return await _context.Set<DadosContato>()
                .Where(d => d.Telefone.Contains(telefone))
                .ToListAsync();
        }
    }
}
