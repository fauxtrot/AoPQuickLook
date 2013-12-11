using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AopQuickLook.Models;
using Caliburn.Micro;
using Common.Logging;
using PostSharp.Aspects.Advices;

namespace AopQuickLook.Main
{
    public class MainWindowViewModel : PropertyChangedBase, IHandle<ConsoleMessage> 
    {
        #region Fields
        
        private string _consoleOutput;

        private IEventAggregator _eventAggregator;

        #endregion

        #region Properties
        public ExampleOne ExampleOne { get; set; }

        public ExampleTwo ExampleTwo { get; set; }

        public ExampleThree ExampleThree { get; set; }

        public string ConsoleOutput
        {
            get { return _consoleOutput; }
            set
            {
                _consoleOutput = value;
                this.NotifyOfPropertyChange(() => ConsoleOutput);
            }
        }
        
        #endregion

        #region
        
        public MainWindowViewModel(ExampleOne exampleOne, ExampleTwo exampletwo, IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            this.ExampleOne = exampleOne;
            this.ExampleTwo = exampletwo;
            //Via PostSharp
            this.ExampleThree = new ExampleThree();
            _eventAggregator.Subscribe(this);

        }
        
        #endregion

        #region Mapped Actions

        public void ExampleOneAction()
        {
            ExampleOne.DoSomethingInteresting();
        }

        public void ExampleThreeAction()
        {
            ExampleThree.DoSomethingSpooky();
        }

        public void ExampleTwoAction()
        {
            ExampleTwo.DoSomethingAwesomer();
        }

        public void ExampleTwoActionError()
        {
            try
            {
                ExampleTwo.DoSomethingWrong();
            }
            catch (Exception ex)
            {
                OutputMessage(ex.ToString());
                //handle on global scale.
            }
        }

        public void ExapmleThreeActionWithCaching()
        {
            OutputMessage("Querying server on Mars...");
            Task<string>.Factory.StartNew(() => ExampleThree.SimulateLongLoad())
                .ContinueWith(x => OutputMessage(x.Result), TaskScheduler.FromCurrentSynchronizationContext());
        }

        public void ClearConsole()
        {
            ConsoleOutput = string.Empty;
        }

        #endregion
        
        #region IHandle Implementations
        public void Handle(ConsoleMessage message)
        {
            OutputMessage(message.Message);
        }

        #endregion

        #region Helper methods
        private void OutputMessage(string message)
        {
            ConsoleOutput = string.Format("{0}{1}{2}", ConsoleOutput, Environment.NewLine, message);
        }
        #endregion
    }
}
