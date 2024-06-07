using AutoMapper;
using AutoMapper.Execution;
using AutoMapper.QueryableExtensions;
using IntroEFCore.Data;
using IntroEFCore.Models;
using IntroEFCore.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace IntroEFCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActoresController : ControllerBase
    {
        private readonly ApplicationDBContext context;
        public readonly IMapper _map;

        public ActoresController(ApplicationDBContext context, IMapper mapper)
        {
            this.context = context;
            _map = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Post(ActorCreacionDTO actorCreaDTO)
        {
            var actor = _map.Map<Actor>(actorCreaDTO);
            context.Add(actor);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Actor>>> Get()
        {
            return await context.Actores.ToListAsync();
        }

        [HttpGet("nombre")]
        public async Task<ActionResult<IEnumerable<Actor>>> Get(string nombre)
        {
            // version 1
            return await context.Actores.Where(a => a.Nombre == nombre).ToListAsync();
        }

        [HttpGet("nombre/V2")]
        public async Task<ActionResult<IEnumerable<Actor>>> GetV2(string nombre)
        {
            // version 2
            return await context.Actores.Where(a => a.Nombre.Contains(nombre)).ToListAsync();
        }

        [HttpGet("RangoNacimiento")]
        public async Task<ActionResult<IEnumerable<Actor>>> GetRangeNac(DateTime fecInit, DateTime fecFin)
        {
            // version 2
            return await context.Actores.Where(a => a.FechaNacimiento >= fecInit && a.FechaNacimiento <= fecFin).ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Actor>> GetById(int id)
        {
            var actor = await context.Actores.FirstOrDefaultAsync(a => a.Id == id);
            if (actor == null) { return NotFound(); }
            else { return actor; }
        }

        [HttpGet("OrdenNombre")]
        public async Task<ActionResult<IEnumerable<Actor>>> GetAllByNac(bool inverso)
        {
            if (inverso)
                return await context.Actores.OrderBy(a => a.Nombre).ToListAsync();
            else
                return await context.Actores.OrderByDescending(a => a.Nombre).ToListAsync();
        }

        [HttpGet("ordenMultiple")]
        public async Task<ActionResult<IEnumerable<Actor>>> GetOrdenMultiple(string nombre)
        {
            return await context.Actores.
                    Where(a => a.Nombre.Contains(nombre))
                    .OrderBy(a => a.Nombre)
                        .ThenBy(a => a.FechaNacimiento)
                    .ToListAsync();

        }

        [HttpGet("IdNombre_anonimo")]
        public async Task<ActionResult> getIdNombre()
        {
            var actores = await context.Actores.Select(a => new { a.Id, a.Nombre }).ToListAsync();
            return Ok(actores);
        }

        [HttpGet("IdNombre_conDTO")]
        public async Task<ActionResult<IEnumerable<ActorDTO>>> getIdNombreDTO()
        {
            return await context.Actores.ProjectTo<ActorDTO>(_map.ConfigurationProvider).ToListAsync();  
        }
    }
}
