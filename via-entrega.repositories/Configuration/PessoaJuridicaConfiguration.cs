using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using via_entrega.entities.Registrations;

namespace via_entrega.repositoriess.Configuration
{
	public class PessoaJuridicaConfiguration : IEntityTypeConfiguration<PessoaJuridica>
	{
		public void Configure(EntityTypeBuilder<PessoaJuridica> builder)
		{
			// Chave primária (herdada de EntityBase)
			builder.HasKey(p => p.Id);

			// Campos obrigatórios
			builder.Property(p => p.Cnpj)
				   .IsRequired()
				   .HasMaxLength(18); // Formato: 00.000.000/0000-00

			builder.Property(p => p.NomeFatasia)
				   .HasMaxLength(150);

			// Campos de auditoria (herdados)
			builder.Property(p => p.CreatedAt).IsRequired();
			builder.Property(p => p.UpdatedAt).IsRequired();

			// Relacionamentos herdados
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
