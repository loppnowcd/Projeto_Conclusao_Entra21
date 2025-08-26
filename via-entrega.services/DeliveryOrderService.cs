using via_entrega.entities.Enums;
using via_entrega.entities.Orders;
using via_entrega.interfaces.Repositories;
using via_entrega.interfaces.Services;
using via_entrega.repositoriess.Registrations;

namespace via_entrega.services
{
	public class DeliveryOrderService : IDeliveryOrderService
	{
		private readonly IDeliveryOrderRepository _deliveryOrderRepository;
		public DeliveryOrderService(IDeliveryOrderRepository deliveryOrderRepository)
		{
			_deliveryOrderRepository = deliveryOrderRepository;
		}

		public async Task<IEnumerable<DeliveryOrder?>> BuscarOrdersPelaPessoa(Guid idPessoa)
		{
			return await _deliveryOrderRepository.BuscarOrdersPelaPessoa(idPessoa);
		}


		public async Task<Guid?> CreateAsync(DeliveryOrder order)
		{
			return await _deliveryOrderRepository.CreateAsync(order);

		}

		public async Task<bool> DeleteAsync(Guid id)
		{
			return await _deliveryOrderRepository.DeleteAsync(id);
		}

		public async Task<List<DeliveryOrder?>> GetAllAsync()
		{
			return await _deliveryOrderRepository.GetAllAsync();

		}

		public async Task<DeliveryOrder?> GetByIdAsync(Guid id)
		{
			return await _deliveryOrderRepository.GetByIdAsync(id);

		}

		public async Task<DeliveryOrder?> UpdateAsync(DeliveryOrder entity)
		{
			return await _deliveryOrderRepository.UpdateAsync(entity);

		}

        public async Task SaveChangesAsync()
        {
            await _deliveryOrderRepository.SaveChangesAsync();
        }

		 Task<IEnumerable<DeliveryOrder>> IDeliveryOrderService.GetAllByUserAsync(string? userId)
		{
			throw new NotImplementedException();
		}
	}
}