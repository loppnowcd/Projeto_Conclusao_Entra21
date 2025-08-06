using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace via_entrega.entities.Orders
{
	public class Product : EntityBase
	{
		public decimal Altura { get; set; }	
		public decimal Largura { get; set; }
		public decimal Comprimento { get; set; }
		public decimal Peso { get; set; }
		public decimal Valor { get; set; }
		public string Descricao { get; set; }
		public byte[] Imagem { get; set; }
		public Guid CollectionOrderId { get; set; }
		public virtual CollectionOrder CollectionOrder { get; set; }
	}
}
