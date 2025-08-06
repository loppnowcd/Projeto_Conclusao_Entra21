using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using via_entrega.entities.Registrations;

namespace via_entrega.repositoriess.Configuration
{
	public class PessoaFisicaConfiguration : IEntityTypeConfiguration<PessoaFisica>
	{
		public void Configure(EntityTypeBuilder<PessoaFisica> builder)
		{
			// Chave primária (herdada de EntityBase)
			builder.HasKey(p => p.Id);

			// Campos obrigatórios e tamanhos
			builder.Property(p => p.Cpf)
				   .IsRequired()
				   .HasMaxLength(14);

			builder.Property(p => p.NomeMae)
				   .HasMaxLength(100);

			builder.Property(p => p.Cor)
				   .HasMaxLength(50);

			builder.Property(p => p.LicenseNumber)
				   .HasMaxLength(20);

			// Campos de auditoria (EntityBase)
			builder.Property(p => p.CreatedAt).IsRequired();
			builder.Property(p => p.UpdatedAt).IsRequired();

			// Enums mapeados como int
			builder.Property(p => p.Sexo)
				   .HasConversion<int>()
				   .IsRequired();

			builder.Property(p => p.EstadoCivil)
				   .HasConversion<int>()
				   .IsRequired();

			// Relacionamento com Veiculo
			builder.HasOne(p => p.Veiculo)
				   .WithMany()
				   .HasForeignKey(p => p.VeiculoId)
				   .OnDelete(DeleteBehavior.Restrict);

			// Relacionamentos herdados (endereços e contatos)
			builder.HasMany(p => p.Enderecos)
				   .WithOne()
				   .HasForeignKey("PessoaId")
				   .OnDelete(DeleteBehavior.Cascade);

			builder.HasMany(p => p.Contatos)
				   .WithOne()
				   .HasForeignKey("PessoaId")
				   .OnDelete(DeleteBehavior.Cascade);
		}
	}
}
