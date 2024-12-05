using MoveisprimeVS.Models;
using MoveisprimeVS.ORM;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace MoveisprimeVS.Repositorio
{
    public class AgendamentoR
    {
        private BdMoveisPrimeContext _context;

        public AgendamentoR(BdMoveisPrimeContext context)
        {
            _context = context;
        }

        // Método para inserir um agendamento
        public bool InserirAgendamento(DateTime dtHoraAgendamento, DateOnly dataAtendimento, TimeOnly horario, int fkUsuarioId, int fkServicoId)
        {
            try
            {
                // Criando uma instância do modelo AtendimentoVM
                var atendimento = new TbAgendamento
                {
                    DtHoraAgendamento = dtHoraAgendamento,
                    DataAtendimento = dataAtendimento,
                    Horario = horario,
                    FkUsuarioId = fkUsuarioId,
                    FkServicoId = fkServicoId
                };

                // Adicionando o atendimento ao contexto
                _context.TbAgendamentos.Add(atendimento);
                _context.SaveChanges(); // Persistindo as mudanças no banco de dados

                return true; // Retorna true indicando sucesso
            }
            catch (Exception ex)
            {
                // Em caso de erro, pode-se logar a exceção (ex.Message)
                return false; // Retorna false em caso de erro
            }
        }

        public List<ViewAgendamentoVM> ListarAtendimentos()
        {
            List<ViewAgendamentoVM> listAtendimentos = new List<ViewAgendamentoVM>();

            // Recuperando todos os atendimentos do DbSet
            var listTb = _context.ViewAgendamentos.ToList();

            // Convertendo os atendimentos de TbAtendimento para AtendimentoVM
            foreach (var item in listTb)
            {
                var atendimento = new ViewAgendamentoVM
                {
                    Id = item.Id,
                    DtHoraAgendamento = item.DtHoraAgendamento,
                    DataAtendimento = item.DataAtendimento,
                    Horario = item.Horario,
                    TipoServico = item.TipoServico,
                    Valor = item.Valor,
                    Nome = item.Nome,
                    Email = item.Email,
                    Telefone = item.Telefone,

                };

                listAtendimentos.Add(atendimento);
            }

            return listAtendimentos;
        }

        // Método para atualizar um agendamento
        public bool AtualizarAgendamento(int id, AgendamentoVM agendamentoVM)
        {
            try
            {
                var agendamento = _context.TbAgendamentos.FirstOrDefault(a => a.Id == id);
                if (agendamento != null)
                {
                    // Atualiza os dados do agendamento
                    agendamento.DtHoraAgendamento = agendamentoVM.DtHoraAgendamento;
                    agendamento.DataAtendimento = agendamentoVM.DataAtendimento;
                    agendamento.Horario = agendamentoVM.Horario;
                    agendamento.FkUsuarioId = agendamentoVM.FkUsuarioId;
                    agendamento.FkServicoId = agendamentoVM.FkServicoId;

                    _context.SaveChanges(); // Salva as alterações no banco

                    return true;
                }

                return false; // Retorna falso se não encontrar o agendamento
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar o agendamento: {ex.Message}");
                return false;
            }
        }

        // Método para excluir um agendamento
        public bool ExcluirAgendamento(int id)
        {
            try
            {
                var agendamento = _context.TbAgendamentos.FirstOrDefault(a => a.Id == id);
                if (agendamento != null)
                {
                    _context.TbAgendamentos.Remove(agendamento); // Remove o agendamento
                    _context.SaveChanges(); // Salva as alterações no banco

                    return true;
                }

                return false; // Retorna falso se não encontrar o agendamento
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao excluir o agendamento com ID {id}: {ex.Message}");
                return false;
            }
        }
        public List<AgendamentoVM> ConsultarAgendamento(string datap)
        {
            DateOnly data = DateOnly.ParseExact(datap, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            string dataFormatada = data.ToString("yyyy-MM-dd"); // Formato desejado: "yyyy-MM-dd"
            Console.WriteLine(dataFormatada);

            try
            {
                // Consulta ao banco de dados, convertendo para o tipo AtendimentoVM
                var ListaAgendamento = _context.TbAgendamentos
                    .Where(a => a.DataAtendimento == DateOnly.Parse(dataFormatada))
                    .Select(a => new AgendamentoVM
                    {
                        // Mapear as propriedades de TbAtendimento para AtendimentoVM
                        // Suponha que TbAtendimento tenha as propriedades Id, DataAtendimento, e outras:
                        Id = a.Id,
                        DtHoraAgendamento = a.DtHoraAgendamento,
                        DataAtendimento = DateOnly.Parse(dataFormatada),
                        Horario = a.Horario,
                        FkUsuarioId = a.FkUsuarioId,
                        FkServicoId = a.FkServicoId
                    })
                    .ToList(); // Converte para uma lista

                return ListaAgendamento;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao consultar agendamentos: {ex.Message}");
                return new List<AgendamentoVM>(); // Retorna uma lista vazia em caso de erro
            }
        }
        public List<UsuarioVM> ListarNomesAgendamentos()
        {
            // Lista para armazenar os usuários com apenas Id e Nome
            List<UsuarioVM> listFun = new List<UsuarioVM>();

            // Obter apenas os campos Id e Nome da tabela TbUsuarios
            var listTb = _context.TbUsuarios
                                 .Select(u => new UsuarioVM
                                 {
                                     Id = u.Id,
                                     Nome = u.Nome
                                 })
                                 .ToList();

            // Retorna a lista já com os campos filtrados
            return listTb;
        }
    }
}
