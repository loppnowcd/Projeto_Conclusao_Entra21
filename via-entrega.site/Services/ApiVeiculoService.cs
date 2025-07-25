using System.Net.Http.Json;
using System.Text.Json.Serialization;
namespace Services;

// ===================================
// === MODELS PARA OS DADOS DA FIPE ===
// ===================================

public class MarcaViewModel
{
    // A API retorna "nome" e "codigo" (minúsculo)
    // Usamos PascalCase em C# por convenção. O System.Text.Json faz a conversão automática.
    public string? Nome { get; set; }
    public string? Codigo { get; set; }
}

public class ModeloViewModel
{
    public string? Nome { get; set; }
    public int Codigo { get; set; }
}

// Classe auxiliar, pois a API de modelos retorna um objeto que contém a lista de modelos.
// Ex: { "modelos": [...], "anos": [...] }
public class FipeModelosResponse
{
    public List<ModeloViewModel>? Modelos { get; set; }
}


// ===================================
// ===     O SERVIÇO DA FIPE       ===
// ===================================

public class FipeApiService
{
    private readonly HttpClient _httpClient;

    // Injeção de dependência do HttpClient, igual ao seu serviço do IBGE
    public FipeApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Busca todas as marcas de carro na API da Fipe.
    /// </summary>
    public async Task<List<MarcaViewModel>> GetMarcasAsync()
    {
        string url = "https://parallelum.com.br/fipe/api/v1/carros/marcas";

        try
        {
            var marcas = await _httpClient.GetFromJsonAsync<List<MarcaViewModel>>(url);
            return marcas?.OrderBy(m => m.Nome).ToList() ?? new List<MarcaViewModel>();
        }
        catch (Exception ex)
        {
            // O ideal é logar o erro aqui
            Console.WriteLine($"Erro ao buscar marcas da FIPE: {ex.Message}");
            return new List<MarcaViewModel>(); // Retorna lista vazia em caso de erro
        }
    }

    /// <summary>
    /// Busca os modelos de uma marca específica usando o código da marca.
    /// </summary>
    /// <param name="codigoMarca">O código da marca (ex: "59" para Fiat)</param>
    public async Task<List<ModeloViewModel>> GetModelosPorMarcaAsync(string codigoMarca)
    {
        // Validação simples para evitar chamadas desnecessárias à API
        if (string.IsNullOrWhiteSpace(codigoMarca))
        {
            return new List<ModeloViewModel>();
        }

        string url = $"https://parallelum.com.br/fipe/api/v1/carros/marcas/{codigoMarca}/modelos";

        try
        {
            // Usamos a classe 'FipeModelosResponse' porque a API não retorna a lista diretamente
            var response = await _httpClient.GetFromJsonAsync<FipeModelosResponse>(url);

            var modelos = response?.Modelos;
            return modelos?.OrderBy(m => m.Nome).ToList() ?? new List<ModeloViewModel>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao buscar modelos da FIPE para a marca {codigoMarca}: {ex.Message}");
            return new List<ModeloViewModel>();
        }
    }
}
