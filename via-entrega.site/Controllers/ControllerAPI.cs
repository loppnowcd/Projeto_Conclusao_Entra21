using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Controllers
{
    [Route("api/marcas")] 
    [ApiController]      
    public class MarcasController : ControllerBase 
    {
        private readonly IHttpClientFactory _clientFactory;

        public MarcasController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        
        [HttpGet]
        public async Task<IActionResult> GetMarcas()
        {
            var requestUrl = "https://parallelum.com.br/fipe/api/v1/carros/marcas";
            var client = _clientFactory.CreateClient();

            try
            {
                var response = await client.GetAsync(requestUrl);

                //if (response.IsSuccessStatusCode)
                //{
                //    var jsonString = await response.Content.ReadAsStringAsync();
                //    var marcas = JsonSerializer.Deserialize<List<DadosMarcaFipe>>(jsonString,
                //        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                //    return Ok(marcas);
                //}
                //else
                //{
                //    return StatusCode((int)response.StatusCode, "Erro ao consultar a API da Fipe.");
                //}
                return Ok();
            }
            catch (HttpRequestException e)
            {
                return StatusCode(503, $"Erro de serviço: {e.Message}");
            }
        }
    }
}