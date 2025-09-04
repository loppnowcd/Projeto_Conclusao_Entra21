using via_entrega.entities.Enums;
using via_entrega.entities.Registrations;

namespace via_entrega.entities.Orders
{
	public class DeliveryOrder : EntityBase
	{
		public Guid? PessoaFisicaId { get; set; }
		public Guid? PessoaJuridicaId { get; set; }
		public Guid CollectionOrderId { get; set; }


		public virtual CollectionOrder CollectionOrder { get; set; }
		public virtual PessoaJuridica PessoaJuridica { get; set; }
		public virtual PessoaFisica PessoaFisica { get; set; }
		public string Observacao { get; set; }
		public DateTime? DataEntrega { get; set; }
		public DateTime DataColeta { get; set; }
		public virtual Status Status { get; set; }

		public byte[] Imagem { get; set; }

	}
}
