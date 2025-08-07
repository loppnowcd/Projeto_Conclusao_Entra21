using Microsoft.EntityFrameworkCore;
using via_entrega.entities.Orders;
using via_entrega.entities.Registrations;
using via_entrega.repositoriess.Configuration;

namespace via_entrega.repositoriess
{
	public class ViaEntregaContext : DbContext
	{
		public ViaEntregaContext(DbContextOptions<ViaEntregaContext> configuration) : base(configuration)
		{
		}
		public DbSet<Pessoa> Pessoas { get; set; }
		public DbSet<PessoaFisica> PessoasFisicas { get; set; }
		public DbSet<PessoaJuridica> PessoasJuridicas { get; set; }
		public DbSet<Veiculo> Veiculos { get; set; }
		public DbSet<CollectionOrder> CollectionOrders { get; set; }
		public DbSet<DeliveryOrder> DeliveryOrders { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<DadosContato> DadosContatos { get; set; }
		public DbSet<DadosEndereco> DadosEnderecos { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfiguration(new DadosContatoConfiguration());
			modelBuilder.ApplyConfiguration(new PessoaConfiguration());
			modelBuilder.ApplyConfiguration(new PessoaFisicaConfiguration());
			modelBuilder.ApplyConfiguration(new PessoaJuridicaConfiguration());
			modelBuilder.ApplyConfiguration(new VeiculoConfiguration());
			modelBuilder.ApplyConfiguration(new CollectionOrderConfiguration());
			modelBuilder.ApplyConfiguration(new DeliveryOrderConfiguration());
			modelBuilder.ApplyConfiguration(new ProductConfiguration());
			modelBuilder.ApplyConfiguration(new DadosEnderecoConfiguration());
			
		}
	}
}