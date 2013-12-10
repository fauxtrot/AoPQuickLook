using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Windsor;

namespace Core
{
    public class MainContainer : WindsorContainer
    {
        static MainContainer()
        {
            _main = new MainContainer();
        }

        private static MainContainer _main;
        public static MainContainer Main
        {
            get
            {
                return _main;
            }
        }
    }
}
