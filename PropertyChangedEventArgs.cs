using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp
{
    public class PropertyChangedEventArgs
    {
        private string eventName;
        public PropertyChangedEventArgs(string name)
        {
            eventName = name;
        }
        public string Event
        {
            get { return eventName; }
            set { eventName = value; }
        }
    }
}
