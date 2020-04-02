using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace FlightSimulatorApp
{
    class SimulatorModel : ISimulatorModel
    {
        volatile Boolean m_stop;
        ITelnetClient m_telnetClient;

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        public SimulatorModel(ITelnetClient telnetClient)
        {
            this.m_telnetClient = telnetClient;
        }

        //connection
        public void connect(string ip, int port)
        {
            m_telnetClient.connect(ip, port);
        }
        public void disconnect()
        {
            m_stop = true;
            m_telnetClient.disconnect();
        }

        //properties
        private double headingDeg;
        public double HeadingDeg
        {
            get { return headingDeg; }
            set
            {
                headingDeg = value;
                this.NotifyPropertyChanged("HeadingDeg");
            }
        }
        private double verticalSpeed;
        public double VerticalSpeed
        {
            get { return verticalSpeed; }
            set
            {
                verticalSpeed = value;
                this.NotifyPropertyChanged("VerticalSpeed");
            }
        }
        private double groundSpeed;
        public double GroundSpeed
        {
            get { return groundSpeed; }
            set
            {
                groundSpeed = value;
                this.NotifyPropertyChanged("GroundSpeed");
            }
        }
        private double airspeed;
        public double Airspeed
        {
            get { return airspeed; }
            set
            {
                airspeed = value;
                this.NotifyPropertyChanged("Airspeed");
            }
        }
        private double alttitude;
        public double Alttitude
        {
            get { return alttitude; }
            set
            {
                alttitude = value;
                this.NotifyPropertyChanged("Alttitude");
            }
        }
        private double rollDeg;
        public double RollDeg
        {
            get { return rollDeg; }
            set
            {
                rollDeg = value;
                this.NotifyPropertyChanged("RollDeg");
            }
        }
        private double pitchDeg;
        public double PitchDeg
        {
            get { return pitchDeg; }
            set
            {
                pitchDeg = value;
                this.NotifyPropertyChanged("PitchDeg");
            }
        }
        private double altimeter;
        public double Altimeter
        {
            get { return altimeter; }
            set
            {
                altimeter = value;
                this.NotifyPropertyChanged("Altimeter");
            }
        }
        private double rudder;
        public double Rudder
        {
            get { return rudder; }
            set
            {
                rudder = value;
                this.NotifyPropertyChanged("Rudder");
            }
        }
        private double elevator;
        public double Elevator
        {
            get { return elevator; }
            set
            {
                elevator = value;
                this.NotifyPropertyChanged("Elevator");
            }
        }
        private double aileron;
        public double Aileron
        {
            get { return aileron; }
            set
            {
                aileron = value;
                this.NotifyPropertyChanged("Aileron");
            }
        }
        private double throttle;
        public double Throttle
        {
            get { return throttle; }
            set
            {
                throttle = value;
                this.NotifyPropertyChanged("Throttle");
            }
        }
        private double latitude;
        public double Latitude
        {
            get { return latitude; }
            set
            {
                latitude = value;
                this.NotifyPropertyChanged("Latitude");
            }
        }
        private double longitude;
        public double Longitude
        {
            get { return longitude; }
            set
            {
                longitude = value;
                this.NotifyPropertyChanged("Longitude");
            }
        }


        public void start()
        {
            new Thread(delegate ()
            {
                while (!m_stop)
                {
                    //for dashbord
                    m_telnetClient.write("get /instrumentation/heading-indicator/indicated-heading-deg\r\n");
                    headingDeg = Double.Parse(m_telnetClient.read());
                    m_telnetClient.write("get /instrumentation/gps/indicated-vertical-speed\r\n");
                    //string temp = m_telnetClient.read();
                    verticalSpeed = Double.Parse(m_telnetClient.read());
                    m_telnetClient.write("get /instrumentation/gps/indicated-ground-speed-kt\r\n");
                    groundSpeed = Double.Parse(m_telnetClient.read());
                    m_telnetClient.write("get /instrumentation/airspeed-indicator/indicated-speed-kt\r\n");
                    airspeed = Double.Parse(m_telnetClient.read());
                    m_telnetClient.write("get /instrumentation/gps/indicated-altitude-ft\r\n");
                    alttitude = Double.Parse(m_telnetClient.read());
                    m_telnetClient.write("get /instrumentation/attitude-indicator/internal-roll-deg\r\n");
                    //string 
                    //temp = m_telnetClient.read();
                    rollDeg = Double.Parse(m_telnetClient.read());
                    m_telnetClient.write("get /instrumentation/attitude-indicator/internal-pitch-deg\r\n");
                    pitchDeg = Double.Parse(m_telnetClient.read());
                    m_telnetClient.write("get /instrumentation/altimeter/indicated-altitude-ft\r\n");
                    altimeter = Double.Parse(m_telnetClient.read());

                    //for boast
                    m_telnetClient.write("get /controls/flight/rudder\r\n");
                    rudder = Double.Parse(m_telnetClient.read());
                    m_telnetClient.write("get /controls/flight/elevator\r\n");
                    elevator = Double.Parse(m_telnetClient.read());
                    m_telnetClient.write("get /controls/flight/aileron\r\n");
                    aileron = Double.Parse(m_telnetClient.read());
                    m_telnetClient.write("get /controls/engines/engine/throttle\r\n");
                    throttle = Double.Parse(m_telnetClient.read());

                    //for map
                    m_telnetClient.write("get /position/latitude-deg\r\n");
                    latitude = Double.Parse(m_telnetClient.read());
                    m_telnetClient.write("get /position/longitude-deg\r\n");
                    longitude = Double.Parse(m_telnetClient.read());

                    Thread.Sleep(250);//TODO change 

                }
            }).Start();
        }


    }
}

