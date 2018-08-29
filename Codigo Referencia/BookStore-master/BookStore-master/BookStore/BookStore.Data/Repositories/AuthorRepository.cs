using BookStore.Domain.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Domain;
using BookStore.Data.DataContext;
using System.Data.Entity;

namespace BookStore.Data.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private BookStoreDataContext _db;

        public AuthorRepository()
        {
            _db = new BookStoreDataContext();
        }


        public void Create(Author entity)
        {
            _db._Authors.Add(entity);
            _db.SaveChanges();   
        }

        public void Delete(int id)
        {
            _db._Authors.Remove(_db._Authors.Find(id));
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public Author Get(int id)
        {
            return _db._Authors.Find(id);
        }

        public List<Author> GET(int skip = 0, int take = 25)
        {
            return _db._Authors.OrderBy(x => x.FirstName).Skip(skip).Take(take).ToList();
        }

        public void Update(Author entity)
        {
            _db.Entry<Author>(entity).State = EntityState.Modified;
            _db.SaveChanges();
        }
    }
}
