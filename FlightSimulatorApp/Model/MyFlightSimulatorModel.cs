﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulatorApp.Model
{
    class MyFlightSimulatorModel : IFlightSimulatorModel
    {
        ITelnetClient telnetClient;
        volatile bool stop;
        private double rudder;
        private double elevator;
        private double aileron;
        private double throttle;
        private double indicated_heading_deg;
        private double gps_indicated_vertical_speed;
        private double gps_indicated_ground_speed_kt;
        private double airspeed_indicator_indicated_speed_kt;
        private double gps_indicated_altitude_ft;
        private double attitude_indicator_internal_roll_deg;
        private double attitude_indicator_internal_pitch_deg;
        private double altimeter_indicated_altitude_ft;
        public MyFlightSimulatorModel(ITelnetClient telnet)
        {
            this.telnetClient = telnet;
            this.stop = false;
            Indicated_heading_deg = 50;
            Gps_indicated_vertical_speed = 60;
            Gps_indicated_ground_speed_kt = 70;
            Airspeed_indicator_indicated_speed_kt = 80;
            Gps_indicated_altitude_ft = 90;
            Attitude_indicator_internal_roll_deg = 100;
            Attitude_indicator_internal_pitch_deg = 102;
            Altimeter_indicated_altitude_ft = 30;
        }
       
        public double Rudder { get { return rudder; } set { rudder = value; NotifyPropertyChanged("Rudder"); } }
        public double Elevator { get { return elevator; } set { elevator = value; NotifyPropertyChanged("Elevator"); } }
        public double Aileron { get { return aileron; } set { aileron = value; NotifyPropertyChanged("Aileron"); } }
        public double Throttle { get { return throttle; } set { throttle = value; NotifyPropertyChanged("Throttle"); } }
        public double Indicated_heading_deg { get { return indicated_heading_deg; } set { indicated_heading_deg = value; NotifyPropertyChanged("Indicated_heading_deg"); } }
        public double Gps_indicated_vertical_speed { get { return gps_indicated_vertical_speed; } set { gps_indicated_vertical_speed = value; NotifyPropertyChanged("Gps_indicated_vertical_speed"); } }
        public double Gps_indicated_ground_speed_kt { get { return gps_indicated_ground_speed_kt; } set { gps_indicated_ground_speed_kt = value; NotifyPropertyChanged("Gps_indicated_ground_speed_kt"); } }
        public double Airspeed_indicator_indicated_speed_kt { get { return airspeed_indicator_indicated_speed_kt; } set { airspeed_indicator_indicated_speed_kt = value; NotifyPropertyChanged("Airspeed_indicator_indicated_speed_kt"); } }
        public double Gps_indicated_altitude_ft { get { return gps_indicated_altitude_ft; } set { gps_indicated_altitude_ft = value; NotifyPropertyChanged("Gps_indicated_altitude_ft"); } }
        public double Attitude_indicator_internal_roll_deg { get { return attitude_indicator_internal_roll_deg; } set { attitude_indicator_internal_roll_deg = value; NotifyPropertyChanged("Attitude_indicator_internal_roll_deg"); } }
        public double Attitude_indicator_internal_pitch_deg { get { return attitude_indicator_internal_pitch_deg; } set { attitude_indicator_internal_pitch_deg = value; NotifyPropertyChanged("Attitude_indicator_internal_pitch_deg"); } }
        public double Altimeter_indicated_altitude_ft { get { return altimeter_indicated_altitude_ft; } set { altimeter_indicated_altitude_ft = value; NotifyPropertyChanged("Altimeter_indicated_altitude_ft"); } }
        public event PropertyChangedEventHandler PropertyChanged;

        public void Connect(string ip, int port)
        {
            telnetClient.Connect(ip, port);
        }

        public void Disconnect()
        {
            telnetClient.Disconnect();
            stop = true; 
        }

        public void Start()
        {
            new Thread(delegate ()
            {
                while (!stop)
                {
                    Rudder = double.Parse(telnetClient.Read());
                    Aileron = double.Parse(telnetClient.Read());
                    Elevator = double.Parse(telnetClient.Read());
                    Throttle = double.Parse(telnetClient.Read());
                    //need more vars
                    Thread.Sleep(250);
                }
            }).Start();
        }
        public void NotifyPropertyChanged(string propName)
        {
            if(this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
                //"nili cohen"//
            }
        }
    }
}
