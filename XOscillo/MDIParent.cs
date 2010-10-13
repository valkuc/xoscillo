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

      private void newParallax_Click(object sender, EventArgs e)
      {
         Form childForm;
         childForm = new VizParallax();
         childForm.MdiParent = this;
         childForm.Text = "Parallax";
         childForm.WindowState = FormWindowState.Maximized;
         childForm.Show();
         
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
            childForm.LoadDataFromFile(FileName);
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

      private void decodeToolStripMenuItem_Click(object sender, EventArgs e)
      {
         BaseForm childForm = new DecodeFSK();
         // Make it a child of this MDI form before showing it.
         childForm.MdiParent = this;
         childForm.Text = ActiveMdiChild.Text;
         childForm.SetDataBlock(((BaseForm)ActiveMdiChild).GetDataBlock());
         childForm.Show();
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

      private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
      {
         MessageBox.Show( "Written by Raul Aguaviva\nThis is work in progress", "Xoscillo");
      }

	}
}
