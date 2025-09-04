using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using via_entrega.entities.Registrations;

namespace via_entrega.repositoriess.Configuration
{
	public class PessoaJuridicaConfiguration : IEntityTypeConfiguration<PessoaJuridica>
	{
		public void Configure(EntityTypeBuilder<PessoaJuridica> builder)
		{
			// Campos obrigatórios
			builder.Property(p => p.Cnpj)
				   .IsRequired()
				   .HasMaxLength(18); // Formato: 00.000.000/0000-00

			builder.Property(p => p.NomeFatasia)
				   .HasMaxLength(150);

		}
	}
}
