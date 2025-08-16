using via_entrega.entities.Registrations;

namespace via_entrega.interfaces.Services
{
    public interface IUsuarioService
    {
        public Task<Guid?> CriarAsync(Usuario usuario);
        public Task<Usuario?> AtualizarAsync(Usuario usuario);
        public Task<bool> AtivarAsync(Guid id);
        public Task<bool> DesativarAsync(Guid id);
    }
}
