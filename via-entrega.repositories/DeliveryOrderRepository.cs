using Microsoft.EntityFrameworkCore;
using via_entrega.entities.Orders;
using via_entrega.interfaces.Repositories;

namespace via_entrega.repositoriess
{
	public class DeliveryOrderRepository : BaseRepository<DeliveryOrder>, IDeliveryOrderRepository
	{
		private readonly ViaEntregaContext _context;


		public DeliveryOrderRepository(ViaEntregaContext context) : base(context)
		{
			_context = context;
		}

		public async Task<IEnumerable<DeliveryOrder?>> BuscarOrdersPelaPessoa(Guid idPessoa)
		{
			return await _dbSet
				.Include(e => e.PessoaFisica)
				.Include(e => e.PessoaJuridica)
				.Where(e => e.PessoaJuridicaId == idPessoa || e.PessoaFisicaId == idPessoa)
				.ToListAsync();
		}
	}
}
