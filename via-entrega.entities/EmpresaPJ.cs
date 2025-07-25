using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using via_entrega.entities.Enums;

namespace via_entrega.entities
{
	public class EmpresaPJ
	{
		public int Id { get; set; }
		public string NomeRazao { get; set; }
		public DadosDocumentos Documento { get; set; }
		public DadosEstadoCivil EstadoCivil { get; set; }
		public DadosEndereco Endereco { get; set; }
		public DadosContato Contato { get; set; }
		
		public bool Status { get; set; }

	}
}
