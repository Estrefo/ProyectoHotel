namespace TEST.Modelos.DTOs
{
    public class ServicioUsuarioDTO
    {

        public int Id { get; set; }

        public int Id_Servicio { get; set; }

        public string Id_Usuario { get; set; } = null!;

        public DateTime? Fecha { get; set; }

    }
}
