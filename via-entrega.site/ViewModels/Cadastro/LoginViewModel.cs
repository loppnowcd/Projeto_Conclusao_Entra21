namespace ViewModels.Cadastro
{
    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Senha { get; set; }
        public bool LembrarSenha { get; set; }

        public List<EnderecoViewModel> Enderecos { get; set; } = new();
        public List<ContatoViewModel> Contatos { get; set; } = new();
    }
}
