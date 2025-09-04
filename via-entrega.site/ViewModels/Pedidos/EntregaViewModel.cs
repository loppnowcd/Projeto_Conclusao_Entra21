using System.ComponentModel.DataAnnotations;
using via_entrega.entities.Orders;

namespace ViewModels.Pedidos
{
    public class EntregaViewModel
    {
        [Required]
        public string Endereco { get; set; }

        [Required]
        public string Destinatario { get; set; }

        public string Telefone { get; set; }
        public string Documento { get; set; }
        public string Observacao { get; set; }
        public string ProdutosJson { get; set; }
        public List<ProdutoViewModel> Produtos { get; set; } = new();

        public CollectionOrder ConverterParaEntidade()
        {
            var collectionOrder = new CollectionOrder
            {
                Endereco = this.Endereco,
                Destinatario = this.Destinatario,
                Telefone = this.Telefone,
                Documento = this.Documento,
                Observacao = this.Observacao,
                Products = this.Produtos.Select(p => p.ConverterParaEntidade()).ToList()
            };
            return collectionOrder;
        }

        public static EntregaViewModel ConverterParaViewModel(CollectionOrder collectionOrder)
        {
            var entregaViewModel = new EntregaViewModel
            {
                Endereco = collectionOrder.Endereco,
                Destinatario = collectionOrder.Destinatario,
                Telefone = collectionOrder.Telefone,
                Documento = collectionOrder.Documento,
                Observacao = collectionOrder.Observacao,
                Produtos = collectionOrder.Products.Select(p => ProdutoViewModel.ConverterParaViewModel(p)).ToList()
            };
            return entregaViewModel;
        }
    }

}
