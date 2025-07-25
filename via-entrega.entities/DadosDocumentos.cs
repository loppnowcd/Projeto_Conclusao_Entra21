using System.ComponentModel.DataAnnotations;

namespace via_entrega.entities
{
    public class DadosDocumentos
    {
        public string CPF { get; set; }

		public string CNPJ { get; set; }
		public string CNH { get; set; }

        [Display(Name = "Data de Expedição da CNH")]
        public DateTime CNHExpedido { get; set; }
    }
}
