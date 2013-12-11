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
                Message = "Example Two: Just doing what I do."
            });
           
        }

        public virtual void DoSomethingWrong()
        {
            _eventing.Publish(new ConsoleMessage
            {
                Message = "Example Two Doesn't feel so good."
            });

            throw new Exception("I've messed up!");
        }
    }
}
