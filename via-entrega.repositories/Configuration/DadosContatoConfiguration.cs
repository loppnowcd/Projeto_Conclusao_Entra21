using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using via_entrega.entities.Orders;
using via_entrega.entities.Registrations;

namespace via_entrega.repositoriess.Configuration
{
	public class DadosContatoConfiguration : IEntityTypeConfiguration<DadosContato>
	{
		public void Configure(EntityTypeBuilder<DadosContato> builder)
		{
			// Chave primária
			builder.HasKey(a => a.Id);

			builder.Property(a => a.Telefone)
				.HasMaxLength(20);

			// Campos de auditoria (da EntityBase)
			builder.Property(a => a.CreatedAt).IsRequired();
			builder.Property(a => a.UpdatedAt).IsRequired();


			builder.HasOne(a => a.Pessoa)
				.WithMany(p => p.Contatos)
				.HasForeignKey(a => a.PessoaId)
				.OnDelete(DeleteBehavior.NoAction);
		}
	}
}
