using via_entrega.entities.Registrations;
using via_entrega.Interfaces.Common;

namespace via_entrega.interfaces.Services
{
    public interface IPessoaJuridicaService : ICRUD<PessoaJuridica>
    {
        public Task<PessoaJuridica> BuscarPorCnpjAsync(string cnpj);
        public Task<IEnumerable<PessoaJuridica>> BuscarPorRazaoSocialAsync(string razaoSocial);
    }
}
