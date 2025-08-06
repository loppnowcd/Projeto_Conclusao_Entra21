using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using via_entrega.entities.Orders;

namespace via_entrega.repositoriess.Configuration
{
	public class ProductConfiguration : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.HasKey(a => a.Id);

			// Required Fields
			builder.Property(a => a.Descricao)
				.IsRequired().HasMaxLength(250);
		
			// Audit Fields
			builder.Property(ur => ur.CreatedAt).IsRequired();
			builder.Property(ur => ur.UpdatedAt).IsRequired();


			// Relationships
			builder.HasOne(a => a.CollectionOrder)
				.WithMany(c => c.Products)
				.HasForeignKey(a => a.CollectionOrderId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
