namespace ViewModels.Orders
{
	public class OrderListItemVM
	{
		public Guid Id { get; set; }
		public DateTime DataColeta { get; set; }
		public DateTime? DataEntrega { get; set; }
		public string Status { get; set; } = "";
		public string? Endereco { get; set; }
		public string? Destinatario { get; set; }
		public string? Telefone { get; set; }
		public decimal? ValorTotal { get; set; }
	}
}
