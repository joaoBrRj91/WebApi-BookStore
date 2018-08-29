using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebApi.Data.DataContexts;
using WebApi.Domain.Contracts;
using WebApi.Domain.Entities;

namespace WebApi.Data.Repositories
{
    public class AutorRepository : IAutorRepository
    {

        private readonly BookStoreDataContext _context;

        public AutorRepository()
        {
            _context = new BookStoreDataContext();
        }

        public IEnumerable<Autor> Get(int Skip = 0, int take = 25)
        {
            return _context.Autores.OrderBy(x => x.PrimeiroNome).Skip(Skip).Take(take).ToList();
        }

        public Autor Get(int id)
        {
            return _context.Autores.Find(id);
        }



        public void Create(Autor entity)
        {
            _context.Autores.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Autor entity)
        {
            _context.Entry<Autor>(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Autores.Remove(_context.Autores.Find(id));
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }


       
    }
}
