using via_entrega.entities.Orders;
using via_entrega.Interfaces.Common;

namespace via_entrega.services
{
	public interface ICollectionOrderService: ICRUD<CollectionOrder>
	{
        public Task<List<CollectionOrder>> GetAllIncludedAsync();
        public CollectionOrder? GetByIdIncluded(Guid id);
    }
}