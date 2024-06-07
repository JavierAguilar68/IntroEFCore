using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace IntroEFCore.Models.Configurations
{
    public class ActorConfig : IEntityTypeConfiguration<Actor>
    {
        public void Configure(EntityTypeBuilder<Actor> builder)
        {
            builder.Property(a => a.Nombre).HasMaxLength(200);
            builder.Property(a => a.FechaNacimiento).HasColumnType("date");
            builder.Property(a => a.Fortuna).HasPrecision(18, 2);
        }
    }
}
