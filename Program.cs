using Microsoft.EntityFrameworkCore;
using MoveisprimeVS.ORM;
using MoveisprimeVS.Repositorio;
using SiteAgendamento.Repositorio;

var builder = WebApplication.CreateBuilder(args);

// Configuração do DbContext com a string de conexão
builder.Services.AddDbContext<BdMoveisPrimeContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registro dos repositórios
builder.Services.AddScoped<UsuarioR>();  // Repositório de usuários
builder.Services.AddScoped<ServicoR>();  // Repositório de serviços
builder.Services.AddScoped<AgendamentoR>();  // Repositório de agendamentos
builder.Services.AddScoped<RelatorioR>();  // Repositório de agendamentos
builder.Services.AddScoped<DashboardRepositorio>();  // Repositório de agendamentos

// Configuração de sessões
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tempo de expiração da sessão
    options.Cookie.HttpOnly = true; // Protege o cookie para ser acessado apenas por HTTP
});

// Configuração de controllers com views
builder.Services.AddControllersWithViews();

// Configuração de autenticação (caso necessário)
builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", options =>
    {
        options.Cookie.Name = "YourAppCookie";
        options.LoginPath = "/Login";  // Defina a rota de login, se necessário
    });

var app = builder.Build();

// Configuração do pipeline de requisições
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");  // Página de erro personalizada em produção
    app.UseHsts();  // Força o uso de HTTPS
}

app.UseHttpsRedirection();  // Redireciona para HTTPS
app.UseStaticFiles();  // Serve arquivos estáticos (CSS, JS, imagens, etc.)

app.UseRouting();  // Habilita o roteamento de URLs

// Adiciona o middleware de sessão
app.UseSession();

// Autenticação (caso tenha configurado)
app.UseAuthentication();
app.UseAuthorization();

// Mapeamento de rotas para os controllers
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Inicializa o aplicativo
app.Run();
