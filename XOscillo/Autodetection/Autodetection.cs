using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.IO.Ports;

namespace XOscillo
{
    class Autodetection<T> where T : Oscillo, new()
    {
        private static readonly bool IsMonoRuntime = (Type.GetType("Mono.Runtime") != null);

        public T TryMTDetection()
        {
            DebugConsole.Instance.Show();

            string[] ports = GetPortNames();
            T[] oscillos = new T[ports.Length];
            ManualResetEvent[] doneEvents = new ManualResetEvent[ports.Length];
            bool[] results = new bool[ports.Length];

            DebugConsole.Instance.EnableLogging(false);

            for (int i = 0; i < results.Length; i++)
            {
                doneEvents[i] = new ManualResetEvent(false);
                oscillos[i] = new T();

                ThreadPool.QueueUserWorkItem(cntx =>
               {
                   int j = (int)cntx;
                   try
                   {
                       results[j] = oscillos[j].Open(ports[j]);
                   }
                   catch
                   {
                   }
                   Console.WriteLine("Signaling {0}: {1} {2}", j, results[j], doneEvents[j]);
                   doneEvents[j].Set();
               },
               i);
            }

            foreach (var e in doneEvents)
            {
                e.WaitOne(1000);
            }

            DebugConsole.Instance.EnableLogging(true);

            for (int i = 0; i < results.Length; i++)
            {
                if (results[i] == true)
                {
                    return oscillos[i];
                }
            }

            return null;
        }

        public T TrySTDetection()
        {
            string[] ports = GetPortNames();

            T oscillo = new T();

            DebugConsole.Instance.AddLn("Autodetecting " + oscillo.GetName() + " port");
            foreach (string portName in ports)
            {
                if (oscillo.Open(portName) == true)
                {
                    DebugConsole.Instance.Hide();
                    return oscillo;
                }
            }

            DebugConsole.Instance.AddLn("Autodetection failed, trying manual mode");

            return null;
        }

        public T TryManualDetection()
        {
            string[] ports = GetPortNames();

            T oscillo = new T();

            ManualSerialPortSelection msps = new ManualSerialPortSelection(ports);
            msps.ShowDialog();

            if (msps.DialogResult != System.Windows.Forms.DialogResult.OK)
            {
                return null;
            }

            oscillo.Open(ports[msps.GetSelection()]);

            return oscillo;
        }

        public T Detection()
        {
            T res;

            DebugConsole.Instance.Add("Checking MT autodetection...");
            res = TryMTDetection();
            if (res != null)
            {
                DebugConsole.Instance.AddLn("OK");
                return res;
            }

            DebugConsole.Instance.AddLn("NOPE");

            DebugConsole.Instance.Show();

            DebugConsole.Instance.Add("Checking ST autodetection...");
            res = TrySTDetection();
            if (res != null)
            {
                DebugConsole.Instance.Hide();
                DebugConsole.Instance.AddLn("OK");
                return res;
            }

            DebugConsole.Instance.AddLn("NOPE");
            DebugConsole.Instance.Add("Forcing Manual...");

            DebugConsole.Instance.Hide();

            return TryManualDetection();
        }

        /// <summary>
        /// Retrieve available serial ports.
        /// </summary>
        /// <returns>Array of serial port names.</returns>
        public static string[] GetPortNames()
        {
            /**
             * Under Mono SerialPort.GetPortNames() returns /dev/ttyS* devices,
             * but Arduino is detected as ttyACM* or ttyUSB*
             * */
            if (IsMonoRuntime)
            {
                var searchPattern = new Regex("ttyACM.+|ttyUSB.+");
                return Directory.GetFiles("/dev").Where(f => searchPattern.IsMatch(f)).ToArray();
            }
            else
            {
                return SerialPort.GetPortNames();
            }
        }
    }
}
