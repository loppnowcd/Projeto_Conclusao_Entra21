using Microsoft.AspNetCore.Mvc;
using ProjetoEntra21.Models;

namespace ProjetoEntra21.Controllers
{
    public class PessoaFController : Controller
    {
        // ==========================================================
        // Esta é a ação que MOSTRA o formulário em branco (GET)
        public IActionResult NovoCadastroPF()
        {
            return View();
        }
        // ==========================================================



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NovoCadastroPF(PessoaF pessoaF)
        {
            // O ModelState.IsValid verifica se os dados recebidos são válidos
            // (ex: se campos obrigatórios foram preenchidos)
            if (ModelState.IsValid)
            {
                // !! AQUI VIRA A LÓGICA PARA SALVAR NO BANCO DE DADOS !!
                // O código seria algo como: _context.PessoasF.Add(pessoaF);
                //                          _context.SaveChanges();

                // Após salvar, redireciona o usuário para a página inicial para que ele não fique na tela de formulário.
                return RedirectToAction("Index", "Home");
            }

            // Se o modelo não for válido (ex: campo nome em branco), 
            // a aplicação retorna para a mesma tela de cadastro,
            // mas desta vez exibindo as mensagens de erro e mantendo os dados que o usuário já digitou.
            return View(pessoaF);
        }
    }
}
