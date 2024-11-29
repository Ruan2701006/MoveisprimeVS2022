using MoveisprimeVS.Models;
using MoveisprimeVS.ORM;
using Microsoft.EntityFrameworkCore;

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
        public bool InserirAgendamento(AgendamentoVM agendamentoVM)
        {
            try
            {
                // Cria uma nova entidade para o agendamento
                var agendamento = new TbAgendamento
                {
                    DtHoraAgendamento = agendamentoVM.DtHoraAgendamento,
                    DataAtendimento = agendamentoVM.DataAtendimento,
                    Horario = agendamentoVM.Horario,
                    FkUsuarioId = agendamentoVM.FkUsuarioId,
                    FkServicoId = agendamentoVM.FkServicoId 
                };

                    _context.TbAgendamentos.Add(agendamento);
                    _context.SaveChanges(); // Salva as alterações no banco

                return true;
            }
            catch (Exception ex)
            {
                // Log do erro ou tratamento
                Console.WriteLine($"Erro ao inserir agendamento: {ex.Message}");
                return false;
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
    }
}
