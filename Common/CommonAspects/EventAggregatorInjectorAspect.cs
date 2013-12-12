using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Common.Logging;
using PostSharp.Aspects;
using PostSharp.Aspects.Advices;

namespace Common.CommonAspects
{
    [Serializable]
    public class EventAggregatorInjectorAspect : OnMethodBoundaryAspect 
    {
        public override void OnEntry(MethodExecutionArgs args)
        {
            var instance = args.Instance;
            var eventing = instance.GetType()
                .GetProperties()
                .FirstOrDefault(x => x.PropertyType.IsInterface && x.PropertyType == typeof(IEventAggregator));
            if (eventing != null)
            {
                eventing.SetValue(instance, EventingProvider.EventingFun());
            }
        }       
    }
}
