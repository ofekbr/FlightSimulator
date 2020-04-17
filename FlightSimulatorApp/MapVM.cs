using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp
{
    public class MapVM : FlightControlViewModel
    {
        public MapVM(ISimulatorModel sm) : base(sm) {}

        public void centerMapCordinate()
        {
            model.centerMapCordinate();
        }

        //properties
        public double VM_Latitude
        {
            get { return model.Latitude; }
        }
        public double VM_Longitude
        {
            get { return model.Longitude; }
        }
        public Location VM_Cordinates
        {
            get { return model.Cordinates; }
        }
        public Location VM_CenterMap
        {
            get { return model.CenterMap; }
        }

        
    }
}
