using Microsoft.AspNetCore.Mvc;
using via_entrega.entities.Registrations;
using via_entrega.interfaces.Services;
using ViewModels.Cadastro;

namespace Controllers
{
    public class CadastroController : Controller
    {
        private readonly IPessoaFisicaService _pessoaFisicaService;
        private readonly IPessoaJuridicaService _pessoaJuridicaService;
        private readonly IUsuarioService _usuarioService;
        public CadastroController(IPessoaFisicaService pessoaFisicaService,
            IPessoaJuridicaService pessoaJuridicaService, IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
            _pessoaFisicaService = pessoaFisicaService;
            _pessoaJuridicaService = pessoaJuridicaService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new CadastroViewModel
            {
                TipoPessoa = TipoPessoa.PessoaFisica,
                Enderecos = new List<EnderecoViewModel> { new EnderecoViewModel() },
                Contatos = new List<ContatoViewModel> { new ContatoViewModel() }
            });
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(CadastroViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Erro"] = "Verifique os dados informados.";
                return View("Index", model); // Retorna a view com os dados preenchidos
            }

            try
            {
                Guid? idPessoa = null;
                if (model.TipoPessoa == TipoPessoa.PessoaJuridica)
                {
                    PessoaJuridica pessoaJuridica = GetDadosPessoaJuridica(model);
                    idPessoa = await _pessoaJuridicaService.CreateAsync(pessoaJuridica);
                }
                else
                {
                    PessoaFisica pessoaFisica = GetDadosPessoaFisica(model);
                    idPessoa = await _pessoaFisicaService.CreateAsync(pessoaFisica);
                }

                Guid? idUsuario = await _usuarioService.CriarAsync(new Usuario
                {
                    Email = model.Email,
                    Senha = model.Senha,
                });

                if (model.TipoPessoa == TipoPessoa.PessoaJuridica)
                {
                    PessoaJuridica pessoaJuridica = await _pessoaJuridicaService.GetByIdAsync(idPessoa.Value);
                    pessoaJuridica.UsuarioId = idUsuario;
                    await _pessoaJuridicaService.UpdateAsync(pessoaJuridica);
                }
                else
                {
                    PessoaFisica pessoaFisica = await _pessoaFisicaService.GetByIdAsync(idPessoa.Value);
                    pessoaFisica.UsuarioId = idUsuario;
                    await _pessoaFisicaService.UpdateAsync(pessoaFisica);
                }

                await _pessoaFisicaService.SaveChangesAsync();

                TempData["Sucesso"] = "Cadastro realizado com sucesso!";
                return RedirectToAction("Index"); 
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Ocorreu um erro inesperado. Verifique os dados e tente novamente.";
                return View("Index", model); 
            }
        }

        private PessoaFisica GetDadosPessoaFisica(CadastroViewModel cadastroViewModel)
        {
            PessoaFisica pessoa = new PessoaFisica()
            {
                Active = true,
                Nome = cadastroViewModel.Nome,
                DataRegistro = cadastroViewModel.DataNascimento,
                Cpf = cadastroViewModel.CPF.OnlyNumbers(),
                NomeMae = cadastroViewModel.NomeMae,
                Sexo = cadastroViewModel.Sexo,
                EstadoCivil = cadastroViewModel.EstadoCivil,
                Enderecos = cadastroViewModel.Enderecos.Select(e => new DadosEndereco
                {
                    Rua = e.Rua,
                    Numero = e.Numero,
                    Bairro = e.Bairro,
                    Cidade = e.Cidade,
                    Estado = e.Estado,
                    CEP = e.CEP,
                    Active = true
                }).ToList(),
                Contatos = []

            };
            return pessoa;
        }

        private PessoaJuridica GetDadosPessoaJuridica(CadastroViewModel cadastroViewModel)
        {
            PessoaJuridica pessoa = new PessoaJuridica()
            {
                Active = true,
                Nome = cadastroViewModel.Nome,
                NomeFatasia = cadastroViewModel.NomeFantasia,
                DataRegistro = cadastroViewModel.DataNascimento,
                Cnpj = cadastroViewModel.CPF.OnlyNumbers(),
                Enderecos = cadastroViewModel.Enderecos.Select(e => new DadosEndereco
                {
                    Rua = e.Rua,
                    Numero = e.Numero,
                    Bairro = e.Bairro,
                    Cidade = e.Cidade,
                    Estado = e.Estado,
                    CEP = e.CEP,
                    Active = true
                }).ToList(),
            };
            return pessoa;
        }
    }

}