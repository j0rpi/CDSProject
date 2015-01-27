/*
 *  
 *   Merriment CDS Project
 * 
 *   File: j0rpiSQL.cs
 *   Purpose: Holds all SQL connections
 *   
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using LocalizationStrings;

namespace j0rpiSQL
{
    public class sqlFunctions
    {
        Localization loc = new Localization();
        public bool login (string username, string password)
        {
            // Define MySQL Connection
            MySqlConnection conn = new MySqlConnection("server=mysql05.citynetwork.se;userid=114794-xs71327;password=valvehelpedme;database=114794-nettest");
            MySqlCommand getauth = new MySqlCommand("SELECT * FROM users WHERE username = '" + username + "' AND password = '" + password + "';");

            // Assign 'getauth' to 'conn'
            getauth.Connection = conn;

            // Open the connection
            conn.Open();

            // Setup SQL reader
            MySqlDataReader sqlreader = getauth.ExecuteReader();

            // Try to connect

            if (sqlreader.Read() != false)
            {
                if (sqlreader.IsDBNull(0) == true)
                {
                    // User exists, proceed.
                    getauth.Connection.Close();
                    getauth.Dispose();
                    sqlreader.Dispose();
                    return false;
                }
                else
                {
                    // User does not exist, do not proceed.   
                    getauth.Connection.Close();
                    getauth.Dispose();
                    sqlreader.Dispose();
                    return true;
                }

            }
            else
            {
                return false;
            }




        }

        public bool register (string username, string password)
        {
            // Define MySQL Connection
            MySqlConnection conn = new MySqlConnection("server=mysql05.citynetwork.se;userid=114794-xs71327;password=valvehelpedme;database=114794-nettest");
            MySqlConnection conn2 = new MySqlConnection("server=mysql05.citynetwork.se;userid=114794-xs71327;password=valvehelpedme;database=114794-nettest");
            MySqlCommand getauth = new MySqlCommand("INSERT INTO users (username, password, usertitle) VALUES ('" + username + "', '" + password + "', 'User')");
            MySqlCommand ifexists = new MySqlCommand("SELECT COUNT(*) FROM users WHERE username = '" + username + "'");

            // Assign 'getauth' to 'conn'
            getauth.Connection = conn;
            ifexists.Connection = conn2;
            // Open the connection
            conn2.Open();

            MySqlDataReader reader = ifexists.ExecuteReader();

            while (reader.Read())
            {

                int count = reader.GetInt32(0);

                if (count == 0)
                {
                    MessageBox.Show(loc.EN_SUCCESS);
                    conn.Open();
                    getauth.ExecuteNonQuery();
                }

                else if (count == 1)
                {
                    MessageBox.Show(loc.EN_USER_TAKEN);
                }



            }




            return false;




        }

        public bool writeAvatar (string avatar, string username)
        {
            // Define MySQL Connection
            MySqlConnection conn = new MySqlConnection("server=mysql05.citynetwork.se;userid=114794-xs71327;password=valvehelpedme;database=114794-nettest");
            MySqlConnection conn2 = new MySqlConnection("server=mysql05.citynetwork.se;userid=114794-xs71327;password=valvehelpedme;database=114794-nettest");
            MySqlCommand getauth = new MySqlCommand("UPDATE users SET avatar='" + avatar + "' WHERE username='" + username + "'");
            MySqlCommand ifexists = new MySqlCommand("SELECT COUNT(*) FROM users WHERE username = '" + username + "'");

            // Assign 'getauth' to 'conn'
            getauth.Connection = conn;
            ifexists.Connection = conn2;
            // Open the connection
            conn2.Open();

            MySqlDataReader reader = ifexists.ExecuteReader();

            while (reader.Read())
            {

                int count = reader.GetInt32(0);

                if (count == 0)
                {
                    MessageBox.Show(loc.EN_ACCCONF_AVATAR_FAILED);
                }

                else if (count == 1)
                {
                    MessageBox.Show(loc.EN_ACCCONF_AVATAR_SAVED);
                    conn.Open();
                    getauth.ExecuteNonQuery();
                }



            }
            return true;











        }

        public bool setPassword(string username, string password)
        {
            // Define MySQL Connection
            MySqlConnection conn = new MySqlConnection("server=mysql05.citynetwork.se;userid=114794-xs71327;password=valvehelpedme;database=114794-nettest");
            MySqlConnection conn2 = new MySqlConnection("server=mysql05.citynetwork.se;userid=114794-xs71327;password=valvehelpedme;database=114794-nettest");
            MySqlCommand getauth = new MySqlCommand("UPDATE users SET password='" + password + "' WHERE username='" + username + "'");
            MySqlCommand ifexists = new MySqlCommand("SELECT COUNT(*) FROM users WHERE username = '" + username + "'");

            // Assign 'getauth' to 'conn'
            getauth.Connection = conn;
            ifexists.Connection = conn2;
            // Open the connection
            conn2.Open();

            MySqlDataReader reader = ifexists.ExecuteReader();

            while (reader.Read())
            {

                int count = reader.GetInt32(0);

                if (count == 0)
                {
                    MessageBox.Show(loc.EN_ERROR + ":" + " " + loc.EN_ACCCONF_PASSWORD_FAILED, loc.EN_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                else if (count == 1)
                {
                    MessageBox.Show(loc.EN_ACCCONF_PASSWORD_SAVED, loc.EN_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conn.Open();
                    getauth.ExecuteNonQuery();
                }



            }
            return true;
        }

        public string GetDBString (string SqlFieldName, MySqlDataReader Reader)
        {
            return Reader[SqlFieldName].Equals(DBNull.Value) ? String.Empty : Reader.GetString(SqlFieldName);
        }


    }
}
