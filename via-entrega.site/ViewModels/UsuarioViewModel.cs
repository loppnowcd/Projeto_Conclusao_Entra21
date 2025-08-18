using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class UsuarioViewModel
    {
        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        public string Senha { get; set; }
        [Required(ErrorMessage = "O campo Confirmar senha é obrigatório.")]
        public string ConfirmarSenha { get; set; }
        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        public string Email { get; set; }
    }
}
