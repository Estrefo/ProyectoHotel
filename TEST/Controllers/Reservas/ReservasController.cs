using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TEST.Components.Pages;
using TEST.Identity;
using TEST.Modelos;
using TEST.Modelos.DTOs;

namespace TEST.Controllers.Reservas
{
    public class ReservasController : IReservasController
    {
        private readonly IDbContextFactory<MyIdentityDbContext> _contextFactory;
        private readonly IMapper _mapper;
        private readonly UserManager<Usuario> _userManager;

        public ReservasController(
            IDbContextFactory<MyIdentityDbContext> contextFactory,
            IMapper mapper,
            UserManager<Usuario> userManager)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async void CrearReserva(ReservaDTO reservaDto)
        {


            using (var context = _contextFactory.CreateDbContext())
            {
                // Crear la reserva
                var reserva = _mapper.Map<Reserva>(reservaDto);


                context.Reservas.Add(reserva);
                await context.SaveChangesAsync();

            }
        }

        public async Task<List<ReservaDTO>> GetReservas()
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var reservas = await context.Reservas.ToListAsync();
                return _mapper.Map<List<ReservaDTO>>(reservas);

            }
        }

        public async Task EliminarReserva(int id)
        {
            using (var context = _contextFactory.CreateDbContext())
            {

                // Buscar la reserva por el ID
                var reserva = await context.Reservas.FirstOrDefaultAsync(r => r.IdReserva == id);

                // Opcional: Restaurar la habitación como disponible si es necesario
                var habitacion = await context.Habitaciones.FirstOrDefaultAsync(h => h.Numero == reserva.NumeroH);
                if (habitacion != null)
                {
                    habitacion.Disponible = true;
                    context.Habitaciones.Update(habitacion);
                }

                // Eliminar la reserva
                context.Reservas.Remove(reserva);
                await context.SaveChangesAsync();

            }
        }

        // Método para obtener una reserva por su ID
        public async Task<ReservaDTO> GetReservaById(int idReserva)
        {
            using (var context = _contextFactory.CreateDbContext())
            {

                // Buscar la reserva con el ID proporcionado
                var reserva = await context.Reservas.FirstOrDefaultAsync(r => r.IdReserva == idReserva);

                if (reserva == null)
                {
                    // Si no se encuentra la reserva, podemos devolver un error o null, según lo que prefieras
                    return null;
                }

                // Mapear la entidad de la reserva a un DTO
                var reservaDto = new ReservaDTO
                {
                    IdReserva = reserva.IdReserva,
                    FechaEntrada = reserva.FechaEntrada,
                    FechaSalida = reserva.FechaSalida,
                    NumeroH = reserva.NumeroH,
                    Total = reserva.Total,
                    IdUsuario = reserva.IdUsuario,
                    FechaReserva = reserva.FechaReserva
                };

                return reservaDto;

            }


        }

        public async Task ActualizarReserva(ReservaDTO reservaDto)
        {
            using (var context = _contextFactory.CreateDbContext())
            {

                // Buscar la reserva existente
                var reserva = await context.Reservas
                    .FirstOrDefaultAsync(r => r.IdReserva == reservaDto.IdReserva);


                // Actualizar la reserva con los nuevos valores del DTO
                reserva.FechaEntrada = (DateTime)reservaDto.FechaEntrada;
                reserva.FechaSalida = (DateTime)reservaDto.FechaSalida;
                reserva.NumeroH = reservaDto.NumeroH;
                reserva.Total = reservaDto.Total;
                reserva.FechaReserva = (DateTime)reservaDto.FechaReserva;

                // Si es necesario, también podrías actualizar otros campos, como el usuario o el estado de la habitación, por ejemplo.
                // Por ejemplo, si la habitación se asigna a una nueva habitación
                var habitacion = await context.Habitaciones.FirstOrDefaultAsync(h => h.Numero == reservaDto.NumeroH);
                if (habitacion != null)
                {
                    habitacion.Disponible = false;
                    context.Habitaciones.Update(habitacion);
                }

                // Guardar los cambios en la base de datos
                context.Reservas.Update(reserva);
                await context.SaveChangesAsync();


            }
        }


    }
}
