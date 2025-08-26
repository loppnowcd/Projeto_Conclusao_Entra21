using via_entrega.entities.Enums;

namespace ViewModels.PessoaFisica
{
	public class OrderCreateVM
	{
		public DateTime? DataEntrega { get; set; }
		public DateTime DataColeta { get; set; }
		public Status Status { get; set; }
		public string NomeCliente { get; set; }
	}
}

