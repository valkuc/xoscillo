using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using XOscillo.Properties;
using XOscillo.Tools;
using XOscillo.VizForms;
using XOscillo.VizForms.Arduino;
using XOscillo.VizForms.Parallax;
using XOscillo.VizForms.Teensy;


namespace XOscillo
{
    public partial class MDIParent : Form
    {
        public MDIParent()
        {
            InitializeComponent();
        }

        private void newAnalogArduino_Click(object sender, EventArgs e)
        {
            /*
            PID childForm = new PID();
            childForm.MdiParent = this;
            childForm.Text = "Analog Arduino";
            //if (childForm.Init() == false)
            {
              // return;
            }
            childForm.Show();
            childForm.WindowState = FormWindowState.Maximized;
             */


            // Create a new instance of the child form.
            AnalogVizArduino childForm;
            childForm = new AnalogVizArduino();
            childForm.MdiParent = this;
            if (childForm.Init() == false)
            {
                return;
            }

            childForm.Show();
            childForm.WindowState = FormWindowState.Maximized;
        }

        private void newAnalogTeensy_Click(object sender, EventArgs e)
        {
            // Create a new instance of the child form.
            AnalogVizTeensy childForm;
            childForm = new AnalogVizTeensy();
            childForm.MdiParent = this;
            if (childForm.Init() == false)
            {
                return;
            }

            childForm.Show();
            childForm.WindowState = FormWindowState.Maximized;
        }


        private void newDigitalArduino_Click(object sender, EventArgs e)
        {
            // Create a new instance of the child form.
            DigitalVizArduino childForm;
            childForm = new DigitalVizArduino();
            childForm.MdiParent = this;
            if (childForm.Init() == false)
            {
                return;
            }
            childForm.Show();
            childForm.WindowState = FormWindowState.Maximized;
        }

        private void newParallax_Click(object sender, EventArgs e)
        {
            VizParallax childForm;
            childForm = new VizParallax();
            childForm.MdiParent = this;
            if (childForm.Init() == false)
            {
                return;
            }
            childForm.Show();
            childForm.WindowState = FormWindowState.Maximized;
        }


        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "XML Files (*.xml)|*.xml|Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;

                FileStream stream = File.Open(fileName, FileMode.Open);
                SerializationHelper sh = SerializationHelper.LoadXML(stream);
                stream.Close();

                VizForm childForm;
                if (sh.dataBlock.m_dataType == DataBlock.DATA_TYPE.ANALOG)
                {
                    childForm = new FileAnalogVizForm(sh);
                }
                else if (sh.dataBlock.m_dataType == DataBlock.DATA_TYPE.DIGITAL)
                {
                    childForm = new FileDigitalVizForm(sh);
                }
                else
                {
                    MessageBox.Show("file not supported");
                    return;
                }

                // Make it a child of this MDI form before showing it.
                childForm.MdiParent = this;
                childForm.Text = fileName;
                childForm.Init();
                childForm.Show();
                childForm.WindowState = FormWindowState.Maximized;

            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "XML Files (*.xml)|*.xml|Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string fileName = saveFileDialog.FileName;

                // Open the file, creating it if necessary.
                using (FileStream stream = File.Open(fileName, FileMode.Create))
                {
                    ((VizForm)ActiveMdiChild).SaveXML(stream);
                }
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: Use System.Windows.Forms.Clipboard to insert the selected text or images into the clipboard
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: Use System.Windows.Forms.Clipboard to insert the selected text or images into the clipboard
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: Use System.Windows.Forms.Clipboard.GetText() or System.Windows.Forms.GetData to retrieve information from the clipboard.
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void decodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form childForm = new DecodeFSK();
            // Make it a child of this MDI form before showing it.
            childForm.MdiParent = this;
            childForm.Text = ActiveMdiChild.Text;
            ((DataBlockAware)childForm).SetDataBlock(((DataBlockAware)ActiveMdiChild).GetDataBlock());
            childForm.Show();
        }

        private void decodeBeeperToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form childForm = new DecodeBeeper();
            // Make it a child of this MDI form before showing it.
            childForm.MdiParent = this;
            childForm.Text = ActiveMdiChild.Text;
            ((DataBlockAware)childForm).SetDataBlock(((DataBlockAware)ActiveMdiChild).GetDataBlock());
            childForm.Show();
        }

        private void decodeFrequencyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form childForm = new DecodeFrequency();
            // Make it a child of this MDI form before showing it.
            childForm.MdiParent = this;
            childForm.Text = ActiveMdiChild.Text;
            ((DataBlockAware)childForm).SetDataBlock(((DataBlockAware)ActiveMdiChild).GetDataBlock());
            childForm.Show();
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Written by Raul Aguaviva\nThis is work in progress", "Xoscillo");
        }

        private void debugConsoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DebugConsole.Instance.Show();
        }

        private void menuStrip_ItemAdded(object sender, ToolStripItemEventArgs e)
        {
            // Catch MDI child sys menu
            if (e.Item.Text == string.Empty)
            {
                e.Item.Image = Resources.oscilloscope;
            }
        }
    }
}
