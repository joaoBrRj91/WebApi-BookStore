using System.Data.Entity;
using WebApi.Data.Mappings;
using WebApi.Domain.Entities;

namespace WebApi.Data.DataContexts
{
    public class BookStoreDataContext : DbContext
    {
        public BookStoreDataContext() : base("LivrariaDbContext")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Livro> Livros { get; set; }
        public DbSet<Autor> Autores { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new LivroMap());
            modelBuilder.Configurations.Add(new AutorMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
