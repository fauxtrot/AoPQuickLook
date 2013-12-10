using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using LoggingModule.Interceptors;

namespace LoggingModule
{
    public class LoggingInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IInterceptor>()
                .ImplementedBy<LoggingInterceptor>().Named("LoggingInterceptor"));
            container.Register(Classes.FromThisAssembly().InNamespace("LoggingModule.Service")
                .WithService
                .DefaultInterfaces()
                .Configure(c => c.LifestylePerThread()));
        }
    }
}
