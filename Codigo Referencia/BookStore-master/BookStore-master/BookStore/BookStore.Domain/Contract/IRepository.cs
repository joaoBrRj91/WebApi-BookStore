using System;
using System.Collections.Generic;

namespace BookStore.Domain.Contract
{
    public interface IRepository<T> : IDisposable
    {
        List<T> GET(int skip=0, int take=25);
        T Get(int id);
        void Create(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
