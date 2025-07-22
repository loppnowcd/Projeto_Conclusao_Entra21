using System.ComponentModel.DataAnnotations;

namespace ProjetoEntra21.Models
{
    public class DadosDocumentos
    {
        public string CPF { get; set; }

        public string CNH { get; set; }

        [Display(Name = "Data de Expedição da CNH")]
        public DateTime CNHExpedido { get; set; }
    }
}
