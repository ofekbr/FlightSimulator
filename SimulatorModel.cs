using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;

namespace FlightSimulatorApp
{
    class SimulatorModel : ISimulatorModel
    {
        volatile Boolean m_stop;
        ITelnetClient m_telnetClient;
        Queue<string> commandsForServer = new Queue<string>();

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
        //for map
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
        private string cordinate;
        public string Cordinates
        {
            get { return cordinate;}
            set 
            {
                cordinate = value;
                this.NotifyPropertyChanged("Cordinates");
            }
        }
        
        //for controlls
        public double Rudder
        {
            get { return rudder; }
            set
            {
                if (value >= -1 && value <= 1)
                {
                    if (rudder != value)
                    {
                        rudder = value;
                        string command = "set /controls/flight/rudder " + rudder.ToString() + "\r\n";
                        commandsForServer.Enqueue(command);
                        this.NotifyPropertyChanged("Rudder");
                    }
                }
            }
        }

        private double elevator;
        public double Elevator
        {
            get { return elevator; }
            set
            {
                if (value >= -1 && value <= 1)
                {
                    if (elevator != value)
                    {
                        elevator = value;
                        string command = "set /controls/flight/elevator " + elevator.ToString() + "\r\n";
                        commandsForServer.Enqueue(command);
                        this.NotifyPropertyChanged("Elevator");
                    }
                }
            }
        }

        private double aileron;
        public double Aileron
        {
            get { return aileron; }
            set
            {
                if (value >= -1 && value <= 1)
                {
                    if (aileron != value)
                    {
                    aileron = value;
                    string command = "set /controls/flight/aileron "+ aileron.ToString() + "\r\n";
                    commandsForServer.Enqueue(command);
                    this.NotifyPropertyChanged("Aileron");
                    }
                }
            }
        }

        private double throttle;
        public double Throttle
        {
            get { return throttle; }
            set
            {
                if (value <= 1 && value >= 0)
                {
                    if (throttle != value)
                    {
                        throttle = value;
                        string command = "set /controls/engines/current-engine/throttle " + throttle.ToString() + "\r\n";
                        commandsForServer.Enqueue(command);
                        this.NotifyPropertyChanged("Throttle");
                    }
                }
            }
        }

        private Boolean mapInisialized =false;
        private string centerMap;
        public string CenterMap
        {
            get {
                if (!mapInisialized) {
                    centerMap = cordinate;
                    mapInisialized = true;
                }
                return centerMap;
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
                    try {
                        HeadingDeg = Double.Parse(m_telnetClient.read());
                    } catch(Exception) {
                        Console.WriteLine("headingDeg value is incorrect");
                    } 
                    m_telnetClient.write("get /instrumentation/gps/indicated-vertical-speed\r\n");
                    try {
                        VerticalSpeed = Double.Parse(m_telnetClient.read());
                    } catch(Exception) {
                        Console.WriteLine("verticalSpeed value is incorrect");
                    }
                    m_telnetClient.write("get /instrumentation/gps/indicated-ground-speed-kt\r\n");
                    try {
                        GroundSpeed = Double.Parse(m_telnetClient.read());
                    } catch(Exception) {
                        Console.WriteLine("groundSpeed value is incorrect");
                    }
                    m_telnetClient.write("get /instrumentation/airspeed-indicator/indicated-speed-kt\r\n");
                    try {
                        Airspeed = Double.Parse(m_telnetClient.read());
                    } catch(Exception e) {
                        Console.WriteLine("airspeed value is incorrect");
                    } 
                    m_telnetClient.write("get /instrumentation/gps/indicated-altitude-ft\r\n");
                    try {
                        Alttitude = Double.Parse(m_telnetClient.read());
                    } catch(Exception) {
                        Console.WriteLine("alttitude value is incorrect");
                    }
                    m_telnetClient.write("get /instrumentation/attitude-indicator/internal-roll-deg\r\n");
                    try {
                        RollDeg = Double.Parse(m_telnetClient.read());
                    } catch(Exception) {
                        Console.WriteLine("rollDeg value is incorrect");
                    }
                    m_telnetClient.write("get /instrumentation/attitude-indicator/internal-pitch-deg\r\n");
                    try {
                        PitchDeg = Double.Parse(m_telnetClient.read());
                    } catch(Exception) {
                        Console.WriteLine("pitchDeg value is incorrect");
                    }
                    m_telnetClient.write("get /instrumentation/altimeter/indicated-altitude-ft\r\n");
                    try {
                        Altimeter = Double.Parse(m_telnetClient.read());
                    } catch(Exception) {
                        Console.WriteLine("altimeter value is incorrect");
                    }
                    
                    //for map
                    m_telnetClient.write("get /position/latitude-deg\r\n");
                    try {
                        Latitude = Double.Parse(m_telnetClient.read());
                    } catch(Exception) {
                        Console.WriteLine("latitude value is incorrect");
                    } 
                    m_telnetClient.write("get /position/longitude-deg\r\n");
                    try {
                        Longitude = Double.Parse(m_telnetClient.read());
                    } catch(Exception) {
                        Console.WriteLine("longitude value is incorrect");
                    }
                    Cordinates = Longitude.ToString() + "," + Latitude.ToString();

                    while (commandsForServer.Count() > 0)
                    {

                        {
                            m_telnetClient.write(commandsForServer.Peek());
                            m_telnetClient.read();
                        }
                        commandsForServer.Dequeue();
                    }


                    Thread.Sleep(250);//TODO change 

                }
                Console.WriteLine("finished \"get\" thread");
            }).Start();
        }
    }
}

