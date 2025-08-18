using via_entrega.entities.Registrations;
using via_entrega.Interfaces.Common;

namespace via_entrega.interfaces.Repositories
{
    public interface IUsuarioRepository : ICRUD<Usuario>
    {
        public Task<bool> AtivarAsync(Guid id);
        public Task<bool> DesativarAsync(Guid id);
    }
}
