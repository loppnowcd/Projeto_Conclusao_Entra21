using via_entrega.entities.Registrations;
using via_entrega.Interfaces.Common;

namespace via_entrega.interfaces.Repositories
{
    public interface IDadosEnderecoRepository : ICRUD<DadosEndereco>
    {
        public Task<IEnumerable<DadosEndereco>> BuscarPorCidadeAsync(string cidade);
        public Task<IEnumerable<DadosEndereco>> BuscarPorCepAsync(string cep);
    }
}
