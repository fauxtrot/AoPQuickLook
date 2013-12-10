using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;

namespace AopQuickLook.Models
{

    public class ExampleOne
    {
        private ILoggingService _loggingService;
        
        public ExampleOne(ILoggingService logService)
        {
            _loggingService = logService;
        }
        
        //require logging, log within the method
        public void DoSomethingInteresting()
        {            
            //what about error handling??
            try
            {
                //We're starting the operation
                _loggingService.Log("Started Doing something interesting at {0}", DateTime.Now);
                //BusinessLogicHere
                WaitForTen();
                //We're Successful in completing the operation
                _loggingService.Log("Doing something interesting Successful");
                //log exit
                //We're starting the operation
                _loggingService.Log("Exiting Operation Do Something Interesting at {0}", DateTime.Now);

            }
            catch (Exception)
            {
                //We're starting the operation
                _loggingService.Log("Doing something interesting failed at {0}", DateTime.Now);
                throw;
            }
        }

        private void WaitForTen()
        {
            System.Threading.Thread.Sleep(10);
            //throw new DivideByZeroException("Hehe... you can do that!");
        }
    }
}
