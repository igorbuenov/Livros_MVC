using LivrosMVC.Data;
using LivrosMVC.Interfaces.Emprestimos;
using LivrosMVC.Interfaces.Login;
using LivrosMVC.Interfaces.Senha;
using LivrosMVC.Interfaces.Sessao;
using LivrosMVC.Services.Emprestimos;
using LivrosMVC.Services.Login;
using LivrosMVC.Services.Senha;
using LivrosMVC.Services.Sessao;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Connection database
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Interfaces => Services
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<ILoginInterface, LoginService>();
builder.Services.AddScoped<IEmprestimosInterface, EmprestimosService>();
builder.Services.AddScoped<ISenhaInterface, SenhaService>();
builder.Services.AddScoped<ISessaoInterface, SessaoService>();

// Sessão usuário
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

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

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
