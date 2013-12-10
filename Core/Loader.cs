using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Castle.Core.Internal;
using Castle.MicroKernel.Registration;
using Castle.Windsor.Installer;
using Common.Logging;
using LoggingModule;
using LoggingModule.Service;

namespace Core
{
    public class Loader
    {
        public static IEventAggregator eventing;

        public static void Load()
        {
            MainContainer.Main.Install(new LoggingInstaller());
            if (eventing == null)
            {
                eventing = MainContainer.Main.Resolve<IEventAggregator>();
            }
            //Aspect Management
            EventingProvider.EventingFun = () => eventing;
        }
    }
}
