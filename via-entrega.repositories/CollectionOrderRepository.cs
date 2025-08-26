using via_entrega.entities.Orders;
// Atenção: no seu projeto, a interface ICollectionOrderRepository está sob o namespace `via_entrega.services`
using via_entrega.services;

namespace via_entrega.repositoriess
{
	public class CollectionOrderRepository : BaseRepository<CollectionOrder>, ICollectionOrderRepository
	{
		private readonly ViaEntregaContext _context;

		public CollectionOrderRepository(ViaEntregaContext context) : base(context)
		{
			_context = context;
		}
	}
}
