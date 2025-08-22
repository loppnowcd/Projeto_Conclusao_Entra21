using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using via_entrega.entities.Registrations;
using via_entrega.interfaces.Common;
using via_entrega.interfaces.Repositories;
using via_entrega.interfaces.Services;
using via_entrega.repositoriess;

namespace via_entrega.services
{
    public class PessoaFisicaService : IPessoaFisicaService
    {
        private readonly IPessoaFisicaRepository _pessoaFisicaRepository;
        private readonly IDadosEnderecoService _dadosEnderecoService;
        private readonly IDadosContatoService _dadosContatoService;
        private readonly IUsuarioService _usuarioService;
        public PessoaFisicaService(IPessoaFisicaRepository pessoaFisicaRepository,
            IDadosEnderecoService dadosEnderecoService,
            IDadosContatoService dadosContatoService, 
            IUsuarioService usuarioService)
        {
            _pessoaFisicaRepository = pessoaFisicaRepository;
            _dadosEnderecoService = dadosEnderecoService;
            _dadosContatoService = dadosContatoService;
            _usuarioService = usuarioService;
        }

        public async Task<PessoaFisica> BuscarPorCpfAsync(string cpf)
        {
            return await _pessoaFisicaRepository.BuscarPorCpfAsync(cpf);
        }

        public async Task<IEnumerable<PessoaFisica>> BuscarPorNomeAsync(string nome)
        {
            return await _pessoaFisicaRepository.BuscarPorNomeAsync(nome);
        }

        public async Task<Guid?> CreateAsync(PessoaFisica pessoa)
        {
            if (string.IsNullOrEmpty(pessoa.Nome))
                throw new ArgumentException("O nome é obrigatório.");

            if (pessoa.Cpf.Length != 11)
                throw new ArgumentException("CPF inválido.");

            bool existente = await _pessoaFisicaRepository.ExisteCpf(pessoa.Cpf);
            if (existente)
                throw new InvalidOperationException("CPF já cadastrado.");

            Guid? idPessoa = await _pessoaFisicaRepository.CreateAsync(pessoa);

            await SaveChangesAsync();

            foreach (DadosEndereco endereco in pessoa.Enderecos)
            {
                endereco.PessoaId = idPessoa;
                await _dadosEnderecoService.CreateAsync(endereco);
                //await SaveChangesAsync();

            }

            foreach (DadosContato contato in pessoa.Contatos)
            {
                contato.PessoaId = idPessoa;
                await _dadosContatoService.CreateAsync(contato);
                //await SaveChangesAsync();

            }


            return idPessoa;

        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _pessoaFisicaRepository.DeleteAsync(id);
        }

        public async Task<List<PessoaFisica?>> GetAllAsync()
        {
            return await _pessoaFisicaRepository.GetAllAsync();
        }

        public async Task<PessoaFisica?> GetByIdAsync(Guid id)
        {
            return await _pessoaFisicaRepository.GetByIdAsync(id);
        }

        public async Task<PessoaFisica?> UpdateAsync(PessoaFisica entity)
        {
            return await _pessoaFisicaRepository.UpdateAsync(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _pessoaFisicaRepository.SaveChangesAsync();
        }
    }
}
