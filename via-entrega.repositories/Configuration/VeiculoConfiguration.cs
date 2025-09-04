using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using via_entrega.entities.Registrations;

namespace via_entrega.repositoriess.Configuration
{
	public class VeiculoConfiguration : IEntityTypeConfiguration<Veiculo>
	{
		public void Configure(EntityTypeBuilder<Veiculo> builder)
		{
			// Chave primária (herdada de EntityBase)
			builder.HasKey(v => v.Id);

			// Propriedades obrigatórias e limites de tamanho
			builder.Property(v => v.MarcaFipe)
				   .IsRequired()
				   .HasMaxLength(100);

			builder.Property(v => v.Modelo)
				   .IsRequired()
				   .HasMaxLength(100);

			builder.Property(v => v.AnoFabricacao)
				   .IsRequired();

			builder.Property(v => v.Cor)
				   .HasMaxLength(50);

			builder.Property(v => v.Placa)
				   .IsRequired()
				   .HasMaxLength(10);

			// Campos de auditoria
			builder.Property(v => v.CreatedAt).IsRequired();
			builder.Property(v => v.UpdatedAt).IsRequired();

			// Relacionamento: Veículo -> Pessoas Físicas
			builder.HasMany(v => v.Pessoas)
				   .WithOne(p => p.Veiculo)
				   .HasForeignKey(p => p.VeiculoId)
				   .OnDelete(DeleteBehavior.SetNull);
		}
	}
}
