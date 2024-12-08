using Microsoft.AspNetCore.Identity;

namespace TEST.Modelos
{
    public class Rol : IdentityRole
    {
        public string Seccion { get; set; }
        public DateTime FechaAlta { get; set; }
    }
}
