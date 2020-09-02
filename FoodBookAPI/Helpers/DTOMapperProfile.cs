using AutoMapper;
using FoodBookAPI.V1.Models;
using FoodBookAPI.V1.Models.DTO;

namespace FoodBookAPI.Helpers
{
    public class DTOMapperProfile : Profile
    {
        public DTOMapperProfile()
        {
            CreateMap<UsuarioDTO, ApplicationUser>()
                .ForMember(dest => dest.UserName, orig => orig.MapFrom(src => src.Email))
                .ForMember(dest => dest.FullName, orig => orig.MapFrom(src => src.Nome));

            CreateMap<LoginUsuarioDTO, ApplicationUser>();

            CreateMap<ComentarioDTO, Comentarios>();

            //CreateMap<Mensagem, MensagemDTO>();
            //CreateMap<PaginationList<Palavra>, PaginationList<PalavraDTO>>();
        }
    }
}
