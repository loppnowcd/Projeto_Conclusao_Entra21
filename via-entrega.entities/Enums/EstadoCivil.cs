using System.ComponentModel.DataAnnotations;

namespace via_entrega.entities.Enums
{
	public enum DadosEstadoCivil
	{
		[Display(Name = "Solteiro")]
		Solteiro,
		[Display(Name = "União Estável")]
		UniaoEstavel,
		[Display(Name = "Casado")]
		Casado,
		[Display(Name = "Divorciado")]
		Divorciado
	}

}

