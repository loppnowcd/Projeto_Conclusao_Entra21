using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using via_entrega.entities.Registrations;

namespace via_entrega.repositoriess.Configuration
{
	public class DadosEnderecoConfiguration : IEntityTypeConfiguration<DadosEndereco>
	{
		public void Configure(EntityTypeBuilder<DadosEndereco> builder)
		{
			// Chave primária
			builder.HasKey(a => a.Id);

			// Campos de auditoria (da EntityBase)
			builder.Property(a => a.CreatedAt).IsRequired();
			builder.Property(a => a.UpdatedAt).IsRequired();

		}
	}
}