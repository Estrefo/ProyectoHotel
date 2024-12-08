using System;
using System.Collections.Generic;

namespace TEST.Modelos;

public partial class Servicio
{
    public int Id_Servicio { get; set; }

    public string Nombre { get; set; } = null!;

    public decimal Precio { get; set; }

    public virtual ICollection<ServicioUsuario> ServicioUsuarios { get; set; } = new List<ServicioUsuario>();
}
