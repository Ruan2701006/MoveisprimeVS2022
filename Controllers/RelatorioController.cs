using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoveisprimeVS.Repositorio;
using MoveisprimeVS.Models;
using System.Diagnostics;
using MoveisprimeVS.ORM;

namespace MoveisprimeVS.Controllers
{
    public class RelatorioController : Controller
    {
        private readonly RelatorioR _relatorioRepositorio;
        private readonly ILogger<RelatorioController> _logger;
        public RelatorioController(RelatorioR relatorioRepositorio, ILogger<RelatorioController> logger)
        {
            _relatorioRepositorio = relatorioRepositorio;
            _logger = logger;
        }       
      
        public IActionResult Index()
        {            
            return View();
        }

        public IActionResult GetAgendamentos([FromQuery] string campo1, [FromQuery] string campo2, [FromQuery] string campo3, [FromQuery] string valor1, [FromQuery] string valor2, [FromQuery] string valor3)
        {
            // Chama o método da service para obter os agendamentos filtrados
            List<ViewAgendamento> agendamentos = _relatorioRepositorio.GetAgendamentos(campo1, campo2, campo3, valor1, valor2, valor3);

            // Retorna os agendamentos em formato JSON
            return Ok(agendamentos);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

