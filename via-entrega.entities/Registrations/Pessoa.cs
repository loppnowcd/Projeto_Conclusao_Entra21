using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace via_entrega.entities.Registrations
{
	public class Pessoa : EntityBase
	{
		
		public string Nome { get; set; }
		public DateTime DataRegistro { get; set; }

		public DateTime DataNascimento { get; set; }
		public virtual ICollection<DadosEndereco> Enderecos { get; set; }
		public virtual ICollection<DadosContato> Contatos { get; set; }



	}
}