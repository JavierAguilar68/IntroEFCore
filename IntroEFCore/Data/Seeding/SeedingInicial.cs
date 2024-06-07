using IntroEFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace IntroEFCore.Data.Seeding
{
    public class SeedingInicial
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            var Samuel = new Actor() { Id = 1005, Nombre = "Samuel L. Jackson", Fortuna = 5700000, FechaNacimiento = new DateTime(1948, 12, 21) };
            var Ruffalo = new Actor() { Id = 1006, Nombre = "Mark Ruffalo", Fortuna = 17000000, FechaNacimiento = new DateTime( 1967 , 11 ,22) };
            var DanielRadcliffe = new Actor() { Id = 1007, Nombre = " Daniel Radcliffe", Fortuna = 94000000, FechaNacimiento = new DateTime( 1989 , 07 ,23) };
            var RupertGrint = new Actor() { Id = 1008, Nombre = " Rupert Grint,", Fortuna = 12400000, FechaNacimiento = new DateTime( 1988 , 08 ,24) };

            modelBuilder.Entity<Actor>().HasData(Samuel, Ruffalo);

            var crimen = new Genero() { Id = 1005, Nombre = "Policiaca" };
            var Guerra = new Genero() { Id = 1006, Nombre = "Guerra" };

            modelBuilder.Entity<Genero>().HasData(crimen, Guerra);

            var spider4 = new Pelicula() { Id = 6, Titulo = "Spider-Man: Across the Spider-Verse (Part One)", FechaEstreno = new DateTime(2022, 10, 7) };
            var potter1 = new Pelicula() { Id = 7, Titulo = "Harry Potter y la piedra filosofal", FechaEstreno = new DateTime(2001, 11, 23) };
            modelBuilder.Entity<Pelicula>().HasData(spider4, potter1);
            
            var comenSpider = new Comentario() { Id = 2, Contenido = "Demasiado loca, no debieron hacerla", Recomendar = true, PeliculaId = 6 };
            var comenHP1 = new Comentario() { Id = 3, Contenido = "El principio de una gran saga", Recomendar = true, PeliculaId = 7 };
            modelBuilder.Entity<Comentario>().HasData(comenHP1, comenSpider);



            // muchos a muchos con salto (Tomar el nombre de la tabla y sus columnas)
            var tablaGeneroPelicula = "GeneroPelicula";
            var generoIdProp = "GenerosId";            
            var peliculaIdProp = "PeliculasId";            

            var ficcion = 1004;
            var fantasia = 1003;
            modelBuilder.Entity(tablaGeneroPelicula).HasData(
                new Dictionary<string, object> { [generoIdProp] = ficcion, [peliculaIdProp] = spider4.Id },
                new Dictionary<string, object> { [generoIdProp] = ficcion, [peliculaIdProp] = potter1.Id },
                new Dictionary<string, object> { [generoIdProp] = fantasia, [peliculaIdProp] = potter1.Id }
                );


            // muchos a muchos sin salto 
            var SamJackPelAct = new PeliculaActor() { ActorId = Samuel.Id, PeliculaId = spider4.Id, Personaje = "Nick Fury", Orden = 4 };
            var RuffaloPelAct = new PeliculaActor() { ActorId = Ruffalo.Id, PeliculaId = spider4.Id, Personaje = "Bruce Banner", Orden = 3 };
            var RadcliffePelAct = new PeliculaActor() { ActorId = DanielRadcliffe.Id, PeliculaId = potter1.Id, Personaje = "Harry Potter", Orden = 1 };
            var GrintPelAct = new PeliculaActor() { ActorId = RupertGrint.Id, PeliculaId = potter1.Id, Personaje = "Ron Weasley", Orden = 2 };

            modelBuilder.Entity<PeliculaActor>().HasData(SamJackPelAct, RuffaloPelAct, RadcliffePelAct, GrintPelAct);

        }
    }
}
