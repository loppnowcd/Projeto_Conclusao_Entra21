using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using via_entrega.entities.Registrations;
using via_entrega.interfaces.Services;
using via_entrega.services;
using ViewModels.Cadastro;

namespace Controllers
{
    //[Authorize]
    public class EnderecoController : Controller
    {
        private readonly IDadosEnderecoService _dadosEnderecoservice;

        public EnderecoController(IDadosEnderecoService service)
        {
            _dadosEnderecoservice = service;
        }

        public IActionResult Index()
        {
            IEnumerable<DadosEndereco?> enderecos =  _dadosEnderecoservice.GetAllAsync().Result;
            IEnumerable<EnderecoViewModel> enderecoViewModels = [];

            foreach (DadosEndereco? endereco in enderecos)
            {
                enderecoViewModels.Append(new EnderecoViewModel().ConverterParaViewModel(endereco!));
            }

            enderecoViewModels.Append(new EnderecoViewModel()
            {
                CEP = "123123",
                Cidade = "asdasd",
                Estado = "asdasd",
                Bairro = "asdasd",
                Rua = "asdasd",
                Numero = "123",
                Id = Guid.NewGuid()
            });

            return View(enderecoViewModels);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            DadosEndereco? endereco = await _dadosEnderecoservice.GetByIdAsync(id);
            if (endereco == null) return NotFound();
            return Ok(endereco);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EnderecoViewModel enderecoViewModel)
        {
            DadosEndereco dadosEndereco = enderecoViewModel.ConverterParaEntidade();
            Guid? id = await _dadosEnderecoservice.CreateAsync(dadosEndereco);

            return Json(new { success = true, message = "Endereço criado com sucesso!" });
        }

        [HttpPut]
        public async Task<IActionResult> Update(Guid id, [FromBody] EnderecoViewModel enderecoViewModel)
        {
            enderecoViewModel.Id = id;
            DadosEndereco dadosEndereco = enderecoViewModel.ConverterParaEntidade();

            DadosEndereco? atualizado = await _dadosEnderecoservice.UpdateAsync(dadosEndereco);
            if (atualizado is null) return Json(new { success = false, message = "Endereço não encontrado!" });

            return Json(new { success = true, message = "Endereço atualizado com sucesso!" });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            bool deletado = await _dadosEnderecoservice.DeleteAsync(id);
            if (!deletado) return Json(new { success = false, message = "Endereço não encontrado!" });

            return Json(new { success = true, message = "Endereço excluído com sucesso!" });
        }


    }
}
