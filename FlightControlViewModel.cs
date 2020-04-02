﻿using System;
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
        public double VM_HeadingDeg
        {
            get { return model.HeadingDeg; }
        }
        public double VM_VerticalSpeed
        {
            get { return model.VerticalSpeed; }
        }
        //private double groundSpeed;
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
        public double VM_Rudder
        {
            get { return model.Rudder; }
        }
        public double VM_Elevator
        {
            get { return model.Elevator; }
        }
        public double VM_Aileron
        {
            get { return model.Aileron; }
        }
        public double VM_Throttle
        {
            get { return model.Throttle; }
        }
        public double VM_Latitude
        {
            get { return model.Latitude; }
        }
        public double VM_Longitude
        {
            get { return model.Longitude; }
        }
    }
}
