using System.ComponentModel.DataAnnotations;
using via_entrega.entities.Registrations;
using via_entrega.services.Auth;

namespace ViewModels.Cadastro
{
	public class VeiculoViewModel
	{
		public Guid Id { get; set; }

		[Required(ErrorMessage = "A marca é obrigatória")]
		[Display(Name = "Marca")]
		public string MarcaFipe { get; set; } = string.Empty;

		[Required(ErrorMessage = "O modelo é obrigatório")]
		[Display(Name = "Modelo")]
		public string Modelo { get; set; } = string.Empty;

		[Required(ErrorMessage = "O ano de fabricação é obrigatório")]
		[Range(1900, 2030, ErrorMessage = "Ano deve estar entre 1900 e 2030")]
		[Display(Name = "Ano de Fabricação")]
		public int AnoFabricacao { get; set; }

		[Required(ErrorMessage = "A cor é obrigatória")]
		[Display(Name = "Cor")]
		public string Cor { get; set; } = string.Empty;

		[Required(ErrorMessage = "A placa é obrigatória")]
		[StringLength(7, MinimumLength = 7, ErrorMessage = "A placa deve ter exatamente 7 caracteres")]
		[Display(Name = "Placa")]
		public string Placa { get; set; } = string.Empty;

		// Método para converter ViewModel para Entidade
		public Veiculo ConverterParaEntidade()
		{
			return new Veiculo
			{
				Id = this.Id,
				MarcaFipe = this.MarcaFipe,
				Modelo = this.Modelo,
				AnoFabricacao = this.AnoFabricacao,
				Cor = this.Cor,
				Placa = this.Placa?.ToUpper(), // Placa sempre maiúscula
				Active = true,
			};
		}

		// Método estático para converter Entidade para ViewModel
		public static VeiculoViewModel ConverterParaViewModel(Veiculo veiculo)
		{
			return new VeiculoViewModel
			{
				Id = veiculo.Id,
				MarcaFipe = veiculo.MarcaFipe,
				Modelo = veiculo.Modelo,
				AnoFabricacao = veiculo.AnoFabricacao,
				Cor = veiculo.Cor,
				Placa = veiculo.Placa
			};
		}

		// Propriedade auxiliar para exibir informações completas do veículo
		public string DescricaoCompleta => $"{MarcaFipe} {Modelo} ({AnoFabricacao}) - {Placa}";
	}
}