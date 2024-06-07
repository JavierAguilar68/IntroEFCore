using AutoMapper;
using IntroEFCore.Data;
using IntroEFCore.Models;
using IntroEFCore.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IntroEFCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenerosController : ControllerBase
    {
        private readonly ApplicationDBContext context;
        public readonly IMapper _mapper;

        public GenerosController(ApplicationDBContext context, IMapper mapper)
        {
            this.context = context;
            _mapper = mapper;
        }        

        [HttpPost]
        public async Task<ActionResult> Post(GeneroCreacionDTO generoCreacion)
        {
            var yaExsiste = await context.Generos.AnyAsync(g => g.Nombre == generoCreacion.Nombre);

            if (yaExsiste) { return BadRequest("Ya existe un genero con el nombre " + generoCreacion.Nombre); }
            var genero = _mapper.Map<Genero>(generoCreacion);
            context.Add(genero);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("varios")]
        public async Task<ActionResult> Post(GeneroCreacionDTO[] generosCreacionDTO)
        {
            var generos = _mapper.Map<Genero[]>(generosCreacionDTO);
            context.AddRange(generos);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genero>>> Get()
        {
            return await context.Generos.ToListAsync();
        }

        //actualizar mediante modelo conectado
        [HttpPut("{id:int}/nombre_conectado")]
        public async Task<ActionResult> Put(int id, string nuevoNombre)
        {
            var genero = await context.Generos.FirstOrDefaultAsync(g => g.Id == id);
            if (genero == null) { return NotFound(); }

            genero.Nombre = nuevoNombre;
            await context.SaveChangesAsync();
            return Ok();
        }


        //actualizar mediante modelo desconectado
        [HttpPut("{id:int}/nombre_desconectado")]
        public async Task<ActionResult> PutDesco(int id, GeneroCreacionDTO generoDto)
        {
            var genero = _mapper.Map<Genero>(generoDto);
            genero.Id = id;
            context.Update(genero);
            await context.SaveChangesAsync();
            return Ok();
        }


        // usa esta forma para borrar
        [HttpDelete("int:id/moderna")]
        public async Task<ActionResult> DeleteNew(int id)
        {
            var filasBorradas = await context.Generos.Where(g => g.Id == id).ExecuteDeleteAsync();
            if (filasBorradas == 0) { return NotFound(); }
            else { return NoContent(); }
        }

        // forma anterior para borrar
        [HttpDelete("{id:int}/anterior")]
        public async Task<ActionResult> DeleteOld(int id)
        {
            var genero = await context.Generos.FirstOrDefaultAsync(g => g.Id==id);
            if (genero == null) { return NotFound(); }

            context.Remove(genero);
            await context.SaveChangesAsync();
            return NoContent();

        }
    }
}
