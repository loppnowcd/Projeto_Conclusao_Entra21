using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using via_entrega.entities.Orders;

namespace via_entrega.repositoriess.Configuration
{
	public class CollectionOrderConfiguration : IEntityTypeConfiguration<CollectionOrder>
	{
		public void Configure(EntityTypeBuilder<CollectionOrder> builder)
		{
			// Chave primária
			builder.HasKey(a => a.Id);

			// Campos obrigatórios
			builder.Property(a => a.Endereco)
				.IsRequired()
				.HasMaxLength(250);

			builder.Property(a => a.Destinatario)
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(a => a.Telefone)
				.HasMaxLength(20);

			builder.Property(a => a.Documento)
				.HasMaxLength(50);

			builder.Property(a => a.Observacao)
				.HasMaxLength(500);

			builder.Property(a => a.ValorTotal)
				.HasColumnType("decimal(18,2)");

			// Campos de auditoria (da EntityBase)
			builder.Property(a => a.CreatedAt).IsRequired();
			builder.Property(a => a.UpdatedAt).IsRequired();

			// Relacionamentos
			builder.HasOne(a => a.PessoaFisica)
				.WithMany()
				.HasForeignKey(a => a.PessoaFisicaId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(a => a.PessoaJuridica)
				.WithMany()
				.HasForeignKey(a => a.PessoaJuridicaId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.HasMany(a => a.Products)
				.WithOne(p => p.CollectionOrder)
				.HasForeignKey(p => p.CollectionOrderId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
