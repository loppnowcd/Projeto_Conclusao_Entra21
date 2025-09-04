using System.ComponentModel.DataAnnotations;
using ProjetoGeanBagattoli.Enums;


namespace ProjetoGeanBagattoli.Models
{
    public class DadosEstadoCivil
    {
        [Display(Name = "Estatus Social")]
        public EstatSocial EstatusSocial { get; set; }

        [Display(Name = "Nome do conjuge")]
        public string? NomeConjuge { get; set; }

        [Display(Name = "Regime do casamento")]
        public RegimCasamento? RegimeCasamento { get; set; }
    }
}
