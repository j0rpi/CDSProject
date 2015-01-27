/*
 *  
 *   Merriment CDS Project
 * 
 *   File: AppDB.cs
 *   Purpose: Holds all functions that can retrieve, edit the AppDB.
 *   
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.IO;

namespace AppDB
{
    class AppDBFunctions
    {
        public void getApps(ListView listview, ListViewItem listviewitem, ImageList imagelist)
        {
             // Fetch apps from AppDB

             string appPath2 = Path.GetDirectoryName(Application.ExecutablePath);
             MySqlConnection conn2 = new MySqlConnection("server=mysql05.citynetwork.se;userid=114794-xs71327;password=valvehelpedme;database=114794-nettest");
             MySqlCommand getAppDB = new MySqlCommand("SELECT * FROM appdb");
             getAppDB.Connection = conn2;
             conn2.Open();
             MySqlDataReader Reader2 = getAppDB.ExecuteReader();

             
             while (Reader2.Read())
             {
                 listview.SmallImageList = imagelist;
                 listview.LargeImageList = imagelist;
                 imagelist.Images.Add(MerrimentCDS.Properties.Resources.icon);

                 listview.StateImageList = imagelist;

                 listviewitem.StateImageIndex = 0;
                 
                 // Fill list view with data (0 = id, 1 = name, 2 = description, 3 = version, 4 = download link)
                 listviewitem = new ListViewItem(Reader2[0].ToString());
                 listviewitem.SubItems.Add(Reader2[1].ToString());
                 listviewitem.SubItems.Add(Reader2[2].ToString());
                 listviewitem.SubItems.Add(Reader2[3].ToString());
                 listviewitem.SubItems.Add(Reader2[4].ToString());

                 // Addition (v0.4beta) - If 'adminonly' is set to 1 in DB, only admins can download and use that app
                 listviewitem.SubItems.Add(Reader2[5].ToString());

                 // Addition (v0.4beta) - Develoepr String
                 listviewitem.SubItems.Add(Reader2[6].ToString());
                 listview.Items.Add(listviewitem);

                 // Addition (v0.5beta) - App Icon (Not shown, but used for properties form)
                 listviewitem.SubItems.Add(Reader2[7].ToString());
                 
                 
             }
             Reader2.Close();
             conn2.Close();
        }

        
    }
}
