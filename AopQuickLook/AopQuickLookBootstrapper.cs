using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AopQuickLook.Main;
using AopQuickLook.Models;
using Caliburn.Micro;
using Castle.Core;
using Castle.Core.Logging;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Common;
using Common.Logging;
using Core;
using Action = System.Action;

namespace AopQuickLook
{
    public class AopQuickLookBootstrapper : Bootstrapper<Main.MainWindowViewModel>
    {
        private SimpleContainer _container;

        protected override void Configure()
        {
            base.Configure();
            ConfigureMainDependencies();
            ConfigureCoreAndModuleDependencies();
            ConfigureTopmostDependencies();
        }

        private void ConfigureTopmostDependencies()
        {
            MainContainer.Main.Register(Component.For<ExampleOne>());
            MainContainer.Main.Register(Component.For<ExampleTwo>().Interceptors(InterceptorReference.ForKey("LoggingInterceptor")).Anywhere);
            MainContainer.Main.Register(Component.For<MainWindowViewModel>()
                .DependsOn(Dependency.OnComponent<IEventAggregator, EventAggregator>())
                .DependsOn(Dependency.OnComponent<ExampleOne, ExampleOne>())
                .DependsOn(Dependency.OnComponent<ExampleTwo, ExampleTwo>()));
        }

        private void ConfigureCoreAndModuleDependencies()
        {
            Core.Loader.Load();
        }

        private void ConfigureMainDependencies()
        {
            MainContainer.Main.Register(Component.For<IEventAggregator>().ImplementedBy<EventAggregator>());
        }

        protected override object GetInstance(Type service, string key)
        {
            if (!MainContainer.Main.Kernel.HasComponent(service))
            {
                return base.GetInstance(service, key);
            }
            return MainContainer.Main.Resolve(service);
            //return base.GetInstance(service, key);
        }
    }
}
