using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Services;
using via_entrega.interfaces.Repositories;
using via_entrega.interfaces.Services;
using via_entrega.repositoriess;
using via_entrega.repositoriess.Registrations;
using via_entrega.services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ViaEntregaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("via-entrega.site")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<IbgeApiService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                                               .AddCookie(options =>
                                               {
                                                   options.LoginPath = "/Account/Login";
                                                   options.LogoutPath = "/Account/Logout";
                                               });

#region Services
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IDeliveryOrderService, DeliveryOrderService>();
builder.Services.AddScoped<IPessoaFisicaService, PessoaFisicaService>();
builder.Services.AddScoped<IPessoaJuridicaService, PessoaJuridicaService>();
builder.Services.AddScoped<IDadosContatoService, DadosContatoService>();
builder.Services.AddScoped<IDadosEnderecoService, DadosEnderecoService>();
#endregion

#region Repositories

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IPessoaFisicaRepository, PessoaFisicaRepository>();
builder.Services.AddScoped<IPessoaJuridicaRepository, PessoaJuridicaRepository>();
builder.Services.AddScoped<IDeliveryOrderRepository, DeliveryOrderRepository>();
builder.Services.AddScoped<IDadosContatoRepository, DadosContatoRepository>();
builder.Services.AddScoped<IDadosEnderecoRepository, DadosEnderecoRepository>();

#endregion



var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
