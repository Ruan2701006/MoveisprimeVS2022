using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MoveisprimeVS.Models;
using MoveisprimeVS.Repositorio;
using System.Diagnostics;

namespace MoveisprimeVS.Controllers
{
    public class ServicoController : Controller
    {
        private readonly ServicoR _servicoRepositorio;
        private readonly ILogger<ServicoController> _logger;
        public ServicoController(ServicoR servicoRepositorio, ILogger<ServicoController> logger)
        {
            _servicoRepositorio = servicoRepositorio;
            _logger = logger;
        }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Cadastro()
        {
            return View();

        }
        public IActionResult Index()
        {
            List<SelectListItem> tipoServico = new List<SelectListItem>
             {
                 new SelectListItem { Value = "0", Text = "Administrador" },
                 new SelectListItem { Value = "1", Text = "Cliente" }
             };

            ViewBag.lstTipoServico = new SelectList(tipoServico, "Value", "Text");

            var Servicos = _servicoRepositorio.ListarServicos();
            return View(Servicos);
        }

        public IActionResult InserirServico(string TipoServico, decimal Valor)
        {
            try
            {
                // Chama o método do repositório que realiza a inserção no banco de dados
                var resultado = _servicoRepositorio.InserirServico(TipoServico, Valor);

                // Verifica o resultado da inserção
                if (resultado)
                {
                    // Se o resultado for verdadeiro, significa que o usuário foi inserido com sucesso
                    return Json(new { success = true, message = "Servico inserido com sucesso!" });
                }
                else
                {
                    // Se o resultado for falso, significa que houve um erro ao tentar inserir o usuário
                    return Json(new { success = false, message = "Erro ao inserir o servico. Tente novamente." });
                }
            }
            catch (Exception ex)
            {
                // Em caso de erro inesperado, captura e exibe o erro
                return Json(new { success = false, message = "Erro ao processar a solicitação. Detalhes: " + ex.Message });
            }
        }

        public IActionResult AtualizarSevico(string TipoServico, decimal Valor)
        {
            try
            {
                // Chama o repositório para atualizar o usuário
                var resultado = _servicoRepositorio.AtualizarServico(TipoServico, Valor);

                if (resultado)
                {
                    return Json(new { success = true, message = "Servico atualizado com sucesso!" });
                }
                else
                {
                    return Json(new { success = false, message = "Erro ao atualizar o servico. Verifique se o servico existe." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Erro ao processar a solicitação. Detalhes: " + ex.Message });
            }
        }

        public IActionResult ExcluirServico(int id)
        {
            try
            {
                // Chama o repositório para excluir o usuário
                var resultado = _servicoRepositorio.ExcluirServico(id);

                if (resultado)
                {
                    return Json(new { success = true, message = "Servico excluído com sucesso!" });
                }
                else
                {
                    // Se o resultado for falso, você pode fornecer uma mensagem mais específica.
                    return Json(new { success = false, message = "Não foi possível excluir o servico. Verifique se ele está vinculado a outros registros no sistema." });
                }
            }
            catch (Exception ex)
            {
                // Captura qualquer erro e inclui a mensagem detalhada da exceção
                return Json(new { success = false, message = "Erro ao processar a solicitação. Detalhes: " + ex.Message });
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

