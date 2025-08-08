using via_entrega.entities.Registrations;
using via_entrega.Interfaces.Common;

namespace via_entrega.interfaces.Repositories
{
    public interface IPessoaJuridicaRepository : ICRUD<PessoaJuridica>
    {
        public Task<PessoaJuridica> BuscarPorCnpjAsync(string cnpj);
        public Task<IEnumerable<PessoaJuridica>> BuscarPorRazaoSocialAsync(string razaoSocial);
    }
}
