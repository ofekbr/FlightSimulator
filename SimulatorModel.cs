using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using System.IO;

namespace FlightSimulatorApp
{
    class SimulatorModel : ISimulatorModel
    {
        String connectionError = "Server is unresponsive !\nPlease restart both simulator server and the program.";
        volatile Boolean m_stop;
        ITelnetClient m_telnetClient;
        Queue<string> commandsForServer = new Queue<string>();

        public event PropertyChangedEventHandler PropertyChanged;
        public event ErrorMessage SendError;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public void NotifySendError(string message)
        {
            if (this.SendError != null)
                this.SendError(message);
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

        public void centerMapCordinate()
        {
            CenterMap = cordinate;
        }

        private Boolean mapInisialized = false;
        private string centerMap;
        public string CenterMap
        {
            get
            {
                if (!mapInisialized)
                {
                    centerMap = cordinate;
                    mapInisialized = true;
                }
                return centerMap;
            }
            set
            {
                centerMap = value;
                this.NotifyPropertyChanged("CenterMap");

            }
        }
        //properties
        private String errorMessage;
        public String ErrorMessage
        {
            get { return errorMessage; }
            set
            {
                errorMessage = value + errorMessage;
                this.NotifyPropertyChanged("ErrorMessage");
            }
        }

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

      
        public void start()
        {
            new Thread(delegate ()
            {
                List<String> getMessages = new List<String>();
                getMessages.Add("get /instrumentation/heading-indicator/indicated-heading-deg\r\n");
                getMessages.Add("get /instrumentation/gps/indicated-vertical-speed\r\n");
                getMessages.Add("get /instrumentation/gps/indicated-ground-speed-kt\r\n");
                getMessages.Add("get /instrumentation/airspeed-indicator/indicated-speed-kt\r\n");
                getMessages.Add("get /instrumentation/gps/indicated-altitude-ft\r\n");
                getMessages.Add("get /instrumentation/attitude-indicator/internal-roll-deg\r\n");
                getMessages.Add("get /instrumentation/attitude-indicator/internal-pitch-deg\r\n");
                getMessages.Add("get /instrumentation/altimeter/indicated-altitude-ft\r\n");
                getMessages.Add("get /position/latitude-deg\r\n");
                getMessages.Add("get /position/longitude-deg\r\n");

                while (!m_stop)
                {
                    bool everythingsFine = true;
                    //for dashbord
                    for (int i = 0; i < 10; i++)
                    {
                        try
                        {
                            m_telnetClient.write(getMessages[i]);
                        }
                        catch (Exception)
                        {
                            //cant write get message, server closed
                            ErrorMessage = connectionError;
                            m_stop = true;
                            break;
                        }
                        try
                        {
                            switch (i)
                            {
                                case 0:
                                    HeadingDeg = Double.Parse(m_telnetClient.read());
                                    break;
                                case 1:
                                    VerticalSpeed = Double.Parse(m_telnetClient.read());
                                    break;
                                case 2:
                                    GroundSpeed = Double.Parse(m_telnetClient.read());
                                    break;
                                case 3:
                                    Airspeed = Double.Parse(m_telnetClient.read());
                                    break;
                                case 4:
                                    Alttitude = Double.Parse(m_telnetClient.read());
                                    break;
                                case 5:
                                    RollDeg = Double.Parse(m_telnetClient.read());
                                    break;
                                case 6:
                                    PitchDeg = Double.Parse(m_telnetClient.read());
                                    break;
                                case 7:
                                    Altimeter = Double.Parse(m_telnetClient.read());
                                    break;
                                case 8:
                                    Latitude = Double.Parse(m_telnetClient.read());
                                    if (Latitude >= 90 || Latitude <= -90)
                                    {
                                        everythingsFine = false;
                                        ErrorMessage = "Latitude out of range";
                                    }
                                    break;
                                case 9:
                                    Longitude = Double.Parse(m_telnetClient.read());
                                    if (Longitude >= 180 || Longitude <= -180)
                                    {
                                        everythingsFine = false;
                                        ErrorMessage = "Longitude out of range";
                                    }
                                    break;
                                default:
                                    ErrorMessage = "shit pavel im sorry dont ruin my grade, something unexcepted happened :(";
                                    break;
                            }
                        }
                        catch (FormatException)
                        {
                            //incorrect values
                            switch (i)
                            {
                                case 0:
                                    ErrorMessage = "HeadingDeg value is incorrect\n";
                                    break;
                                case 1:
                                    ErrorMessage = "verticalSpeed value is incorrect\n";
                                    break;
                                case 2:
                                    ErrorMessage = "GroundSpeed value is incorrect\n";
                                    break;
                                case 3:
                                    ErrorMessage = "Airspeed value is incorrect\n";
                                    break;
                                case 4:
                                    ErrorMessage = "Alttitude value is incorrect\n";
                                    break;
                                case 5:
                                    ErrorMessage = "RollDeg value is incorrect\n";
                                    break;
                                case 6:
                                    ErrorMessage = "PitchDeg value is incorrect\n";
                                    break;
                                case 7:
                                    ErrorMessage = "Altimeter value is incorrect\n";
                                    break;
                                case 8:
                                    ErrorMessage = "Latitude value is incorrect\n";
                                    everythingsFine = false;
                                    break;
                                case 9:
                                    ErrorMessage = "Longitude value is incorrect\n";
                                    everythingsFine = false;
                                    break;
                                default:
                                    ErrorMessage = "shit\n";
                                    break;
                            }
                        }
                        catch (Exception)
                        {
                            // cant read values, server closed.
                            ErrorMessage = connectionError;
                            m_stop = true;
                            break;
                        }
                    }
                    if (everythingsFine)
                    {
                    Cordinates = Longitude.ToString() + "," + Latitude.ToString();
                    }

                    while (commandsForServer.Count() > 0)
                    {
                        {
                            try
                            {
                            m_telnetClient.write(commandsForServer.Peek());
                            m_telnetClient.read();
                            }
                            catch (Exception)
                            {
                                ErrorMessage = connectionError;
                                m_stop = true;
                                break;
                            }
                        }
                        commandsForServer.Dequeue();
                    }
                    Thread.Sleep(250);                    
                }
                Console.WriteLine("finished get and set thread");
            }).Start();
        }
    }
}

