using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Common.Logging;

namespace AopQuickLook.Models
{
    public class ExampleTwo
    {
        private IEventAggregator _eventing;

        public ExampleTwo(IEventAggregator eventing)
        {
            _eventing = eventing;
        }

        public virtual void DoSomethingAwesomer()
        {
            _eventing.Publish(new ConsoleMessage
            {
                Message = "Example Two: I'm going to wait now."
            });
           
            throw new Exception("I've messed up!");
        }
    }
}
