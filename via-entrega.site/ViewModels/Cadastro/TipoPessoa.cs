using System.ComponentModel.DataAnnotations;

namespace ViewModels.Cadastro
{
    public enum TipoPessoa
    {
        [Display(Name = "Pessoa Física")]
        PessoaFisica = 1,
        [Display(Name = "Pessoa Jurídica")]
        PessoaJuridica = 2
    }
}
