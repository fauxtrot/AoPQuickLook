using System;
using Caliburn.Micro;

namespace Common.CommonAspects
{
    public static class EventingProvider
    {
        public static Func<IEventAggregator> EventingFun { get; set; }
    }
}