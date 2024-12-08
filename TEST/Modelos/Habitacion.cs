using System;
using System.Collections.Generic;

namespace TEST.Modelos;

public partial class Habitacion
{
    public int Numero { get; set; }

    public int Planta { get; set; }

    public decimal Precio { get; set; }

    public bool Disponible { get; set; }

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
