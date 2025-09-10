using System.ComponentModel.DataAnnotations;
using via_entrega.entities.Orders;
using Newtonsoft.Json;

namespace ViewModels.Pedidos
{
    public class EntregaViewModel
    {
        public Guid Id { get; set; } = Guid.Empty;

        [Required(ErrorMessage = "O endereço é obrigatório")]
        [StringLength(500, ErrorMessage = "O endereço deve ter no máximo 500 caracteres")]
        [Display(Name = "Endereço")]
        public string Endereco { get; set; } = string.Empty;

        [Required(ErrorMessage = "O destinatário é obrigatório")]
        [StringLength(200, ErrorMessage = "O nome do destinatário deve ter no máximo 200 caracteres")]
        [Display(Name = "Destinatário")]
        public string Destinatario { get; set; } = string.Empty;

        [StringLength(20, ErrorMessage = "O telefone deve ter no máximo 20 caracteres")]
        [Display(Name = "Telefone")]
        [RegularExpression(@"^(\+?55\s?)?(\(?[1-9]{2}\)?\s?)?[9]?[0-9]{4}-?[0-9]{4}$",
            ErrorMessage = "Formato de telefone inválido")]
        public string? Telefone { get; set; }

        [StringLength(20, ErrorMessage = "O documento deve ter no máximo 20 caracteres")]
        [Display(Name = "Documento")]
        public string? Documento { get; set; }

        [StringLength(1000, ErrorMessage = "A observação deve ter no máximo 1000 caracteres")]
        [Display(Name = "Observação")]
        public string? Observacao { get; set; }

        [Display(Name = "Produtos")]
        public string ProdutosJson { get; set; } = "[]";

        public List<ProdutoViewModel> Produtos { get; set; } = new();

        [Display(Name = "Valor Total")]
        public decimal ValorTotal => Produtos?.Sum(p => p.Valor * p.Quantidade) ?? 0;

        [Display(Name = "Peso Total")]
        public decimal PesoTotal => Produtos?.Sum(p => p.Peso * p.Quantidade) ?? 0;

        [Display(Name = "Quantidade de Itens")]
        public int QuantidadeItens => Produtos?.Sum(p => p.Quantidade) ?? 0;

        public bool IsEdicao => Id != Guid.Empty;

        public CollectionOrder ConverterParaEntidade()
        {
            var collectionOrder = new CollectionOrder
            {
                Id = this.Id == Guid.Empty ? Guid.NewGuid() : this.Id,
                Endereco = this.Endereco?.Trim() ?? string.Empty,
                Destinatario = this.Destinatario?.Trim() ?? string.Empty,
                Telefone = this.Telefone?.Trim(),
                Documento = this.Documento?.Trim(),
                Observacao = this.Observacao?.Trim(),
                ValorTotal = this.ValorTotal,
                Products = this.Produtos?.Select(p => p.ConverterParaEntidade()).ToList() ?? new List<Product>()
            };

            // Associar CollectionOrderId aos produtos
            foreach (var product in collectionOrder.Products)
            {
                product.CollectionOrderId = collectionOrder.Id;
            }

            return collectionOrder;
        }

        public static EntregaViewModel ConverterParaViewModel(CollectionOrder collectionOrder)
        {
            if (collectionOrder == null)
                throw new ArgumentNullException(nameof(collectionOrder));

            var produtos = collectionOrder.Products?.Select(p => ProdutoViewModel.ConverterParaViewModel(p)).ToList()
                          ?? new List<ProdutoViewModel>();

            var entregaViewModel = new EntregaViewModel
            {
                Id = collectionOrder.Id,
                Endereco = collectionOrder.Endereco ?? string.Empty,
                Destinatario = collectionOrder.Destinatario ?? string.Empty,
                Telefone = collectionOrder.Telefone,
                Documento = collectionOrder.Documento,
                Observacao = collectionOrder.Observacao,
                Produtos = produtos,
                ProdutosJson = JsonConvert.SerializeObject(produtos)
            };

            return entregaViewModel;
        }

        public void PrepararParaEdicao()
        {
            if (Produtos?.Any() == true)
            {
                ProdutosJson = JsonConvert.SerializeObject(Produtos);
            }
        }

        public bool ValidarDocumento()
        {
            if (string.IsNullOrWhiteSpace(Documento))
                return true; // Campo opcional

            // Remove caracteres não numéricos
            var documentoLimpo = new string(Documento.Where(char.IsDigit).ToArray());

            // Validação básica de CPF (11 dígitos) ou CNPJ (14 dígitos)
            return documentoLimpo.Length == 11 || documentoLimpo.Length == 14;
        }

        public string ObterMascaraTelefone()
        {
            if (string.IsNullOrWhiteSpace(Telefone))
                return string.Empty;

            var telefoneNumeros = new string(Telefone.Where(char.IsDigit).ToArray());

            return telefoneNumeros.Length switch
            {
                10 => $"({telefoneNumeros.Substring(0, 2)}) {telefoneNumeros.Substring(2, 4)}-{telefoneNumeros.Substring(6, 4)}",
                11 => $"({telefoneNumeros.Substring(0, 2)}) {telefoneNumeros.Substring(2, 5)}-{telefoneNumeros.Substring(7, 4)}",
                _ => Telefone
            };
        }

        public string ObterMascaraDocumento()
        {
            if (string.IsNullOrWhiteSpace(Documento))
                return string.Empty;

            var documentoNumeros = new string(Documento.Where(char.IsDigit).ToArray());

            return documentoNumeros.Length switch
            {
                11 => $"{documentoNumeros.Substring(0, 3)}.{documentoNumeros.Substring(3, 3)}.{documentoNumeros.Substring(6, 3)}-{documentoNumeros.Substring(9, 2)}", // CPF
                14 => $"{documentoNumeros.Substring(0, 2)}.{documentoNumeros.Substring(2, 3)}.{documentoNumeros.Substring(5, 3)}/{documentoNumeros.Substring(8, 4)}-{documentoNumeros.Substring(12, 2)}", // CNPJ
                _ => Documento
            };
        }

        // Implementação de IValidatableObject para validações customizadas
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            // Validação de documento
            if (!string.IsNullOrWhiteSpace(Documento) && !ValidarDocumento())
            {
                results.Add(new ValidationResult("Documento deve ser um CPF (11 dígitos) ou CNPJ (14 dígitos) válido.", new[] { nameof(Documento) }));
            }

            // Validação de produtos
            if (Produtos == null || !Produtos.Any())
            {
                results.Add(new ValidationResult("É necessário adicionar pelo menos um produto.", new[] { nameof(Produtos) }));
            }
            else
            {
                // Validar cada produto
                for (int i = 0; i < Produtos.Count; i++)
                {
                    var produto = Produtos[i];
                    var produtoErrors = produto.Validate(new ValidationContext(produto));

                    foreach (var error in produtoErrors)
                    {
                        results.Add(new ValidationResult($"Produto {i + 1}: {error.ErrorMessage}", new[] { $"Produtos[{i}]" }));
                    }
                }

                // Validações de negócio dos produtos
                var pesoTotal = PesoTotal;
                if (pesoTotal > 500) // Limite de 500kg total
                {
                    results.Add(new ValidationResult("O peso total dos produtos não pode exceder 500kg.", new[] { nameof(Produtos) }));
                }

                var valorTotal = ValorTotal;
                if (valorTotal > 50000) // Limite de R$ 50.000 total
                {
                    results.Add(new ValidationResult("O valor total dos produtos não pode exceder R$ 50.000,00.", new[] { nameof(Produtos) }));
                }
            }

            return results;
        }

        // Método para limpar e formatar dados antes do processamento
        public void PrepararDados()
        {
            Endereco = Endereco?.Trim();
            Destinatario = Destinatario?.Trim();
            Telefone = LimparTelefone(Telefone);
            Documento = LimparDocumento(Documento);
            Observacao = Observacao?.Trim();

            // Processar produtos JSON se necessário
            if (!string.IsNullOrWhiteSpace(ProdutosJson) && (Produtos == null || !Produtos.Any()))
            {
                try
                {
                    Produtos = JsonConvert.DeserializeObject<List<ProdutoViewModel>>(ProdutosJson) ?? new List<ProdutoViewModel>();
                }
                catch (JsonException)
                {
                    Produtos = new List<ProdutoViewModel>();
                }
            }
        }

        // Método para verificar se é uma edição válida
        public bool PodeSerEditado()
        {
            return Id != Guid.Empty;
        }

        // Método para obter resumo da entrega
        public string ObterResumo()
        {
            var qtdProdutos = QuantidadeItens;
            var peso = PesoTotal;
            var valor = ValorTotal;

            return $"{qtdProdutos} item{(qtdProdutos != 1 ? "s" : "")} • {peso:N1}kg • R$ {valor:N2}";
        }

        // Métodos auxiliares privados
        private static string LimparTelefone(string telefone)
        {
            if (string.IsNullOrWhiteSpace(telefone))
                return null;

            var numeros = new string(telefone.Where(char.IsDigit).ToArray());
            return numeros.Length >= 10 ? numeros : telefone;
        }

        private static string LimparDocumento(string documento)
        {
            if (string.IsNullOrWhiteSpace(documento))
                return null;

            var numeros = new string(documento.Where(char.IsDigit).ToArray());
            return numeros.Length >= 11 ? numeros : documento;
        }
    }
}