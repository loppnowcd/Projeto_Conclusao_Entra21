using System.ComponentModel.DataAnnotations;

namespace ViewModels.Orders
{
	public class OrderCreateVM
	{
		// Coleta/Entrega simplificados conforme suas entidades atuais
		[Required]
		[Display(Name = "Endereço de Coleta/Entrega")]
		public string Endereco { get; set; } = "";

		[Display(Name = "Destinatário")]
		public string? Destinatario { get; set; }

		[Display(Name = "Telefone")]
		public string? Telefone { get; set; }

		[Display(Name = "Documento")]
		public string? Documento { get; set; }

		[Display(Name = "Observação")]
		public string? Observacao { get; set; }

		[Display(Name = "Valor Total")]
		public decimal? ValorTotal { get; set; }

		[Required]
		[Display(Name = "Data da Coleta")]
		public DateTime DataColeta { get; set; } = DateTime.Today.AddDays(1);

		[Display(Name = "Data da Entrega (opcional)")]
		public DateTime? DataEntrega { get; set; }
	}
}
