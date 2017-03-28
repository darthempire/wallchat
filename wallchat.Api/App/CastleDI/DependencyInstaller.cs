using System.Web.Http.Controllers;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using wallchat.DAL.App.Contracts;
using wallchat.DAL.App.Implementations;

namespace wallchat.Api.App.CastleDI
{
    public class DependencyInstaller : IWindsorInstaller
    {
        public void Install ( IWindsorContainer container, IConfigurationStore store )
        {
            container.Register (
                //Component.For<ILogService>()
                //    .ImplementedBy<LogService>()
                //    .LifeStyle.PerWebRequest,

                Component.For<IDatabaseFactory> ( )
                    .ImplementedBy<DatabaseFactory> ( )
                    .LifeStyle.PerWebRequest,
                Component.For<IUnitOfWork> ( )
                    .ImplementedBy<UnitOfWork> ( )
                    .LifeStyle.PerWebRequest,
                AllTypes.FromThisAssembly ( ).BasedOn<IHttpController> ( ).LifestyleTransient ( ),
                AllTypes.FromAssemblyNamed ("wallchat.Service")
                    .Where (type => type.Name.EndsWith ("Service")).
                    WithServiceAllInterfaces ( ).
                    LifestylePerWebRequest ( ),
                AllTypes.FromAssemblyNamed ("wallchat.Repository")
                    .Where (type => type.Name.EndsWith ("Repository")).
                    WithServiceAllInterfaces ( ).
                    LifestylePerWebRequest ( )
            );
        }
    }
}