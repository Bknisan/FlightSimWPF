﻿using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace FlightSimulator.Connection
{
    class Commands
    {
        // client member
        private TcpClient client; 
        // send messeges
        private BinaryWriter sender; 
        // check if already connected
        public bool Connected { get; set; } = false; 

        #region Singleton
        private static Commands m_Instance = null;
        public static Commands Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new Commands();
                }
                return m_Instance;
            }
        }
        #endregion

        public void Reset() { m_Instance = null; }
        // connect to server
        public void Connect(string ip, int port)
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), port);
            client = new TcpClient();
            while (!client.Connected) // keep trynig to connect
            {
                try { client.Connect(ep); }
                catch (Exception) { }
            }
            Connected = true;
            sender = new BinaryWriter(client.GetStream());

        }

        // change sim vals.
        public void ChangeValues(string input)
        {
            // check if sent empty set.
            if (string.IsNullOrEmpty(input)) return;
            // split commands by new line indicator.
            string[] setters = input.Split('\n');
            foreach (string command in setters)
            {
                // change command with win new line indicator.
                string tmp = command + "\r\n";
                // send setter comverted to binary.
                sender.Write(System.Text.Encoding.ASCII.GetBytes(tmp));
                System.Threading.Thread.Sleep(2000);
            }
        }
    }
}
