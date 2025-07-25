using Microsoft.EntityFrameworkCore;
using via_entrega.entities;

namespace via_entrega.repositoriess
{
	public class ViaEntregaContext : DbContext
	{
		public ViaEntregaContext(DbContextOptions<ViaEntregaContext> configuration) : base(configuration)
		{
		}

		public DbSet<Driver> Drivers { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}
	}
}