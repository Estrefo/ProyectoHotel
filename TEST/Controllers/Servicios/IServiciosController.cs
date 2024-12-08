using TEST.Modelos.DTOs;

namespace TEST.Controllers.Servicios
{
    public interface IServiciosController
    {
        Task<List<ServicioDTO>> GetServicios();

        Task<ServicioDTO> GetServicioById(int idServicio);
    }
}
