using via_entrega.entities.Registrations;

namespace via_entrega.entities.Orders
{
	public class CollectionOrder : EntityBase
	{
		public Guid? PessoaFisicaId { get; set; }
		public Guid? PessoaJuridicaId { get; set; }
		public virtual ICollection<Product> Products { get; set; }
		public virtual PessoaJuridica PessoaJuridica { get; set; }
		public virtual PessoaFisica PessoaFisica { get; set; }
		public string Endereco { get; set; }
		public string Destinatario { get; set; }
		public string Telefone { get; set; }
		public string Documento { get; set; }
		public string Observacao { get; set; }
		public decimal ValorTotal { get; set; }


	}
}
