

using System.Data.Entity.ModelConfiguration;
using WebApi.Domain.Entities;

namespace WebApi.Data.Mappings
{
    public class AutorMap : EntityTypeConfiguration<Autor>
    {

        public AutorMap()
        {
            ToTable("Author");

            HasKey(i => i.Id);

            Property(p => p.PrimeiroNome)
                .HasMaxLength(60)
                .IsRequired();

            Property(p => p.SobreNome)
                .HasMaxLength(60)
                .IsRequired();
        }
    }
}
