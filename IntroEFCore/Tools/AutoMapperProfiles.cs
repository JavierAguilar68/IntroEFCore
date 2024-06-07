using AutoMapper;
using IntroEFCore.Models;
using IntroEFCore.Models.DTO;

namespace IntroEFCore.Tools
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<GeneroCreacionDTO, Genero>().ReverseMap();
            CreateMap<ActorCreacionDTO, Actor>().ReverseMap();
            CreateMap<ActorDTO, Actor>().ReverseMap();

            CreateMap<PeliculaCreacionDTO, Pelicula>()
                .ForMember(ent => ent.Generos, dto => 
                dto.MapFrom(campo => campo.Generos.Select(id => new Genero { Id = id })));

            CreateMap<PeliculaActorCreacionDTO, PeliculaActor>().ReverseMap();
            CreateMap<ComentarioCreacionDTO, Comentario>().ReverseMap();
        }
    }
}
