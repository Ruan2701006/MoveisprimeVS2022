using Microsoft.EntityFrameworkCore;
using MoveisprimeVS.ORM;
using MoveisprimeVS.Repositorio;
using SiteAgendamento.Repositorio;

var builder = WebApplication.CreateBuilder(args);

// Configura��o do DbContext com a string de conex�o
builder.Services.AddDbContext<BdMoveisPrimeContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registro dos reposit�rios
builder.Services.AddScoped<UsuarioR>();  // Reposit�rio de usu�rios
builder.Services.AddScoped<ServicoR>();  // Reposit�rio de servi�os
builder.Services.AddScoped<AgendamentoR>();  // Reposit�rio de agendamentos
builder.Services.AddScoped<RelatorioR>();  // Reposit�rio de agendamentos
builder.Services.AddScoped<DashboardRepositorio>();  // Reposit�rio de agendamentos

// Configura��o de sess�es
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tempo de expira��o da sess�o
    options.Cookie.HttpOnly = true; // Protege o cookie para ser acessado apenas por HTTP
});

// Configura��o de controllers com views
builder.Services.AddControllersWithViews();

// Configura��o de autentica��o (caso necess�rio)
builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", options =>
    {
        options.Cookie.Name = "YourAppCookie";
        options.LoginPath = "/Login";  // Defina a rota de login, se necess�rio
    });

var app = builder.Build();

// Configura��o do pipeline de requisi��es
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");  // P�gina de erro personalizada em produ��o
    app.UseHsts();  // For�a o uso de HTTPS
}

app.UseHttpsRedirection();  // Redireciona para HTTPS
app.UseStaticFiles();  // Serve arquivos est�ticos (CSS, JS, imagens, etc.)

app.UseRouting();  // Habilita o roteamento de URLs

// Adiciona o middleware de sess�o
app.UseSession();

// Autentica��o (caso tenha configurado)
app.UseAuthentication();
app.UseAuthorization();

// Mapeamento de rotas para os controllers
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Inicializa o aplicativo
app.Run();
