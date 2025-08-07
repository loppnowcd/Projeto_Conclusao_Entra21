using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using via_entrega.entities.Registrations;

namespace via_entrega.repositoriess.Configuration
{
	public class PessoaFisicaConfiguration : IEntityTypeConfiguration<PessoaFisica>
	{
		public void Configure(EntityTypeBuilder<PessoaFisica> builder)
		{
			

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

			
			// Enums mapeados como int
			builder.Property(p => p.Sexo)
				   .HasConversion<int>()
				   .IsRequired();

			builder.Property(p => p.EstadoCivil)
				   .HasConversion<int>()
				   .IsRequired();


		}
	}
}
