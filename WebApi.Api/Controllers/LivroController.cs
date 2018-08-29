using System.Web.Http;
using WebApi.Domain.Contracts;

namespace WebApi.Api.Controllers
{
    public class LivroController : ApiController
    {
        private ILivroRepository _repository;

        public LivroController(ILivroRepository repository)
        {
            _repository = repository;
        }

        //NÃO É NECESSARIO CHAMAR O DISPOSE NOS ENDPOINTS
        //A PROPRIA CAMADA QUE CHAMA O CONTROLLER VAI CHAMAR  O DISPOSE
        //LOGO APÓS O RECEBIMENTO DOS DADOS DE UM DETERMINADP ENDPOINT
        protected override void Dispose(bool disposing)
        {
            _repository.Dispose();
        }
    }
}
