using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TEST.Identity;
using TEST.Modelos;
using TEST.Modelos.DTOs;

namespace TEST.Controllers.Habitaciones
{
    public class HabitacionesController : IHabitacionesController
    {

        private readonly IDbContextFactory<MyIdentityDbContext> _contextFactory;
        private readonly IMapper _mapper;
        private readonly UserManager<Usuario> _userManager;


        public HabitacionesController(IDbContextFactory<MyIdentityDbContext> contextFactory, IMapper mapper, UserManager<Usuario> userManager)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<List<HabitacionDTO>> GetHabitaciones()
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                List<Modelos.Habitacion> habitaciones = await context.Habitaciones.ToListAsync();
                return _mapper.Map<List<HabitacionDTO>>(habitaciones);
            }
        }

        ////PRUEBA
        public async Task<List<HabitacionDTO>> GetHabitacionesDisponibles(DateTime? fechaEntrada, DateTime? fechaSalida)
        {

            // Verifica que las fechas no sean nulas
            if (!fechaEntrada.HasValue || !fechaSalida.HasValue)
            {
                return new List<HabitacionDTO>(); // Devolver una lista vacía si las fechas no son válidas
            }

            using (var context = _contextFactory.CreateDbContext())
            {
                // Buscar las habitaciones cuya fecha de reserva no se cruce con el rango solicitado
                var habitacionesDisponibles = await context.Habitaciones
                    .Where(h => !context.Reservas
                        .Any(r => r.NumeroH == h.Numero &&
                                  ((r.FechaEntrada <= fechaSalida && r.FechaSalida >= fechaEntrada) ||
                                   (r.FechaEntrada >= fechaEntrada && r.FechaEntrada <= fechaSalida) ||
                                   (r.FechaSalida >= fechaEntrada && r.FechaSalida <= fechaSalida)))
                    )
                    .ToListAsync();

                return _mapper.Map<List<HabitacionDTO>>(habitacionesDisponibles);
            }
        }

        public async Task<HabitacionDTO> GetHabitacionById(int id)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                Habitacion habitacion = await context.Habitaciones.FirstOrDefaultAsync(p => p.Numero == id);
                HabitacionDTO? habitacionDTO = _mapper.Map<HabitacionDTO>(habitacion);
                return habitacionDTO;
            }
        }

        public async void EditarHabitacion(HabitacionDTO habitacion)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                Habitacion habitacionTemp = _mapper.Map<Habitacion>(habitacion);

                context.Update(habitacionTemp);
                await context.SaveChangesAsync();
            }
        }
    }
}
