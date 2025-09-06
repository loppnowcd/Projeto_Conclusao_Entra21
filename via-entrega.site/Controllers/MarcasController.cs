using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ViewModels;

namespace Controllers
{
	[Route("api/marcas")]
	[ApiController]
	public class MarcasController : ControllerBase
	{
		private readonly IHttpClientFactory _clientFactory;
		private readonly ILogger<MarcasController> _logger;

		public MarcasController(IHttpClientFactory clientFactory, ILogger<MarcasController> logger)
		{
			_clientFactory = clientFactory;
			_logger = logger;
		}

		[HttpGet]
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
					var marcas = JsonSerializer.Deserialize<List<MarcaFipeDto>>(jsonString,
						new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

					if (marcas != null && marcas.Any())
					{
						// Ordenar marcas alfabeticamente
						var marcasOrdenadas = marcas
							.OrderBy(m => m.Nome)
							.Select(m => new { nome = m.Nome, codigo = m.Codigo })
							.ToList();

						return Ok(marcasOrdenadas);
					}

					return Ok(GetMarcasPadrao());
				}
				else
				{
					_logger.LogWarning("Erro na API FIPE. Status: {StatusCode}", response.StatusCode);
					return Ok(GetMarcasPadrao());
				}
			}
			catch (HttpRequestException ex)
			{
				_logger.LogError(ex, "Erro de conexão com API FIPE");
				return Ok(GetMarcasPadrao());
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Erro inesperado ao consultar API FIPE");
				return Ok(GetMarcasPadrao());
			}
		}

		// Método para retornar marcas padrão quando a API falha
		private List<object> GetMarcasPadrao()
		{
			return new List<object>
			{
				new { nome = "Audi", codigo = "6" },
				new { nome = "BMW", codigo = "7" },
				new { nome = "Chevrolet", codigo = "22" },
				new { nome = "Citroën", codigo = "25" },
				new { nome = "Fiat", codigo = "21" },
				new { nome = "Ford", codigo = "23" },
				new { nome = "Honda", codigo = "26" },
				new { nome = "Hyundai", codigo = "27" },
				new { nome = "Jeep", codigo = "28" },
				new { nome = "Mercedes-Benz", codigo = "29" },
				new { nome = "Nissan", codigo = "30" },
				new { nome = "Peugeot", codigo = "31" },
				new { nome = "Renault", codigo = "32" },
				new { nome = "Toyota", codigo = "33" },
				new { nome = "Volkswagen", codigo = "34" }
			};
		}
	}
}