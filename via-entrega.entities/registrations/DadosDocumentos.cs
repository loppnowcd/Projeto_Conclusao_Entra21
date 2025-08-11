using System.ComponentModel.DataAnnotations;

namespace via_entrega.entities.Registrations
{
    public class DadosDocumentos
    {
               		
		public string CNH { get; set; }

        [Display(Name = "Data de Expedição da CNH")]
        public DateTime CNHExpedido { get; set; }
    }
}
