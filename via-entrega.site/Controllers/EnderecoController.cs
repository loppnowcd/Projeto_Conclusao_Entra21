using Extensions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using via_entrega.entities.Registrations;
using via_entrega.interfaces.Services;
using ViewModels.Cadastro;

namespace Controllers
{
	//[Authorize]
	public class EnderecoController : Controller
	{
		private readonly IDadosEnderecoService _dadosEnderecoService;
		private readonly ILogger<EnderecoController> _logger;

		public EnderecoController(IDadosEnderecoService dadosEnderecoService, ILogger<EnderecoController> logger)
		{
			_dadosEnderecoService = dadosEnderecoService;
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
				List<DadosEndereco?> lista = await _dadosEnderecoService.GetAllAsync();
				var enderecosViewModel = lista
					.Where(e => e != null && e.Active && e.PessoaId == Request.HttpContext.GetPessoaId())
					.Select(e =>  EnderecoViewModel.ConverterParaViewModel(e!))
					.ToList();

				return PartialView("_Table", enderecosViewModel);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Erro ao carregar lista de endereços");
				return PartialView("_Table", new List<EnderecoViewModel>());
			}
		}

		// Buscar por Id (para preencher o modal na edição)
		[HttpGet]
		public async Task<IActionResult> GetById(Guid id)
		{
			try
			{
				var endereco = await _dadosEnderecoService.GetByIdAsync(id);
				if (endereco == null)
					return NotFound();

				var enderecoViewModel = EnderecoViewModel.ConverterParaViewModel(endereco);
				return Json(enderecoViewModel);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Erro ao buscar endereço por ID: {Id}", id);
				return NotFound();
			}
		}

		// Criar/Editar (salva; decide pela presença do id)
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Save(EnderecoViewModel enderecoViewModel)
		{
			try
			{
				ModelState.Remove(nameof(enderecoViewModel.Id));

				if (!ModelState.IsValid)
				{
					var errors = ModelState.Values
						.SelectMany(v => v.Errors)
						.Select(e => e.ErrorMessage);
					return BadRequest(string.Join("; ", errors));
				}

				// Validação adicional do CEP (formato brasileiro)
				if (!ValidarCEPBrasileiro(enderecoViewModel.CEP))
				{
					return BadRequest("Formato de CEP inválido. Use 99999-999 ou 99999999.");
				}

				var endereco = enderecoViewModel.ConverterParaEntidade();

				if (enderecoViewModel.Id == Guid.Empty)
				{
					// Criar novo
					endereco.PessoaId = Request.HttpContext.GetPessoaId();
                    var novoId = await _dadosEnderecoService.CreateAsync(endereco);
					if (novoId == null)
						return BadRequest("Não foi possível criar o endereço.");

					await _dadosEnderecoService.SaveChangesAsync();
					return Ok(new { id = novoId });
				}
				else
				{
					// Atualizar existente
					var atualizado = await _dadosEnderecoService.UpdateAsync(endereco);
					if (atualizado == null)
						return BadRequest("Não foi possível atualizar o endereço.");

					await _dadosEnderecoService.SaveChangesAsync();
					return Ok(new { id = enderecoViewModel.Id });
				}
			}
			catch (ArgumentException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Erro ao salvar endereço");
				return BadRequest("Erro interno ao salvar o endereço.");
			}
		}

		// Excluir
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(Guid id)
		{
			try
			{
				var sucesso = await _dadosEnderecoService.DeleteAsync(id);
				if (!sucesso)
					return BadRequest("Não foi possível excluir o endereço.");

				await _dadosEnderecoService.SaveChangesAsync();
				return Ok();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Erro ao excluir endereço: {Id}", id);
				return BadRequest("Erro interno ao excluir o endereço.");
			}
		}

		// Método auxiliar para validar CEP brasileiro
		private bool ValidarCEPBrasileiro(string cep)
		{
			if (string.IsNullOrWhiteSpace(cep))
				return false;

			cep = cep.Replace("-", "").Replace(" ", "");

			// CEP deve ter exatamente 8 dígitos numéricos
			return System.Text.RegularExpressions.Regex.IsMatch(cep, @"^[0-9]{8}$");
		}
	}
}