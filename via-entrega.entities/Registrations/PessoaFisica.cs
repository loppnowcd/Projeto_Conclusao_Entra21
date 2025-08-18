using System.ComponentModel.DataAnnotations;
using via_entrega.entities.Enums;

namespace via_entrega.entities.Registrations
{
	public class PessoaFisica : Pessoa
	{
		
		public string Cpf { get; set; }
<<<<<<< HEAD
=======
		//
		public string NomeMae { get; set; }
>>>>>>> f2206b0fb4de47f8f3e6d4d4250bcd3a3b47a9ad
		public Sexo Sexo { get; set; }	
		public DadosEstadoCivil EstadoCivil { get; set; }
		
		public string LicenseNumber { get; set; } = string.Empty;
	}
}