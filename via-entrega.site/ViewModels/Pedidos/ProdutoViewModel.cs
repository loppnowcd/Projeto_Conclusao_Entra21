using System.ComponentModel.DataAnnotations;
using via_entrega.entities.Orders;

namespace ViewModels.Pedidos
{
    public class ProdutoViewModel
    {
        [Required(ErrorMessage = "A descrição do produto é obrigatória.")]
        public string? Descricao { get; set; }
        [Required]
        public decimal Altura { get; set; }
        [Required]
        public decimal Largura { get; set; }
        [Required]
        public decimal Comprimento { get; set; }
        [Required]
        public decimal Peso { get; set; }
        [Required]
        public decimal Valor { get; set; }
        [Required]
        public int Quantidade { get; set; }
        [Required]
        public string? Imagem { get; set; }

        public Product ConverterParaEntidade()
        {
            return new Product
            {
                Descricao = this.Descricao!,
                Altura = this.Altura,
                Largura = this.Largura,
                Comprimento = this.Comprimento,
                Peso = this.Peso,
                Valor = this.Valor,
                Imagem = this.Imagem != null ? GetImage() : null!
            };
        }

        public static ProdutoViewModel ConverterParaViewModel(Product product)
        {
            return new ProdutoViewModel
            {
                Descricao = product.Descricao!,
                Altura = product.Altura,
                Largura = product.Largura,
                Comprimento = product.Comprimento,
                Peso = product.Peso,
                Valor = product.Valor,
                Imagem = product.Imagem != null ? Convert.ToBase64String(product.Imagem) : null!
            };
        }

        private byte[] GetImage()
        {
            try
            {
                string base64Data = this.Imagem;
                if (base64Data.Contains(","))
                {
                    base64Data = base64Data.Substring(base64Data.IndexOf(",") + 1);
                }

                byte[] imagemBytes = Convert.FromBase64String(base64Data);

                return imagemBytes;
            }
            catch (FormatException)
            {
                return null;
            }
        }
    }

   
}
