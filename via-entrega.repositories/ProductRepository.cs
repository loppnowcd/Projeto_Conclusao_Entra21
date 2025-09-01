using via_entrega.entities.Orders;
using via_entrega.interfaces.Repositories;

namespace via_entrega.repositoriess
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ViaEntregaContext viaEntregaContext) : base(viaEntregaContext)
        {
            
        }
    }
}
