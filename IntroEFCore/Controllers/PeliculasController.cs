using AutoMapper;
using IntroEFCore.Data;
using IntroEFCore.Models;
using IntroEFCore.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace IntroEFCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeliculasController : ControllerBase
    {
        private readonly ApplicationDBContext context;
        private readonly IMapper _map;

        public PeliculasController(ApplicationDBContext context, IMapper Mapper)
        {
            this.context = context;
            _map = Mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Post(PeliculaCreacionDTO peliculaCreaDTO)
        {
            var pelicula = _map.Map<Pelicula>(peliculaCreaDTO);

            if (pelicula.Generos is not null)
            {
                foreach (var genero in pelicula.Generos)
                {
                    context.Entry(genero).State = EntityState.Unchanged;
                }
            }

            if (pelicula.PeliculaActores is not null)
            {
                for (int i = 0; i < pelicula.PeliculaActores.Count; i++)
                {
                    pelicula.PeliculaActores[i].Orden = i + 1;
                }
            }
            context.Add(pelicula);
            await context.SaveChangesAsync();
            return Ok();
        }


        // end point con Eager Loading que me permite incluir tablas relacionadas
        [HttpGet]
        public async Task<ActionResult<Pelicula>> Get(int Id)
        {
            // eliminar las referencias circulares en program agregar a builder.Services.AddControllers()
            // .AddJsonOptions(opt => opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            var pelicula = await context.Peliculas
                .Include(p =>p.Generos)
                .Include(p => p.PeliculaActores.OrderBy(pa => pa.Orden ))
                    .ThenInclude(pa => pa.Actor)
                .Include(p => p.Comentarios).FirstOrDefaultAsync(p => p.Id == Id);

            if (pelicula == null) { return NotFound(); }
            else { return pelicula;  } 
        }

        [HttpGet("select/{id:int}")]
        public async Task<ActionResult> GetSelect(int id)
        {
            var pelicula = await context.Peliculas
                .Select(pel => new
                {
                    pel.Id, pel.Titulo, 
                    Generos = pel.Generos.Select(g => g.Nombre).ToList(),
                    Actores = pel.PeliculaActores.OrderBy(pa =>pa.Orden).Select(pa => new
                    {
                        Id = pa.ActorId, 
                        pa.Actor.Nombre,
                        pa.Personaje
                    }),
                    CantidadComentarios = pel.Comentarios.Count()
                })
                .FirstOrDefaultAsync(p => p.Id ==  id);

            if (pelicula == null) { return NotFound(); }
            else { return Ok(pelicula); }
            
        }


        // borrar entidad  en cascada
        [HttpDelete("int:id/moderna")]
        public async Task<ActionResult> DeleteNew(int id)
        {
            var filasBorradas = await context.Peliculas
                .Where(g => g.Id == id).ExecuteDeleteAsync();
            if (filasBorradas == 0) { return NotFound(); }
            else { return NoContent(); }
        }
    }
}
