using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IntroEFCore.Models.Configurations
{
    public class PeliculaActorConf : IEntityTypeConfiguration<PeliculaActor>
    {
        public void Configure(EntityTypeBuilder<PeliculaActor> builder)
        {
            builder.HasKey(pa => new { pa.ActorId, pa.PeliculaId });
            builder.Property(pa => pa.Personaje).HasMaxLength(150);
        }

    }
}
