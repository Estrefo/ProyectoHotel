using AutoMapper;
using TEST.Modelos;
using TEST.Modelos.DTOs;

namespace TEST.Automapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterDTO, Usuario>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))  // Asumiendo que UserName es el mismo que Email
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Nif, opt => opt.MapFrom(src => src.Nif));

            CreateMap<ReservaDTO, Reserva>().ReverseMap();
            CreateMap<HabitacionDTO, Habitacion>().ReverseMap();
            CreateMap<ServicioDTO, Servicio>().ReverseMap();
            CreateMap<ServicioUsuarioDTO, ServicioUsuario>().ReverseMap();
        }
    }
}
