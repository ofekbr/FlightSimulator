using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Microsoft.Maps.MapControl.WPF;

namespace FlightSimulatorApp
{
    public delegate void ErrorMessage(String message);

    public interface ISimulatorModel : INotifyPropertyChanged
    {
        event ErrorMessage SendError;

        //connection
        void connect(string ip, int port);
        void disconnect();
        void start();

        void centerMapCordinate();

        String ErrorMessage { get; set; }
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
        Location Cordinates {set; get; }
        Location CenterMap { get;}
    }
}
