using System;
using System.Collections.Generic;

namespace WebApi.Domain.Entities
{
    public class Livro
    {
        public int Id { get; set; }
        public string Titulo{ get; set; }
        public decimal Preco { get; set; }
        public DateTime DataLancamento { get; set; }
        public ICollection<Autor> Autores { get; set; }
    }
}
