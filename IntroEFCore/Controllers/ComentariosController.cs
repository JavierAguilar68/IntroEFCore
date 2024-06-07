using AutoMapper;
using IntroEFCore.Data;
using IntroEFCore.Models;
using IntroEFCore.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IntroEFCore.Controllers
{
    [Route("api/peliculas/{peliculaId:int}/[controller]")]
    [ApiController]
    public class ComentariosController : ControllerBase
    {
        private readonly ApplicationDBContext context;
        private readonly IMapper _map;

        public ComentariosController(ApplicationDBContext context, IMapper Mapper)
        {
            this.context = context;
            _map = Mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Post(int peliculaId, ComentarioCreacionDTO comentarioCreaDTO)
        {
            var comentario = _map.Map<Comentario>(comentarioCreaDTO);
            comentario.PeliculaId = peliculaId;

            context.Add(comentario);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
