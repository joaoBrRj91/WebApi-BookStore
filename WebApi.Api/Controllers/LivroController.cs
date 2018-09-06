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
using WebApi.Domain.Entities;



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
                    var obterLivrosAsync =  Task.Run(() => _repository.ObterLivrosComAutores());

                    var livros =  await obterLivrosAsync;

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

        [HttpPost]
        [Route("livros")]
        public async Task<HttpResponseMessage> Post(Livro livro)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                var criarLivrosAsync = Task.Run(() =>  _repository.Create(livro));
                response = Request.CreateResponse(HttpStatusCode.Created, livro);
                await criarLivrosAsync;
            }
            catch
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, "fallha ao criar o livro");
                throw;
            }

            return response;
        }



        [HttpPut]
        [Route("livros")]
        public async Task<HttpResponseMessage> Put(Livro livro)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                 var atualizarLivroAsync = Task.Run(() => _repository.Update(livro));
                response = Request.CreateResponse(HttpStatusCode.OK, livro);
                await atualizarLivroAsync;
            }
            catch (Exception)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, "Falha ao Atualizar o livro");
                throw;
            }

            return response;
        }

      

        [HttpDelete]
        [Route("livros/{id}")]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                var  deletarLivroAsync = Task.Run(() => _repository.Delete(id));
                response = Request.CreateResponse(HttpStatusCode.OK, "Livro removido com sucesso");
                await deletarLivroAsync;
            }
            catch (Exception)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, "Fallha ao tentar remover o livro");
                throw;
            }

            return response;
        }

      

        protected override void Dispose(bool disposing)
        {

            _repository.Dispose();
        }
    }
}
