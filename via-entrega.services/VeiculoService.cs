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
			if (string.IsNullOrWhiteSpace(veiculo.Modelo))
				throw new ArgumentException("O modelo é obrigatório.");

			if (string.IsNullOrWhiteSpace(veiculo.Placa) || veiculo.Placa.Length != 7)
				throw new ArgumentException("Placa inválida.");

			Guid? id =  await _veiculoRepositorio.CreateAsync(veiculo);
            await SaveChangesAsync();
            return id;
		}

        public async Task<List<Veiculo?>> GetAllAsync()
        {
            return await _veiculoRepositorio.GetAllAsync();
        }

        public async Task<Veiculo?> UpdateAsync(Veiculo veiculo)
        {
			Veiculo? veiculoAtualiado =  await _veiculoRepositorio.UpdateAsync(veiculo);
            await SaveChangesAsync();
            return veiculoAtualiado;

		}
        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _veiculoRepositorio.DeleteAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _veiculoRepositorio.SaveChangesAsync();
        }
    }
}

