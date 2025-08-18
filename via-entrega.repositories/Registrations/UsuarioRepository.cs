using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using via_entrega.entities.Registrations;
using via_entrega.interfaces.Repositories;

namespace via_entrega.repositoriess.Registrations
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(ViaEntregaContext viaEntregaContext) : base(viaEntregaContext)
        {
        }

        public async Task<bool> AtivarAsync(Guid id)
        {
            Usuario? usuario = await this.GetByIdAsync(id);

            if(usuario is null) return false;

            usuario.Ativo = true;
            await UpdateAsync(usuario);
            return true;
        }

        public async Task<bool> DesativarAsync(Guid id)
        {
            Usuario? usuario = await this.GetByIdAsync(id);

            if (usuario is null) return false;

            usuario.Ativo = false;
            await UpdateAsync(usuario);
            return true;
        }
    }
}
