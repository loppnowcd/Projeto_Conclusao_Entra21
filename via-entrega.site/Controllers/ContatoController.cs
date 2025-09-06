using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using via_entrega.entities.Registrations;
using via_entrega.interfaces.Services;
using ViewModels.Cadastro;

namespace Controllers
{
	//[Authorize]
	public class ContatoController : Controller
	{
		private readonly IDadosContatoService _contatoService;
		private readonly ILogger<ContatoController> _logger;

		public ContatoController(IDadosContatoService contatoService, ILogger<ContatoController> logger)
		{
			_contatoService = contatoService;
			_logger = logger;
		}


		// Página
		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		// Lista parcial (tabela)
		[HttpGet]
		public async Task<IActionResult> List()
		{
			List<DadosContato?> lista = await _contatoService.GetAllAsync();

			var newLista = lista.SkipWhile(x=> !x.Active).Select(c => ContatoViewModel.MapearParaViewModel(c!)).ToList();

	
			return PartialView("_Table", newLista);
		}

		// Buscar por Id (para preencher o modal na edição)
		[HttpGet]
		public async Task<IActionResult> GetById(Guid id)
		{
			var vm = await _contatoService.GetByIdAsync(id);
			if (vm == null) return NotFound();
			return Json(vm);
		}

		// Criar/Editar (salva; decide pela presença do id)
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Save(ContatoViewModel contatoViewModel)
		{
			ModelState.Remove(nameof(contatoViewModel.Id));
			if (!ModelState.IsValid)
				return BadRequest("Dados inválidos.");

			if (contatoViewModel.Id == Guid.Empty)
			{
				var novoId = await _contatoService.CreateAsync(contatoViewModel.MapearParaEntidade());
				if (novoId == null) return BadRequest("Não foi possível criar o contato.");
				return Ok(new { id = novoId });
			}
			else
			{
				var atualizado = await _contatoService.UpdateAsync(contatoViewModel.MapearParaEntidade());
				if (atualizado is null) return BadRequest("Não foi possível atualizar o contato.");
				return Ok(new { id = contatoViewModel.Id });
			}
		}

		// Excluir
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(Guid id)
		{
			var ok = await _contatoService.DeleteAsync(id);
			if (!ok) return BadRequest("Não foi possível excluir o contato.");
			return Ok();
		}
	}
}