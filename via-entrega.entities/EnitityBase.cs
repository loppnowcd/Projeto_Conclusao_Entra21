using via_entrega.Interfaces.Common;

namespace via_entrega.entities
{
	public abstract class EnitityBase : IEntityBase
	{
		public Guid Id { get; set; }
		public bool Active { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
	}
}
