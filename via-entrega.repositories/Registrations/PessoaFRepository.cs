using System;
using System.Threading.Tasks;
using via_entrega.entities;
using via_entrega.repositories.Base; // Ajuste conforme o local do BaseRepository
using via_entrega.repositories.Context; // Ajuste conforme o local do ViaEntregaContext

namespace via_entrega.repositories.Registrations
{
    public class PessoaFRepository : BaseRepository<PessoaF>
    {
        public PessoaFRepository(ViaEntregaContext viaEntregaContext) : base(viaEntregaContext)
        {
        }

        public override async Task<Guid?> CreateAsync(PessoaF entity)
        {
            // Validações específicas para PessoaF
            return await base.CreateAsync(entity);
        }
    }
}