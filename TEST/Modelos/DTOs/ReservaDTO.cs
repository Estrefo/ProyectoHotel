namespace TEST.Modelos.DTOs
{
    public class ReservaDTO
    {
        public int IdReserva { get; set; }

        public string IdUsuario { get; set; } = null!;

        public int NumeroH { get; set; }

        public DateTime? FechaEntrada { get; set; }

        public DateTime? FechaSalida { get; set; }

        public DateTime? FechaReserva { get; set; }

        public decimal Total { get; set; }

    }
}
