using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic; // <-- Adicionar este
using ProjetoEntra21.Models;

[Route("api/[controller]")]
[ApiController]
public class VeiculosController : ControllerBase
{
    private readonly IHttpClientFactory _clientFactory;

    public VeiculosController(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    // ==========================================================
    // === NOVO ENDPOINT PARA BUSCAR TODAS AS MARCAS DE CARROS ===
    // ==========================================================
    [HttpGet("marcas")]
    public async Task<IActionResult> GetMarcas()
    {
        var requestUrl = "https://parallelum.com.br/fipe/api/v1/carros/marcas";
        var client = _clientFactory.CreateClient();

        try
        {
            var response = await client.GetAsync(requestUrl);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var marcas = JsonSerializer.Deserialize<List<DadosMarcaFipe>>(jsonString,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return Ok(marcas);
            }
            else
            {
                return StatusCode((int)response.StatusCode, "Erro ao consultar a API da Fipe.");
            }
        }
        catch (HttpRequestException e)
        {
            return StatusCode(503, $"Erro de serviço: {e.Message}");
        }
    }

   
}