using AutoMapper;
using FoodBookAPI.V1.Models;
using FoodBookAPI.V1.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodBookAPI.Helpers
{
    public class DTOMapperProfile : Profile
    {
        public DTOMapperProfile()
        {
            CreateMap<ApplicationUser, UsuarioDTO>()
                .ForMember(dest => dest.Nome, orig => orig.MapFrom(src => src.FullName));

            //CreateMap<Mensagem, MensagemDTO>();
            //CreateMap<PaginationList<Palavra>, PaginationList<PalavraDTO>>();
        }
    }
}
