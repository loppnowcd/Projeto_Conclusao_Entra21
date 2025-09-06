using System.ComponentModel.DataAnnotations;
using via_entrega.entities.Registrations;

namespace ViewModels.Cadastro
{
	public class EnderecoViewModel
	{
		public Guid Id { get; set; }

		[Required(ErrorMessage = "A rua é obrigatória")]
		[Display(Name = "Rua")]
		[MaxLength(200, ErrorMessage = "A rua deve ter no máximo 200 caracteres")]
		public string Rua { get; set; } = string.Empty;

		[Required(ErrorMessage = "O número é obrigatório")]
		[Display(Name = "Número")]
		[MaxLength(10, ErrorMessage = "O número deve ter no máximo 10 caracteres")]
		public string Numero { get; set; } = string.Empty;

		[Required(ErrorMessage = "O bairro é obrigatório")]
		[Display(Name = "Bairro")]
		[MaxLength(100, ErrorMessage = "O bairro deve ter no máximo 100 caracteres")]
		public string Bairro { get; set; } = string.Empty;

		[Required(ErrorMessage = "A cidade é obrigatória")]
		[Display(Name = "Cidade")]
		[MaxLength(100, ErrorMessage = "A cidade deve ter no máximo 100 caracteres")]
		public string Cidade { get; set; } = string.Empty;

		[Required(ErrorMessage = "O estado é obrigatório")]
		[Display(Name = "Estado")]
		[MaxLength(2, ErrorMessage = "Use a sigla do estado (ex: SP, RJ)")]
		[MinLength(2, ErrorMessage = "Use a sigla do estado (ex: SP, RJ)")]
		public string Estado { get; set; } = string.Empty;

		[Required(ErrorMessage = "O CEP é obrigatório")]
		[Display(Name = "CEP")]
		[RegularExpression(@"^\d{5}-?\d{3}$", ErrorMessage = "CEP deve estar no formato 99999-999")]
		public string CEP { get; set; } = string.Empty;

		// Método para converter ViewModel para Entidade
		public DadosEndereco ConverterParaEntidade()
		{
			return new DadosEndereco
			{
				Id = this.Id,
				CEP = FormatarCEP(this.CEP),
				Rua = this.Rua?.Trim(),
				Numero = this.Numero?.Trim(),
				Bairro = this.Bairro?.Trim(),
				Cidade = this.Cidade?.Trim(),
				Estado = this.Estado?.ToUpper().Trim(),
				Active = true
			};
		}

		// Método estático para converter Entidade para ViewModel
		public static EnderecoViewModel ConverterParaViewModel(DadosEndereco endereco)
		{
			return new EnderecoViewModel
			{
				Id = endereco.Id,
				CEP = endereco.CEP,
				Rua = endereco.Rua,
				Numero = endereco.Numero,
				Bairro = endereco.Bairro,
				Cidade = endereco.Cidade,
				Estado = endereco.Estado
			};
		}

		// Propriedade auxiliar para exibir endereço completo
		public string EnderecoCompleto => $"{Rua}, {Numero} - {Bairro}, {Cidade}/{Estado} - CEP: {CEP}";

		// Método auxiliar para formatar CEP
		private string FormatarCEP(string cep)
		{
			if (string.IsNullOrWhiteSpace(cep))
				return string.Empty;

			cep = cep.Replace("-", "").Replace(" ", "");

			if (cep.Length == 8)
				return $"{cep.Substring(0, 5)}-{cep.Substring(5, 3)}";

			return cep;
		}
	}
}