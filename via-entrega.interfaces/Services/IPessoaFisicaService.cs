using via_entrega.entities.Registrations;
using via_entrega.Interfaces.Common;

namespace via_entrega.interfaces.Services
{
	public interface IPessoaFisicaService<T> : ICRUD<PessoaFisica>

	{
		public Task<PessoaFisica> BuscarPorCpfAsync(string cpf);
		public Task<IEnumerable<PessoaFisica>> BuscarPorNomeAsync(string nome);

	}
}



