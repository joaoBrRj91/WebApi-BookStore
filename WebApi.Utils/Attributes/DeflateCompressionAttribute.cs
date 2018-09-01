using BookStore.Utils.Helpers;
using System.Net.Http;
using System.Web.Http.Filters;

/// <summary>
/// Realiza a compressão do "body" da requisição, ou seja, da mensagem propriamente dita
/// para que possa ser trafegado menos dados na rede; diminuindo o cabeçalho da requisição
/// e consequentemente melhorando a performance no transporte de dados.
/// Veja que mencionei PERFOMANCE NO TRANSPORTE DE DADOS e não da aplicação/serviço;pois como estamos
/// interceptando a requisição e realizando mais alguns passos de processamnto para comprimir os dados
/// estamos de forma proporcional diminuindo a performance da aplicação; todavia temos um ganho 
/// no transporte de dados e na quantidade de consumo dos mesmos.
/// É bom utilizar em endpoints que vão retornar muitos dados.
/// </summary>
namespace BookStore.Utils.Attributes
{
    public class DeflateCompressionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actContext)
        {
            var content = actContext.Response.Content;
            var bytes = content == null ? null : content.ReadAsByteArrayAsync().Result;
            var zlibbedContent = bytes == null ? new byte[0] :
            CompressionHelper.DeflateByte(bytes);
            actContext.Response.Content = new ByteArrayContent(zlibbedContent);
            actContext.Response.Content.Headers.Remove("Content-Type");
            actContext.Response.Content.Headers.Add("Content-encoding", "deflate");
            actContext.Response.Content.Headers.Add("Content-Type", "application/json");
            base.OnActionExecuted(actContext);
        }
    }
}
