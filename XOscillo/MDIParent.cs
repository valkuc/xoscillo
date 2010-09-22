using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;


namespace XOscillo
{
	public partial class MDIParent : Form
	{
		public MDIParent()
		{
			InitializeComponent();

			//ShowNewForm(null, null);
         /*
         DataBlock db;
         VizForm childForm;

         childForm = new DecodeFSK();
         // Make it a child of this MDI form before showing it.
         childForm.MdiParent = this;
         childForm.Text = "L:\\Users\\raguaviv\\Documents\\500micros.xml";
         db = new DataBlock();
         db.LoadXML(childForm.Text);
         childForm.SetDataBlock(db);
         childForm.Show();

         childForm = new DecodeFSK();
         // Make it a child of this MDI form before showing it.
         childForm.MdiParent = this;
         childForm.Text = "L:\\Users\\raguaviv\\Documents\\billete_bru_072729837.xml";
         db = new DataBlock();
         db.LoadXML(childForm.Text);
         childForm.SetDataBlock(db);
         childForm.Show();
         */

		}

      private void toolStripMenuItem1_Click(object sender, EventArgs e)
      {
			// Create a new instance of the child form.
         Form childForm;
         childForm = new VizArduino();
         childForm.MdiParent = this;
         childForm.Text = "Arduino";
         childForm.Show();
         childForm.WindowState = FormWindowState.Maximized;
      }

      private void newArduino2_Click(object sender, EventArgs e)
      {
         /*
         // Create a new instance of the child form.
         Form childForm;
         childForm = new ArduinoForm2();
         childForm.MdiParent = this;
         childForm.Text = "Arduino2";
         childForm.Show();
         childForm.WindowState = FormWindowState.Maximized;
          */
      }


      private void newParallax_Click(object sender, EventArgs e)
      {
         Form childForm;
         childForm = new VizParallax();
         childForm.MdiParent = this;
         childForm.Text = "Parallax";
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
				string FileName = openFileDialog.FileName;

            VizBuffer childForm = new VizBuffer();
            // Make it a child of this MDI form before showing it.
            childForm.MdiParent = this;
            childForm.Text = FileName;
            childForm.Show();
            childForm.WindowState = FormWindowState.Maximized;


            DataBlock db = new DataBlock();
            //db.GenerateSin(20);
            db.LoadXML(FileName);
            childForm.SetDataBlock(db);
            
            //childForm.SetBbasicOscillo.GetGraphControl();
            //basicOscillo.SetSampleRates(new UInt32[15] { 100, 250, 500, 1000, 2500, 5000, 10000, 25000, 50000, 100000, 250000, 500000, 1000000, 2500000, 5000000 });
            //basicOscillo.SampleRateIndex = 7;         

         }
		}

		private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
         saveFileDialog.Filter = "XML Files (*.xml)|*.xml|Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
			if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
			{
				string FileName = saveFileDialog.FileName;
				// TODO: Add code here to save the current contents of the form to a file.
            ((VizForm)ActiveMdiChild).GetDataBlock().Save(FileName);
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

		private void toolStripTime_SelectedIndexChanged(object sender, EventArgs e)
		{
			//oscillo.currentTimeBase = oscillo.GetTimeBase()[toolStripTime.SelectedIndex];
		}

		private void toolStripTime_Click(object sender, EventArgs e)
		{

		}

		private void toolStripEdge_Click(object sender, EventArgs e)
		{

		}


		private void MDIParent1_FormClosed(object sender, FormClosedEventArgs e)
		{
			//oscillo.ExitRun();
		}

      private void button1_Click(object sender, EventArgs e)
      {

      }

      private void play_CheckedChanged(object sender, EventArgs e)
      {
         //oscillo.pause = play.Checked;
      }

      private void saveToolStripMenuItem_Click(object sender, EventArgs e)
      {
         //MdiChildren.
      }

      private void decodeToolStripMenuItem_Click(object sender, EventArgs e)
      {
         BaseForm childForm = new DecodeFSK();
         // Make it a child of this MDI form before showing it.
         childForm.MdiParent = this;
         childForm.Text = ActiveMdiChild.Text;
         childForm.SetDataBlock(((BaseForm)ActiveMdiChild).GetDataBlock());
         childForm.Show();
         //childForm.WindowState = FormWindowState.Maximized;
      }

      private void decodeBeeperToolStripMenuItem_Click(object sender, EventArgs e)
      {
         BaseForm childForm = new DecodeBeeper();
         // Make it a child of this MDI form before showing it.
         childForm.MdiParent = this;
         childForm.Text = ActiveMdiChild.Text;
         childForm.SetDataBlock(((BaseForm)ActiveMdiChild).GetDataBlock());
         childForm.Show();

      }

      private void decodeFrequencyToolStripMenuItem_Click(object sender, EventArgs e)
      {
         BaseForm childForm = new DecodeFrequency();
         // Make it a child of this MDI form before showing it.
         childForm.MdiParent = this;
         childForm.Text = ActiveMdiChild.Text;
         childForm.SetDataBlock(((BaseForm)ActiveMdiChild).GetDataBlock());
         childForm.Show();

      }



	}
}
