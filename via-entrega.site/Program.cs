using Microsoft.EntityFrameworkCore;
using Services;
using via_entrega.interfaces.Repositories;
using via_entrega.repositoriess;
using via_entrega.repositoriess.Registrations;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ViaEntregaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("via-entrega.site")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<IbgeApiService>();
//builder.Services
//	.AddAuth0WebAppAuthentication(options => {
//		options.Domain = builder.Configuration["Auth0:Domain"];
//		options.ClientId = builder.Configuration["Auth0:ClientId"];
//	});


builder.Services.AddScoped(typeof(IPessoaFisicaRepository), typeof(PessoaFisicaRepository));

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
