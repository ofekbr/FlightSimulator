using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp
{
    public class DashBoardVM : FlightControlViewModel
    {
        public DashBoardVM(ISimulatorModel sm) : base(sm) { }

        //properties
        public double VM_HeadingDeg
        {
            get { return model.HeadingDeg; }
        }
        public double VM_VerticalSpeed
        {
            get { return model.VerticalSpeed; }
        }
        public double VM_GroundSpeed
        {
            get { return model.GroundSpeed; }
        }
        public double VM_AirSpeed
        {
            get { return model.Airspeed; }

        }
        public double VM_Alttitude
        {
            get { return model.Alttitude; }
        }
        public double VM_RollDeg
        {
            get { return model.RollDeg; }
        }
        public double VM_PitchDeg
        {
            get { return model.PitchDeg; }
        }
        public double VM_Altimeter
        {
            get { return model.Altimeter; }

        }
    }
}
