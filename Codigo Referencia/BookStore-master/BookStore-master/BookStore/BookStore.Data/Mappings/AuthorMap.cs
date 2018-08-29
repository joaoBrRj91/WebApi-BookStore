using BookStore.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Data.Mappings
{
    public class AuthorMap : EntityTypeConfiguration<Author>
    {
        public AuthorMap()
        {
            ToTable("Author");

            HasKey(x => x.id);

            Property(X => X.FirstName).HasMaxLength(60).IsRequired();
            Property(X => X.LastName).HasMaxLength(60).IsRequired();

        }
    }
}
