namespace ViewModels.Pedidos
{
	public class EntregaDetalheViewModel
	{
		public Guid Id { get; set; }
		public DateTime DataCriacao { get; set; }
		public string Destinatario { get; set; }
		public string TelefoneFormatado { get; set; }
		public string Documento { get; set; }
		public string Endereco { get; set; }
		public string Observacao { get; set; }
		public string TotalProdutos { get; set; }
		public double PesoTotal { get; set; }
		public double VolumeTotal { get; set; }
		public double ValorTotal { get; set; }
		public DateTime? DataAtualizacao { get; set; }
		public List<ProdutoDetalheViewModel> Produtos { get; set; }
	}
}
