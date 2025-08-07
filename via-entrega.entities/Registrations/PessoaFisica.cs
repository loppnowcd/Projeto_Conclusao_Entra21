using System.ComponentModel.DataAnnotations;
using via_entrega.entities.Enums;

namespace via_entrega.entities.Registrations
{
	public class PessoaFisica : Pessoa
	{
		
		public string Cpf { get; set; }
		public string NomeMae { get; set; }
		public Sexo Sexo { get; set; }	
		public string Cor { get; set; }
		public DadosEstadoCivil EstadoCivil { get; set; }
		
		public string LicenseNumber { get; set; } = string.Empty;
	}
}