using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using via_entrega.entities.Orders;
using via_entrega.Interfaces.Common;

namespace via_entrega.interfaces.Repositories
{
	public interface IDeliveryOrderRepository : ICRUD<DeliveryOrder>
	{
		public Task<IEnumerable<DeliveryOrder?>> BuscarOrdersPelaPessoa(Guid idPessoa);

	}
}
