using via_entrega.entities.Registrations;
using via_entrega.interfaces.Repositories;
using via_entrega.repositoriess;

namespace via_entrega.repositoriess.Registrations
{
	public class PessoaFisicaRepository : BaseRepository<PessoaFisica>, IPessoaFisicaRepository
	{
		public PessoaFisicaRepository(ViaEntregaContext viaEntregaContext) : base(viaEntregaContext)
		{
		}

	}
}