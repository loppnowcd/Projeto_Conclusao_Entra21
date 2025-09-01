using via_entrega.entities.Orders;
using via_entrega.interfaces.Repositories;
using via_entrega.interfaces.Services;

namespace via_entrega.services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Guid?> CreateAsync(Product product)
        {
            return await _productRepository.CreateAsync(product);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _productRepository.DeleteAsync(id);
        }

        public async Task<List<Product?>> GetAllAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

        public async Task<Product?> UpdateAsync(Product entity)
        {
            return await _productRepository.UpdateAsync(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _productRepository.SaveChangesAsync();
        }
    }
}
