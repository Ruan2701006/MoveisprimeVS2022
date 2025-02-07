using MoveisprimeVS.ORM;
using MoveisprimeVS.Models;
using Microsoft.EntityFrameworkCore;
using Highsoft.Web.Mvc.Charts;
using System.Collections.Generic;

namespace SiteAgendamento.Repositorio
{
    public class DashboardRepositorio
    {
        private BdMoveisPrimeContext _context;


        public DashboardRepositorio(BdMoveisPrimeContext context)
        {
            _context = context;

        }

        public List<LineSeriesData> ObterDadosGrafico()
        {
            return new List<LineSeriesData>
        {
            new LineSeriesData { Y = 10 },
            new LineSeriesData { Y = 25 },
            new LineSeriesData { Y = 35 },
            new LineSeriesData { Y = 50 }
        };
        }

    }
}
