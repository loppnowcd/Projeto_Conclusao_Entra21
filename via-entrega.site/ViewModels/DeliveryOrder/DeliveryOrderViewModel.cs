using via_entrega.entities.Enums;

namespace ViewModels.PessoaFisica
{
	public class DeliveryOrderViewModel
	{
		public Guid Id { get; set; }
		public Guid ClienteId { get; set; }
		public string? EnderecoColeta { get; set; }
		public string? EnderecoEntrega { get; set; }
		public Status Status { get; set; }
		public DateTime DataColeta { get; set; }
		public DateTime? DataEntrega { get; set; }
	}
}

