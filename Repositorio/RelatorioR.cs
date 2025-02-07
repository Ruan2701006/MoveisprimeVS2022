using MoveisprimeVS.Models;
using MoveisprimeVS.ORM;
using Microsoft.EntityFrameworkCore;

namespace MoveisprimeVS.Repositorio
{
    public class RelatorioR 
    {

        private BdMoveisPrimeContext _context;
        public RelatorioR(BdMoveisPrimeContext context)
        {
            _context = context;
        }

        public List<ViewAgendamento> GetAgendamentos(
string campo1, string campo2, string campo3,
string valor1, string valor2, string valor3)
        {
            if (string.IsNullOrEmpty(campo1) && string.IsNullOrEmpty(campo2) && string.IsNullOrEmpty(campo3) &&
                string.IsNullOrEmpty(valor1) && string.IsNullOrEmpty(valor2) && string.IsNullOrEmpty(valor3))
            {
                return _context.ViewAgendamentos
                    .OrderBy(a => a.DtHoraAgendamento)
                    .Take(1000)
                    .ToList();
            }

            var query = _context.ViewAgendamentos.AsQueryable();

            // Método auxiliar para adicionar filtros dinamicamente
            query = AplicarFiltro(query, campo1, valor1);
            query = AplicarFiltro(query, campo2, valor2);
            query = AplicarFiltro(query, campo3, valor3);

            return query
                .OrderBy(a => a.DtHoraAgendamento)
                .Take(1000)
                .ToList();
        }

        private IQueryable<ViewAgendamento> AplicarFiltro(IQueryable<ViewAgendamento> query, string campo, string valor)
        {
            if (string.IsNullOrEmpty(campo) || string.IsNullOrEmpty(valor))
                return query;

            switch (campo.ToLower())
            {
                case "tiposervico":
                    return query.Where(a => a.TipoServico.Contains(valor));
                case "nome":
                    return query.Where(a => a.Nome.Contains(valor));
                case "email":
                    return query.Where(a => a.Email.Contains(valor));
                case "telefone":
                    return query.Where(a => a.Telefone.Contains(valor));
                case "valor":
                    if (decimal.TryParse(valor, out decimal valorDecimal))
                        return query.Where(a => a.Valor == valorDecimal);
                    break;
                case "dataatendimento":
                    if (DateTime.TryParse(valor, out DateTime dateValue))
                    {
                        DateOnly dateOnlyValue = DateOnly.FromDateTime(dateValue);
                        return query.Where(a => a.DataAtendimento == dateOnlyValue);
                    }
                    break;
                case "horario":
                    if (DateTime.TryParse(valor, out DateTime timeValue))
                        return query.Where(a => a.Horario.Hour == timeValue.Hour && a.Horario.Minute == timeValue.Minute);
                    break;
            }

            return query;
        }


    }

}


