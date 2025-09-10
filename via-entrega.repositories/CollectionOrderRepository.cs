using Microsoft.EntityFrameworkCore;
using via_entrega.entities.Orders;
using via_entrega.interfaces.Repositories;

namespace via_entrega.repositoriess
{
    public class CollectionOrderRepository : BaseRepository<CollectionOrder>, ICollectionOrderRepository
    {
        public CollectionOrderRepository(ViaEntregaContext viaEntregaContext) : base(viaEntregaContext)
        {
        }

        public async Task<List<CollectionOrder>> GetAllIncludedAsync()
        {
            return await _dbSet.Include(co => co.Products).ToListAsync();
        }

        public  CollectionOrder? GetByIdIncluded(Guid id)
        {
            return  _dbSet.Include(co => co.Products).FirstOrDefault(x=> x.Id == id);
        }
    }
}
