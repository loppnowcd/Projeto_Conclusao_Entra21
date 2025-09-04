using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using via_entrega.entities.Registrations;
using via_entrega.interfaces.Services;
using ViewModels.Cadastro;

namespace Controllers
{
	[Authorize]
	public class ContatosController : Controller
	{
		private readonly IDadosContatoService _contatoService;
		private readonly ILogger<ContatosController> _logger;

		public ContatosController(IDadosContatoService contatoService, ILogger<ContatosController> logger)
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

			var newLista = lista.Select(c => ContatoViewModel.MapearParaViewModel(c!)).ToList();

			newLista.Add(new ContatoViewModel()
			{
				Celular = "123123123123",
				Email = "teste@teste.com",
				Id = Guid.NewGuid(),
				Telefone = "123123123"
			});
			newLista.Add(new ContatoViewModel()
			{
				Celular = "123123123123",
				Email = "teste@teste.com",
				Id = Guid.NewGuid(),
				Telefone = "123123123"
			}); newLista.Add(new ContatoViewModel()
			{
				Celular = "123123123123",
				Email = "teste@teste.com",
				Id = Guid.NewGuid(),
				Telefone = "123123123"
			}); newLista.Add(new ContatoViewModel()
			{
				Celular = "123123123123",
				Email = "teste@teste.com",
				Id = Guid.NewGuid(),
				Telefone = "123123123"
			}); newLista.Add(new ContatoViewModel()
			{
				Celular = "123123123123",
				Email = "teste@teste.com",
				Id = Guid.NewGuid(),
				Telefone = "123123123"
			}); newLista.Add(new ContatoViewModel()
			{
				Celular = "123123123123",
				Email = "teste@teste.com",
				Id = Guid.NewGuid(),
				Telefone = "123123123"
			}); newLista.Add(new ContatoViewModel()
			{
				Celular = "123123123123",
				Email = "teste@teste.com",
				Id = Guid.NewGuid(),
				Telefone = "123123123"
			}); newLista.Add(new ContatoViewModel()
			{
				Celular = "123123123123",
				Email = "teste@teste.com",
				Id = Guid.NewGuid(),
				Telefone = "123123123"
			}); newLista.Add(new ContatoViewModel()
			{
				Celular = "123123123123",
				Email = "teste@teste.com",
				Id = Guid.NewGuid(),
				Telefone = "123123123"
			}); newLista.Add(new ContatoViewModel()
			{
				Celular = "123123123123",
				Email = "teste@teste.com",
				Id = Guid.NewGuid(),
				Telefone = "123123123"
			});
			newLista.Add(new ContatoViewModel()
			{
				Celular = "666",
				Email = "piranha@teste.com",
				Id = Guid.NewGuid(),
				Telefone = "47 88888888"
			});
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