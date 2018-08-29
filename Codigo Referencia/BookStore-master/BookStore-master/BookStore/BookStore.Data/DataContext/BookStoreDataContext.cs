using BookStore.Data.Mappings;
using BookStore.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Data.DataContext
{
   public class BookStoreDataContext : DbContext
    {
        public BookStoreDataContext() : base("BookStoreConnectionString")
        {
            Configuration.LazyLoadingEnabled = false;   // carregamento preguiçoso 
            Configuration.ProxyCreationEnabled = false; // destivar o proxy  
        }

        public DbSet<Book> _Books { get; set; }
        public DbSet<Author> _Authors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BookMap());
            modelBuilder.Configurations.Add(new AuthorMap());

            base.OnModelCreating(modelBuilder);
        }

    }
}
