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

        [CachingAspect(TimeSpanFromSeconds = 10)]
        public string SimulateLongLoad()
        {
            System.Threading.Thread.Sleep(3000);
            return "This took 3 seconds if it's not cached.";
        }
    }
}
