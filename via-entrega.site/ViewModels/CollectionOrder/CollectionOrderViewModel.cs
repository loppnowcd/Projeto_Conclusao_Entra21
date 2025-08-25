using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ViewModels.CollectionOrder
{
	public class ProductViewModel
	{
		[Required, StringLength(150)]
		[Display(Name = "Descrição do produto")]
		public string Descricao { get; set; } = default!;

		[Range(0.1, 1000)]
		[Display(Name = "Altura (cm)")]
		public decimal AlturaCm { get; set; }

		[Range(0.1, 1000)]
		[Display(Name = "Largura (cm)")]
		public decimal LarguraCm { get; set; }

		[Range(0.1, 1000)]
		[Display(Name = "Comprimento (cm)")]
		public decimal ComprimentoCm { get; set; }

		[Range(0.01, 10000)]
		[Display(Name = "Peso (kg)")]
		public decimal PesoKg { get; set; }

		[Range(0.0, 1000000)]
		[Display(Name = "Valor (R$)")]
		public decimal Valor { get; set; }

		[Range(1, 10000)]
		[Display(Name = "Quantidade")]
		public int Quantidade { get; set; }

		// Sua página lê a imagem em base64 (FileReader). Vamos receber esse base64 aqui.
		// Caso prefira upload tradicional por arquivo, troque por IFormFile e ajuste o form.
		[Display(Name = "Imagem (base64)")]
		public string? ImagemBase64 { get; set; }
	}

	public class collectionOrderViewModel
	{
		[Required, StringLength(200)]
		[Display(Name = "Endereço de Destino")]
		public string Endereco { get; set; } = default!;

		[Required, StringLength(120)]
		[Display(Name = "Destinatário")]
		public string Destinatario { get; set; } = default!;

		[Phone, StringLength(30)]
		[Display(Name = "Telefone")]
		public string? Telefone { get; set; }

		// Se quiser validar CPF/CNPJ, coloque um Regex aqui conforme sua regra.
		[StringLength(20)]
		[Display(Name = "Documento")]
		public string? Documento { get; set; }

		[StringLength(1000)]
		[Display(Name = "Observação")]
		public string? Observacao { get; set; }

		[MinLength(1, ErrorMessage = "Inclua ao menos um produto.")]
		[Display(Name = "Produtos")]
		public List<ProductViewModel> Produtos { get; set; } = new();
	}
}
