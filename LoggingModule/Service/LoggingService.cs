using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Common.Logging;

namespace LoggingModule.Service
{
    public class LoggingService : ILoggingService
    {
        private IEventAggregator _eventing;

        public LoggingService(IEventAggregator eventing)
        {
            _eventing = eventing;
        }

        public void Log(string message)
        {
            var cm = new ConsoleMessage
            {
                Message = message
            };
            _eventing.Publish(cm);
            //Console.WriteLine(message);
        }

        public void Log(string message, params object[] formatObjects)
        {
            var cm = new ConsoleMessage
            {
                Message = string.Format(message, formatObjects)
            };
            _eventing.Publish(cm);
        }
    }
}
