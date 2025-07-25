using via_entrega.entities;

namespace via_entrega.repositoriess.Registrations
{
	public class DriverRepository : BaseRepository<Driver>
	{
		public DriverRepository(ViaEntregaContext viaEntregaContext) : base(viaEntregaContext)
		{
		}

		public override async Task<Guid?> CreateAsync(Driver entity)
		{
			//crio validações específicas para o driver
			return await base.CreateAsync(entity);
		}
	}
}
