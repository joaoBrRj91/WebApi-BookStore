using System.Collections.Generic;

namespace WebApi.Domain
{
    public class Autor
    {
        public int Id { get; set; }
        public string PrimeiroNome { get; set; }
        public string  SobreNome { get; set; }
        public ICollection<Livro> Livros { get; set; }
    }
}
