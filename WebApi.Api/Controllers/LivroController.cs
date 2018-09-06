using System.Net.Http;
using System.Web.Http;
using WebApi.Domain.Contracts;
using System.Linq;
using System.Net;
using System.Text;
using System;
using BookStore.Utils.Attributes;
using WebApi.OutputCache.V2;
using System.Threading.Tasks;



/*TODO: Refatorar,pois a camada de apresentação
        só conhece a camada de aplicação. O unico elemento injetado
        no controller será o service;o repository será injetado no construtor
        da aplicação
        
     
         NÃO É NECESSARIO CHAMAR O DISPOSE NOS ENDPOINTS
         A PROPRIA CAMADA QUE CHAMA O CONTROLLER VAI CHAMAR  O DISPOSE
         LOGO APÓS O RECEBIMENTO DOS DADOS DE UM DETERMINADP ENDPOINT
        
     
     */
namespace WebApi.Api.Controllers
{

    /// <summary>
    /// api significa que só será retornado dados
    /// public significa que a api pode ser acessada fora da intranet;logo
    /// deve haver uma autenticação para consumir
    /// v1 versão da api
    /// public espera receber uma autenticação
    /// </summary>
    [RoutePrefix("api/public/v1")]
    public class LivroController : ApiController
    {

        private ILivroRepository _repository;


        public LivroController(ILivroRepository repository)
        {
            _repository = repository;
        }

       
        //O cache no lado do cliente fica no disco( "disco" da maquina fisica da requsição)
        //O cache no lado do servidor fica armazenada no servidor/maquina fisca onde está a aplicação
        [Route("livros")]
        [HttpGet]
        [DeflateCompression]
        [CacheOutput(ClientTimeSpan =100,ServerTimeSpan =100)]
        public async Task<HttpResponseMessage> Get()
        {
            HttpResponseMessage response;

            using (response = new HttpResponseMessage())
            {
                try
                {
                    var livrosAsync =  Task.Run(() => _repository.ObterLivrosComAutores());

                    var livros =  await livrosAsync;

                    if (livros != null &&  livros.Count() > 0)
                        response = Request.CreateResponse(HttpStatusCode.OK, livros);
                    else
                        response = Request.CreateResponse(HttpStatusCode.NoContent, "nenhum registro encontrado");

                }
                catch (Exception ex)
                {
                    StringBuilder mensagem = new StringBuilder();
                    mensagem.AppendLine("Ocorreu algum erro no acesso ao serviço");
                    mensagem.AppendLine("Erro:");
                    mensagem.AppendLine(ex.Message);
                    response = Request.CreateResponse(HttpStatusCode.InternalServerError, mensagem.ToString());
                }
            }

            return response;
        }

        protected override void Dispose(bool disposing)
        {

            _repository.Dispose();
        }
    }
}
