using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using PostSharp.Aspects;

namespace Common.CommonAspects
{
    [Serializable]
    public class CachingAspect : OnMethodBoundaryAspect
    {
        private string _methodName;

        private static ConcurrentDictionary<string, CacheObject> backingStore;

        public int TimeSpanFromSeconds { get; set; }

        static CachingAspect()
        {
            backingStore = new ConcurrentDictionary<string, CacheObject>();
        }

        public CachingAspect()
        {
            TimeSpanFromSeconds = 30;
        }

        public override void CompileTimeInitialize(MethodBase method, AspectInfo aspectInfo)
        {
            this._methodName = method.DeclaringType.FullName + "." + method.Name;
        }

        private string GetCacheKey(object instance, Arguments args)
        {
            if (instance == null && args.Count == 0)
            {
                return this._methodName;
            }

            StringBuilder sb = new StringBuilder();

            if (instance != null)
            {
                sb.AppendFormat("{0}; ", instance);
            }

            foreach (var argument in args)
            {
                sb.Append(argument ?? "null");
                sb.Append(", ");
            }

            return sb.ToString();
        }

        public override void OnEntry(MethodExecutionArgs args)
        {
            var key = GetCacheKey(args.Instance, args.Arguments);
            CultivateCache(key);
            //check to see if the key exists, 
            if (!backingStore.ContainsKey(key))
            {
                args.MethodExecutionTag = key;
            }
            else
            {
                args.ReturnValue = backingStore[key].Value;
                args.FlowBehavior = FlowBehavior.Return;
            }
            //if it does return it from the cache, 
            //otherwise proceed and get the result cache it for later.
        }

        private void CultivateCache(string key)
        {
            if (backingStore.ContainsKey(key))
            {
                var cacheObj = backingStore[key];
                if (cacheObj.Created - DateTime.Now > cacheObj.Lifetime)
                {
                    CacheObject val;
                    //cull
                    backingStore.TryRemove(key, out val);
                    val = null;
                }
            }
        }

        private CacheObject CreateCacheObject(object o)
        {
            return new CacheObject()
            {
                Value = o,
                Created = DateTime.Now,
                Lifetime = TimeSpan.FromSeconds(TimeSpanFromSeconds)
            };
        }

        public override void OnSuccess(MethodExecutionArgs args)
        {
            var key = GetCacheKey(args.Instance, args.Arguments);
            var val = CreateCacheObject(args.ReturnValue);
            backingStore.AddOrUpdate(key, val, (s, existingValue) => val);
        }

        internal class CacheObject
        {
            public DateTime Created { get; set; }
            public TimeSpan Lifetime { get; set; }
            public object Value { get; set; }
        }
    }
}
