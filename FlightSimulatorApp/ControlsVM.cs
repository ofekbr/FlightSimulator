using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp
{
    public class ControlsVM : FlightControlViewModel
    {
        public ControlsVM(ISimulatorModel sm) : base(sm) { }

        public double VM_Rudder
        {
            get { return model.Rudder; }
            set
            {
                model.Rudder = value;
                this.NotifyPropertyChanged("Rudder");
            }
        }
        public double VM_Elevator
        {
            get { return model.Elevator; }
            set
            {
                model.Elevator = value;
                this.NotifyPropertyChanged("Elevator");
            }
        }
        public double VM_Aileron
        {
            get { return model.Aileron; }
            set
            {
                model.Aileron = value;
                this.NotifyPropertyChanged("Aileron");
            }
        }
        public double VM_Throttle
        {
            get { return model.Throttle; }
            set
            {
                model.Throttle = value;
                this.NotifyPropertyChanged("Throttle");
            }
        }
    }
}
