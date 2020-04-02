using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp
{
    public interface ISimulatorModel : INotifyPropertyChanged
    {
        //connection
        void connect(string ip, int port);
        void disconnect();
        void start();
        double HeadingDeg { set; get; }
        double VerticalSpeed { set; get; }
        double GroundSpeed { set; get; }
        double Airspeed { set; get; }
        double Alttitude { set; get; }
        double RollDeg { set; get; }
        double PitchDeg { set; get; }
        double Altimeter { set; get; }
        double Rudder { set; get; }
        double Elevator { set; get; }
        double Aileron { set; get; }
        double Throttle { set; get; }
        double Latitude { set; get; }
        double Longitude { set; get; }
    }
}
