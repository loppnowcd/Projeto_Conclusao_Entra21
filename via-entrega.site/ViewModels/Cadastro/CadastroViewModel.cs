using System.ComponentModel.DataAnnotations;
using via_entrega.entities.Enums;

namespace ViewModels.Cadastro
{
    public class CadastroViewModel
    {
        [Required]
        public TipoPessoa TipoPessoa { get; set; }

        // Comuns
        [Required]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Senha { get; set; }

        [Compare("Senha", ErrorMessage = "As senhas não conferem.")]
        public string ConfirmarSenha { get; set; }

        // Pessoa Física
        public string? CPF { get; set; }

        public string? NomeMae { get; set; }

        public Sexo Sexo { get; set; }

        public DadosEstadoCivil EstadoCivil { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        // Pessoa Jurídica
        public string? CNPJ { get; set; }

        public string? NomeFantasia { get; set; }

        // Endereços e Contatos
        public List<EnderecoViewModel> Enderecos { get; set; } = new();
        public List<ContatoViewModel> Contatos { get; set; } = new();
    }
}
