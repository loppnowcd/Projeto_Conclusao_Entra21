namespace via_entrega.Interfaces.Common
{
	public interface IEntityBase
	{
		public Guid Id { get; set; }
		public bool Active { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
	}
}
