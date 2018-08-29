using System.Collections.Generic;

namespace WebApi.Domain.Entities
{
    public class Autor
    {
        #region Propriedades públicas
        public int Id { get; set; }
        public string PrimeiroNome { get; set; }
        public string  SobreNome { get; set; }
        public  ICollection<Livro> Livros { get; set; }
        #endregion


        private int _quantidadeLivrosAutor;


        //TODO: Validar com o service é usar um container DI para injetar a interface do service
        public void ValidarNomeDoAutor()
        {

        }

        public void ObterTotalDeLivrosAutor()
        {

        }
    }
}
