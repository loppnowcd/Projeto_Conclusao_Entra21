using Extensions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using via_entrega.entities.Registrations;
using via_entrega.interfaces.Services;
using ViewModels.Cadastro;

namespace Controllers
{
	//[Authorize]
	public class VeiculoController : Controller
	{
		private readonly IVeiculoService _veiculoService;
		private readonly ILogger<VeiculoController> _logger;

		public VeiculoController(IVeiculoService veiculoService, ILogger<VeiculoController> logger)
		{
			_veiculoService = veiculoService;
			_logger = logger;
		}

		// Página principal
		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		// Lista parcial (tabela)
		[HttpGet]
		public async Task<IActionResult> List()
		{
			try
			{
				List<Veiculo?> lista = await _veiculoService.GetAllAsync();
				var veiculosViewModel = lista
					.Where(v => v != null && v.Active && v.PessoaId == HttpContext.GetPessoaId())
					.Select(v => VeiculoViewModel.ConverterParaViewModel(v!))
					.ToList();

				return PartialView("_Table", veiculosViewModel);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Erro ao carregar lista de veículos");
				return PartialView("_Table", new List<VeiculoViewModel>());
			}
		}

		// Buscar por Id (para preencher o modal na edição)
		[HttpGet]
		public async Task<IActionResult> GetById(Guid id)
		{
			try
			{
				var veiculo = await _veiculoService.GetByIdAsync(id);
				if (veiculo == null)
					return NotFound();

				var veiculoViewModel = VeiculoViewModel.ConverterParaViewModel(veiculo);
				return Json(veiculoViewModel);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Erro ao buscar veículo por ID: {Id}", id);
				return NotFound();
			}
		}

		// Criar/Editar (salva; decide pela presença do id)
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Save(VeiculoViewModel veiculoViewModel)
		{
			try
			{
				ModelState.Remove(nameof(veiculoViewModel.Id));

				if (!ModelState.IsValid)
				{
					var errors = ModelState.Values
						.SelectMany(v => v.Errors)
						.Select(e => e.ErrorMessage);
					return BadRequest(string.Join("; ", errors));
				}

				// Validação adicional da placa (formato brasileiro)
				if (!ValidarPlacaBrasileira(veiculoViewModel.Placa))
				{
					return BadRequest("Formato de placa inválido. Use ABC1234 ou ABC1D23.");
				}

				var veiculo = veiculoViewModel.ConverterParaEntidade();

				if (veiculoViewModel.Id == Guid.Empty)
				{
					veiculo.PessoaId = HttpContext.GetPessoaId();
                    // Criar novo
                    var novoId = await _veiculoService.CreateAsync(veiculo);
					if (novoId == null)
						return BadRequest("Não foi possível criar o veículo.");

					await _veiculoService.SaveChangesAsync();
					return Ok(new { id = novoId });
				}
				else
				{
					// Atualizar existente
					var atualizado = await _veiculoService.UpdateAsync(veiculo);
					if (atualizado == null)
						return BadRequest("Não foi possível atualizar o veículo.");

					await _veiculoService.SaveChangesAsync();
					return Ok(new { id = veiculoViewModel.Id });
				}
			}
			catch (ArgumentException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Erro ao salvar veículo");
				return BadRequest("Erro interno ao salvar o veículo.");
			}
		}

		// Excluir
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(Guid id)
		{
			try
			{
				var sucesso = await _veiculoService.DeleteAsync(id);
				if (!sucesso)
					return BadRequest("Não foi possível excluir o veículo.");

				await _veiculoService.SaveChangesAsync();
				return Ok();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Erro ao excluir veículo: {Id}", id);
				return BadRequest("Erro interno ao excluir o veículo.");
			}
		}

		// Método auxiliar para validar placa brasileira
		private bool ValidarPlacaBrasileira(string placa)
		{
			if (string.IsNullOrWhiteSpace(placa) || placa.Length != 7)
				return false;

			placa = placa.ToUpper().Replace("-", "").Replace(" ", "");

			// Formato antigo: ABC1234
			var formatoAntigo = System.Text.RegularExpressions.Regex.IsMatch(placa, @"^[A-Z]{3}[0-9]{4}$");

			// Formato Mercosul: ABC1D23
			var formatoMercosul = System.Text.RegularExpressions.Regex.IsMatch(placa, @"^[A-Z]{3}[0-9]{1}[A-Z]{1}[0-9]{2}$");

			return formatoAntigo || formatoMercosul;
		}
	}
}