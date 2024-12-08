using Microsoft.AspNetCore.Mvc;
using TEST.Modelos;
using TEST.Modelos.DTOs;

namespace TEST.Controllers.Reservas
{
    public interface IReservasController
    {
        void CrearReserva(ReservaDTO reservaDto);
        Task<List<ReservaDTO>> GetReservas();
        Task EliminarReserva(int id);
        Task<ReservaDTO> GetReservaById(int idReserva);
        Task ActualizarReserva(ReservaDTO reservaDto);
    }
}
