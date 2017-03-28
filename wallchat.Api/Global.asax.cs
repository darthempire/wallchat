using System;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using Castle.Windsor;
using wallchat.Api.App.CastleDI;

namespace wallchat.Api
{
    public class WebApiApplication : HttpApplication
    {
        private readonly IWindsorContainer _container;

        public WebApiApplication()
        {
            _container =
                new WindsorContainer ( ).Install (new DependencyInstaller ( ));
        }

        public override void Dispose ()
        {
            _container.Dispose ( );
            base.Dispose ( );
        }

        private void Application_Start ( object sender, EventArgs e )
        {
            // Code that runs on application startup
            //AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure (WebApiConfig.Register);
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
            GlobalConfiguration.Configuration.Services.Replace (
                typeof ( IHttpControllerActivator ),
                new WindsorActivator (_container));
        }
    }
}