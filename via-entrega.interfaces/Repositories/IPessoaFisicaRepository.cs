
using via_entrega.entities.Registrations;
using via_entrega.Interfaces.Common;
using System.Threading.Tasks;

namespace via_entrega.interfaces.Repositories
{
	public interface IPessoaFisicaRepository : ICRUD<PessoaFisica>
	{
		public Task<PessoaFisica> BuscarPorCpfAsync(string cpf);
		public Task<IEnumerable<PessoaFisica>> BuscarPorNomeAsync(string nome);
	}
}
