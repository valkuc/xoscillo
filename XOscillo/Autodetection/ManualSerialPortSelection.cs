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
        public string[] ports;

        public ManualSerialPortSelection(string[] ports)
        {
            this.ports = ports;
            InitializeComponent();
        }

        private void ManualSerialPortSelection_Load(object sender, EventArgs e)
        {
            foreach (string portName in ports)
            {
                comboBox1.Items.Add(portName);
            }

            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        public int GetSelection()
        {
            return comboBox1.SelectedIndex;
        }
    }
}
