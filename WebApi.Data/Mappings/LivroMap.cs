using System.Data.Entity.ModelConfiguration;
using WebApi.Domain.Entities;

namespace WebApi.Data.Mappings
{
    public class LivroMap : EntityTypeConfiguration<Livro>
    {

        public LivroMap()
        {
            ToTable("livros");

            HasKey(i => i.Id);

            Property(p=> p.Titulo)
                .HasMaxLength(255)
                .IsRequired();

            Property(p => p.Preco)
                .IsRequired()
                .HasColumnType("decimal");

            Property(p => p.DataLancamento)
                .IsRequired();

            HasMany(x => x.Autores)
                .WithMany(x => x.Livros);
        }
    }
}
