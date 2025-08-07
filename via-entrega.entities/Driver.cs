namespace via_entrega.entities
{
	public class Driver : EntityBase
	{
		public string Name { get; set; } = string.Empty;
		public string LicenseNumber { get; set; } = string.Empty;
		public string VehicleType { get; set; } = string.Empty;
	}
}