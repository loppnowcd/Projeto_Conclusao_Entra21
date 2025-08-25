using via_entrega.entities.Enums;

namespace ViewModels.PessoaFisica
{
	public class PessoaFisicaViewModel
	{
		public Guid Id { get; set; }
		public string Nome { get; set; } = string.Empty;
		public string? Cpf { get; set; }
		public string? Email { get; set; }
		public string? Telefone { get; set; }
	}
}

