using via_entrega.entities.Enums;
using via_entrega.entities.Orders;

namespace via_entrega.services
{
	public class CollectionOrderService : ICollectionOrderService<CollectionOrder>
	{
		private readonly ICollectionOrderRepository _collectionOrderRepository;

		public CollectionOrderService(ICollectionOrderRepository collectionOrderService)
		{
			_collectionOrderRepository = collectionOrderService;
		}

		public async Task<Guid?> CreateAsync(CollectionOrder entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));
			if (entity.CreatedAt == DateTime.MinValue)
				throw new ArgumentException("Data de coleta inválida.");
			if (entity.Endereco == null || string.IsNullOrWhiteSpace(entity.Endereco)) 
				throw new ArgumentException("Endereço de origem é obrigatório.");
			if (entity.Destinatario == null || string.IsNullOrWhiteSpace(entity.Destinatario))
				throw new ArgumentException("Endereço de destino é obrigatório.");

			// status inicial
			//entity.Status = Status.AguardandoColeta;
			//entity.DataCriacao = DateTime.UtcNow;

			return await _collectionOrderRepository.CreateAsync(entity);
			
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
	}
}
