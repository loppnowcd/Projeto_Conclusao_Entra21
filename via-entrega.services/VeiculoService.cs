using via_entrega.entities.Registrations;
using via_entrega.interfaces.Repositories;
using via_entrega.interfaces.Services;

namespace via_entrega.services
{
    public class VeiculoService : IVeiculoService
    {
        private readonly IVeiculoRepository _veiculoRepositorio;
        public VeiculoService(IVeiculoRepository veiculoRepositorio)
        {
            _veiculoRepositorio = veiculoRepositorio;
        }


        public async Task<Veiculo?> GetByIdAsync(Guid id)
        {
            return await _veiculoRepositorio.GetByIdAsync(id);
        }

        public async Task<Guid?> CreateAsync(Veiculo veiculo)
        {
            if (veiculo == null) throw new ArgumentNullException("Veiculo está nulo");

            if (string.IsNullOrEmpty(veiculo.Placa))
                throw new ArgumentNullException("Placa não informado");

            return await _veiculoRepositorio.CreateAsync(veiculo);
        }

        public async Task<List<Veiculo?>> GetAllAsync()
        {
            return await _veiculoRepositorio.GetAllAsync();
        }

        public async Task<Veiculo?> UpdateAsync(Veiculo veiculo)
        {
            return await _veiculoRepositorio.UpdateAsync(veiculo);
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _veiculoRepositorio.DeleteAsync(id);
        }
    }
}

