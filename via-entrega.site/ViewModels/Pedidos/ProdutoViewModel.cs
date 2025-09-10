using System.ComponentModel.DataAnnotations;

using via_entrega.entities.Orders;

namespace ViewModels.Pedidos
{
    public class ProdutoViewModel : IValidatableObject
    {
        [Required(ErrorMessage = "A descrição do produto é obrigatória.")]
        [StringLength(200, ErrorMessage = "A descrição deve ter no máximo 200 caracteres.")]
        [Display(Name = "Descrição")]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "A altura é obrigatória.")]
        [Range(0.1, 1000, ErrorMessage = "A altura deve estar entre 0.1 e 1000 cm.")]
        [Display(Name = "Altura (cm)")]
        public decimal Altura { get; set; }

        [Required(ErrorMessage = "A largura é obrigatória.")]
        [Range(0.1, 1000, ErrorMessage = "A largura deve estar entre 0.1 e 1000 cm.")]
        [Display(Name = "Largura (cm)")]
        public decimal Largura { get; set; }

        [Required(ErrorMessage = "O comprimento é obrigatório.")]
        [Range(0.1, 1000, ErrorMessage = "O comprimento deve estar entre 0.1 e 1000 cm.")]
        [Display(Name = "Comprimento (cm)")]
        public decimal Comprimento { get; set; }

        [Required(ErrorMessage = "O peso é obrigatório.")]
        [Range(0.01, 1000, ErrorMessage = "O peso deve estar entre 0.01 e 1000 kg.")]
        [Display(Name = "Peso (kg)")]
        public decimal Peso { get; set; }

        [Required(ErrorMessage = "O valor é obrigatório.")]
        [Range(0.01, 100000, ErrorMessage = "O valor deve estar entre R$ 0,01 e R$ 100.000,00.")]
        [Display(Name = "Valor (R$)")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "A quantidade é obrigatória.")]
        [Range(1, 1000, ErrorMessage = "A quantidade deve estar entre 1 e 1000 unidades.")]
        [Display(Name = "Quantidade")]
        public int Quantidade { get; set; } = 1;

        [Display(Name = "Imagem")]
        public string? Imagem { get; set; }

        // Propriedades calculadas
        [Display(Name = "Volume (cm³)")]
        public decimal Volume => Altura * Largura * Comprimento;

        [Display(Name = "Volume (L)")]
        public decimal VolumeEmLitros => Volume / 1000;

        [Display(Name = "Peso Total")]
        public decimal PesoTotal => Peso * Quantidade;

        [Display(Name = "Valor Total")]
        public decimal ValorTotal => Valor * Quantidade;

        [Display(Name = "Volume Total")]
        public decimal VolumeTotal => Volume * Quantidade;

        [Display(Name = "Densidade")]
        public decimal Densidade => Volume > 0 ? Peso / Volume * 1000 : 0; // kg/m³

        public Product ConverterParaEntidade()
        {
            return new Product
            {
                Id = Guid.NewGuid(),
                Descricao = this.Descricao?.Trim() ?? string.Empty,
                Altura = this.Altura,
                Largura = this.Largura,
                Comprimento = this.Comprimento,
                Peso = this.Peso,
                Valor = this.Valor,
                Imagem = this.Imagem != null ? GetImageBytes() : null
            };
        }

        public static ProdutoViewModel ConverterParaViewModel(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            return new ProdutoViewModel
            {
                Descricao = product.Descricao ?? string.Empty,
                Altura = product.Altura,
                Largura = product.Largura,
                Comprimento = product.Comprimento,
                Peso = product.Peso,
                Valor = product.Valor,
                Quantidade = 1, // A entidade Product não tem quantidade, assumimos 1
                Imagem = product.Imagem != null ? Convert.ToBase64String(product.Imagem) : null
            };
        }

        // Implementação de IValidatableObject para validações customizadas
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            // Validação de volume máximo (1m³ = 1.000.000 cm³)
            if (Volume > 1000000)
            {
                results.Add(new ValidationResult(
                    "O volume do produto não pode exceder 1m³ (1.000.000 cm³).",
                    new[] { nameof(Altura), nameof(Largura), nameof(Comprimento) }));
            }

            // Validação de densidade (muito leve ou muito pesado pode indicar erro)
            var densidade = Densidade;
            if (densidade < 0.1m) // Muito leve (menos que 0.1 kg/m³)
            {
                results.Add(new ValidationResult(
                    "A densidade do produto parece muito baixa. Verifique as dimensões e peso.",
                    new[] { nameof(Peso), nameof(Altura), nameof(Largura), nameof(Comprimento) }));
            }
            else if (densidade > 10000m) // Muito pesado (mais que 10 ton/m³)
            {
                results.Add(new ValidationResult(
                    "A densidade do produto parece muito alta. Verifique as dimensões e peso.",
                    new[] { nameof(Peso), nameof(Altura), nameof(Largura), nameof(Comprimento) }));
            }

            // Validação de proporções extremas
            var dimensoes = new[] { Altura, Largura, Comprimento }.OrderBy(x => x).ToArray();
            var razaoAspecto = dimensoes[2] / dimensoes[0]; // maior/menor dimensão

            if (razaoAspecto > 100) // Uma dimensão é mais de 100x maior que outra
            {
                results.Add(new ValidationResult(
                    "As proporções do produto parecem incomuns. Verifique se as dimensões estão corretas.",
                    new[] { nameof(Altura), nameof(Largura), nameof(Comprimento) }));
            }

            // Validação de imagem (se houver)
            if (!string.IsNullOrEmpty(Imagem))
            {
                try
                {
                    var imageBytes = GetImageBytes();
                    if (imageBytes != null && imageBytes.Length > 5 * 1024 * 1024) // 5MB
                    {
                        results.Add(new ValidationResult(
                            "A imagem não pode exceder 5MB.",
                            new[] { nameof(Imagem) }));
                    }
                }
                catch
                {
                    results.Add(new ValidationResult(
                        "Formato de imagem inválido.",
                        new[] { nameof(Imagem) }));
                }
            }

            return results;
        }

        // Método para obter bytes da imagem
        private byte[]? GetImageBytes()
        {
            if (string.IsNullOrEmpty(Imagem))
                return null;

            try
            {
                string base64Data = Imagem;

                // Remove o prefixo data:image/...;base64, se existir
                if (base64Data.Contains(","))
                {
                    base64Data = base64Data.Substring(base64Data.IndexOf(",") + 1);
                }

                return Convert.FromBase64String(base64Data);
            }
            catch (FormatException)
            {
                throw new ArgumentException("Formato de imagem inválido");
            }
        }

        // Método para validar se a imagem é válida
        public bool ImagemValida()
        {
            if (string.IsNullOrEmpty(Imagem))
                return true; // Imagem é opcional

            try
            {
                var bytes = GetImageBytes();
                return bytes != null && bytes.Length > 0 && bytes.Length <= 5 * 1024 * 1024;
            }
            catch
            {
                return false;
            }
        }

        // Método para obter resumo do produto
        public string ObterResumo()
        {
            var volume = VolumeEmLitros;
            return $"{Descricao} • {Quantidade}x • {Peso:N1}kg • {volume:N1}L • R$ {ValorTotal:N2}";
        }

        // Método para obter dimensões formatadas
        public string ObterDimensoesFormatadas()
        {
            return $"{Altura:N1} × {Largura:N1} × {Comprimento:N1} cm";
        }

        // Método para validar dimensões mínimas para transporte
        public bool ValidarDimensoesTransporte()
        {
            // Nenhuma dimensão pode ser menor que 1cm para transporte
            return Altura >= 1 && Largura >= 1 && Comprimento >= 1;
        }

        // Método para calcular frete estimado (exemplo básico)
        public decimal CalcularFreteEstimado(decimal precoPorKg = 5.0m, decimal precoPorLitro = 2.0m)
        {
            var fretePorPeso = PesoTotal * precoPorKg;
            var fretePorVolume = VolumeTotal / 1000 * precoPorLitro; // Convert cm³ to L

            // Retorna o maior entre peso e volume (cubagem)
            return Math.Max(fretePorPeso, fretePorVolume) * Quantidade;
        }

        // Método para verificar se é um produto frágil (baseado na densidade)
        public bool EhFragil()
        {
            // Produtos com densidade muito baixa podem ser frágeis
            return Densidade < 100; // Menos de 100 kg/m³
        }

        // Método para verificar se precisa de cuidados especiais
        public bool PrecisaCuidadosEspeciais()
        {
            return EhFragil() || PesoTotal > 30 || VolumeTotal > 50000; // > 50L
        }

        // Método para obter categoria de tamanho
        public string ObterCategoriaTamanho()
        {
            var volume = VolumeTotal / 1000; // em litros

            return volume switch
            {
                <= 1 => "Pequeno",
                <= 10 => "Médio",
                <= 50 => "Grande",
                _ => "Extra Grande"
            };
        }
    }
}