using Microsoft.AspNetCore.Identity;

namespace TEST.Modelos
{
    public class Usuario : IdentityUser
    {
        public string Nif { get; set; } = null!;

        public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
        public virtual ICollection<ServicioUsuario> ServicioUsuarios { get; set; } = new List<ServicioUsuario>();

    }
}
