using System.ComponentModel.DataAnnotations;

namespace via_entrega.entities
{
    public class DadosContato
    {
        public string Telefone { get; set; }

        [Display(Name = "E-mail")]
        public string eMail { get; set; }
    }
}
