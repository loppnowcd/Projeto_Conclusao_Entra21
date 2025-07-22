using System.ComponentModel.DataAnnotations;

namespace ProjetoEntra21.Models
{
    public class DadosContato
    {
        public string Telefone { get; set; }

        [Display(Name = "E-mail")]
        public string eMail { get; set; }
    }
}
