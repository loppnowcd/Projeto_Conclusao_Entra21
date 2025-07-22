using System.Text.Json.Serialization;

namespace ProjetoEntra21.Models
{
    public class DadosModelo
    {
        [JsonPropertyName("Código")]
        public string Codigo { get; set; } // O código pode ser numérico, mas string é mais seguro

        [JsonPropertyName("Marca")]
        public string Nome { get; set; }
    }
}