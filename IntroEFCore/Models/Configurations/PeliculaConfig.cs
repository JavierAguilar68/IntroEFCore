using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace IntroEFCore.Models.Configurations
{
    public class PeliculaConfig : IEntityTypeConfiguration<Pelicula>
    {
        public void Configure(EntityTypeBuilder<Pelicula> builder)
        {
            builder.Property(a => a.Titulo).HasMaxLength(350);
            builder.Property(a => a.FechaEstreno).HasColumnType("date");
        }
    }
}
