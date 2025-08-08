using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using via_entrega.entities;
using via_entrega.interfaces.Repositories;

namespace via_entrega.repositoriess
{
    public class VeiculoRepository : BaseRepository<Veiculo>, IVeiculoRepository
    {
        public VeiculoRepository(ViaEntregaContext viaEntregaContext) : base(viaEntregaContext)
        {
            
        }
    }
}
