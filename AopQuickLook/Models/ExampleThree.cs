using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Common.CommonAspects;
using Common.Logging;

namespace AopQuickLook.Models
{
    [PropertyInjectionAspect]
    public class ExampleThree
    {
        public IEventAggregator _eventing { get; set; }
        
        [LoggingServiceAspect]
        public void DoSomethingSpooky()
        {
            _eventing.Publish(new ConsoleMessage { Message = "Yes, yes, spooky isn't it..." });
        }
    }
}
