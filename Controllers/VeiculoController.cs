using Microsoft.AspNetCore.Mvc;
using ProjetoEntra21.Models;

namespace ProjetoEntra21.Controllers
{
    public class VeiculoController : Controller
    {
        // ==========================================================
        // PASSO 1: ADICIONE ESTE MÉTODO QUE ESTAVA FALTANDO
        // Esta é a ação que MOSTRA o formulário em branco (GET)
        public IActionResult CadastroVeiculo()
        {
            return View();
        }
        // ==========================================================


        // PASSO 2: DESCOMENTE O [HttpPost] AQUI
        // Este é o seu método que já existe (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CadastroVeiculo(Veiculo veiculo)
        {
            // O ModelState.IsValid verifica se os dados recebidos são válidos
            // (ex: se campos obrigatórios foram preenchidos)
            if (ModelState.IsValid)
            {
                // !! AQUI VIRA A LÓGICA PARA SALVAR NO BANCO DE DADOS !!
                // Por enquanto, vamos apenas simular que deu tudo certo.
                // O código seria algo como: _context.PessoasF.Add(pessoaF);
                //                          _context.SaveChanges();

                // Após salvar, redireciona o usuário para a página inicial para que ele não fique na tela de formulário.
                return RedirectToAction("Index", "Home");
            }

            // Se o modelo não for válido (ex: campo nome em branco), 
            // a aplicação retorna para a mesma tela de cadastro,
            // mas desta vez exibindo as mensagens de erro e mantendo os dados que o usuário já digitou.
            return View(veiculo);
        }
    }
}