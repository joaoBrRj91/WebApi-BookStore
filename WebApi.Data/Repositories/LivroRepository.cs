using System.Collections.Generic;
using WebApi.Data.DataContexts;
using WebApi.Domain.Contracts;
using WebApi.Domain.Entities;
using System.Linq;
using System.Data.Entity;

namespace WebApi.Data.Repositories
{
    public class LivroRepository : ILivroRepository
    {
        private readonly BookStoreDataContext _context;


        public LivroRepository(BookStoreDataContext context)
        {
            _context = context;
        } 

        public IEnumerable<Livro> Get(int Skip = 0, int take = 25)
        {
            return _context
                   .Livros
                   .OrderBy(l=>l.Titulo)
                   .Skip(Skip)
                   .Take(take)
                   .AsEnumerable();
        }

        public Livro Get(int id) => _context.Livros.Find(id);


        public IEnumerable<Livro> ObterLivrosComAutores(int skip = 0, int take = 25)
        {
            return _context
                    .Livros
                    .Include(a=>a.Autores)
                    .OrderBy(l => l.Titulo)
                    .Skip(skip)
                    .Take(take)
                    .AsEnumerable();
        }

        public Livro ObterLivroComAutores(int livroId)
        {
            return _context
                    .Livros
                    .Where(l => l.Id.Equals(livroId))
                    .Include(a => a.Autores)
                    .OrderBy(l => l.Titulo)
                    .FirstOrDefault();

        }


        public void Create(Livro entity)
        {
            _context.Livros.Add(entity);
            _context.SaveChanges();
        }

       
        public void Update(Livro entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Livros.Remove(_context.Livros.Find(id));
            _context.SaveChanges();
        }


        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
