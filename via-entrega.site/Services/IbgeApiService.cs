namespace Services;
using System.Net.Http.Json; // necessario ter o pacote System.Net.Http.Json

// definição simples de um Model para a Cidade
public class CidadeViewModel
{
    public int Id { get; set; }
    public string? Nome { get; set; }
}

public class IbgeApiService
{
    private readonly HttpClient _httpClient;

    // Usaremos injeção de dependência para obter o HttpClient
    public IbgeApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<CidadeViewModel>> GetCidadesPorEstado(string uf)
    {
        // O endpoint espera a sigla do estado, ex: "SC", "SP", "RJ"
        string url = $"https://servicodados.ibge.gov.br/api/v1/localidades/estados/{uf}/municipios";

        var cidades = await _httpClient.GetFromJsonAsync<List<CidadeViewModel>>(url);

        // Retorna a lista de cidades ordenada por nome
        return cidades?.OrderBy(c => c.Nome).ToList() ?? new List<CidadeViewModel>();
    }
}