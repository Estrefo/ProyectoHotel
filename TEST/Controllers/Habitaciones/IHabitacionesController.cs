using TEST.Modelos.DTOs;

namespace TEST.Controllers.Habitaciones
{
    public interface IHabitacionesController
    {
        Task<List<HabitacionDTO>> GetHabitaciones();
        Task<HabitacionDTO> GetHabitacionById(int id);
        void EditarHabitacion(HabitacionDTO habitacion);
        Task<List<HabitacionDTO>> GetHabitacionesDisponibles(DateTime? fechaEntrada, DateTime? fechaSalida);

    }
}
