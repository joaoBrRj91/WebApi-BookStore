
using Newtonsoft.Json.Serialization;
using System.Web.Http;

namespace WebApi.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            //Remove o XML Formatter - Padrão REST é json
            var formatters = GlobalConfiguration.Configuration.Formatters;
            formatters.Remove(formatters.XmlFormatter);

            //Formatar o json para nomenclatura padrão
            var jsonSettings = formatters.JsonFormatter.SerializerSettings;
            jsonSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            jsonSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            //Removendo a referencia circular originada com o include e a desativação do lazy loading
            //Com essa formatação incluimos nos objetos as referencias em relação ao array
            /*Por exemplo : se tivermos um objeto livro que possui muitos autores,mas os autores 
             possuem muitos livros.Caso tenhamos o lazy loading desativado isso irá gerar uma referencia circular
             e um erro na geração do json.Com essa configuração adicionamos somente uma referencia(valor
             da posição do array)  para os objetos intenros que geram referencia circular  e não mais os objetos em si*/
            formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;

            // Web API 2 routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
