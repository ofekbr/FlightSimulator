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
        ControlsVM controlVM;
        MapVM mapVM;
        DashBoardVM dashVM;
        ErrorVm errorVM;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Model = new SimulatorModel(new MyTelnetClient());
            ControlsVM = new ControlsVM(model);
            MapVM = new MapVM(model);
            DashVM = new DashBoardVM(model);
            errorVM = new ErrorVm(model);
        }

        public ErrorVm ErrorVm
        {
            get
            {
                return this.errorVM;
            }
            set
            {
                this.errorVM = value;
            }
        }

        public ControlsVM ControlsVM
        {
            get
            {
                return this.controlVM;
            }
            set
            {
                this.controlVM = value;
            }
        }

        public MapVM MapVM
        {
            get
            {
                return this.mapVM;
            }
            set
            {
                this.mapVM = value;
            }
        }

        public DashBoardVM DashVM
        {
            get
            {
                return this.dashVM;
            }
            set
            {
                this.dashVM = value;
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
