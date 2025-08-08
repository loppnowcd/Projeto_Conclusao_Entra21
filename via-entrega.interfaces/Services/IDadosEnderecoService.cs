using via_entrega.entities.Registrations;
using via_entrega.Interfaces.Common;

namespace via_entrega.interfaces.Services
{
    public interface IDadosEnderecoService<T> : ICRUD<DadosEndereco>
    {
        public Task<IEnumerable<DadosEndereco>> BuscarPorCidadeAsync(string cidade);
        public Task<IEnumerable<DadosEndereco>> BuscarPorCepAsync(string cep);
    }
}
