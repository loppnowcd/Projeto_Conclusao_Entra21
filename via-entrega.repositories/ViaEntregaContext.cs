using Microsoft.EntityFrameworkCore;
using via_entrega.entities.Orders;
using via_entrega.repositoriess.Configuration;

namespace via_entrega.repositoriess
{
	public class ViaEntregaContext : DbContext
	{
		public ViaEntregaContext(DbContextOptions<ViaEntregaContext> configuration) : base(configuration)
		{
		}

		public DbSet<Product> Products { get; set; }
		public DbSet<CollectionOrder> CollectionOrders { get; set; }
		public DbSet<DeliveryOrder> DeliveryOrders { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfiguration(new PessoaFisicaConfiguration()); 
			modelBuilder.ApplyConfiguration(new CollectionOrderConfiguration());
			modelBuilder.ApplyConfiguration(new DeliveryOrderConfiguration());

		}
	}
}