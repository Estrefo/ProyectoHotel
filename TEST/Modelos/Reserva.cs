using System;
using System.Collections.Generic;

namespace TEST.Modelos;

public partial class Reserva
{
    public int IdReserva { get; set; }

    public string IdUsuario { get; set; } = null!;

    public int NumeroH { get; set; }

    public DateTime FechaEntrada { get; set; }

    public DateTime FechaSalida { get; set; }

    public DateTime FechaReserva { get; set; }

    public decimal Total { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    public virtual Habitacion NumeroHNavigation { get; set; } = null!;
}
