using via_entrega.entities.Registrations;
using via_entrega.interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace via_entrega.repositoriess.Registrations
{
    public class PessoaJuridicaRepository : BaseRepository<PessoaJuridica>, IPessoaJuridicaRepository
    {
        private readonly ViaEntregaContext _context;

        public PessoaJuridicaRepository(ViaEntregaContext viaEntregaContext) : base(viaEntregaContext)
        {
            _context = viaEntregaContext;
        }

        public async Task<PessoaJuridica> BuscarPorCnpjAsync(string cnpj)
        {
            return await _context.Set<PessoaJuridica>().FirstOrDefaultAsync(p => p.Cnpj == cnpj);
        }

        public async Task<IEnumerable<PessoaJuridica>> BuscarPorRazaoSocialAsync(string razaoSocial)
        {
            return await _context.Set<PessoaJuridica>()
                .Where(p => p.Nome.Contains(razaoSocial))
                .ToListAsync();
        }
    }
}
