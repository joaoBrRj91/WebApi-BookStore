using BookStore.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Data.Mappings
{
    public class BookMap : EntityTypeConfiguration<Book>
    {
        public BookMap()
        {
            ToTable("Book");

            HasKey(X => X.id);

            Property(X => X.Title).HasMaxLength(255).IsRequired();
            Property(X => X.Price).IsRequired().HasColumnType("Money");
            Property(X => X.RelaseDate).IsRequired();

            HasMany(x => x.Authors).WithMany(x => x.Books);
        }
    }
}
