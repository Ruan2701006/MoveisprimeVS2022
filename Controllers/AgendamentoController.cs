using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MoveisprimeVS.Models;
using MoveisprimeVS.Repositorio;
using System.Diagnostics;

namespace MoveisprimeVS.Controllers
{
    public class AgendamentoController : Controller
    {
        private readonly AgendamentoR _agendamentoRepositorio;
        private readonly ILogger<ServicoController> _logger;

        public AgendamentoController(AgendamentoR agendamnentoRepositorio, ILogger<ServicoController> logger)
        {
            _agendamentoRepositorio = agendamnentoRepositorio;
            _logger = logger;
        }

        public IActionResult Index()
        {
            // Criar a lista de SelectListItems, onde o 'Value' será o 'Id' e o 'Text' será o 'TipoServico'
            List<SelectListItem> tipoServico = new List<SelectListItem>
             {
                 new SelectListItem { Value = "0", Text = "Consultoria em TI" },
                 new SelectListItem { Value = "1", Text = "Desenvolvimento de Software" },
                  new SelectListItem { Value = "2", Text = "Suporte Técnico" },
                 new SelectListItem { Value = "3", Text = "Treinamento Corporativo" },
                  new SelectListItem { Value = "4", Text = "Auditoria de Sistemas" },
                 new SelectListItem { Value = "5", Text = "Implementação de ERP" }
             };

            // Passar a lista para a View usando ViewBag
            ViewBag.lstTipoServico = new SelectList(tipoServico, "Value", "Text");
            var atendimentos = _agendamentoRepositorio.ListarAtendimentos();
            return View(atendimentos);
        }

        public IActionResult Cadastro_Agendamento()
        {
            return View();
        }

        public IActionResult Gerenciamento_Agendamento_Usuario()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult ConsultarAgendamento(string data)
        {

            var agendamento = _agendamentoRepositorio.ConsultarAgendamento(data);

            if (agendamento != null)
            {
                return Json(agendamento);
            }
            else
            {
                return NotFound();
            }

        }
    }
}
