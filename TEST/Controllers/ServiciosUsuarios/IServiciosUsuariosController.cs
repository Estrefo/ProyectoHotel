using TEST.Modelos.DTOs;

namespace TEST.Controllers.ServiciosUsuarios
{
    public interface IServiciosUsuariosController
    {
        Task CrearServicioUsuario(ServicioUsuarioDTO servicioUsuarioDto);
        Task<List<ServicioUsuarioDTO>> GetServiciosUsuarios();
        Task EliminarServicioUsuario(int id);
        Task<ServicioUsuarioDTO> GetServicioUsuarioById(int id);
        Task<bool> ActualizarServicioUsuario(ServicioUsuarioDTO servicioUsuarioDto);


    }
}
