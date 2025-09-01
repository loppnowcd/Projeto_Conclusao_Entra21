using via_entrega.entities.Orders;
using via_entrega.Interfaces.Common;

namespace via_entrega.interfaces.Repositories
{
    public interface ICollectionOrderRepository : ICRUD<CollectionOrder>
    {
        public Task<List<CollectionOrder>> GetAllIncludedAsync();
        public CollectionOrder? GetByIdIncluded(Guid id);
    }
}