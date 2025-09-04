using via_entrega.entities.Registrations;

namespace ViewModels.Cadastro
{
    public class EnderecoViewModel
    {
        public DadosEndereco ConverterParaEntidade ()
        {
            return new DadosEndereco
            {
                Id = this.Id,
                CEP = this.CEP,
                Rua = this.Rua,
                Numero = this.Numero,
                Bairro = this.Bairro,
                Cidade = this.Cidade,
                Estado = this.Estado,
            };
        }

        public EnderecoViewModel ConverterParaViewModel(DadosEndereco enderecoViewModel)
        {
            return new EnderecoViewModel
            {
                Id = enderecoViewModel.Id,
                CEP = enderecoViewModel.CEP,
                Rua = enderecoViewModel.Rua,
                Numero = enderecoViewModel.Numero,
                Bairro = enderecoViewModel.Bairro,
                Cidade = enderecoViewModel.Cidade,
                Estado = enderecoViewModel.Estado,
            };
        }

        public Guid Id { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string CEP { get; set; }
    }
}
