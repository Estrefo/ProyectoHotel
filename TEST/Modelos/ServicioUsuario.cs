namespace TEST.Modelos;

public partial class ServicioUsuario
{
    public int Id { get; set; }

    public int Id_Servicio { get; set; }

    public string Id_Usuario { get; set; } = null!;

    public DateTime? Fecha { get; set; }

    public virtual Servicio IdServicioNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
