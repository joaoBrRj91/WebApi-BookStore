
using System.Collections.Generic;
using WebApi.Domain.Entities;

namespace WebApi.Domain.Contracts
{
    public interface ILivroRepository : IRepository<Livro>
    {
        IEnumerable<Livro> ObterLivrosComAutores(int skip = 0, int take = 25);
        Livro ObterLivroComAutores(int livroId);
    }
}
