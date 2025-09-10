using System.ComponentModel.DataAnnotations;

using via_entrega.entities.Orders;
namespace ViewModels.Pedidos;
public class EntregaResumoViewModel
{
    public Guid Id { get; set; }

    [Display(Name = "Destinatário")]
    public string Destinatario { get; set; } = string.Empty;

    [Display(Name = "Endereço")]
    public string Endereco { get; set; } = string.Empty;

    [Display(Name = "Telefone")]
    public string Telefone { get; set; } = string.Empty;

    [Display(Name = "Data de Criação")]
    public DateTime DataCriacao { get; set; }

    [Display(Name = "Quantidade de Produtos")]
    public int QuantidadeProdutos { get; set; }

    [Display(Name = "Valor Total")]
    public decimal ValorTotal { get; set; }

    [Display(Name = "Peso Total")]
    public decimal PesoTotal { get; set; }

    [Display(Name = "Status")]
    public string Status { get; set; } = "Pendente";

    [Display(Name = "Observação")]
    public string Observacao { get; set; } = string.Empty;

    // Propriedades computadas
    public string TelefoneFormatado => ObterMascaraTelefone(Telefone);
    public string EnderecoResumido => Endereco?.Length > 50 ? $"{Endereco.Substring(0, 47)}..." : Endereco ?? "";
    public string DestinatarioResumido => Destinatario?.Length > 30 ? $"{Destinatario.Substring(0, 27)}..." : Destinatario ?? "";
    public string StatusClass => Status?.ToLower() switch
    {
        "pendente" => "bg-warning",
        "coletado" => "bg-info",
        "entregue" => "bg-success",
        "cancelado" => "bg-danger",
        _ => "bg-secondary"
    };

    public string StatusIcon => Status?.ToLower() switch
    {
        "pendente" => "fas fa-clock",
        "coletado" => "fas fa-truck",
        "entregue" => "fas fa-check-circle",
        "cancelado" => "fas fa-times-circle",
        _ => "fas fa-question-circle"
    };

    // Método para criar resumo a partir de CollectionOrder
    public static EntregaResumoViewModel CriarResumo(CollectionOrder collectionOrder)
    {
        if (collectionOrder == null)
            throw new ArgumentNullException(nameof(collectionOrder));

        return new EntregaResumoViewModel
        {
            Id = collectionOrder.Id,
            Destinatario = collectionOrder.Destinatario ?? string.Empty,
            Endereco = collectionOrder.Endereco ?? string.Empty,
            Telefone = collectionOrder.Telefone ?? string.Empty,
            DataCriacao = collectionOrder.CreatedAt,
            QuantidadeProdutos = collectionOrder.Products?.Count ?? 0,
            ValorTotal = collectionOrder.ValorTotal,
            PesoTotal = collectionOrder.Products?.Sum(p => p.Peso) ?? 0,
            Observacao = collectionOrder.Observacao ?? string.Empty,
            Status = "Pendente" // Por padrão, novas entregas são pendentes
        };
    }

    private static string ObterMascaraTelefone(string telefone)
    {
        if (string.IsNullOrWhiteSpace(telefone))
            return string.Empty;

        var telefoneNumeros = new string(telefone.Where(char.IsDigit).ToArray());

        return telefoneNumeros.Length switch
        {
            10 => $"({telefoneNumeros.Substring(0, 2)}) {telefoneNumeros.Substring(2, 4)}-{telefoneNumeros.Substring(6, 4)}",
            11 => $"({telefoneNumeros.Substring(0, 2)}) {telefoneNumeros.Substring(2, 5)}-{telefoneNumeros.Substring(7, 4)}",
            _ => telefone
        };
    }
}