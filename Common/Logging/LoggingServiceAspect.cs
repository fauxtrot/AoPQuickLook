using System;
using Caliburn.Micro;
using PostSharp.Aspects;

namespace Common.Logging
{
    [Serializable]
    public class LoggingServiceAspect : PostSharp.Aspects.OnMethodBoundaryAspect
    {
        public override void OnEntry(MethodExecutionArgs args)
        {
            EventingProvider.EventingFun().Publish(new ConsoleMessage
            {
                Message = string.Format("Hello From Postsharp on Entry of {0}", args.Method.Name)
            });
        }

        public override void OnSuccess(MethodExecutionArgs args)
        {
            EventingProvider.EventingFun().Publish(new ConsoleMessage
            {
                Message = string.Format("Hello From Postsharp on Success of {0}", args.Method.Name)
            });
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            EventingProvider.EventingFun().Publish(new ConsoleMessage
            {
                Message = string.Format("Hello From Postsharp on Exit of {0}", args.Method.Name)
            });
        }

        public override void OnException(MethodExecutionArgs args)
        {
            EventingProvider.EventingFun().Publish(new ConsoleMessage
            {
                Message = string.Format("On noes! From Postsharp on Exception of {0}", args.Method.Name)
            });
        }
    }

    public static class EventingProvider
    {
        public static Func<IEventAggregator> EventingFun { get; set; }
    }
}
