/*
 *  
 *   Merriment CDS Project
 * 
 *   File: accSettings.cs
 *   Purpose: Form for account settings, such as password, avatar etc.
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
using MySql.Data.MySqlClient;
using System.IO;
using j0rpiSQL;
using LocalizationStrings;

namespace MerrimentCDS
{
    public partial class accSettings : Form
    {
        public accSettings()
        {
            InitializeComponent();
        }

        Localization loc = new Localization();

        private void button1_Click(object sender, EventArgs e)
        {
            string appPath = Path.GetDirectoryName(Application.ExecutablePath);
            System.IO.StreamReader file = new System.IO.StreamReader(appPath + "/lastuser.txt");
            string user;
            user = file.ReadLine();
            file.Close();
            sqlFunctions sql = new sqlFunctions();
            if (sql.writeAvatar(textBox1.Text, user) == true)
            {
                this.Close();
            }
            else
            {
                
            }
        }

        private void accSettings_Load(object sender, EventArgs e)
        {

        }

        private void label1_Paint(object sender, PaintEventArgs e)
        {
            ((Label)sender).Text = loc.GDIFIX;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            e.Graphics.DrawString("" + loc.EN_ACCCONF_AVATAR, new Font("Moire", 8, FontStyle.Bold), new SolidBrush(Color.White), new PointF(0.0f, 0.0f));
        }

        private void button1_Paint(object sender, PaintEventArgs e)
        {

            ((Button)sender).Text = loc.GDIFIX;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            e.Graphics.DrawString("" + loc.EN_ACCCONF_SAVE_CLOSE, new Font("Moire", 8, FontStyle.Bold), new SolidBrush(Color.White), new PointF(12.0f, 4.0f));
        }

        private void label2_Paint(object sender, PaintEventArgs e)
        {
            ((Label)sender).Text = loc.GDIFIX;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            e.Graphics.DrawString("" + loc.EN_ACCCONF_CHANGE_PASSWORD, new Font("Moire", 8, FontStyle.Bold), new SolidBrush(Color.White), new PointF(0.0f, 0.0f));
        }

        private void button2_Paint(object sender, PaintEventArgs e)
        {
            ((Button)sender).Text = loc.GDIFIX;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            e.Graphics.DrawString("" + loc.EN_ACCCONF_SAVE_CLOSE, new Font("Moire", 8, FontStyle.Bold), new SolidBrush(Color.White), new PointF(12.0f, 4.0f));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string appPath = Path.GetDirectoryName(Application.ExecutablePath);
            System.IO.StreamReader file = new System.IO.StreamReader(appPath + "/lastuser.txt");
            string user;
            user = file.ReadLine();
            file.Close();
            sqlFunctions sql = new sqlFunctions();
            if (sql.setPassword(user, textBox2.Text) == true)
            {
                this.Close();
            }
            else
            {

            }
        }
    }
}
