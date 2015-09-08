using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using XOscillo.Acquirers;
using XOscillo.Graph;
using XOscillo.VizForms;

namespace XOscillo
{
    public class CommonToolStrip : MyToolbar
    {
        VizForm vizForm;
        private Acquirer m_acquirer = null;
        private Oscillo m_Oscillo;
        private System.Windows.Forms.ToolStripButton clone;
        private System.Windows.Forms.ToolStripButton play = null;
        private System.Windows.Forms.ToolStripLabel timeLabel;
        public System.Windows.Forms.ToolStripComboBox time;
        private GraphControl graphControl;

        public System.EventHandler selectedIndexChanged;

        public CommonToolStrip(CommonToolStrip ct)
            : this(ct.vizForm, null, ct.graphControl, ct.m_Oscillo)
        {
        }

        public CommonToolStrip(VizForm vf, Acquirer acq, GraphControl gc, Oscillo os)
        {
            vizForm = vf;
            m_acquirer = acq;
            graphControl = gc;
            m_Oscillo = os;

            this.timeLabel = new System.Windows.Forms.ToolStripLabel();
            this.clone = new System.Windows.Forms.ToolStripButton();
            this.play = new System.Windows.Forms.ToolStripButton();
            this.time = new System.Windows.Forms.ToolStripComboBox();


            this.toolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.timeLabel,
            this.time,
            this.clone,
            this.play});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip2";
            this.toolStrip.Size = new System.Drawing.Size(497, 25);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip2";

            // 
            // time
            // 
            this.time.Name = "time";
            this.time.Size = new System.Drawing.Size(50, 25);
            this.time.SelectedIndexChanged += new System.EventHandler(this.time_SelectedIndexChanged);

            // 
            // clone
            // 
            this.clone.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.clone.Image = null;// ((System.Drawing.Image)(resources.GetObject("clone.Image")));
            this.clone.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.clone.Name = "clone";
            this.clone.Size = new System.Drawing.Size(42, 22);
            this.clone.Text = "Clone";
            this.clone.Click += new System.EventHandler(this.clone_Click);

            // 
            // play
            // 
            if (m_acquirer != null)
            {
                this.play.CheckOnClick = true;
                this.play.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
                this.play.Image = global::XOscillo.Properties.Resources.play;
                this.play.ImageTransparentColor = System.Drawing.Color.Magenta;
                this.play.Margin = new System.Windows.Forms.Padding(1);
                this.play.Name = "play";
                this.play.Size = new System.Drawing.Size(23, 23);
                this.play.Text = "toolStripButton2";
                this.play.CheckedChanged += new System.EventHandler(this.play_CheckedChanged);
                this.play.Checked = true;
            }

            this.toolStrip.Items.Add(new System.Windows.Forms.ToolStripSeparator());

            //
            // Channels
            //
            if (m_Oscillo != null)
            {
                for (int i = 0; i < m_Oscillo.GetNumberOfSupportedChannels(); i++)
                {
                    CheckBox cb = new CheckBox();
                    cb.Text = "Ch" + i;
                    cb.Tag = i;
                    cb.Appearance = Appearance.Button;
                    cb.CheckStateChanged += ((s, ex) =>
                    {
                        m_Oscillo.SetChannel((int)cb.Tag, cb.CheckState == CheckState.Checked);
                        SetAcquirerStatus(); // since the above doesnt fire a CheckStateChanged
                    });
                    ToolStripControlHost host = new ToolStripControlHost(cb);

                    if (i == 0)
                    {
                        cb.Checked = true;
                    }

                    this.toolStrip.Items.Add(host);
                }
            }

        }

        private void play_CheckedChanged(object sender, EventArgs e)
        {
            play.Image = play.Checked ? global::XOscillo.Properties.Resources.pause : global::XOscillo.Properties.Resources.play;
            SetAcquirerStatus();
        }

        private void clone_Click(object sender, EventArgs e)
        {
            vizForm.Clone();
        }

        private void time_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectedIndexChanged != null)
            {
                selectedIndexChanged(sender, e);
            }

            graphControl.SetSecondsPerDivision(float.Parse(time.SelectedItem.ToString()));
        }

        private void SetAcquirerStatus()
        {
            if (m_Oscillo.GetNumberOfEnabledChannels() == 0)
            {
                m_acquirer.Stop();
                return;
            }

            if (play.Checked == true)
            {
                m_acquirer.Play();
            }
            else
            {
                m_acquirer.Stop();
            }

        }
    }
}
