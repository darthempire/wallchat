using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace wallchat.Api
{
    public static class WebApiConfig
    {
        public static void Register ( HttpConfiguration config )
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes( );

            config.Routes.MapHttpRoute(
                "ActionRoute",
                "api/{controller}/{action}/{id}",
                new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute (
                "DefaultApi",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional }
            );

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>( ).First( );
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver( );

            config.Formatters.JsonFormatter.SupportedMediaTypes
                .Add (new MediaTypeHeaderValue ("text/html"));
        }
    }
}