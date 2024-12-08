using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TEST.Identity;
using TEST.Modelos.DTOs;
using TEST.Modelos;
using Microsoft.EntityFrameworkCore;

namespace TEST.Controllers.ServiciosUsuarios
{
    public class ServiciosUsuariosController : IServiciosUsuariosController
    {
        private readonly IDbContextFactory<MyIdentityDbContext> _contextFactory;
        private readonly IMapper _mapper;
        private readonly UserManager<Usuario> _userManager;


        public ServiciosUsuariosController(IDbContextFactory<MyIdentityDbContext> contextFactory, IMapper mapper, UserManager<Usuario> userManager)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task  CrearServicioUsuario(ServicioUsuarioDTO servicioUsuarioDto)
        {


            using (var context = _contextFactory.CreateDbContext())
            {
                // Crear el servicio
                var servicioUsuario = _mapper.Map<ServicioUsuario>(servicioUsuarioDto);



                context.ServicioUsuarios.Add(servicioUsuario);
                await context.SaveChangesAsync();

            }


        }

        public async Task<List<ServicioUsuarioDTO>> GetServiciosUsuarios()
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var serviciosUsuarios = await context.ServicioUsuarios.ToListAsync();
                return _mapper.Map<List<ServicioUsuarioDTO>>(serviciosUsuarios);

            }
        }

        public async Task EliminarServicioUsuario(int id)
        {
            using (var context = _contextFactory.CreateDbContext())
            {

                // Buscar la reserva por el ID
                var servicioUsuario = await context.ServicioUsuarios.FirstOrDefaultAsync(r => r.Id == id);

                // Eliminar la reserva
                context.ServicioUsuarios.Remove(servicioUsuario);
                await context.SaveChangesAsync();

            }
        }

        public async Task<ServicioUsuarioDTO> GetServicioUsuarioById(int id)
        {
            using (var context = _contextFactory.CreateDbContext())
            {

                // Buscar la reserva con el ID proporcionado
                var servicioUsuario = await context.ServicioUsuarios.FirstOrDefaultAsync(r => r.Id == id);

                if (servicioUsuario == null)
                {
                    // Si no se encuentra la reserva, podemos devolver un error o null, según lo que prefieras
                    return null;
                }

                // Mapear la entidad de la reserva a un DTO
                var servicioUsuarioDto = new ServicioUsuarioDTO
                {
                    Id = servicioUsuario.Id,
                    Id_Servicio = servicioUsuario.Id_Servicio,
                    Id_Usuario = servicioUsuario.Id_Usuario,
                    Fecha = servicioUsuario.Fecha
                };

                return servicioUsuarioDto;

            }


        }

        public async Task<bool> ActualizarServicioUsuario(ServicioUsuarioDTO servicioUsuarioDto)
        {
            using (var context = _contextFactory.CreateDbContext())
            {

                // Buscar la reserva existente
                var ServicioUsuario = await context.ServicioUsuarios
                    .FirstOrDefaultAsync(r => r.Id == servicioUsuarioDto.Id);

                // Actualizar la reserva con los nuevos valores del DTO
                ServicioUsuario.Id_Usuario = servicioUsuarioDto.Id_Usuario;
                ServicioUsuario.Id_Servicio = servicioUsuarioDto.Id_Servicio;
                ServicioUsuario.Fecha = servicioUsuarioDto.Fecha;

                // Guardar los cambios en la base de datos
                context.ServicioUsuarios.Update(ServicioUsuario);
                await context.SaveChangesAsync();

                return true;

            }
        }


    }
}
