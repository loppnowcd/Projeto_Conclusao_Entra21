using via_entrega.entities.Orders;
using via_entrega.interfaces.Repositories;
using via_entrega.interfaces.Services;

namespace via_entrega.services
{
    public class CollectionOrderService : ICollectionOrderService
    {
        private readonly ICollectionOrderRepository _collectionOrderRepository;
        private readonly IProductService _productService;

        public CollectionOrderService(ICollectionOrderRepository collectionOrderService,
            IProductService productService)
        {
            _collectionOrderRepository = collectionOrderService;
            _productService = productService;
        }


        public async Task<List<CollectionOrder>> GetAllIncludedAsync()
        {
            return await _collectionOrderRepository.GetAllIncludedAsync();
        }

        public CollectionOrder? GetByIdIncluded(Guid id)
        {
            return _collectionOrderRepository.GetByIdIncluded(id);
        }

        public async Task<Guid?> CreateAsync(CollectionOrder collectionOrder)
        {
            if (collectionOrder == null) throw new ArgumentNullException(nameof(collectionOrder));
            if (string.IsNullOrEmpty(collectionOrder.Endereco))
                throw new ArgumentException("Endereço de origem é obrigatório.");
            if (string.IsNullOrEmpty(collectionOrder.Destinatario))
                throw new ArgumentException("Endereço de destino é obrigatório.");

            collectionOrder.CreatedAt = DateTime.UtcNow;
            Guid? collectionOrderId = await _collectionOrderRepository.CreateAsync(collectionOrder);
            if (collectionOrderId is null)
                throw new ArgumentException("Ordem de coleta não registrada.");

            foreach (Product product in collectionOrder.Products)
            {
                product.CollectionOrderId = collectionOrderId.Value;
                await _productService.CreateAsync(product);
            }

            await SaveChangesAsync();

            return await _collectionOrderRepository.CreateAsync(collectionOrder);

        }


        public async Task<bool> DeleteAsync(Guid id)
        {

            return await _collectionOrderRepository.DeleteAsync(id);
        }

        public async Task<List<CollectionOrder?>> GetAllAsync()
        {
            return await _collectionOrderRepository.GetAllAsync();
        }

        public async Task<CollectionOrder?> GetByIdAsync(Guid id)
        {
            return await _collectionOrderRepository.GetByIdAsync(id);
        }



        public async Task<CollectionOrder?> UpdateAsync(CollectionOrder entity)
        {
            return await _collectionOrderRepository.UpdateAsync(entity);

        }

        public async Task SaveChangesAsync()
        {
            await _collectionOrderRepository.SaveChangesAsync();
        }
    }
}
