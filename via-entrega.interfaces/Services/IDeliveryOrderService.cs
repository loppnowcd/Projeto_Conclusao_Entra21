using via_entrega.entities.Orders;
using via_entrega.Interfaces.Common;

namespace via_entrega.interfaces.Services
{
	public interface IDeliveryOrderService : ICRUD<DeliveryOrder>
	{
		public  Task<IEnumerable<DeliveryOrder?>> BuscarOrdersPelaPessoa(Guid idPessoa);
		public  Task<IEnumerable<DeliveryOrder>> GetAllByUserAsync(string? userId);
	}
}
