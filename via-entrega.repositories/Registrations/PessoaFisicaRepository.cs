
using via_entrega.entities.Registrations;
using via_entrega.interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace via_entrega.repositoriess.Registrations
{
	public class PessoaFisicaRepository : BaseRepository<PessoaFisica>, IPessoaFisicaRepository
	{
		private readonly ViaEntregaContext _context;

		public PessoaFisicaRepository(ViaEntregaContext viaEntregaContext) : base(viaEntregaContext)
		{
			_context = viaEntregaContext;
		}

		public async Task<PessoaFisica> BuscarPorCpfAsync(string cpf)
		{
			return await _context.Set<PessoaFisica>().FirstOrDefaultAsync(p => p.Cpf == cpf);
		}

		public async Task<IEnumerable<PessoaFisica>> BuscarPorNomeAsync(string nome)
		{
			return await _context.Set<PessoaFisica>()
				.Where(p => p.Nome.Contains(nome))
				.ToListAsync();
		}
	}
}
