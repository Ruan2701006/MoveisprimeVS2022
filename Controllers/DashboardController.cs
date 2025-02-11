using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MoveisprimeVS.Models;
using MoveisprimeVS.Repositorio;
using System.Diagnostics;
using System.Text.Json;
using MoveisprimeVS.Models;
using SiteAgendamento.Repositorio;

namespace MoveisprimeVS.Controllers
{
    public class DashboardController : Controller
    {
        private readonly DashboardRepositorio _dashboardRepositorio;
        private readonly ILogger<DashboardController> _logger;
        public DashboardController(DashboardRepositorio dashboardRepositorio, ILogger<DashboardController> logger)
        {
            _dashboardRepositorio = dashboardRepositorio;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var dadosGrafico = _dashboardRepositorio.ObterDadosGrafico();
            ViewBag.ChartData = JsonSerializer.Serialize(dadosGrafico.Select(d => d.Y));
            return View();
        }

        // Método para contar agendamentos por ano
        public IActionResult ContarAgendamentosPorAno(int ano)
        {
            int totalAgendamentos = _dashboardRepositorio.ContarAgendamentosPorAno(ano);
            return Json(totalAgendamentos);  // Redireciona para a View Index
        }

        // Método para contar usuários por ano
        public IActionResult ContarUsuariosPorAno(int ano)
        {
            int totalUsuarios = _dashboardRepositorio.ContarUsuariosPorAno(ano);
            return Json(totalUsuarios);  // Redireciona para a View Index
        }

        // Método para somar o lucro por ano
        public IActionResult SomarLucroPorAno(int ano)
        {
            decimal lucroTotal = _dashboardRepositorio.SomarLucroPorAno(ano);
            return Json(lucroTotal);  // Redireciona para a View Index
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult ContarAgendamentosPorMes(int ano)
        {
            try
            {
                var agendamentosPorMes = _dashboardRepositorio.ContarAgendamentosPorMes(ano);
                return Ok(agendamentosPorMes);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao contar agendamentos: {ex.Message}");
            }
        }
        public IActionResult ContarUsuariosPorMes(int ano)
        {
            try
            {
                var usuariosPorMes = _dashboardRepositorio.ContarUsuariosPorMes(ano);
                return Ok(usuariosPorMes);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao contar usuários: {ex.Message}");
            }
        }
        public IActionResult SomarLucroPorMes(int ano)
        {
            try
            {
                var lucroPorMes = _dashboardRepositorio.SomarLucroPorMes(ano);
                return Ok(lucroPorMes);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao calcular lucro: {ex.Message}");
            }
        }
    }

}



