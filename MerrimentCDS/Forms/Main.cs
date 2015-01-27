/*
 *  
 *   Merriment CDS Project
 * 
 *   File: Main.cs
 *   Purpose: The UI
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
using System.IO;
using MySql.Data.MySqlClient;
using System.Net;
using j0rpiSQL;
using AppDB;
using LocalizationStrings;
using System.Globalization;
using System.Runtime.InteropServices;
namespace MerrimentCDS
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            
        }
        Localization loc = new Localization();
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

        // Let any control control form position
        #region FormMovePosition
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,
                         int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        #endregion
        private void Main_Load(object sender, EventArgs e)
        {

            
            
            // Read userdata
            sqlFunctions sql = new sqlFunctions();
            string appPath = Path.GetDirectoryName(Application.ExecutablePath);
            System.IO.StreamReader file = new System.IO.StreamReader(appPath + "/lastuser.txt");
            string user;
            user = file.ReadLine();
            file.Close();
            MySqlConnection conn = new MySqlConnection("server=mysql05.citynetwork.se;userid=114794-xs71327;password=valvehelpedme;database=114794-nettest");
            MySqlCommand getauth = new MySqlCommand("SELECT * FROM users WHERE username = '" + user + "';");
            getauth.Parameters.AddWithValue("username", user);
            getauth.Connection = conn;
            conn.Open();
            MySqlDataReader Reader = getauth.ExecuteReader();
            this.Text = "Merriment CDS :: " + loc.EN_LOGGED_IN_AS + user;
            //label5.Text = "Merriment CDS :: " + loc.EN_LOGGED_IN_AS + user;
           
            while (Reader.Read())
             {
                 label1.Text = (sql.GetDBString("username", Reader));
                 label2.Text = (sql.GetDBString("usertitle", Reader));
                 pictureBox1.ImageLocation = (sql.GetDBString("avatar", Reader));
                 if (sql.GetDBString("isadmin", Reader) == "no")
                 {
                     label3.Text = string.Empty;
                 }
                 else
                 {
                     label3.Text = loc.EN_ADMIN;
                    
                 }
             }
            Reader.Close();
            conn.Close();

            // Fill listview with all applications which we will fetch from the database
            ListViewItem listviewitem = new ListViewItem();
            AppDBFunctions appDB = new AppDBFunctions();
            appDB.getApps(listView1, listviewitem, imageList1);

            
        }


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Open account settings form
            accSettings accSettings = new accSettings();
            accSettings.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            // Try to download selected application
            try
            {
                toolStripProgressBar1.Visible = true;
                toolStripStatusLabel2.Visible = true;
                string appPath = Path.GetDirectoryName(Application.ExecutablePath);
                WebClient webClient = new WebClient();
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
                string name = listView1.SelectedItems[0].SubItems[1].Text;
                string id = listView1.SelectedItems[0].SubItems[0].Text;
                System.IO.Directory.CreateDirectory(appPath + "/apps");
                System.IO.Directory.CreateDirectory(appPath + "/apps/" + id);

                if (listView1.SelectedItems[0].SubItems[5].Text == "yes" && label3.Text == "Admin Account")
                {
                    if (listView1.SelectedItems[0].SubItems[2].Text.Contains(loc.EN_LINUX))
                    {
                        MessageBox.Show(loc.EN_LINUX_APP, loc.EN_LINUX_COMP, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    webClient.DownloadFileAsync(new Uri(listView1.SelectedItems[0].SubItems[4].Text), appPath + @"/apps/" + id + @"/" + name + ".exe");
                    toolStripStatusLabel1.Text = loc.EN_STATUS + loc.EN_STATUS_DOWNLOADING;
                    toolStripStatusLabel2.Text = "''" + name + "''";
                   
                }
                else if (listView1.SelectedItems[0].SubItems[5].Text == "yes" && label3.Text == string.Empty)
                {
                    toolStripProgressBar1.Visible = false;
                    toolStripStatusLabel2.Visible = false;
                    MessageBox.Show(loc.EN_ERROR + ":" + loc.EN_DOWNLOAD_ERROR_NO_ACCESS  + Environment.NewLine + loc.EN_DOWNLOAD_ERROR_NO_ACCESS_ACCOUNT, loc.EN_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    toolStripStatusLabel1.Text = loc.EN_STATUS + " " + loc.EN_STATUS_IDLE;
                }
                else if (listView1.SelectedItems[0].SubItems[5].Text == "no" && label3.Text == string.Empty)
                {
                    if (listView1.SelectedItems[0].SubItems[2].Text.Contains(loc.EN_LINUX))
                    {
                        MessageBox.Show(loc.EN_LINUX_APP, loc.EN_LINUX_COMP, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    webClient.DownloadFileAsync(new Uri(listView1.SelectedItems[0].SubItems[4].Text), appPath + @"/apps/" + id + @"/" + name + ".exe");
                    toolStripStatusLabel1.Text = loc.EN_STATUS + " " + loc.EN_STATUS_DOWNLOADING;
                    toolStripStatusLabel2.Text = "''" + name + "''";
                }
                else if (listView1.SelectedItems[0].SubItems[5].Text == "no" && label3.Text == loc.EN_ADMIN)
                {
                    if (listView1.SelectedItems[0].SubItems[2].Text.Contains(loc.EN_LINUX))
                    {
                        MessageBox.Show(loc.EN_LINUX_APP, loc.EN_LINUX_COMP,  MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    webClient.DownloadFileAsync(new Uri(listView1.SelectedItems[0].SubItems[4].Text), appPath + @"/apps/" + id + @"/" + name + ".exe");
                    toolStripStatusLabel1.Text = loc.EN_STATUS + " " + loc.EN_STATUS_DOWNLOADING;
                    toolStripStatusLabel2.Text = "''" + name + "''";
                }

            }
            catch
            {
                // Disable progressbar, and display a messagebox, since the download failed.
                toolStripProgressBar1.Visible = false;
                toolStripStatusLabel2.Visible = false;
                MessageBox.Show(loc.EN_ERROR + ":" + " " + loc.EN_DOWNLOAD_ERROR_NOT_AVAILABLE, loc.EN_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                toolStripStatusLabel1.Text = loc.EN_STATUS + " " + loc.EN_STATUS_IDLE;
                
            }
            
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            
        }

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolStripProgressBar1.Value = e.ProgressPercentage;
        }
        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            toolStripProgressBar1.Visible = false;
            MessageBox.Show(loc.EN_DOWNLOAD_COMPLETE, loc.EN_SUCCESS, MessageBoxButtons.OK, MessageBoxIcon.Information);
            toolStripStatusLabel1.Text = loc.EN_STATUS + " " + loc.EN_STATUS_IDLE;
            toolStripStatusLabel2.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            
            
        }

        private void listView1_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            System.Drawing.Drawing2D.LinearGradientBrush GradientBrush = new System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, Color.Black, Color.FromArgb(33, 33, 33), 270);

            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            e.Graphics.FillRectangle(GradientBrush, e.Bounds);

            e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.FromArgb(255, 33, 33, 33))), e.Bounds.X + 0, e.Bounds.Y + 0, e.Bounds.Width - 0, e.Bounds.Height - 0);
            
            e.Graphics.DrawString(" " + e.Header.Text, new Font("Moire", 9), new SolidBrush(Color.White), e.Bounds.X +0, e.Bounds.Y + 5);
        }

        private void listView1_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            e.Graphics.DrawString(" " + e.SubItem.Text, new Font("Moire", 8.5f), new SolidBrush(Color.White), e.Bounds.X + 0, e.Bounds.Y + 2);

            e.Graphics.DrawRectangle(Pens.Black, e.Bounds);
        }

        private void listView1_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            




            
            if (e.Item.Selected)
            {

                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(120, 66, 66, 66)), e.Bounds);


            }
        }

        

        private void toolStripStatusLabel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void label1_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void button1_Paint(object sender, PaintEventArgs e)
        {
            ((Button)sender).Text = loc.GDIFIX;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            e.Graphics.DrawString("" + loc.EN_INSTALL, new Font("Moire", 10, FontStyle.Bold), new SolidBrush(Color.White), new PointF(30.0f, 3.0f));
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            // Clear listview before refreshing, we don't want any duplicates.
            listView1.Items.Clear();
            
            try
            {
                ListViewItem listviewitem = new ListViewItem();
                AppDBFunctions appDB = new AppDBFunctions();
                appDB.getApps(listView1, listviewitem, imageList1);
            }
            catch
            {
                MessageBox.Show(loc.EN_APPDB_REFRESH_FAIL, loc.EN_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Paint(object sender, PaintEventArgs e)
        {
            ((Label)sender).Text = loc.GDIFIX;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            e.Graphics.DrawString("" + "** = Administrators Only", new Font("Moire", 8, FontStyle.Bold), new SolidBrush(Color.White), new PointF(0.0f, 0.0f));
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            listView1.OwnerDraw = false;
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void listView1_MouseDown(object sender, MouseEventArgs e)
        {
            
        }
        private void ChangeLanguage(string lang)
        {
            foreach (Control c in this.Controls)
            {
                ComponentResourceManager resources = new ComponentResourceManager(typeof(Form1));
                resources.ApplyResources(c, c.Name, new CultureInfo(lang));
            }
        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            
             
        }

        private void pictureBox5_MouseDown(object sender, MouseEventArgs e)
        {
            
            pictureBox5.Image = MerrimentCDS.Properties.Resources.instal_pressl;
            

            
        }

        private void pictureBox5_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox5.Image = MerrimentCDS.Properties.Resources.install;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            // We don't want multiple sections of the same code, so we will just click the button via this picture button
            button1.PerformClick();
        }

        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox3.Image = MerrimentCDS.Properties.Resources.close_press;
        }

        private void pictureBox3_MouseUp(object sender, MouseEventArgs e)
        {
            
        }

        private void pictureBox3_MouseUp_1(object sender, MouseEventArgs e)
        {
            pictureBox3.Image = MerrimentCDS.Properties.Resources.close_static;
        }

        private void pictureBox4_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox4.Image = MerrimentCDS.Properties.Resources.minimize_press;
        }

        private void pictureBox4_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox4.Image = MerrimentCDS.Properties.Resources.minimize_static;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            try
            {
                noItemSelectedToolStripMenuItem.Text = listView1.SelectedItems[0].SubItems[1].Text;
                noItemSelectedToolStripMenuItem.Enabled = false;
            }
            catch
            {
                noItemSelectedToolStripMenuItem.Text = "No Item Selected";
            }
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // Show the app properties form, and set it's controls text
                AppProperties appprops = new AppProperties();
                appprops.Show();

                appprops.label1.Text = listView1.SelectedItems[0].SubItems[1].Text;
                appprops.textBox1.Text = listView1.SelectedItems[0].SubItems[2].Text;
                appprops.label3.Text = listView1.SelectedItems[0].SubItems[3].Text;
                appprops.label9.Text = listView1.SelectedItems[0].SubItems[6].Text;
                appprops.pictureBox3.ImageLocation = listView1.SelectedItems[0].SubItems[7].Text;

                // Read the files head, to get it's file size.
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(listView1.SelectedItems[0].SubItems[4].Text);
                req.Method = "HEAD";
                HttpWebResponse resp = (HttpWebResponse)(req.GetResponse());
                long len = resp.ContentLength;

                appprops.label8.Text = BytesToString(len).ToString().Replace(",", ".");
            }
            catch
            {

            }
        }

        // Convert the files bytes to proper size (b, kb, mb, gb etc)
        static String BytesToString(long byteCount)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" }; 
            if (byteCount == 0)
                return "0" + suf[0];
            long bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return (Math.Sign(byteCount) * num).ToString() + suf[place];
        }
    }
}
