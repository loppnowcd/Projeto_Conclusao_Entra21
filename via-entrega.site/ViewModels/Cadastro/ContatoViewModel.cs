using System.Runtime.CompilerServices;

using Extensions;

using via_entrega.entities.Registrations;

namespace ViewModels.Cadastro
{
	public class ContatoViewModel
	{
		public Guid Id { get; set; }
		public string Celular { get; set; }
		public string? Telefone { get; set; }
		public string Email { get; set; }


		public static ContatoViewModel MapearParaViewModel(DadosContato dadosContato)
		{
			return new ContatoViewModel
			{
				Id = dadosContato.Id,
				Celular = dadosContato.Celular,
				Telefone = dadosContato.Telefone,
				Email = dadosContato.Email
			};
		}

		public DadosContato MapearParaEntidade(HttpContext httpContext)
		{
			return new DadosContato
			{
				Id = this.Id,
				Celular = this.Celular,
				Telefone = this.Telefone,
				Email = this.Email,
				PessoaId = httpContext.GetPessoaId()
			};
		}
	}
}
