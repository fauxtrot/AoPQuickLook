using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Internal;
using Castle.DynamicProxy;
using Common.Logging;

namespace LoggingModule.Interceptors
{
    public class LoggingInterceptor : IInterceptor
    {
        public ILoggingService _LoggingService { get; set; }

        public void Intercept(IInvocation invocation)
        {
            _LoggingService.Log("Entering Method {0}", invocation.Method);
            try
            {
                invocation.Proceed();
                _LoggingService.Log("Method {0} Successful", invocation.Method);
            }
            catch (Exception)
            {
                _LoggingService.Log("An error was encountered for the Method: {0}", invocation.Method);
                invocation.Arguments.ForEach(arg => _LoggingService.Log("{0}", arg));
                throw;
            }
            finally
            {
                _LoggingService.Log("Exiting Method {0}", invocation.Method);
            }
        }
    }
}
