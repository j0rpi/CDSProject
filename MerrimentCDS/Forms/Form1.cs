/*
 *  
 *   Merriment CDS Project
 * 
 *   File: Form1.cs
 *   Purpose: Main Login Form
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
using System.Threading;
using System.Globalization;
using System.Runtime.InteropServices;
namespace MerrimentCDS
{
    public partial class Form1 : Form
    {
        public Form1()
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
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect, // x-coordinate of upper-left corner
            int nTopRect, // y-coordinate of upper-left corner
            int nRightRect, // x-coordinate of lower-right corner
            int nBottomRect, // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
         );
        #endregion
        #region LanguageStuff
        string enUS = "en-US";
        string svSE = "sv-SE";
        Localization loc = new Localization();

        #endregion
        // Let any control control form position
        #region FormMovePosition
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,
                         int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        
        
        
        private void Form1_Load(object sender, EventArgs e)
        #endregion


        {
            // What language is set in the config?
            string appPath2 = Path.GetDirectoryName(Application.ExecutablePath);
            System.IO.StreamReader langfile = new System.IO.StreamReader(appPath2 + "/lang.txt");
            string lang;
            lang = langfile.ReadLine();

            if (lang == enUS)
            {
                ChangeLanguage("en-US");
                langfile.Close();
            }
            else if (lang == svSE)
            {
                ChangeLanguage("sv-SE");
                langfile.Close();
         
            }
           
            //Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }
            
        private void button1_Click(object sender, EventArgs e)
        {
            
            // Login with specified account data
            sqlFunctions sql = new sqlFunctions();
            if (sql.login(textBox1.Text, textBox2.Text) == true)
            {
                
                // This will trigger a successfull login
                string appPath = Path.GetDirectoryName(Application.ExecutablePath);

                System.IO.StreamWriter file = new System.IO.StreamWriter(appPath + "/lastuser.txt");
                file.WriteLine(textBox1.Text);
                file.Close();
               
                // Open the main GUI
                Main main = new Main();
                main.Show();
                //this.Hide();
                
            }
            else
            {
                // The login failed, since the user doesn't match anything in the database.
                label3.Visible = true;
                
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
            // Register 
            sqlFunctions sql = new sqlFunctions();
            if (sql.register(textBox1.Text, textBox2.Text) == true)
            {
                // Register Succeeded
            }
            else
            {
                // Register Failed

            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            // Add the ability to actually press 'enter' while password field is focused, for faster access// What language is set in the config?
            if (e.KeyCode == Keys.Enter)
            {

                button1.PerformClick();
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            // Placeholder, remove later on
            textBox1.Text = "j0rpi";
            textBox2.Text = "valvehelpedme";
        }

        private void label1_Paint(object sender, PaintEventArgs e)
        {
            
            ((Label)sender).Text = loc.GDIFIX;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            e.Graphics.DrawString("" + loc.EN_USERNAME, new Font("Moire", 8, FontStyle.Regular), new SolidBrush(Color.White), new PointF(0.0f, 0.0f));
        }

        private void label2_Paint(object sender, PaintEventArgs e)
        {
            ((Label)sender).Text = loc.GDIFIX;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            e.Graphics.DrawString("" + loc.EN_PASSWORD, new Font("Moire", 8, FontStyle.Regular), new SolidBrush(Color.White), new PointF(0.0f, 0.0f));
        }

        private void button1_Paint(object sender, PaintEventArgs e)
        {
            ((Button)sender).Text = loc.GDIFIX;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            e.Graphics.DrawString("" + loc.EN_LOGIN, new Font("Moire", 8, FontStyle.Regular), new SolidBrush(Color.White), new PointF(26.0f, 5.0f));
        }

        private void linkLabel1_Paint(object sender, PaintEventArgs e)
        {
            ((LinkLabel)sender).Text = "                                                ";
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            e.Graphics.DrawString("" + "REGISTER NEW ACCOUNT", new Font("Moire", 8, FontStyle.Regular | FontStyle.Underline), new SolidBrush(Color.White), new PointF(0.0f, 0.0f));
        }

        private void label3_Paint(object sender, PaintEventArgs e)
        {
            ((Label)sender).Text = loc.GDIFIX;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            e.Graphics.DrawString("" + loc.EN_INVALID_LOGIN, new Font("Moire", 8, FontStyle.Regular), new SolidBrush(Color.Red), new PointF(0.0f, 0.0f));
        }


        private void button2_Click_3(object sender, EventArgs e)
        {
            ChangeLanguage("sv-SE");
        }


        private void ChangeLanguage(string lang)
        {
            foreach (Control c in this.Controls)
            {
                ComponentResourceManager resources = new ComponentResourceManager(typeof(Form1));
                resources.ApplyResources(c, c.Name, new CultureInfo(lang));
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //e.Graphics.DrawRectangle(new Pen(Color.FromArgb(255, 1, 1, 1), 2), this.DisplayRectangle);   
            //ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, Color.Black, 0, ButtonBorderStyle.Solid, Color.Black, 0, ButtonBorderStyle.Solid, Color.Black, 0, ButtonBorderStyle.Solid, Color.Black, 0, ButtonBorderStyle.Solid);
        }

        

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox2.Image = MerrimentCDS.Properties.Resources.close_press;
        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox2.Image = MerrimentCDS.Properties.Resources.close_static;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox1.Image = MerrimentCDS.Properties.Resources.minimize_press;
            
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox1.Image = MerrimentCDS.Properties.Resources.minimize_static;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void label4_Paint(object sender, PaintEventArgs e)
        {
            ((Label)sender).Text = loc.GDIFIX;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            e.Graphics.DrawString("" + "Copyright © 2013 Merriment Game Studio. All Rights Reserved.", new Font("Moire", 8, FontStyle.Regular), new SolidBrush(Color.White), new PointF(0.0f, 0.0f));
        }


        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            // Login with specified account data
            sqlFunctions sql = new sqlFunctions();
            if (sql.login(textBox1.Text, textBox2.Text) == true)
            {

                // This will trigger a successfull login
                string appPath = Path.GetDirectoryName(Application.ExecutablePath);

                System.IO.StreamWriter file = new System.IO.StreamWriter(appPath + "/lastuser.txt");
                file.WriteLine(textBox1.Text);
                file.Close();

                // Open the main GUI
                Main main = new Main();
                main.Show();
                this.Hide();
            }
            else
            {
                // The login failed, since the user doesn't match anything in the database.
                label3.Visible = true;

            }
        }

        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox3.Image = MerrimentCDS.Properties.Resources.login_press;
        }

        private void pictureBox3_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox3.Image = MerrimentCDS.Properties.Resources.login;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
       
    }
}
