using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp
{
    public class FlightControlViewModel : INotifyPropertyChanged
    {
        private ISimulatorModel model;

        public FlightControlViewModel(ISimulatorModel sm)
        {
            this.model = sm;
            model.PropertyChanged +=
                delegate (Object sender, PropertyChangedEventArgs e)
                {
                    NotifyPropertyChanged("VM_" + e.Event);
                };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public void connect(string ip, int port)
        {
            model.connect(ip, port);
        }

        public void start()
        {
            model.start();
        }

        //properties
        private double headingDeg;
        public double HeadingDeg
        {
            get { return headingDeg; }
            set
            {
                headingDeg = value;
                this.NotifyPropertyChanged("headingDeg");
            }
        }
        private double verticalSpeed;
        public double VerticalSpeed
        {
            get { return verticalSpeed; }
            set
            {
                verticalSpeed = value;
                this.NotifyPropertyChanged("verticalSpeed");
            }
        }
        private double groundSpeed;
        public double GroundSpeed
        {
            get { return groundSpeed; }
            set
            {
                groundSpeed = value;
                this.NotifyPropertyChanged("groundSpeed");
            }
        }
        private double airspeed;
        public double AirSpeed
        {
            get { return airspeed; }
            set
            {
                airspeed = value;
                this.NotifyPropertyChanged("airspeed");
            }
        }
        private double alttitude;
        public double Alttitude
        {
            get { return alttitude; }
            set
            {
                alttitude = value;
                this.NotifyPropertyChanged("alttitude");
            }
        }
        private double rollDeg;
        public double RollDeg
        {
            get { return rollDeg; }
            set
            {
                rollDeg = value;
                this.NotifyPropertyChanged("rollDeg");
            }
        }
        private double pitchDeg;
        public double PitchDeg
        {
            get { return pitchDeg; }
            set
            {
                pitchDeg = value;
                this.NotifyPropertyChanged("pitchDeg");
            }
        }
        private double altimeter;
        public double Altimeter
        {
            get { return altimeter; }
            set
            {
                altimeter = value;
                this.NotifyPropertyChanged("altimeter");
            }
        }
        private double rudder;
        public double Rudder
        {
            get { return rudder; }
            set
            {
                rudder = value;
                this.NotifyPropertyChanged("rudder");
            }
        }
        private double elevator;
        public double Elevator
        {
            get { return elevator; }
            set
            {
                elevator = value;
                this.NotifyPropertyChanged("elevator");
            }
        }
        private double aileron;
        public double Aileron
        {
            get { return aileron; }
            set
            {
                aileron = value;
                this.NotifyPropertyChanged("aileron");
            }
        }
        private double throttle;
        public double Throttle
        {
            get { return throttle; }
            set
            {
                throttle = value;
                this.NotifyPropertyChanged("throttle");
            }
        }
        private double latitude;
        public double Latitude
        {
            get { return latitude; }
            set
            {
                latitude = value;
                this.NotifyPropertyChanged("latitude");
            }
        }
        private double longitude;
        public double Longitude
        {
            get { return longitude; }
            set
            {
                longitude = value;
                this.NotifyPropertyChanged("longitude");
            }
        }
        public float Up
        {
            get; set;
        }
        public float Down
        {
            get; set;
        }
        public float Left
        {
            get; set;
        }
        public float Right
        {
            get; set;
        }
    }
}
