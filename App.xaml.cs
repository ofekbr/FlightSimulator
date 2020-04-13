using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FlightSimulatorApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        ISimulatorModel model;
        FlightControlViewModel vm;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Model = new SimulatorModel(new MyTelnetClient());
            VM = new FlightControlViewModel(model);
        }

        public FlightControlViewModel VM
        {
            get
            {
                return this.vm;
            }
            set
            {
                this.vm = value;
            }
        }
        public ISimulatorModel Model
        {
            get
            {
                return this.model;
            }
            set
            {
                this.model = value;
            }
        }
    }
}
