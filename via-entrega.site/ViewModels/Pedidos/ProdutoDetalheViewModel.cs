namespace ViewModels.Pedidos
{
	public class ProdutoDetalheViewModel
	{
		public Guid Id { get; set; }
		public double Largura { get; set; }
		public double Altura { get; set; }
		public double Peso { get; set; }
		public double Comprimento { get; set; }
		public string Descricao { get; set; }
		public byte[] Imagem { get; set; }
		public int Quantidade { get; set; }
		public double Valor { get; set; }
	}
}
