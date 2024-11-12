﻿using System;
using System.Collections.Generic;

namespace MoveisprimeVS.ORM;

public partial class TbAtendimento
{
    public int Id { get; set; }

    public DateTime DtHoraAgendamento { get; set; }

    public DateOnly DataAtendimento { get; set; }

    public TimeOnly Horario { get; set; }

    public int FkUsuarioId { get; set; }

    public int FkServicoId { get; set; }

    public virtual TbServico FkServico { get; set; } = null!;

    public virtual TbUsuario FkUsuario { get; set; } = null!;
}
