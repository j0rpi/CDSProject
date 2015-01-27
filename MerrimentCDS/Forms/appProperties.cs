/*
 *  
 *   Merriment CDS Project
 * 
 *   File: appProperties.cs
 *   Purpose: Shows all info regarding selected application
 *   
 * 
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MerrimentCDS
{
    public partial class AppProperties : Form
    {
        public AppProperties()
        {
            InitializeComponent();
        }
        // Eliminate flickering
        #region FlickerKiller
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }
        #endregion
        Main main = new Main();

        private void AppProperties_Load(object sender, EventArgs e)
        {
            if (label3.Text == "")
            {
                label3.Text = "N/A";
            }
            else
            {

            }

            if (label8.Text == "label8")
            {
                label8.Text = "N/A";
            }
            else
            {

            }
            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox2.Image = MerrimentCDS.Properties.Resources.close_press;
        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox2.Image = MerrimentCDS.Properties.Resources.close_static;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox1.Image = MerrimentCDS.Properties.Resources.minimize_press;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox1.Image = MerrimentCDS.Properties.Resources.minimize_static;
        }
    }
}
