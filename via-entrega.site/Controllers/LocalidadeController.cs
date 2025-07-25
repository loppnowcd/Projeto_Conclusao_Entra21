using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

// Atributo de Rota para organizar os endpoints
[Route("api/[controller]")] // As chamadas serão feitas para /api/Localidade/...
[ApiController] // Indica que este controller responde a requisições de API
[Authorize]
public class LocalidadeController : Controller
{
    private readonly IbgeApiService _ibgeApiService;

    public LocalidadeController(IbgeApiService ibgeApiService)
    {
        _ibgeApiService = ibgeApiService;
    }

    // A rota para este método será /api/Localidade/{estado}
    // Ex: /api/Localidade/SC
    [HttpGet("{estado}")]
    public async Task<IActionResult> GetCidades(string estado)
    {
        if (string.IsNullOrWhiteSpace(estado))
        {
            return BadRequest("O estado não pode ser nulo ou vazio.");
        }

        var cidades = await _ibgeApiService.GetCidadesPorEstado(estado);
        return Ok(cidades); // Ok() é o jeito mais comum de retornar JSON em API Controllers
    }
}