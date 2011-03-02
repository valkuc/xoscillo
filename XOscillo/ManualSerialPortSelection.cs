using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

namespace XOscillo
{
   public partial class ManualSerialPortSelection : Form
   {
      Oscillo m_os;

      public ManualSerialPortSelection(Oscillo os)
      {
         m_os = os;
         InitializeComponent();
      }

      private void ManualSerialPortSelection_Load(object sender, EventArgs e)
      {
         string[] ports = SerialPort.GetPortNames();

         DebugConsole.Instance.AddLn("Autodetecting " + m_os.GetName() + " port");
         foreach (string portName in ports)
         {
            comboBox1.Items.Add(portName);
            
            if ( m_os.Open(portName) == true )
            {
               this.BeginInvoke(new MethodInvoker(this.Close));
            }            
         }

         DebugConsole.Instance.AddLn("Autodetection failed, trying manual mode");

         comboBox1.SelectedIndex = 0;

      }


      private void button1_Click(object sender, EventArgs e)
      {
         string portName = (string)comboBox1.SelectedItem;
         if (m_os.Open(portName) == true)
         {
            this.Close();
         }
         else
         {
            DebugConsole.Instance.AddLn("Detection failed, close this console and try again.");
            DebugConsole.Instance.Show();
         }
      }
   }
}
