using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using via_entrega.entities.Orders;

namespace via_entrega.repositoriess.Configuration
{
	public class DeliveryOrderConfiguration : IEntityTypeConfiguration<DeliveryOrder>
	{
		public void Configure(EntityTypeBuilder<DeliveryOrder> builder)
		{
			// Chave primária
			builder.HasKey(a => a.Id);

			// Campos obrigatórios
			builder.Property(a => a.DataColeta)
				   .IsRequired();

			builder.Property(a => a.Observacao)
				   .HasMaxLength(500);

			builder.Property(a => a.Imagem)
				   .HasColumnType("varbinary(max)"); // SQL Server

			builder.Property(a => a.Status)
				   .HasConversion<int>() // Mapeia enum para int
				   .IsRequired();

			// Campos de auditoria (herdados)
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

			builder.HasOne(a => a.CollectionOrder)
				   .WithMany()
				   .HasForeignKey(a => a.CollectionOrderId)
				   .OnDelete(DeleteBehavior.Cascade);
		}
	}
}
