using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TEST.Identity;
using TEST.Modelos.DTOs;
using TEST.Modelos;
using Microsoft.EntityFrameworkCore;

namespace TEST.Controllers.Servicios
{
    public class ServiciosController : IServiciosController
    {

        private readonly IDbContextFactory<MyIdentityDbContext> _contextFactory;
        private readonly IMapper _mapper;
        private readonly UserManager<Usuario> _userManager;


        public ServiciosController(IDbContextFactory<MyIdentityDbContext> contextFactory, IMapper mapper, UserManager<Usuario> userManager)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<List<ServicioDTO>> GetServicios()
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                List<Servicio> servicios = await context.Servicios.ToListAsync();

                return _mapper.Map<List<ServicioDTO>>(servicios);
            }

        }

        public async Task<ServicioDTO> GetServicioById(int idServicio)
        {
            using (var context = _contextFactory.CreateDbContext())
            {

                // Buscar la reserva con el ID proporcionado
                var servicio = await context.Servicios.FirstOrDefaultAsync(r => r.Id_Servicio == idServicio);

                if (servicio == null)
                {
                    // Si no se encuentra la reserva, podemos devolver un error o null, según lo que prefieras
                    return null;
                }

                // Mapear la entidad de la reserva a un DTO
                var servicioDto = new ServicioDTO
                {
                    Id_Servicio = servicio.Id_Servicio,
                    Nombre = servicio.Nombre,
                    Precio = servicio.Precio
                };

                return servicioDto;

            }


        }

    }
}
