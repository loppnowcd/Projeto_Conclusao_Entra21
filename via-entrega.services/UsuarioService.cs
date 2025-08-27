using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using via_entrega.entities.Registrations;
using via_entrega.interfaces.Repositories;
using via_entrega.interfaces.Services;
using via_entrega.repositoriess.Registrations;

namespace via_entrega.services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Guid?> CriarAsync(Usuario usuario)
        {
            Guid? id = await _usuarioRepository.CreateAsync(usuario);

            if (Guid.TryParse(id.Value.ToString(), out Guid result))
            {
                EnviarEmailConfirmacao(usuario.Email);
            }

            return id;
        }

        private void EnviarEmailConfirmacao(string email)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient();

                smtpClient.Port = 587;
                smtpClient.Host = "smtps.bol.com.br";
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Timeout = 100000;
                smtpClient.Credentials = new NetworkCredential("mateus.henryke@bol.com.br", "Mateus.2025");

                MailMessage mail = new MailMessage();
                mail.Body = "Este é o corpo do email de confirmação.";
                mail.Subject = "Confirmação de Email";
                mail.To.Add(email);
                mail.From = new MailAddress("mateus.henryke@bol.com.br", "Lindinho");
                smtpClient.SendAsync(mail, null);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<ClaimsPrincipal?> Login(string email, string password)
        {
            Usuario? usuario = await _usuarioRepository.BuscarPorEmail(email);

            if (usuario is null || usuario.Senha != password)
                return null;

            // Criar claims do usuário
            List<Claim>? claims = new()
            {
                new Claim(ClaimTypes.Name, usuario.Email),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Role, "Usuario"),
                new Claim("UserId", usuario.Id.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            return claimsPrincipal;
        }

        public async Task<Usuario?> AtualizarAsync(Usuario usuario)
        {
            return await _usuarioRepository.UpdateAsync(usuario);
        }

        public async Task<bool> AtivarAsync(Guid id)
        {
            return await _usuarioRepository.AtivarAsync(id);
        }

        public async Task<bool> DesativarAsync(Guid id)
        {
            return await _usuarioRepository.DesativarAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _usuarioRepository.SaveChangesAsync();
        }
    }
}