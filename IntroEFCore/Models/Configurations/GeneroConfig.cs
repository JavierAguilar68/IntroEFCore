using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace IntroEFCore.Models.Configurations
{
    public class GeneroConfig : IEntityTypeConfiguration<Genero>
    {
        public void Configure(EntityTypeBuilder<Genero> builder)
        {
            builder.Property(e => e.Nombre).HasMaxLength(150);

            builder.HasIndex(e => e.Nombre).IsUnique();
        }
    }
}
