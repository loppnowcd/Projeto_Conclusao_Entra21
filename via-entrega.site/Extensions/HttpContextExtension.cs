using via_entrega.services.Auth;

namespace Extensions
{
    public static class HttpContextExtension
    {
        public static Guid? GetPessoaId(this HttpContext httpContext)
        {
            if (httpContext.User.Identity is not null && httpContext.User.Identity.IsAuthenticated)
            {
                var idPessoaClaim = httpContext.User.Claims.FirstOrDefault(c => c.Type == CustomClaims.Pessoa);
                if (idPessoaClaim != null && Guid.TryParse(idPessoaClaim.Value, out Guid idPessoa))
                {
                    return idPessoa;
                }
            }
            return null;
        }
    }
}
