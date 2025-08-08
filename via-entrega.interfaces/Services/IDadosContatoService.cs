using via_entrega.entities.Registrations;
using via_entrega.Interfaces.Common;

namespace via_entrega.interfaces.Services
{
    public interface IDadosContatoService<T> : ICRUD<DadosContato>
    {
        public Task<IEnumerable<DadosContato>> BuscarPorEmailAsync(string email);
        public Task<IEnumerable<DadosContato>> BuscarPorTelefoneAsync(string telefone);
    }
}
