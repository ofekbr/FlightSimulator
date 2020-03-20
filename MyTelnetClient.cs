﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FlightSimulatorApp
{
    class MyTelnetClient : ITelnetClient
    {
        private TcpClient tcpclnt;
        private Stream stm;
        public void write(string command)
        {
            ASCIIEncoding asen = new ASCIIEncoding();
            byte[] ba = asen.GetBytes(command);
            Console.WriteLine("Transmitting....." + command);

            stm.Write(ba, 0, ba.Length);

            Console.WriteLine("the command was send succesfuly");
        }
        public string read()
        {
            byte[] bb = new byte[100];
            int k = stm.Read(bb, 0, 100);


            StringBuilder builder = new StringBuilder();
            foreach (char value in bb)
            {
                builder.Append(value);
            }

            string returnedValue = builder.ToString();
            //for floating point
            returnedValue = Regex.Match(returnedValue, @"\d+.\d+").Value;
            //for integer
            string temp = Regex.Match(returnedValue, @"\d+").Value;

            if (returnedValue == "")
                return temp;
            else return returnedValue;

        }
        public void disconnect()
        {
            tcpclnt.Close();
        }
        public void connect(string ip, int port)
        {
            tcpclnt = new TcpClient();
            Console.WriteLine("Connecting.....");

            tcpclnt.Connect(ip, port);
            // use the ipaddress as in the server program

            Console.WriteLine("Connected");
            Console.Write("Enter the string to be transmitted : ");

            stm = tcpclnt.GetStream();
        }
    }
}
