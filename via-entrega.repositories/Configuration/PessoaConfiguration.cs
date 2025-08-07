using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using via_entrega.entities.Registrations;

namespace via_entrega.repositoriess.Configuration
{
	public class PessoaConfiguration : IEntityTypeConfiguration<Pessoa>
	{
		public void Configure(EntityTypeBuilder<Pessoa> builder)
		{
			builder.HasKey(p => p.Id);

			// Campos comuns
			builder.Property(p => p.Nome).IsRequired().HasMaxLength(150);
			builder.Property(p => p.DataNascimento).IsRequired();
			builder.Property(p => p.DataRegistro).IsRequired();
			builder.Property(p => p.CreatedAt).IsRequired();
			builder.Property(p => p.UpdatedAt).IsRequired();

			// Relacionamentos
			builder.HasMany(p => p.Enderecos)
				.WithOne(p=> p.Pessoa)
				.HasForeignKey(p=> p.PessoaId)
				.OnDelete(DeleteBehavior.NoAction);


			// Herança por TPH (Table-Per-Hierarchy)
			builder.HasDiscriminator<string>("TipoPessoa")
				.HasValue<PessoaFisica>("Fisica")
				.HasValue<PessoaJuridica>("Juridica");

			// Relacionamento com Veiculo
			builder.HasOne(p => p.Veiculo)
				   .WithMany()
				   .HasForeignKey(p => p.VeiculoId)
				   .OnDelete(DeleteBehavior.Restrict);
		}
	}
}
